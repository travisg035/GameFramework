//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource update success event.
    /// </summary>
    public sealed class ResourceUpdateSuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceUpdateSuccessEventArgs"/> class.
        /// </summary>
        public ResourceUpdateSuccessEventArgs()
        {
            Name = null;
            DownloadPath = null;
            DownloadUri = null;
            Length = 0;
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
        /// Gets the resource length.
        /// </summary>
        public int Length
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
        /// Creates a new resource update success event.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="downloadPath">The destination path where the resource is stored after download.</param>
        /// <param name="downloadUri">The resource download URI.</param>
        /// <param name="length">The resource length.</param>
        /// <param name="compressedLength">The compressed resource length.</param>
        /// <returns>The created resource update success event.</returns>
        public static ResourceUpdateSuccessEventArgs Create(string name, string downloadPath, string downloadUri, int length, int compressedLength)
        {
            ResourceUpdateSuccessEventArgs resourceUpdateSuccessEventArgs = ReferencePool.Acquire<ResourceUpdateSuccessEventArgs>();
            resourceUpdateSuccessEventArgs.Name = name;
            resourceUpdateSuccessEventArgs.DownloadPath = downloadPath;
            resourceUpdateSuccessEventArgs.DownloadUri = downloadUri;
            resourceUpdateSuccessEventArgs.Length = length;
            resourceUpdateSuccessEventArgs.CompressedLength = compressedLength;
            return resourceUpdateSuccessEventArgs;
        }

        /// <summary>
        /// Clears the resource update success event.
        /// </summary>
        public override void Clear()
        {
            Name = null;
            DownloadPath = null;
            DownloadUri = null;
            Length = 0;
            CompressedLength = 0;
        }
    }
}
