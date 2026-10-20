//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Net;
using System.Net.Sockets;

namespace GameFramework.Network
{
    /// <summary>
    /// Network channel interface.
    /// </summary>
    public interface INetworkChannel
    {
        /// <summary>
        /// Gets the network channel name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the socket used by the network channel.
        /// </summary>
        Socket Socket
        {
            get;
        }

        /// <summary>
        /// Gets whether it is connected.
        /// </summary>
        bool Connected
        {
            get;
        }

        /// <summary>
        /// Gets the network service type.
        /// </summary>
        ServiceType ServiceType
        {
            get;
        }

        /// <summary>
        /// Gets the network address family.
        /// </summary>
        AddressFamily AddressFamily
        {
            get;
        }

        /// <summary>
        /// Gets the number of packets to be sent.
        /// </summary>
        int SendPacketCount
        {
            get;
        }

        /// <summary>
        /// Gets the cumulative number of packets sent.
        /// </summary>
        int SentPacketCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of received unprocessed packets.
        /// </summary>
        int ReceivePacketCount
        {
            get;
        }

        /// <summary>
        /// Gets the cumulative number of packets received.
        /// </summary>
        int ReceivedPacketCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets whether to reset the heartbeat elapsed time when receiving a packet.
        /// </summary>
        bool ResetHeartBeatElapseSecondsWhenReceivePacket
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the number of missed heartbeats.
        /// </summary>
        int MissHeartBeatCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the heartbeat interval in seconds.
        /// </summary>
        float HeartBeatInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the heartbeat elapsed time in seconds.
        /// </summary>
        float HeartBeatElapseSeconds
        {
            get;
        }

        /// <summary>
        /// Registers a packet handler.
        /// </summary>
        /// <param name="handler">The packet handler to register.</param>
        void RegisterHandler(IPacketHandler handler);

        /// <summary>
        /// Sets the default event handler.
        /// </summary>
        /// <param name="handler">The default event handler to set.</param>
        void SetDefaultHandler(EventHandler<Packet> handler);

        /// <summary>
        /// Connects to the remote host.
        /// </summary>
        /// <param name="ipAddress">Remote host IP address.</param>
        /// <param name="port">Remote host port.</param>
        void Connect(IPAddress ipAddress, int port);

        /// <summary>
        /// Connects to the remote host.
        /// </summary>
        /// <param name="ipAddress">Remote host IP address.</param>
        /// <param name="port">Remote host port.</param>
        /// <param name="userData">User-defined data.</param>
        void Connect(IPAddress ipAddress, int port, object userData);

        /// <summary>
        /// Closes the network channel.
        /// </summary>
        void Close();

        /// <summary>
        /// Sends a packet to the remote host.
        /// </summary>
        /// <typeparam name="T">Packet type.</typeparam>
        /// <param name="packet">Packet to send.</param>
        void Send<T>(T packet) where T : Packet;
    }
}
