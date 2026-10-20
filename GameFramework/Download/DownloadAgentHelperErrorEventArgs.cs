//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Download
{
    /// <summary>
    /// Download agent helper error event.
    /// </summary>
    public sealed class DownloadAgentHelperErrorEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DownloadAgentHelperErrorEventArgs class.
        /// </summary>
        public DownloadAgentHelperErrorEventArgs()
        {
            DeleteDownloading = false;
            ErrorMessage = null;
        }

        /// <summary>
        /// Gets whether to delete the file currently being downloaded.
        /// </summary>
        public bool DeleteDownloading
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a download agent helper error event.
        /// </summary>
        /// <param name="deleteDownloading">Whether to delete the file currently being downloaded.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <returns>The created download agent helper error event.</returns>
        public static DownloadAgentHelperErrorEventArgs Create(bool deleteDownloading, string errorMessage)
        {
            DownloadAgentHelperErrorEventArgs downloadAgentHelperErrorEventArgs = ReferencePool.Acquire<DownloadAgentHelperErrorEventArgs>();
            downloadAgentHelperErrorEventArgs.DeleteDownloading = deleteDownloading;
            downloadAgentHelperErrorEventArgs.ErrorMessage = errorMessage;
            return downloadAgentHelperErrorEventArgs;
        }

        /// <summary>
        /// Clears the download agent helper error event.
        /// </summary>
        public override void Clear()
        {
            DeleteDownloading = false;
            ErrorMessage = null;
        }
    }
}
