//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource update changed event.
    /// </summary>
    public sealed class ResourceUpdateChangedEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceUpdateChangedEventArgs"/> class.
        /// </summary>
        public ResourceUpdateChangedEventArgs()
        {
            Name = null;
            DownloadPath = null;
            DownloadUri = null;
            CurrentLength = 0;
            CompressedLength = 0;
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
        /// Creates a new resource update changed event.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="downloadPath">The destination path where the resource is stored after download.</param>
        /// <param name="downloadUri">The resource download URI.</param>
        /// <param name="currentLength">The current downloaded length.</param>
        /// <param name="compressedLength">The compressed resource length.</param>
        /// <returns>The created resource update changed event.</returns>
        public static ResourceUpdateChangedEventArgs Create(string name, string downloadPath, string downloadUri, int currentLength, int compressedLength)
        {
            ResourceUpdateChangedEventArgs resourceUpdateChangedEventArgs = ReferencePool.Acquire<ResourceUpdateChangedEventArgs>();
            resourceUpdateChangedEventArgs.Name = name;
            resourceUpdateChangedEventArgs.DownloadPath = downloadPath;
            resourceUpdateChangedEventArgs.DownloadUri = downloadUri;
            resourceUpdateChangedEventArgs.CurrentLength = currentLength;
            resourceUpdateChangedEventArgs.CompressedLength = compressedLength;
            return resourceUpdateChangedEventArgs;
        }

        /// <summary>
        /// Clears the resource update changed event.
        /// </summary>
        public override void Clear()
        {
            Name = null;
            DownloadPath = null;
            DownloadUri = null;
            CurrentLength = 0;
            CompressedLength = 0;
        }
    }
}
