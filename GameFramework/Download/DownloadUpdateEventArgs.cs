//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Download
{
    /// <summary>
    /// Download update event.
    /// </summary>
    public sealed class DownloadUpdateEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DownloadUpdateEventArgs class.
        /// </summary>
        public DownloadUpdateEventArgs()
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
        /// Creates a download update event.
        /// </summary>
        /// <param name="serialId">The serial ID of the download task.</param>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Download URI.</param>
        /// <param name="currentLength">Current length.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created download update event.</returns>
        public static DownloadUpdateEventArgs Create(int serialId, string downloadPath, string downloadUri, long currentLength, object userData)
        {
            DownloadUpdateEventArgs downloadUpdateEventArgs = ReferencePool.Acquire<DownloadUpdateEventArgs>();
            downloadUpdateEventArgs.SerialId = serialId;
            downloadUpdateEventArgs.DownloadPath = downloadPath;
            downloadUpdateEventArgs.DownloadUri = downloadUri;
            downloadUpdateEventArgs.CurrentLength = currentLength;
            downloadUpdateEventArgs.UserData = userData;
            return downloadUpdateEventArgs;
        }

        /// <summary>
        /// Clears the download update event.
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
