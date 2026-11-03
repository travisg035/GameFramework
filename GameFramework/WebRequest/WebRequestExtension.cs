//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace GameFramework.WebRequest
{
    /// <summary>
    /// Web request extension methods providing asynchronous operations.
    /// </summary>
    public static class WebRequestExtension
    {
        private const int DefaultPriority = 0;

        /// <summary>
        /// Adds a web request task asynchronously.
        /// </summary>
        /// <param name="webRequestManager">The web request manager.</param>
        /// <param name="webRequestUri">The web request URI.</param>
        /// <param name="postData">Post data in binary bytes.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the web request operation returning response bytes.</returns>
        public static Task<byte[]> AddWebRequestAsync(this IWebRequestManager webRequestManager, string webRequestUri, byte[] postData = null, string tag = null, int priority = DefaultPriority, object userData = null)
        {
            if (webRequestManager == null)
            {
                throw new GameFrameworkException("Web request manager is invalid.");
            }

            if (string.IsNullOrEmpty(webRequestUri))
            {
                throw new GameFrameworkException("Web request URI is invalid.");
            }

            TaskCompletionSource<byte[]> taskCompletionSource = new TaskCompletionSource<byte[]>(TaskCreationOptions.RunContinuationsAsynchronously);

            int serialId = 0;
            EventHandler<WebRequestSuccessEventArgs> successHandler = null;
            EventHandler<WebRequestFailureEventArgs> failureHandler = null;

            void Unsubscribe()
            {
                webRequestManager.WebRequestSuccess -= successHandler;
                webRequestManager.WebRequestFailure -= failureHandler;
            }

            successHandler = (sender, e) =>
            {
                if (e.SerialId == serialId)
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetResult(e.GetWebResponseBytes());
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

            webRequestManager.WebRequestSuccess += successHandler;
            webRequestManager.WebRequestFailure += failureHandler;

            try
            {
                serialId = webRequestManager.AddWebRequest(webRequestUri, postData, tag, priority, userData);
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
