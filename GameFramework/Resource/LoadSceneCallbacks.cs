//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load scene callback set.
    /// </summary>
    public sealed class LoadSceneCallbacks
    {
        private readonly LoadSceneSuccessCallback m_LoadSceneSuccessCallback;
        private readonly LoadSceneFailureCallback m_LoadSceneFailureCallback;
        private readonly LoadSceneUpdateCallback m_LoadSceneUpdateCallback;
        private readonly LoadSceneDependencyAssetCallback m_LoadSceneDependencyAssetCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSceneCallbacks"/> class.
        /// </summary>
        /// <param name="loadSceneSuccessCallback">The load scene success callback method.</param>
        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback)
            : this(loadSceneSuccessCallback, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSceneCallbacks"/> class.
        /// </summary>
        /// <param name="loadSceneSuccessCallback">The load scene success callback method.</param>
        /// <param name="loadSceneFailureCallback">The load scene failure callback method.</param>
        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback, LoadSceneFailureCallback loadSceneFailureCallback)
            : this(loadSceneSuccessCallback, loadSceneFailureCallback, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSceneCallbacks"/> class.
        /// </summary>
        /// <param name="loadSceneSuccessCallback">The load scene success callback method.</param>
        /// <param name="loadSceneUpdateCallback">The load scene update callback method.</param>
        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback, LoadSceneUpdateCallback loadSceneUpdateCallback)
            : this(loadSceneSuccessCallback, null, loadSceneUpdateCallback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSceneCallbacks"/> class.
        /// </summary>
        /// <param name="loadSceneSuccessCallback">The load scene success callback method.</param>
        /// <param name="loadSceneDependencyAssetCallback">The load scene dependency asset callback method.</param>
        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback, LoadSceneDependencyAssetCallback loadSceneDependencyAssetCallback)
            : this(loadSceneSuccessCallback, null, null, loadSceneDependencyAssetCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSceneCallbacks"/> class.
        /// </summary>
        /// <param name="loadSceneSuccessCallback">The load scene success callback method.</param>
        /// <param name="loadSceneFailureCallback">The load scene failure callback method.</param>
        /// <param name="loadSceneUpdateCallback">The load scene update callback method.</param>
        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback, LoadSceneFailureCallback loadSceneFailureCallback, LoadSceneUpdateCallback loadSceneUpdateCallback)
            : this(loadSceneSuccessCallback, loadSceneFailureCallback, loadSceneUpdateCallback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSceneCallbacks"/> class.
        /// </summary>
        /// <param name="loadSceneSuccessCallback">The load scene success callback method.</param>
        /// <param name="loadSceneFailureCallback">The load scene failure callback method.</param>
        /// <param name="loadSceneDependencyAssetCallback">The load scene dependency asset callback method.</param>
        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback, LoadSceneFailureCallback loadSceneFailureCallback, LoadSceneDependencyAssetCallback loadSceneDependencyAssetCallback)
            : this(loadSceneSuccessCallback, loadSceneFailureCallback, null, loadSceneDependencyAssetCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSceneCallbacks"/> class.
        /// </summary>
        /// <param name="loadSceneSuccessCallback">The load scene success callback method.</param>
        /// <param name="loadSceneFailureCallback">The load scene failure callback method.</param>
        /// <param name="loadSceneUpdateCallback">The load scene update callback method.</param>
        /// <param name="loadSceneDependencyAssetCallback">The load scene dependency asset callback method.</param>
        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback, LoadSceneFailureCallback loadSceneFailureCallback, LoadSceneUpdateCallback loadSceneUpdateCallback, LoadSceneDependencyAssetCallback loadSceneDependencyAssetCallback)
        {
            if (loadSceneSuccessCallback == null)
            {
                throw new GameFrameworkException("Load scene success callback is invalid.");
            }

            m_LoadSceneSuccessCallback = loadSceneSuccessCallback;
            m_LoadSceneFailureCallback = loadSceneFailureCallback;
            m_LoadSceneUpdateCallback = loadSceneUpdateCallback;
            m_LoadSceneDependencyAssetCallback = loadSceneDependencyAssetCallback;
        }

        /// <summary>
        /// Gets the load scene success callback method.
        /// </summary>
        public LoadSceneSuccessCallback LoadSceneSuccessCallback
        {
            get
            {
                return m_LoadSceneSuccessCallback;
            }
        }

        /// <summary>
        /// Gets the load scene failure callback method.
        /// </summary>
        public LoadSceneFailureCallback LoadSceneFailureCallback
        {
            get
            {
                return m_LoadSceneFailureCallback;
            }
        }

        /// <summary>
        /// Gets the load scene update callback method.
        /// </summary>
        public LoadSceneUpdateCallback LoadSceneUpdateCallback
        {
            get
            {
                return m_LoadSceneUpdateCallback;
            }
        }

        /// <summary>
        /// Gets the load scene dependency asset callback method.
        /// </summary>
        public LoadSceneDependencyAssetCallback LoadSceneDependencyAssetCallback
        {
            get
            {
                return m_LoadSceneDependencyAssetCallback;
            }
        }
    }
}
