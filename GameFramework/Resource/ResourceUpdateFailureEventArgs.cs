//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource update failure event.
    /// </summary>
    public sealed class ResourceUpdateFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceUpdateFailureEventArgs"/> class.
        /// </summary>
        public ResourceUpdateFailureEventArgs()
        {
            Name = null;
            DownloadUri = null;
            RetryCount = 0;
            TotalRetryCount = 0;
            ErrorMessage = null;
        }

        /// <summary>
        /// Gets the resource name.
        /// </summary>
        public string Name
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
        /// Gets the number of retries already performed.
        /// </summary>
        public int RetryCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the configured total retry count.
        /// </summary>
        public int TotalRetryCount
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
        /// Creates a new resource update failure event.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="downloadUri">The download URI.</param>
        /// <param name="retryCount">The number of retries already performed.</param>
        /// <param name="totalRetryCount">The configured total retry count.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The created resource update failure event.</returns>
        /// <remarks>When the retry count reaches the configured total retry count, no further retries will be attempted.</remarks>
        public static ResourceUpdateFailureEventArgs Create(string name, string downloadUri, int retryCount, int totalRetryCount, string errorMessage)
        {
            ResourceUpdateFailureEventArgs resourceUpdateFailureEventArgs = ReferencePool.Acquire<ResourceUpdateFailureEventArgs>();
            resourceUpdateFailureEventArgs.Name = name;
            resourceUpdateFailureEventArgs.DownloadUri = downloadUri;
            resourceUpdateFailureEventArgs.RetryCount = retryCount;
            resourceUpdateFailureEventArgs.TotalRetryCount = totalRetryCount;
            resourceUpdateFailureEventArgs.ErrorMessage = errorMessage;
            return resourceUpdateFailureEventArgs;
        }

        /// <summary>
        /// Clears the resource update failure event.
        /// </summary>
        public override void Clear()
        {
            Name = null;
            DownloadUri = null;
            RetryCount = 0;
            TotalRetryCount = 0;
            ErrorMessage = null;
        }
    }
}
