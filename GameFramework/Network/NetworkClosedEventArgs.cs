//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// Network closed event.
    /// </summary>
    public sealed class NetworkClosedEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the NetworkClosedEventArgs class.
        /// </summary>
        public NetworkClosedEventArgs()
        {
            NetworkChannel = null;
        }

        /// <summary>
        /// Gets the network channel.
        /// </summary>
        public INetworkChannel NetworkChannel
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a network closed event.
        /// </summary>
        /// <param name="networkChannel">Network channel.</param>
        /// <returns>The created network closed event.</returns>
        public static NetworkClosedEventArgs Create(INetworkChannel networkChannel)
        {
            NetworkClosedEventArgs networkClosedEventArgs = ReferencePool.Acquire<NetworkClosedEventArgs>();
            networkClosedEventArgs.NetworkChannel = networkChannel;
            return networkClosedEventArgs;
        }

        /// <summary>
        /// Clears the network closed event.
        /// </summary>
        public override void Clear()
        {
            NetworkChannel = null;
        }
    }
}
