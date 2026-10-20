//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load resource agent helper asynchronous read file to resource complete event.
    /// </summary>
    public sealed class LoadResourceAgentHelperReadFileCompleteEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadResourceAgentHelperReadFileCompleteEventArgs"/> class.
        /// </summary>
        public LoadResourceAgentHelperReadFileCompleteEventArgs()
        {
            Resource = null;
        }

        /// <summary>
        /// Gets the loaded resource object.
        /// </summary>
        public object Resource
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new load resource agent helper asynchronous read file to resource complete event.
        /// </summary>
        /// <param name="resource">The loaded resource object.</param>
        /// <returns>The created load resource agent helper asynchronous read file to resource complete event.</returns>
        public static LoadResourceAgentHelperReadFileCompleteEventArgs Create(object resource)
        {
            LoadResourceAgentHelperReadFileCompleteEventArgs loadResourceAgentHelperReadFileCompleteEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperReadFileCompleteEventArgs>();
            loadResourceAgentHelperReadFileCompleteEventArgs.Resource = resource;
            return loadResourceAgentHelperReadFileCompleteEventArgs;
        }

        /// <summary>
        /// Clears the load resource agent helper asynchronous read file to resource complete event.
        /// </summary>
        public override void Clear()
        {
            Resource = null;
        }
    }
}
