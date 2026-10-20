//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource update all complete event.
    /// </summary>
    public sealed class ResourceUpdateAllCompleteEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceUpdateAllCompleteEventArgs"/> class.
        /// </summary>
        public ResourceUpdateAllCompleteEventArgs()
        {
        }

        /// <summary>
        /// Creates a new resource update all complete event.
        /// </summary>
        /// <returns>The created resource update all complete event.</returns>
        public static ResourceUpdateAllCompleteEventArgs Create()
        {
            return ReferencePool.Acquire<ResourceUpdateAllCompleteEventArgs>();
        }

        /// <summary>
        /// Clears the resource update all complete event.
        /// </summary>
        public override void Clear()
        {
        }
    }
}
