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
    /// Download manager interface.
    /// </summary>
    public interface IDownloadManager
    {
        /// <summary>
        /// Gets or sets whether download is paused.
        /// </summary>
        bool Paused
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the total number of download agents.
        /// </summary>
        int TotalAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of free download agents.
        /// </summary>
        int FreeAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of working download agents.
        /// </summary>
        int WorkingAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of waiting download tasks.
        /// </summary>
        int WaitingTaskCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the threshold size in bytes for flushing the buffer to disk.
        /// </summary>
        int FlushSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the download timeout in seconds.
        /// </summary>
        float Timeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current download speed.
        /// </summary>
        float CurrentSpeed
        {
            get;
        }

        /// <summary>
        /// Download start event.
        /// </summary>
        event EventHandler<DownloadStartEventArgs> DownloadStart;

        /// <summary>
        /// Download update event.
        /// </summary>
        event EventHandler<DownloadUpdateEventArgs> DownloadUpdate;

        /// <summary>
        /// Download success event.
        /// </summary>
        event EventHandler<DownloadSuccessEventArgs> DownloadSuccess;

        /// <summary>
        /// Download failure event.
        /// </summary>
        event EventHandler<DownloadFailureEventArgs> DownloadFailure;

        /// <summary>
        /// Adds a download agent helper.
        /// </summary>
        /// <param name="downloadAgentHelper">The download agent helper to add.</param>
        void AddDownloadAgentHelper(IDownloadAgentHelper downloadAgentHelper);

        /// <summary>
        /// Gets the download task information by serial ID.
        /// </summary>
        /// <param name="serialId">The serial ID of the download task to get information.</param>
        /// <returns>The download task information.</returns>
        TaskInfo GetDownloadInfo(int serialId);

        /// <summary>
        /// Gets the download task information by tag.
        /// </summary>
        /// <param name="tag">The tag of the download task to get information.</param>
        /// <returns>The download task information.</returns>
        TaskInfo[] GetDownloadInfos(string tag);

        /// <summary>
        /// Gets the download task information by tag.
        /// </summary>
        /// <param name="tag">The tag of the download task to get information.</param>
        /// <param name="results">The download task information.</param>
        void GetDownloadInfos(string tag, List<TaskInfo> results);

        /// <summary>
        /// Gets all download task information.
        /// </summary>
        /// <returns>All download task information.</returns>
        TaskInfo[] GetAllDownloadInfos();

        /// <summary>
        /// Gets all download task information.
        /// </summary>
        /// <param name="results">All download task information.</param>
        void GetAllDownloadInfos(List<TaskInfo> results);

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <returns>The serial ID of the added download task.</returns>
        int AddDownload(string downloadPath, string downloadUri);

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="tag">Download task tag.</param>
        /// <returns>The serial ID of the added download task.</returns>
        int AddDownload(string downloadPath, string downloadUri, string tag);

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="priority">Download task priority.</param>
        /// <returns>The serial ID of the added download task.</returns>
        int AddDownload(string downloadPath, string downloadUri, int priority);

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The serial ID of the added download task.</returns>
        int AddDownload(string downloadPath, string downloadUri, object userData);

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="tag">Download task tag.</param>
        /// <param name="priority">Download task priority.</param>
        /// <returns>The serial ID of the added download task.</returns>
        int AddDownload(string downloadPath, string downloadUri, string tag, int priority);

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="tag">Download task tag.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The serial ID of the added download task.</returns>
        int AddDownload(string downloadPath, string downloadUri, string tag, object userData);

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="priority">Download task priority.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The serial ID of the added download task.</returns>
        int AddDownload(string downloadPath, string downloadUri, int priority, object userData);

        /// <summary>
        /// Adds a download task.
        /// </summary>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="tag">Download task tag.</param>
        /// <param name="priority">Download task priority.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The serial ID of the added download task.</returns>
        int AddDownload(string downloadPath, string downloadUri, string tag, int priority, object userData);

        /// <summary>
        /// Removes a download task by serial ID.
        /// </summary>
        /// <param name="serialId">The serial ID of the download task to remove.</param>
        /// <returns>Whether the download task was removed successfully.</returns>
        bool RemoveDownload(int serialId);

        /// <summary>
        /// Removes download tasks by tag.
        /// </summary>
        /// <param name="tag">The tag of the download tasks to remove.</param>
        /// <returns>The number of removed download tasks.</returns>
        int RemoveDownloads(string tag);

        /// <summary>
        /// Removes all download tasks.
        /// </summary>
        /// <returns>The number of removed download tasks.</returns>
        int RemoveAllDownloads();
    }
}
