//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource verify start event.
    /// </summary>
    public sealed class ResourceVerifyStartEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceVerifyStartEventArgs"/> class.
        /// </summary>
        public ResourceVerifyStartEventArgs()
        {
            Count = 0;
            TotalLength = 0L;
        }

        /// <summary>
        /// Gets the count of resources to verify.
        /// </summary>
        public int Count
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total length of resources to verify.
        /// </summary>
        public long TotalLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new resource verify start event.
        /// </summary>
        /// <param name="count">The count of resources to verify.</param>
        /// <param name="totalLength">The total length of resources to verify.</param>
        /// <returns>The created resource verify start event.</returns>
        public static ResourceVerifyStartEventArgs Create(int count, long totalLength)
        {
            ResourceVerifyStartEventArgs resourceVerifyStartEventArgs = ReferencePool.Acquire<ResourceVerifyStartEventArgs>();
            resourceVerifyStartEventArgs.Count = count;
            resourceVerifyStartEventArgs.TotalLength = totalLength;
            return resourceVerifyStartEventArgs;
        }

        /// <summary>
        /// Clears the resource verify start event.
        /// </summary>
        public override void Clear()
        {
            Count = 0;
            TotalLength = 0L;
        }
    }
}
