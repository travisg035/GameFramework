//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource apply start event.
    /// </summary>
    public sealed class ResourceApplyStartEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceApplyStartEventArgs"/> class.
        /// </summary>
        public ResourceApplyStartEventArgs()
        {
            ResourcePackPath = null;
            Count = 0;
            TotalLength = 0L;
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
        /// Gets the count of resources to apply.
        /// </summary>
        public int Count
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total length of resources to apply.
        /// </summary>
        public long TotalLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new resource apply start event.
        /// </summary>
        /// <param name="resourcePackPath">The resource pack path.</param>
        /// <param name="count">The count of resources to apply.</param>
        /// <param name="totalLength">The total length of resources to apply.</param>
        /// <returns>The created resource apply start event.</returns>
        public static ResourceApplyStartEventArgs Create(string resourcePackPath, int count, long totalLength)
        {
            ResourceApplyStartEventArgs resourceApplyStartEventArgs = ReferencePool.Acquire<ResourceApplyStartEventArgs>();
            resourceApplyStartEventArgs.ResourcePackPath = resourcePackPath;
            resourceApplyStartEventArgs.Count = count;
            resourceApplyStartEventArgs.TotalLength = totalLength;
            return resourceApplyStartEventArgs;
        }

        /// <summary>
        /// Clears the resource apply start event.
        /// </summary>
        public override void Clear()
        {
            ResourcePackPath = null;
            Count = 0;
            TotalLength = 0L;
        }
    }
}
