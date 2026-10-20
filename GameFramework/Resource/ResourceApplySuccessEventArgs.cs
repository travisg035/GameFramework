//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource apply success event.
    /// </summary>
    public sealed class ResourceApplySuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceApplySuccessEventArgs"/> class.
        /// </summary>
        public ResourceApplySuccessEventArgs()
        {
            Name = null;
            ApplyPath = null;
            ResourcePackPath = null;
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
        /// Gets the destination path where the resource is stored after applying.
        /// </summary>
        public string ApplyPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the resource pack path.
        /// </summary>
        public string ResourcePackPath
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
        /// Creates a new resource apply success event.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="applyPath">The destination path where the resource is stored after applying.</param>
        /// <param name="resourcePackPath">The resource pack path.</param>
        /// <param name="length">The resource length.</param>
        /// <param name="compressedLength">The compressed resource length.</param>
        /// <returns>The created resource apply success event.</returns>
        public static ResourceApplySuccessEventArgs Create(string name, string applyPath, string resourcePackPath, int length, int compressedLength)
        {
            ResourceApplySuccessEventArgs resourceApplySuccessEventArgs = ReferencePool.Acquire<ResourceApplySuccessEventArgs>();
            resourceApplySuccessEventArgs.Name = name;
            resourceApplySuccessEventArgs.ApplyPath = applyPath;
            resourceApplySuccessEventArgs.ResourcePackPath = resourcePackPath;
            resourceApplySuccessEventArgs.Length = length;
            resourceApplySuccessEventArgs.CompressedLength = compressedLength;
            return resourceApplySuccessEventArgs;
        }

        /// <summary>
        /// Clears the resource apply success event.
        /// </summary>
        public override void Clear()
        {
            Name = null;
            ApplyPath = null;
            ResourcePackPath = null;
            Length = 0;
            CompressedLength = 0;
        }
    }
}
