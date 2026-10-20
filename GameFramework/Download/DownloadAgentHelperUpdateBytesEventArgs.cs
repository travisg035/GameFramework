//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Download
{
    /// <summary>
    /// Download agent helper update bytes event.
    /// </summary>
    public sealed class DownloadAgentHelperUpdateBytesEventArgs : GameFrameworkEventArgs
    {
        private byte[] m_Bytes;

        /// <summary>
        /// Initializes a new instance of the DownloadAgentHelperUpdateBytesEventArgs class.
        /// </summary>
        public DownloadAgentHelperUpdateBytesEventArgs()
        {
            m_Bytes = null;
            Offset = 0;
            Length = 0;
        }

        /// <summary>
        /// Gets the offset in the bytes.
        /// </summary>
        public int Offset
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the length of the bytes.
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a download agent helper update bytes event.
        /// </summary>
        /// <param name="bytes">Downloaded bytes.</param>
        /// <param name="offset">Offset in the bytes.</param>
        /// <param name="length">Length of the bytes.</param>
        /// <returns>The created download agent helper update bytes event.</returns>
        public static DownloadAgentHelperUpdateBytesEventArgs Create(byte[] bytes, int offset, int length)
        {
            if (bytes == null)
            {
                throw new GameFrameworkException("Bytes is invalid.");
            }

            if (offset < 0 || offset >= bytes.Length)
            {
                throw new GameFrameworkException("Offset is invalid.");
            }

            if (length <= 0 || offset + length > bytes.Length)
            {
                throw new GameFrameworkException("Length is invalid.");
            }

            DownloadAgentHelperUpdateBytesEventArgs downloadAgentHelperUpdateBytesEventArgs = ReferencePool.Acquire<DownloadAgentHelperUpdateBytesEventArgs>();
            downloadAgentHelperUpdateBytesEventArgs.m_Bytes = bytes;
            downloadAgentHelperUpdateBytesEventArgs.Offset = offset;
            downloadAgentHelperUpdateBytesEventArgs.Length = length;
            return downloadAgentHelperUpdateBytesEventArgs;
        }

        /// <summary>
        /// Clears the download agent helper update bytes event.
        /// </summary>
        public override void Clear()
        {
            m_Bytes = null;
            Offset = 0;
            Length = 0;
        }

        /// <summary>
        /// Gets the downloaded bytes.
        /// </summary>
        public byte[] GetBytes()
        {
            return m_Bytes;
        }
    }
}
