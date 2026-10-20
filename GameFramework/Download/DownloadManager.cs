//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.Download
{
    /// <summary>
    /// Download manager.
    /// </summary>
    internal sealed partial class DownloadManager : GameFrameworkModule, IDownloadManager
    {
        private const int OneMegaBytes = 1024 * 1024;

        private readonly TaskPool<DownloadTask> m_TaskPool;
        private readonly DownloadCounter m_DownloadCounter;
        private int m_FlushSize;
        private float m_Timeout;
        private EventHandler<DownloadStartEventArgs> m_DownloadStartEventHandler;
        private EventHandler<DownloadUpdateEventArgs> m_DownloadUpdateEventHandler;
        private EventHandler<DownloadSuccessEventArgs> m_DownloadSuccessEventHandler;
        private EventHandler<DownloadFailureEventArgs> m_DownloadFailureEventHandler;

        /// <summary>
        /// Initializes a new instance of the DownloadManager class.
        /// </summary>
        public DownloadManager()
        {
            m_TaskPool = new TaskPool<DownloadTask>();
            m_DownloadCounter = new DownloadCounter(1f, 10f);
            m_FlushSize = OneMegaBytes;
            m_Timeout = 30f;
            m_DownloadStartEventHandler = null;
            m_DownloadUpdateEventHandler = null;
            m_DownloadSuccessEventHandler = null;
            m_DownloadFailureEventHandler = null;
        }

        /// <summary>
        /// Gets the priority of the game framework module.
        /// </summary>
        /// <remarks>Modules with higher priority will be polled first and shut down later.</remarks>
        internal override int Priority
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// Gets or sets whether download is paused.
        /// </summary>
        public bool Paused
        {
            get
            {
                return m_TaskPool.Paused;
            }
            set
            {
                m_TaskPool.Paused = value;
            }
        }

        /// <summary>
        /// Gets the total number of download agents.
        /// </summary>
        public int TotalAgentCount
        {
            get
            {
                return m_TaskPool.TotalAgentCount;
            }
        }

        /// <summary>
        /// Gets the number of free download agents.
        /// </summary>
        public int FreeAgentCount
        {
            get
            {
                return m_TaskPool.FreeAgentCount;
            }
        }

        /// <summary>
        /// Gets the number of working download agents.
        /// </summary>
        public int WorkingAgentCount
        {
            get
            {
                return m_TaskPool.WorkingAgentCount;
            }
        }

        /// <summary>
        /// Gets the number of waiting download tasks.
        /// </summary>
        public int WaitingTaskCount
        {
            get
            {
                return m_TaskPool.WaitingTaskCount;
            }
        }

        /// <summary>
        /// Gets or sets the threshold size in bytes for flushing the buffer to disk.
        /// </summary>
        public int FlushSize
        {
            get
            {
                return m_FlushSize;
            }
            set
            {
                m_FlushSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the download timeout in seconds.
        /// </summary>
        public float Timeout
        {
            get
            {
                return m_Timeout;
            }
            set
            {
                m_Timeout = value;
            }
        }

        /// <summary>
        /// Gets the current download speed.
        /// </summary>
        public float CurrentSpeed
        {
            get
            {
                return m_DownloadCounter.CurrentSpeed;
            }
        }

        /// <summary>
        /// Download start event.
        /// </summary>
        public event EventHandler<DownloadStartEventArgs> DownloadStart
        {
            add
            {
                m_DownloadStartEventHandler += value;
            }
            remove
            {
                m_DownloadStartEventHandler -= value;
            }
        }

        /// <summary>
        /// Download update event.
        /// </summary>
        public event EventHandler<DownloadUpdateEventArgs> DownloadUpdate
        {
            add
            {
                m_DownloadUpdateEventHandler += value;
            }
            remove
            {
                m_DownloadUpdateEventHandler -= value;
            }
        }

        /// <summary>
        /// Download success event.
        /// </summary>
        public event EventHandler<DownloadSuccessEventArgs> DownloadSuccess
        {
            add
            {
                m_DownloadSuccessEventHandler += value;
            }
            remove
            {
                m_DownloadSuccessEventHandler -= value;
            }
        }

        /// <summary>
        /// Download failure event.
        /// </summary>
        public event EventHandler<DownloadFailureEventArgs> DownloadFailure
        {
            add
            {
                m_DownloadFailureEventHandler += value;
            }
            remove
            {
                m_DownloadFailureEventHandler -= value;
            }
        }

        /// <summary>
        /// Polls the download manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_TaskPool.Update(elapseSeconds, realElapseSeconds);
            m_DownloadCounter.Update(elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// Shuts down and cleans up the download manager.
        /// </summary>
        internal override void Shutdown()
        {
            m_TaskPool.Shutdown();
            m_DownloadCounter.Shutdown();
        }

        /// <summary>
        /// Adds a download agent helper.
        /// </summary>
        /// <param name="downloadAgentHelper">The download agent helper to add.</param>
        public void AddDownloadAgentHelper(IDownloadAgentHelper downloadAgentHelper)
        {
            DownloadAgent agent = new DownloadAgent(downloadAgentHelper);
            agent.DownloadAgentStart += OnDownloadAgentStart;
            agent.DownloadAgentUpdate += OnDownloadAgentUpdate;
            agent.DownloadAgentSuccess += OnDownloadAgentSuccess;
            agent.DownloadAgentFailure += OnDownloadAgentFailure;

            m_TaskPool.AddAgent(agent);
        }

        /// <summary>
        /// Gets the download task information by serial ID.
        /// </summary>
        /// <param name="serialId">The serial ID of the download task to get information.</param>
        /// <returns>The download task information.</returns>
        public TaskInfo GetDownloadInfo(int serialId)
        {
            return m_TaskPool.GetTaskInfo(serialId);
        }

        /// <summary>
        /// Gets the download task information by tag.
        /// </summary>
        /// <param name="tag">The tag of the download task to get information.</param>
        /// <returns>The download task information.</returns>
        public TaskInfo[] GetDownloadInfos(string tag)
        {
            return m_TaskPool.GetTaskInfos(tag);
        }

        /// <summary>
        /// Gets the download task information by tag.
        /// </summary>
        /// <param name="tag">The tag of the download task to get information.</param>
        /// <param name="results">The download task information.</param>
        public void GetDownloadInfos(string tag, List<TaskInfo> results)
        {
            m_TaskPool.GetTaskInfos(tag, results);
        }

        /// <summary>
        /// Gets all download task information.
        /// </summary>
        /// <returns>All download task information.</returns>
        public TaskInfo[] GetAllDownloadInfos()
        {
            return m_TaskPool.GetAllTaskInfos();
        }

        /// <summary>
        /// Gets all download task information.
        /// </summary>
        /// <param name="results">All download task information.</param>
        public void GetAllDownloadInfos(List<TaskInfo> results)
        {
            m_TaskPool.GetAllTaskInfos(results);
        }

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <returns>The serial ID of the added download task.</returns>
        public int AddDownload(string downloadPath, string downloadUri)
        {
            return AddDownload(downloadPath, downloadUri, null, Constant.DefaultPriority, null);
        }

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="tag">Download task tag.</param>
        /// <returns>The serial ID of the added download task.</returns>
        public int AddDownload(string downloadPath, string downloadUri, string tag)
        {
            return AddDownload(downloadPath, downloadUri, tag, Constant.DefaultPriority, null);
        }

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="priority">Download task priority.</param>
        /// <returns>The serial ID of the added download task.</returns>
        public int AddDownload(string downloadPath, string downloadUri, int priority)
        {
            return AddDownload(downloadPath, downloadUri, null, priority, null);
        }

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The serial ID of the added download task.</returns>
        public int AddDownload(string downloadPath, string downloadUri, object userData)
        {
            return AddDownload(downloadPath, downloadUri, null, Constant.DefaultPriority, userData);
        }

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="tag">Download task tag.</param>
        /// <param name="priority">Download task priority.</param>
        /// <returns>The serial ID of the added download task.</returns>
        public int AddDownload(string downloadPath, string downloadUri, string tag, int priority)
        {
            return AddDownload(downloadPath, downloadUri, tag, priority, null);
        }

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="tag">Download task tag.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The serial ID of the added download task.</returns>
        public int AddDownload(string downloadPath, string downloadUri, string tag, object userData)
        {
            return AddDownload(downloadPath, downloadUri, tag, Constant.DefaultPriority, userData);
        }

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="priority">Download task priority.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The serial ID of the added download task.</returns>
        public int AddDownload(string downloadPath, string downloadUri, int priority, object userData)
        {
            return AddDownload(downloadPath, downloadUri, null, priority, userData);
        }

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="tag">Download task tag.</param>
        /// <param name="priority">Download task priority.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The serial ID of the added download task.</returns>
        public int AddDownload(string downloadPath, string downloadUri, string tag, int priority, object userData)
        {
            if (string.IsNullOrEmpty(downloadPath))
            {
                throw new GameFrameworkException("Download path is invalid.");
            }

            if (string.IsNullOrEmpty(downloadUri))
            {
                throw new GameFrameworkException("Download uri is invalid.");
            }

            if (TotalAgentCount <= 0)
            {
                throw new GameFrameworkException("You must add download agent first.");
            }

            DownloadTask downloadTask = DownloadTask.Create(downloadPath, downloadUri, tag, priority, m_FlushSize, m_Timeout, userData);
            m_TaskPool.AddTask(downloadTask);
            return downloadTask.SerialId;
        }

        /// <summary>
        /// Removes a download task by serial ID.
        /// </summary>
        /// <param name="serialId">The serial ID of the download task to remove.</param>
        /// <returns>Whether the download task was removed successfully.</returns>
        public bool RemoveDownload(int serialId)
        {
            return m_TaskPool.RemoveTask(serialId);
        }

        /// <summary>
        /// Removes download tasks by tag.
        /// </summary>
        /// <param name="tag">The tag of the download tasks to remove.</param>
        /// <returns>The number of removed download tasks.</returns>
        public int RemoveDownloads(string tag)
        {
            return m_TaskPool.RemoveTasks(tag);
        }

        /// <summary>
        /// Removes all download tasks.
        /// </summary>
        /// <returns>The number of removed download tasks.</returns>
        public int RemoveAllDownloads()
        {
            return m_TaskPool.RemoveAllTasks();
        }

        private void OnDownloadAgentStart(DownloadAgent sender)
        {
            if (m_DownloadStartEventHandler != null)
            {
                DownloadStartEventArgs downloadStartEventArgs = DownloadStartEventArgs.Create(sender.Task.SerialId, sender.Task.DownloadPath, sender.Task.DownloadUri, sender.CurrentLength, sender.Task.UserData);
                m_DownloadStartEventHandler(this, downloadStartEventArgs);
                ReferencePool.Release(downloadStartEventArgs);
            }
        }

        private void OnDownloadAgentUpdate(DownloadAgent sender, int deltaLength)
        {
            m_DownloadCounter.RecordDeltaLength(deltaLength);
            if (m_DownloadUpdateEventHandler != null)
            {
                DownloadUpdateEventArgs downloadUpdateEventArgs = DownloadUpdateEventArgs.Create(sender.Task.SerialId, sender.Task.DownloadPath, sender.Task.DownloadUri, sender.CurrentLength, sender.Task.UserData);
                m_DownloadUpdateEventHandler(this, downloadUpdateEventArgs);
                ReferencePool.Release(downloadUpdateEventArgs);
            }
        }

        private void OnDownloadAgentSuccess(DownloadAgent sender, long length)
        {
            if (m_DownloadSuccessEventHandler != null)
            {
                DownloadSuccessEventArgs downloadSuccessEventArgs = DownloadSuccessEventArgs.Create(sender.Task.SerialId, sender.Task.DownloadPath, sender.Task.DownloadUri, sender.CurrentLength, sender.Task.UserData);
                m_DownloadSuccessEventHandler(this, downloadSuccessEventArgs);
                ReferencePool.Release(downloadSuccessEventArgs);
            }
        }

        private void OnDownloadAgentFailure(DownloadAgent sender, string errorMessage)
        {
            if (m_DownloadFailureEventHandler != null)
            {
                DownloadFailureEventArgs downloadFailureEventArgs = DownloadFailureEventArgs.Create(sender.Task.SerialId, sender.Task.DownloadPath, sender.Task.DownloadUri, errorMessage, sender.Task.UserData);
                m_DownloadFailureEventHandler(this, downloadFailureEventArgs);
                ReferencePool.Release(downloadFailureEventArgs);
            }
        }
    }
}
