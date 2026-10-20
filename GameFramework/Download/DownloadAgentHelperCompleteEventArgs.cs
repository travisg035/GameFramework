//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Download
{
    /// <summary>
    /// Download agent helper complete event.
    /// </summary>
    public sealed class DownloadAgentHelperCompleteEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DownloadAgentHelperCompleteEventArgs class.
        /// </summary>
        public DownloadAgentHelperCompleteEventArgs()
        {
            Length = 0L;
        }

        /// <summary>
        /// Gets the downloaded data length.
        /// </summary>
        public long Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a download agent helper complete event.
        /// </summary>
        /// <param name="length">Downloaded data length.</param>
        /// <returns>The created download agent helper complete event.</returns>
        public static DownloadAgentHelperCompleteEventArgs Create(long length)
        {
            if (length < 0L)
            {
                throw new GameFrameworkException("Length is invalid.");
            }

            DownloadAgentHelperCompleteEventArgs downloadAgentHelperCompleteEventArgs = ReferencePool.Acquire<DownloadAgentHelperCompleteEventArgs>();
            downloadAgentHelperCompleteEventArgs.Length = length;
            return downloadAgentHelperCompleteEventArgs;
        }

        /// <summary>
        /// Clears the download agent helper complete event.
        /// </summary>
        public override void Clear()
        {
            Length = 0L;
        }
    }
}
