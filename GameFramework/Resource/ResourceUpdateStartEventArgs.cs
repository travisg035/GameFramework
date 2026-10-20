//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource update start event.
    /// </summary>
    public sealed class ResourceUpdateStartEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceUpdateStartEventArgs"/> class.
        /// </summary>
        public ResourceUpdateStartEventArgs()
        {
            Name = null;
            DownloadPath = null;
            DownloadUri = null;
            CurrentLength = 0;
            CompressedLength = 0;
            RetryCount = 0;
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
        /// Gets the destination path where the resource is stored after download.
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
        /// Gets the current downloaded length.
        /// </summary>
        public int CurrentLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the compressed resource length.
        /// </summary>
        public int CompressedLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of download retries already performed.
        /// </summary>
        public int RetryCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new resource update start event.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="downloadPath">The destination path where the resource is stored after download.</param>
        /// <param name="downloadUri">The resource download URI.</param>
        /// <param name="currentLength">The current downloaded length.</param>
        /// <param name="compressedLength">The compressed resource length.</param>
        /// <param name="retryCount">The number of download retries already performed.</param>
        /// <returns>The created resource update start event.</returns>
        public static ResourceUpdateStartEventArgs Create(string name, string downloadPath, string downloadUri, int currentLength, int compressedLength, int retryCount)
        {
            ResourceUpdateStartEventArgs resourceUpdateStartEventArgs = ReferencePool.Acquire<ResourceUpdateStartEventArgs>();
            resourceUpdateStartEventArgs.Name = name;
            resourceUpdateStartEventArgs.DownloadPath = downloadPath;
            resourceUpdateStartEventArgs.DownloadUri = downloadUri;
            resourceUpdateStartEventArgs.CurrentLength = currentLength;
            resourceUpdateStartEventArgs.CompressedLength = compressedLength;
            resourceUpdateStartEventArgs.RetryCount = retryCount;
            return resourceUpdateStartEventArgs;
        }

        /// <summary>
        /// Clears the resource update start event.
        /// </summary>
        public override void Clear()
        {
            Name = null;
            DownloadPath = null;
            DownloadUri = null;
            CurrentLength = 0;
            CompressedLength = 0;
            RetryCount = 0;
        }
    }
}
