//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load resource agent helper asynchronous load asset complete event.
    /// </summary>
    public sealed class LoadResourceAgentHelperLoadCompleteEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadResourceAgentHelperLoadCompleteEventArgs"/> class.
        /// </summary>
        public LoadResourceAgentHelperLoadCompleteEventArgs()
        {
            Asset = null;
        }

        /// <summary>
        /// Gets the loaded asset.
        /// </summary>
        public object Asset
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new load resource agent helper asynchronous load asset complete event.
        /// </summary>
        /// <param name="asset">The loaded asset.</param>
        /// <returns>The created load resource agent helper asynchronous load asset complete event.</returns>
        public static LoadResourceAgentHelperLoadCompleteEventArgs Create(object asset)
        {
            LoadResourceAgentHelperLoadCompleteEventArgs loadResourceAgentHelperLoadCompleteEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperLoadCompleteEventArgs>();
            loadResourceAgentHelperLoadCompleteEventArgs.Asset = asset;
            return loadResourceAgentHelperLoadCompleteEventArgs;
        }

        /// <summary>
        /// Clears the load resource agent helper asynchronous load asset complete event.
        /// </summary>
        public override void Clear()
        {
            Asset = null;
        }
    }
}
