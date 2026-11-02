//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Net;
using System.Net.Sockets;

namespace GameFramework.Network
{
    internal sealed partial class NetworkManager : GameFrameworkModule, INetworkManager
    {
        /// <summary>
        /// UDP network channel.
        /// </summary>
        private sealed class UdpNetworkChannel : NetworkChannelBase
        {
            private const int SioUdpConnReset = -1744830452;
            private const int ReceiveBufferLength = 1024 * 64;

            private readonly AsyncCallback m_ReceiveCallback;
            private readonly byte[] m_ReceiveBuffer;

            /// <summary>
            /// Initializes a new instance of the UdpNetworkChannel class.
            /// </summary>
            /// <param name="name">The network channel name.</param>
            /// <param name="networkChannelHelper">The network channel helper.</param>
            public UdpNetworkChannel(string name, INetworkChannelHelper networkChannelHelper)
                : base(name, networkChannelHelper)
            {
                m_ReceiveCallback = ReceiveCallback;
                m_ReceiveBuffer = new byte[ReceiveBufferLength];
            }

            /// <summary>
            /// Gets the network service type.
            /// </summary>
            public override ServiceType ServiceType
            {
                get
                {
                    return ServiceType.Udp;
                }
            }

            /// <summary>
            /// Connects to the remote host.
            /// </summary>
            /// <param name="ipAddress">Remote host IP address.</param>
            /// <param name="port">Remote host port.</param>
            /// <param name="userData">User-defined data.</param>
            public override void Connect(IPAddress ipAddress, int port, object userData)
            {
                base.Connect(ipAddress, port, userData);
                m_Socket = new Socket(ipAddress.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                if (m_Socket == null)
                {
                    string errorMessage = "Initialize network channel failure.";
                    if (NetworkChannelError != null)
                    {
                        NetworkChannelError(this, NetworkErrorCode.SocketError, SocketError.Success, errorMessage);
                        return;
                    }

                    throw new GameFrameworkException(errorMessage);
                }

                // Ignore ICMP Port Unreachable on Windows
                try
                {
                    m_Socket.IOControl(SioUdpConnReset, new byte[] { 0 }, null);
                }
                catch
                {
                }

                m_NetworkChannelHelper.PrepareForConnecting();

                try
                {
                    m_Socket.Connect(ipAddress, port);
                }
                catch (Exception exception)
                {
                    if (NetworkChannelError != null)
                    {
                        SocketException socketException = exception as SocketException;
                        NetworkChannelError(this, NetworkErrorCode.ConnectError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, exception.ToString());
                        return;
                    }

                    throw;
                }

                m_SentPacketCount = 0;
                m_ReceivedPacketCount = 0;

                lock (m_SendPacketPool)
                {
                    m_SendPacketPool.Clear();
                }

                m_ReceivePacketPool.Clear();

                lock (m_HeartBeatState)
                {
                    m_HeartBeatState.Reset(true);
                }

                m_Active = true;

                if (NetworkChannelConnected != null)
                {
                    NetworkChannelConnected(this, userData);
                }

                ReceiveAsync();
            }

            protected override bool ProcessSend()
            {
                if (m_Socket == null || !m_Active)
                {
                    return false;
                }

                if (m_SendPacketPool.Count <= 0)
                {
                    return false;
                }

                while (m_SendPacketPool.Count > 0)
                {
                    Packet packet = null;
                    lock (m_SendPacketPool)
                    {
                        if (m_SendPacketPool.Count > 0)
                        {
                            packet = m_SendPacketPool.Dequeue();
                        }
                    }

                    if (packet == null)
                    {
                        break;
                    }

                    m_SendState.Reset();

                    bool serializeResult = false;
                    try
                    {
                        serializeResult = m_NetworkChannelHelper.Serialize(packet, m_SendState.Stream);
                    }
                    catch (Exception exception)
                    {
                        m_Active = false;
                        if (NetworkChannelError != null)
                        {
                            SocketException socketException = exception as SocketException;
                            NetworkChannelError(this, NetworkErrorCode.SerializeError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, exception.ToString());
                            return false;
                        }

                        throw;
                    }

                    if (!serializeResult)
                    {
                        string errorMessage = "Serialized packet failure.";
                        if (NetworkChannelError != null)
                        {
                            NetworkChannelError(this, NetworkErrorCode.SerializeError, SocketError.Success, errorMessage);
                            return false;
                        }

                        throw new GameFrameworkException(errorMessage);
                    }

                    try
                    {
                        // ponytail: synchronous socket.Send is non-blocking for OS UDP buffer, flushes all queued datagrams in tick.
                        m_Socket.Send(m_SendState.Stream.GetBuffer(), 0, (int)m_SendState.Stream.Length, SocketFlags.None);
                    }
                    catch (Exception exception)
                    {
                        m_Active = false;
                        if (NetworkChannelError != null)
                        {
                            SocketException socketException = exception as SocketException;
                            NetworkChannelError(this, NetworkErrorCode.SendError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, exception.ToString());
                            return false;
                        }

                        throw;
                    }

                    m_SentPacketCount++;
                }

                return true;
            }

            private void ReceiveAsync()
            {
                if (!m_Active || m_Socket == null)
                {
                    return;
                }

                try
                {
                    m_Socket.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, m_ReceiveCallback, m_Socket);
                }
                catch (Exception exception)
                {
                    m_Active = false;
                    if (NetworkChannelError != null)
                    {
                        SocketException socketException = exception as SocketException;
                        NetworkChannelError(this, NetworkErrorCode.ReceiveError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, exception.ToString());
                        return;
                    }

                    throw;
                }
            }

            private void ReceiveCallback(IAsyncResult ar)
            {
                Socket socket = (Socket)ar.AsyncState;
                if (!m_Active || socket == null)
                {
                    return;
                }

                int bytesReceived = 0;
                try
                {
                    bytesReceived = socket.EndReceive(ar);
                }
                catch (Exception exception)
                {
                    m_Active = false;
                    if (NetworkChannelError != null)
                    {
                        SocketException socketException = exception as SocketException;
                        NetworkChannelError(this, NetworkErrorCode.ReceiveError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, exception.ToString());
                        return;
                    }

                    throw;
                }

                if (bytesReceived > 0)
                {
                    m_ReceiveState.Stream.Position = 0L;
                    m_ReceiveState.Stream.SetLength(bytesReceived);
                    m_ReceiveState.Stream.Write(m_ReceiveBuffer, 0, bytesReceived);
                    m_ReceiveState.Stream.Position = 0L;

                    object customErrorData = null;
                    IPacketHeader packetHeader = null;
                    try
                    {
                        packetHeader = m_NetworkChannelHelper.DeserializePacketHeader(m_ReceiveState.Stream, out customErrorData);
                    }
                    catch (Exception exception)
                    {
                        m_Active = false;
                        if (NetworkChannelError != null)
                        {
                            SocketException socketException = exception as SocketException;
                            NetworkChannelError(this, NetworkErrorCode.DeserializePacketHeaderError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, exception.ToString());
                            return;
                        }

                        throw;
                    }

                    if (customErrorData != null && NetworkChannelCustomError != null)
                    {
                        NetworkChannelCustomError(this, customErrorData);
                    }

                    if (packetHeader != null)
                    {
                        Packet packet = null;
                        try
                        {
                            packet = m_NetworkChannelHelper.DeserializePacket(packetHeader, m_ReceiveState.Stream, out customErrorData);
                        }
                        catch (Exception exception)
                        {
                            m_Active = false;
                            if (NetworkChannelError != null)
                            {
                                SocketException socketException = exception as SocketException;
                                NetworkChannelError(this, NetworkErrorCode.DeserializePacketError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, exception.ToString());
                                return;
                            }

                            throw;
                        }

                        if (customErrorData != null && NetworkChannelCustomError != null)
                        {
                            NetworkChannelCustomError(this, customErrorData);
                        }

                        if (packet != null)
                        {
                            m_ReceivePacketPool.Fire(this, packet);
                            m_ReceivedPacketCount++;

                            lock (m_HeartBeatState)
                            {
                                m_HeartBeatState.Reset(m_ResetHeartBeatElapseSecondsWhenReceivePacket);
                            }
                        }
                    }
                }

                ReceiveAsync();
            }
        }
    }
}
