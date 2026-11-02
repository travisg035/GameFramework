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
        /// KCP network channel (reliable UDP).
        /// </summary>
        private sealed class KcpNetworkChannel : NetworkChannelBase
        {
            private const int SioUdpConnReset = -1744830452;
            private const int ReceiveBufferLength = 1024 * 64;
            private const uint DefaultConv = 1;
            private const int DefaultMtu = 1400;
            private const int DefaultWindowSize = 128;
            private const int DefaultNoDelay = 1;
            private const int DefaultInterval = 10;
            private const int DefaultFastResend = 2;
            private const bool DefaultCongestionControl = false;

            private readonly AsyncCallback m_ReceiveCallback;
            private readonly byte[] m_ReceiveBuffer;
            private readonly byte[] m_PacketBuffer;
            private Kcp m_Kcp;
            private uint m_ClockMilliseconds;

            /// <summary>
            /// Initializes a new instance of the KcpNetworkChannel class.
            /// </summary>
            /// <param name="name">The network channel name.</param>
            /// <param name="networkChannelHelper">The network channel helper.</param>
            public KcpNetworkChannel(string name, INetworkChannelHelper networkChannelHelper)
                : base(name, networkChannelHelper)
            {
                m_ReceiveCallback = ReceiveCallback;
                m_ReceiveBuffer = new byte[ReceiveBufferLength];
                m_PacketBuffer = new byte[ReceiveBufferLength];
                m_Kcp = null;
                m_ClockMilliseconds = 0;
            }

            /// <summary>
            /// Gets the network service type.
            /// </summary>
            public override ServiceType ServiceType
            {
                get
                {
                    return ServiceType.Kcp;
                }
            }

            /// <summary>
            /// Network channel polling.
            /// </summary>
            /// <param name="elapseSeconds">Logical elapsed seconds.</param>
            /// <param name="realElapseSeconds">Real elapsed seconds.</param>
            public override void Update(float elapseSeconds, float realElapseSeconds)
            {
                m_ClockMilliseconds += (uint)(realElapseSeconds * 1000f);
                base.Update(elapseSeconds, realElapseSeconds);
            }

            protected override void ProcessReceive()
            {
                base.ProcessReceive();

                if (m_Socket == null || !m_Active || m_Kcp == null)
                {
                    return;
                }

                lock (m_Kcp)
                {
                    m_Kcp.Update(m_ClockMilliseconds);

                    while (true)
                    {
                        int peekSize = m_Kcp.PeekSize();
                        if (peekSize <= 0)
                        {
                            break;
                        }

                        int bytesRead = m_Kcp.Recv(m_PacketBuffer, 0, peekSize);
                        if (bytesRead <= 0)
                        {
                            break;
                        }

                        ProcessReceivedBytes(m_PacketBuffer, bytesRead);
                    }
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

                uint conv = DefaultConv;
                int mtu = DefaultMtu;
                int sendWindow = DefaultWindowSize;
                int receiveWindow = DefaultWindowSize;
                int nodelay = DefaultNoDelay;
                int interval = DefaultInterval;
                int fastResend = DefaultFastResend;
                bool congestionControl = DefaultCongestionControl;

                IKcpChannelHelper kcpHelper = m_NetworkChannelHelper as IKcpChannelHelper;
                if (kcpHelper != null)
                {
                    conv = kcpHelper.Conv;
                    mtu = kcpHelper.Mtu > 0 ? kcpHelper.Mtu : DefaultMtu;
                    sendWindow = kcpHelper.SendWindow > 0 ? kcpHelper.SendWindow : DefaultWindowSize;
                    receiveWindow = kcpHelper.ReceiveWindow > 0 ? kcpHelper.ReceiveWindow : DefaultWindowSize;
                    nodelay = kcpHelper.NoDelay;
                    interval = kcpHelper.Interval > 0 ? kcpHelper.Interval : DefaultInterval;
                    fastResend = kcpHelper.FastResend;
                    congestionControl = kcpHelper.CongestionControl;
                }

                m_Kcp = new Kcp(conv, KcpOutput, m_Socket);
                m_Kcp.SetMtu(mtu);
                m_Kcp.SetWindowSize(sendWindow, receiveWindow);
                m_Kcp.SetNoDelay(nodelay, interval, fastResend, congestionControl ? 0 : 1);
                m_ClockMilliseconds = 0;

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

            public override void Shutdown()
            {
                base.Shutdown();
                m_Kcp = null;
            }

            protected override bool ProcessSend()
            {
                if (m_Kcp == null || m_Socket == null || !m_Active)
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

                    lock (m_Kcp)
                    {
                        m_Kcp.Send(m_SendState.Stream.GetBuffer(), 0, (int)m_SendState.Stream.Length);
                    }

                    m_SentPacketCount++;
                }

                return true;
            }

            private void KcpOutput(byte[] buffer, int size, object userData)
            {
                Socket socket = (Socket)userData;
                if (!m_Active || socket == null || size <= 0)
                {
                    return;
                }

                try
                {
                    socket.Send(buffer, size, SocketFlags.None);
                }
                catch (Exception exception)
                {
                    m_Active = false;
                    if (NetworkChannelError != null)
                    {
                        SocketException socketException = exception as SocketException;
                        NetworkChannelError(this, NetworkErrorCode.SendError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, exception.ToString());
                        return;
                    }

                    throw;
                }
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

                if (bytesReceived > 0 && m_Kcp != null)
                {
                    lock (m_Kcp)
                    {
                        m_Kcp.Input(m_ReceiveBuffer, 0, bytesReceived);
                    }
                }

                ReceiveAsync();
            }

            private void ProcessReceivedBytes(byte[] buffer, int length)
            {
                m_ReceiveState.Stream.Position = 0L;
                m_ReceiveState.Stream.SetLength(length);
                m_ReceiveState.Stream.Write(buffer, 0, length);
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
        }
    }
}
