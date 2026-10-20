//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load resource agent helper asynchronous parse binary bytes to resource complete event.
    /// </summary>
    public sealed class LoadResourceAgentHelperParseBytesCompleteEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadResourceAgentHelperParseBytesCompleteEventArgs"/> class.
        /// </summary>
        public LoadResourceAgentHelperParseBytesCompleteEventArgs()
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
        /// Creates a new load resource agent helper asynchronous parse binary bytes to resource complete event.
        /// </summary>
        /// <param name="resource">The loaded resource object.</param>
        /// <returns>The created load resource agent helper asynchronous parse binary bytes to resource complete event.</returns>
        public static LoadResourceAgentHelperParseBytesCompleteEventArgs Create(object resource)
        {
            LoadResourceAgentHelperParseBytesCompleteEventArgs loadResourceAgentHelperParseBytesCompleteEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperParseBytesCompleteEventArgs>();
            loadResourceAgentHelperParseBytesCompleteEventArgs.Resource = resource;
            return loadResourceAgentHelperParseBytesCompleteEventArgs;
        }

        /// <summary>
        /// Clears the load resource agent helper asynchronous parse binary bytes to resource complete event.
        /// </summary>
        public override void Clear()
        {
            Resource = null;
        }
    }
}
