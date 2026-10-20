//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load resource agent helper asynchronous read binary bytes complete event.
    /// </summary>
    public sealed class LoadResourceAgentHelperReadBytesCompleteEventArgs : GameFrameworkEventArgs
    {
        private byte[] m_Bytes;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadResourceAgentHelperReadBytesCompleteEventArgs"/> class.
        /// </summary>
        public LoadResourceAgentHelperReadBytesCompleteEventArgs()
        {
            m_Bytes = null;
        }

        /// <summary>
        /// Creates a new load resource agent helper asynchronous read binary bytes complete event.
        /// </summary>
        /// <param name="bytes">The binary bytes of the resource.</param>
        /// <returns>The created load resource agent helper asynchronous read binary bytes complete event.</returns>
        public static LoadResourceAgentHelperReadBytesCompleteEventArgs Create(byte[] bytes)
        {
            LoadResourceAgentHelperReadBytesCompleteEventArgs loadResourceAgentHelperReadBytesCompleteEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperReadBytesCompleteEventArgs>();
            loadResourceAgentHelperReadBytesCompleteEventArgs.m_Bytes = bytes;
            return loadResourceAgentHelperReadBytesCompleteEventArgs;
        }

        /// <summary>
        /// Clears the load resource agent helper asynchronous read binary bytes complete event.
        /// </summary>
        public override void Clear()
        {
            m_Bytes = null;
        }

        /// <summary>
        /// Gets the binary bytes of the resource.
        /// </summary>
        /// <returns>The binary bytes of the resource.</returns>
        public byte[] GetBytes()
        {
            return m_Bytes;
        }
    }
}
