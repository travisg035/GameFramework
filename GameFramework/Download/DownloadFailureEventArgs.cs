//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Download
{
    /// <summary>
    /// Download failure event.
    /// </summary>
    public sealed class DownloadFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DownloadFailureEventArgs class.
        /// </summary>
        public DownloadFailureEventArgs()
        {
            SerialId = 0;
            DownloadPath = null;
            DownloadUri = null;
            ErrorMessage = null;
            UserData = null;
        }

        /// <summary>
        /// Gets the serial ID of the download task.
        /// </summary>
        public int SerialId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the download path.
        /// </summary>
        public string DownloadPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the download URI.
        /// </summary>
        public string DownloadUri
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
        /// Gets the user-defined data.
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a download failure event.
        /// </summary>
        /// <param name="serialId">The serial ID of the download task.</param>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Download URI.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created download failure event.</returns>
        public static DownloadFailureEventArgs Create(int serialId, string downloadPath, string downloadUri, string errorMessage, object userData)
        {
            DownloadFailureEventArgs downloadFailureEventArgs = ReferencePool.Acquire<DownloadFailureEventArgs>();
            downloadFailureEventArgs.SerialId = serialId;
            downloadFailureEventArgs.DownloadPath = downloadPath;
            downloadFailureEventArgs.DownloadUri = downloadUri;
            downloadFailureEventArgs.ErrorMessage = errorMessage;
            downloadFailureEventArgs.UserData = userData;
            return downloadFailureEventArgs;
        }

        /// <summary>
        /// Clears the download failure event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            DownloadPath = null;
            DownloadUri = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
