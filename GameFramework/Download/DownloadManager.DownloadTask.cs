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
        /// Download task.
        /// </summary>
        private sealed class DownloadTask : TaskBase
        {
            private static int s_Serial = 0;

            private DownloadTaskStatus m_Status;
            private string m_DownloadPath;
            private string m_DownloadUri;
            private int m_FlushSize;
            private float m_Timeout;

            /// <summary>
            /// Initializes a new instance of the DownloadTask class.
            /// </summary>
            public DownloadTask()
            {
                m_Status = DownloadTaskStatus.Todo;
                m_DownloadPath = null;
                m_DownloadUri = null;
                m_FlushSize = 0;
                m_Timeout = 0f;
            }

            /// <summary>
            /// Gets or sets the download task status.
            /// </summary>
            public DownloadTaskStatus Status
            {
                get
                {
                    return m_Status;
                }
                set
                {
                    m_Status = value;
                }
            }

            /// <summary>
            /// Gets the download path.
            /// </summary>
            public string DownloadPath
            {
                get
                {
                    return m_DownloadPath;
                }
            }

            /// <summary>
            /// Gets the original download URI.
            /// </summary>
            public string DownloadUri
            {
                get
                {
                    return m_DownloadUri;
                }
            }

            /// <summary>
            /// Gets the threshold size in bytes for flushing the buffer to disk.
            /// </summary>
            public int FlushSize
            {
                get
                {
                    return m_FlushSize;
                }
            }

            /// <summary>
            /// Gets the download timeout in seconds.
            /// </summary>
            public float Timeout
            {
                get
                {
                    return m_Timeout;
                }
            }

            /// <summary>
            /// Gets the description of the download task.
            /// </summary>
            public override string Description
            {
                get
                {
                    return m_DownloadPath;
                }
            }

            /// <summary>
            /// Creates a download task.
            /// </summary>
            /// <param name="downloadPath">Download path.</param>
            /// <param name="downloadUri">Original download URI.</param>
            /// <param name="tag">Download task tag.</param>
            /// <param name="priority">Download task priority.</param>
            /// <param name="flushSize">The threshold size in bytes for flushing the buffer to disk.</param>
            /// <param name="timeout">Download timeout in seconds.</param>
            /// <param name="userData">User-defined data.</param>
            /// <returns>The created download task.</returns>
            public static DownloadTask Create(string downloadPath, string downloadUri, string tag, int priority, int flushSize, float timeout, object userData)
            {
                DownloadTask downloadTask = ReferencePool.Acquire<DownloadTask>();
                downloadTask.Initialize(++s_Serial, tag, priority, userData);
                downloadTask.m_DownloadPath = downloadPath;
                downloadTask.m_DownloadUri = downloadUri;
                downloadTask.m_FlushSize = flushSize;
                downloadTask.m_Timeout = timeout;
                return downloadTask;
            }

            /// <summary>
            /// Clears the download task.
            /// </summary>
            public override void Clear()
            {
                base.Clear();
                m_Status = DownloadTaskStatus.Todo;
                m_DownloadPath = null;
                m_DownloadUri = null;
                m_FlushSize = 0;
                m_Timeout = 0f;
            }
        }
    }
}
