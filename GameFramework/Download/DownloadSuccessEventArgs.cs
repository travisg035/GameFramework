//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Download
{
    /// <summary>
    /// Download success event.
    /// </summary>
    public sealed class DownloadSuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DownloadSuccessEventArgs class.
        /// </summary>
        public DownloadSuccessEventArgs()
        {
            SerialId = 0;
            DownloadPath = null;
            DownloadUri = null;
            CurrentLength = 0L;
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
        /// Gets the current length.
        /// </summary>
        public long CurrentLength
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
        /// Creates a download success event.
        /// </summary>
        /// <param name="serialId">The serial ID of the download task.</param>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Download URI.</param>
        /// <param name="currentLength">Current length.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created download success event.</returns>
        public static DownloadSuccessEventArgs Create(int serialId, string downloadPath, string downloadUri, long currentLength, object userData)
        {
            DownloadSuccessEventArgs downloadSuccessEventArgs = ReferencePool.Acquire<DownloadSuccessEventArgs>();
            downloadSuccessEventArgs.SerialId = serialId;
            downloadSuccessEventArgs.DownloadPath = downloadPath;
            downloadSuccessEventArgs.DownloadUri = downloadUri;
            downloadSuccessEventArgs.CurrentLength = currentLength;
            downloadSuccessEventArgs.UserData = userData;
            return downloadSuccessEventArgs;
        }

        /// <summary>
        /// Clears the download success event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            DownloadPath = null;
            DownloadUri = null;
            CurrentLength = 0L;
            UserData = null;
        }
    }
}
