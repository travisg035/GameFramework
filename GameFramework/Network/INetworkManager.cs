//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.Network
{
    /// <summary>
    /// Network manager interface.
    /// </summary>
    public interface INetworkManager
    {
        /// <summary>
        /// Gets the number of network channels.
        /// </summary>
        int NetworkChannelCount
        {
            get;
        }

        /// <summary>
        /// Network connected event.
        /// </summary>
        event EventHandler<NetworkConnectedEventArgs> NetworkConnected;

        /// <summary>
        /// Network closed event.
        /// </summary>
        event EventHandler<NetworkClosedEventArgs> NetworkClosed;

        /// <summary>
        /// Network miss heartbeat event.
        /// </summary>
        event EventHandler<NetworkMissHeartBeatEventArgs> NetworkMissHeartBeat;

        /// <summary>
        /// Network error event.
        /// </summary>
        event EventHandler<NetworkErrorEventArgs> NetworkError;

        /// <summary>
        /// Network custom error event.
        /// </summary>
        event EventHandler<NetworkCustomErrorEventArgs> NetworkCustomError;

        /// <summary>
        /// Checks whether the network channel exists.
        /// </summary>
        /// <param name="name">The network channel name.</param>
        /// <returns>Whether the network channel exists.</returns>
        bool HasNetworkChannel(string name);

        /// <summary>
        /// Gets a network channel.
        /// </summary>
        /// <param name="name">The network channel name.</param>
        /// <returns>The network channel to get.</returns>
        INetworkChannel GetNetworkChannel(string name);

        /// <summary>
        /// Gets all network channels.
        /// </summary>
        /// <returns>All network channels.</returns>
        INetworkChannel[] GetAllNetworkChannels();

        /// <summary>
        /// Gets all network channels.
        /// </summary>
        /// <param name="results">All network channels.</param>
        void GetAllNetworkChannels(List<INetworkChannel> results);

        /// <summary>
        /// Creates a network channel.
        /// </summary>
        /// <param name="name">The network channel name.</param>
        /// <param name="serviceType">Network service type.</param>
        /// <param name="networkChannelHelper">The network channel helper.</param>
        /// <returns>The created network channel.</returns>
        INetworkChannel CreateNetworkChannel(string name, ServiceType serviceType, INetworkChannelHelper networkChannelHelper);

        /// <summary>
        /// Destroys a network channel.
        /// </summary>
        /// <param name="name">The network channel name.</param>
        /// <returns>Whether the network channel was destroyed successfully.</returns>
        bool DestroyNetworkChannel(string name);
    }
}
