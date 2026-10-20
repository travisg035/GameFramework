//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource verify success event.
    /// </summary>
    public sealed class ResourceVerifySuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceVerifySuccessEventArgs"/> class.
        /// </summary>
        public ResourceVerifySuccessEventArgs()
        {
            Name = null;
            Length = 0;
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
        /// Gets the resource length.
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new resource verify success event.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="length">The resource length.</param>
        /// <returns>The created resource verify success event.</returns>
        public static ResourceVerifySuccessEventArgs Create(string name, int length)
        {
            ResourceVerifySuccessEventArgs resourceVerifySuccessEventArgs = ReferencePool.Acquire<ResourceVerifySuccessEventArgs>();
            resourceVerifySuccessEventArgs.Name = name;
            resourceVerifySuccessEventArgs.Length = length;
            return resourceVerifySuccessEventArgs;
        }

        /// <summary>
        /// Clears the resource verify success event.
        /// </summary>
        public override void Clear()
        {
            Name = null;
            Length = 0;
        }
    }
}
