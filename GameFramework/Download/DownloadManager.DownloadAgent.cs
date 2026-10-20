//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.IO;

namespace GameFramework.Download
{
    internal sealed partial class DownloadManager : GameFrameworkModule, IDownloadManager
    {
        /// <summary>
        /// Download agent.
        /// </summary>
        private sealed class DownloadAgent : ITaskAgent<DownloadTask>, IDisposable
        {
            private readonly IDownloadAgentHelper m_Helper;
            private DownloadTask m_Task;
            private FileStream m_FileStream;
            private int m_WaitFlushSize;
            private float m_WaitTime;
            private long m_StartLength;
            private long m_DownloadedLength;
            private long m_SavedLength;
            private bool m_Disposed;

            public GameFrameworkAction<DownloadAgent> DownloadAgentStart;
            public GameFrameworkAction<DownloadAgent, int> DownloadAgentUpdate;
            public GameFrameworkAction<DownloadAgent, long> DownloadAgentSuccess;
            public GameFrameworkAction<DownloadAgent, string> DownloadAgentFailure;

            /// <summary>
            /// Initializes a new instance of the DownloadAgent class.
            /// </summary>
            /// <param name="downloadAgentHelper">Download agent helper.</param>
            public DownloadAgent(IDownloadAgentHelper downloadAgentHelper)
            {
                if (downloadAgentHelper == null)
                {
                    throw new GameFrameworkException("Download agent helper is invalid.");
                }

                m_Helper = downloadAgentHelper;
                m_Task = null;
                m_FileStream = null;
                m_WaitFlushSize = 0;
                m_WaitTime = 0f;
                m_StartLength = 0L;
                m_DownloadedLength = 0L;
                m_SavedLength = 0L;
                m_Disposed = false;

                DownloadAgentStart = null;
                DownloadAgentUpdate = null;
                DownloadAgentSuccess = null;
                DownloadAgentFailure = null;
            }

            /// <summary>
            /// Gets the download task.
            /// </summary>
            public DownloadTask Task
            {
                get
                {
                    return m_Task;
                }
            }

            /// <summary>
            /// Gets the waiting time in seconds.
            /// </summary>
            public float WaitTime
            {
                get
                {
                    return m_WaitTime;
                }
            }

            /// <summary>
            /// Gets the length that already existed when the download started.
            /// </summary>
            public long StartLength
            {
                get
                {
                    return m_StartLength;
                }
            }

            /// <summary>
            /// Gets the downloaded length of this download.
            /// </summary>
            public long DownloadedLength
            {
                get
                {
                    return m_DownloadedLength;
                }
            }

            /// <summary>
            /// Gets the current length.
            /// </summary>
            public long CurrentLength
            {
                get
                {
                    return m_StartLength + m_DownloadedLength;
                }
            }

            /// <summary>
            /// Gets the saved length.
            /// </summary>
            public long SavedLength
            {
                get
                {
                    return m_SavedLength;
                }
            }

            /// <summary>
            /// Initializes the download agent.
            /// </summary>
            public void Initialize()
            {
                m_Helper.DownloadAgentHelperUpdateBytes += OnDownloadAgentHelperUpdateBytes;
                m_Helper.DownloadAgentHelperUpdateLength += OnDownloadAgentHelperUpdateLength;
                m_Helper.DownloadAgentHelperComplete += OnDownloadAgentHelperComplete;
                m_Helper.DownloadAgentHelperError += OnDownloadAgentHelperError;
            }

            /// <summary>
            /// Updates the download agent per frame.
            /// </summary>
            /// <param name="elapseSeconds">Logical elapsed seconds.</param>
            /// <param name="realElapseSeconds">Real elapsed seconds.</param>
            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                if (m_Task.Status == DownloadTaskStatus.Doing)
                {
                    m_WaitTime += realElapseSeconds;
                    if (m_WaitTime >= m_Task.Timeout)
                    {
                        DownloadAgentHelperErrorEventArgs downloadAgentHelperErrorEventArgs = DownloadAgentHelperErrorEventArgs.Create(false, "Timeout");
                        OnDownloadAgentHelperError(this, downloadAgentHelperErrorEventArgs);
                        ReferencePool.Release(downloadAgentHelperErrorEventArgs);
                    }
                }
            }

            /// <summary>
            /// Shutdowns and cleans up the download agent.
            /// </summary>
            public void Shutdown()
            {
                Dispose();

                m_Helper.DownloadAgentHelperUpdateBytes -= OnDownloadAgentHelperUpdateBytes;
                m_Helper.DownloadAgentHelperUpdateLength -= OnDownloadAgentHelperUpdateLength;
                m_Helper.DownloadAgentHelperComplete -= OnDownloadAgentHelperComplete;
                m_Helper.DownloadAgentHelperError -= OnDownloadAgentHelperError;
            }

            /// <summary>
            /// Starts processing the download task.
            /// </summary>
            /// <param name="task">The download task to process.</param>
            /// <returns>The start task status.</returns>
            public StartTaskStatus Start(DownloadTask task)
            {
                if (task == null)
                {
                    throw new GameFrameworkException("Task is invalid.");
                }

                m_Task = task;

                m_Task.Status = DownloadTaskStatus.Doing;
                string downloadFile = Utility.Text.Format("{0}.download", m_Task.DownloadPath);

                try
                {
                    if (File.Exists(downloadFile))
                    {
                        m_FileStream = File.OpenWrite(downloadFile);
                        m_FileStream.Seek(0L, SeekOrigin.End);
                        m_StartLength = m_SavedLength = m_FileStream.Length;
                        m_DownloadedLength = 0L;
                    }
                    else
                    {
                        string directory = Path.GetDirectoryName(m_Task.DownloadPath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        m_FileStream = new FileStream(downloadFile, FileMode.Create, FileAccess.Write);
                        m_StartLength = m_SavedLength = m_DownloadedLength = 0L;
                    }

                    if (DownloadAgentStart != null)
                    {
                        DownloadAgentStart(this);
                    }

                    if (m_StartLength > 0L)
                    {
                        m_Helper.Download(m_Task.DownloadUri, m_StartLength, m_Task.UserData);
                    }
                    else
                    {
                        m_Helper.Download(m_Task.DownloadUri, m_Task.UserData);
                    }

                    return StartTaskStatus.CanResume;
                }
                catch (Exception exception)
                {
                    DownloadAgentHelperErrorEventArgs downloadAgentHelperErrorEventArgs = DownloadAgentHelperErrorEventArgs.Create(false, exception.ToString());
                    OnDownloadAgentHelperError(this, downloadAgentHelperErrorEventArgs);
                    ReferencePool.Release(downloadAgentHelperErrorEventArgs);
                    return StartTaskStatus.UnknownError;
                }
            }

            /// <summary>
            /// Resets the download agent.
            /// </summary>
            public void Reset()
            {
                m_Helper.Reset();

                if (m_FileStream != null)
                {
                    m_FileStream.Close();
                    m_FileStream = null;
                }

                m_Task = null;
                m_WaitFlushSize = 0;
                m_WaitTime = 0f;
                m_StartLength = 0L;
                m_DownloadedLength = 0L;
                m_SavedLength = 0L;
            }

            /// <summary>
            /// Releases all resources used by the DownloadAgent.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Releases unmanaged and - optionally - managed resources.
            /// </summary>
            /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
            private void Dispose(bool disposing)
            {
                if (m_Disposed)
                {
                    return;
                }

                if (disposing)
                {
                    if (m_FileStream != null)
                    {
                        m_FileStream.Dispose();
                        m_FileStream = null;
                    }
                }

                m_Disposed = true;
            }

            private void OnDownloadAgentHelperUpdateBytes(object sender, DownloadAgentHelperUpdateBytesEventArgs e)
            {
                m_WaitTime = 0f;
                try
                {
                    m_FileStream.Write(e.GetBytes(), e.Offset, e.Length);
                    m_WaitFlushSize += e.Length;
                    m_SavedLength += e.Length;

                    if (m_WaitFlushSize >= m_Task.FlushSize)
                    {
                        m_FileStream.Flush();
                        m_WaitFlushSize = 0;
                    }
                }
                catch (Exception exception)
                {
                    DownloadAgentHelperErrorEventArgs downloadAgentHelperErrorEventArgs = DownloadAgentHelperErrorEventArgs.Create(false, exception.ToString());
                    OnDownloadAgentHelperError(this, downloadAgentHelperErrorEventArgs);
                    ReferencePool.Release(downloadAgentHelperErrorEventArgs);
                }
            }

            private void OnDownloadAgentHelperUpdateLength(object sender, DownloadAgentHelperUpdateLengthEventArgs e)
            {
                m_WaitTime = 0f;
                m_DownloadedLength += e.DeltaLength;
                if (DownloadAgentUpdate != null)
                {
                    DownloadAgentUpdate(this, e.DeltaLength);
                }
            }

            private void OnDownloadAgentHelperComplete(object sender, DownloadAgentHelperCompleteEventArgs e)
            {
                m_WaitTime = 0f;
                m_DownloadedLength = e.Length;
                if (m_SavedLength != CurrentLength)
                {
                    throw new GameFrameworkException("Internal download error.");
                }

                m_Helper.Reset();
                m_FileStream.Close();
                m_FileStream = null;

                if (File.Exists(m_Task.DownloadPath))
                {
                    File.Delete(m_Task.DownloadPath);
                }

                File.Move(Utility.Text.Format("{0}.download", m_Task.DownloadPath), m_Task.DownloadPath);

                m_Task.Status = DownloadTaskStatus.Done;

                if (DownloadAgentSuccess != null)
                {
                    DownloadAgentSuccess(this, e.Length);
                }

                m_Task.Done = true;
            }

            private void OnDownloadAgentHelperError(object sender, DownloadAgentHelperErrorEventArgs e)
            {
                m_Helper.Reset();
                if (m_FileStream != null)
                {
                    m_FileStream.Close();
                    m_FileStream = null;
                }

                if (e.DeleteDownloading)
                {
                    File.Delete(Utility.Text.Format("{0}.download", m_Task.DownloadPath));
                }

                m_Task.Status = DownloadTaskStatus.Error;

                if (DownloadAgentFailure != null)
                {
                    DownloadAgentFailure(this, e.ErrorMessage);
                }

                m_Task.Done = true;
            }
        }
    }
}
