//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace GameFramework.Download
{
    /// <summary>
    /// Download extension methods providing asynchronous operations.
    /// </summary>
    public static class DownloadExtension
    {
        private const int DefaultPriority = 0;

        /// <summary>
        /// Adds a download task asynchronously.
        /// </summary>
        /// <param name="downloadManager">The download manager.</param>
        /// <param name="downloadPath">Download path.</param>
        /// <param name="downloadUri">Original download URI.</param>
        /// <param name="tag">Download task tag.</param>
        /// <param name="priority">Download task priority.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the download operation returning the downloaded file path.</returns>
        public static Task<string> AddDownloadAsync(this IDownloadManager downloadManager, string downloadPath, string downloadUri, string tag = null, int priority = DefaultPriority, object userData = null)
        {
            if (downloadManager == null)
            {
                throw new GameFrameworkException("Download manager is invalid.");
            }

            if (string.IsNullOrEmpty(downloadPath))
            {
                throw new GameFrameworkException("Download path is invalid.");
            }

            if (string.IsNullOrEmpty(downloadUri))
            {
                throw new GameFrameworkException("Download URI is invalid.");
            }

            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            int serialId = 0;
            EventHandler<DownloadSuccessEventArgs> successHandler = null;
            EventHandler<DownloadFailureEventArgs> failureHandler = null;

            void Unsubscribe()
            {
                downloadManager.DownloadSuccess -= successHandler;
                downloadManager.DownloadFailure -= failureHandler;
            }

            successHandler = (sender, e) =>
            {
                if (e.SerialId == serialId)
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetResult(e.DownloadPath);
                }
            };

            failureHandler = (sender, e) =>
            {
                if (e.SerialId == serialId)
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetException(new GameFrameworkException(e.ErrorMessage));
                }
            };

            downloadManager.DownloadSuccess += successHandler;
            downloadManager.DownloadFailure += failureHandler;

            try
            {
                serialId = downloadManager.AddDownload(downloadPath, downloadUri, tag, priority, userData);
            }
            catch (Exception exception)
            {
                Unsubscribe();
                taskCompletionSource.TrySetException(exception);
            }

            return taskCompletionSource.Task;
        }
    }
}
