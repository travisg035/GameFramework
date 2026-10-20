//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Download
{
    internal sealed partial class DownloadManager : GameFrameworkModule, IDownloadManager
    {
        /// <summary>
        /// Download task status.
        /// </summary>
        private enum DownloadTaskStatus : byte
        {
            /// <summary>
            /// Ready to download.
            /// </summary>
            Todo = 0,

            /// <summary>
            /// Downloading.
            /// </summary>
            Doing,

            /// <summary>
            /// Download completed.
            /// </summary>
            Done,

            /// <summary>
            /// Download error.
            /// </summary>
            Error
        }
    }
}
