//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Download
{
    /// <summary>
    /// Download agent helper update length event.
    /// </summary>
    public sealed class DownloadAgentHelperUpdateLengthEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DownloadAgentHelperUpdateLengthEventArgs class.
        /// </summary>
        public DownloadAgentHelperUpdateLengthEventArgs()
        {
            DeltaLength = 0;
        }

        /// <summary>
        /// Gets the delta length of the downloaded data.
        /// </summary>
        public int DeltaLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a download agent helper update length event.
        /// </summary>
        /// <param name="deltaLength">Delta length of the downloaded data.</param>
        /// <returns>The created download agent helper update length event.</returns>
        public static DownloadAgentHelperUpdateLengthEventArgs Create(int deltaLength)
        {
            if (deltaLength <= 0)
            {
                throw new GameFrameworkException("Delta length is invalid.");
            }

            DownloadAgentHelperUpdateLengthEventArgs downloadAgentHelperUpdateLengthEventArgs = ReferencePool.Acquire<DownloadAgentHelperUpdateLengthEventArgs>();
            downloadAgentHelperUpdateLengthEventArgs.DeltaLength = deltaLength;
            return downloadAgentHelperUpdateLengthEventArgs;
        }

        /// <summary>
        /// Clears the download agent helper update length event.
        /// </summary>
        public override void Clear()
        {
            DeltaLength = 0;
        }
    }
}
