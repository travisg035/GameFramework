//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// Network miss heartbeat event.
    /// </summary>
    public sealed class NetworkMissHeartBeatEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the NetworkMissHeartBeatEventArgs class.
        /// </summary>
        public NetworkMissHeartBeatEventArgs()
        {
            NetworkChannel = null;
            MissCount = 0;
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
        /// Gets the number of missed heartbeats.
        /// </summary>
        public int MissCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a network miss heartbeat event.
        /// </summary>
        /// <param name="networkChannel">Network channel.</param>
        /// <param name="missCount">Number of missed heartbeats.</param>
        /// <returns>The created network miss heartbeat event.</returns>
        public static NetworkMissHeartBeatEventArgs Create(INetworkChannel networkChannel, int missCount)
        {
            NetworkMissHeartBeatEventArgs networkMissHeartBeatEventArgs = ReferencePool.Acquire<NetworkMissHeartBeatEventArgs>();
            networkMissHeartBeatEventArgs.NetworkChannel = networkChannel;
            networkMissHeartBeatEventArgs.MissCount = missCount;
            return networkMissHeartBeatEventArgs;
        }

        /// <summary>
        /// Clears the network miss heartbeat event.
        /// </summary>
        public override void Clear()
        {
            NetworkChannel = null;
            MissCount = 0;
        }
    }
}
