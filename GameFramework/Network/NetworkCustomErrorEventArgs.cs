//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// Network custom error event.
    /// </summary>
    public sealed class NetworkCustomErrorEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the NetworkCustomErrorEventArgs class.
        /// </summary>
        public NetworkCustomErrorEventArgs()
        {
            NetworkChannel = null;
            CustomErrorData = null;
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
        /// Gets the custom error data.
        /// </summary>
        public object CustomErrorData
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a network custom error event.
        /// </summary>
        /// <param name="networkChannel">Network channel.</param>
        /// <param name="customErrorData">Custom error data.</param>
        /// <returns>The created network custom error event.</returns>
        public static NetworkCustomErrorEventArgs Create(INetworkChannel networkChannel, object customErrorData)
        {
            NetworkCustomErrorEventArgs networkCustomErrorEventArgs = ReferencePool.Acquire<NetworkCustomErrorEventArgs>();
            networkCustomErrorEventArgs.NetworkChannel = networkChannel;
            networkCustomErrorEventArgs.CustomErrorData = customErrorData;
            return networkCustomErrorEventArgs;
        }

        /// <summary>
        /// Clears the network custom error event.
        /// </summary>
        public override void Clear()
        {
            NetworkChannel = null;
            CustomErrorData = null;
        }
    }
}
