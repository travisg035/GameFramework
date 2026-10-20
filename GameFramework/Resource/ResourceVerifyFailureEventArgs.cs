//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource verify failure event.
    /// </summary>
    public sealed class ResourceVerifyFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceVerifyFailureEventArgs"/> class.
        /// </summary>
        public ResourceVerifyFailureEventArgs()
        {
            Name = null;
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
        /// Creates a new resource verify failure event.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <returns>The created resource verify failure event.</returns>
        public static ResourceVerifyFailureEventArgs Create(string name)
        {
            ResourceVerifyFailureEventArgs resourceVerifyFailureEventArgs = ReferencePool.Acquire<ResourceVerifyFailureEventArgs>();
            resourceVerifyFailureEventArgs.Name = name;
            return resourceVerifyFailureEventArgs;
        }

        /// <summary>
        /// Clears the resource verify failure event.
        /// </summary>
        public override void Clear()
        {
            Name = null;
        }
    }
}
