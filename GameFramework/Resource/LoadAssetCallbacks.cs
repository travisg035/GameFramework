//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load asset callback set.
    /// </summary>
    public sealed class LoadAssetCallbacks
    {
        private readonly LoadAssetSuccessCallback m_LoadAssetSuccessCallback;
        private readonly LoadAssetFailureCallback m_LoadAssetFailureCallback;
        private readonly LoadAssetUpdateCallback m_LoadAssetUpdateCallback;
        private readonly LoadAssetDependencyAssetCallback m_LoadAssetDependencyAssetCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAssetCallbacks"/> class.
        /// </summary>
        /// <param name="loadAssetSuccessCallback">The load asset success callback method.</param>
        public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback)
            : this(loadAssetSuccessCallback, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAssetCallbacks"/> class.
        /// </summary>
        /// <param name="loadAssetSuccessCallback">The load asset success callback method.</param>
        /// <param name="loadAssetFailureCallback">The load asset failure callback method.</param>
        public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback, LoadAssetFailureCallback loadAssetFailureCallback)
            : this(loadAssetSuccessCallback, loadAssetFailureCallback, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAssetCallbacks"/> class.
        /// </summary>
        /// <param name="loadAssetSuccessCallback">The load asset success callback method.</param>
        /// <param name="loadAssetUpdateCallback">The load asset update callback method.</param>
        public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback, LoadAssetUpdateCallback loadAssetUpdateCallback)
            : this(loadAssetSuccessCallback, null, loadAssetUpdateCallback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAssetCallbacks"/> class.
        /// </summary>
        /// <param name="loadAssetSuccessCallback">The load asset success callback method.</param>
        /// <param name="loadAssetDependencyAssetCallback">The load asset dependency asset callback method.</param>
        public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback, LoadAssetDependencyAssetCallback loadAssetDependencyAssetCallback)
            : this(loadAssetSuccessCallback, null, null, loadAssetDependencyAssetCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAssetCallbacks"/> class.
        /// </summary>
        /// <param name="loadAssetSuccessCallback">The load asset success callback method.</param>
        /// <param name="loadAssetFailureCallback">The load asset failure callback method.</param>
        /// <param name="loadAssetUpdateCallback">The load asset update callback method.</param>
        public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback, LoadAssetFailureCallback loadAssetFailureCallback, LoadAssetUpdateCallback loadAssetUpdateCallback)
            : this(loadAssetSuccessCallback, loadAssetFailureCallback, loadAssetUpdateCallback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAssetCallbacks"/> class.
        /// </summary>
        /// <param name="loadAssetSuccessCallback">The load asset success callback method.</param>
        /// <param name="loadAssetFailureCallback">The load asset failure callback method.</param>
        /// <param name="loadAssetDependencyAssetCallback">The load asset dependency asset callback method.</param>
        public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback, LoadAssetFailureCallback loadAssetFailureCallback, LoadAssetDependencyAssetCallback loadAssetDependencyAssetCallback)
            : this(loadAssetSuccessCallback, loadAssetFailureCallback, null, loadAssetDependencyAssetCallback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAssetCallbacks"/> class.
        /// </summary>
        /// <param name="loadAssetSuccessCallback">The load asset success callback method.</param>
        /// <param name="loadAssetFailureCallback">The load asset failure callback method.</param>
        /// <param name="loadAssetUpdateCallback">The load asset update callback method.</param>
        /// <param name="loadAssetDependencyAssetCallback">The load asset dependency asset callback method.</param>
        public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback, LoadAssetFailureCallback loadAssetFailureCallback, LoadAssetUpdateCallback loadAssetUpdateCallback, LoadAssetDependencyAssetCallback loadAssetDependencyAssetCallback)
        {
            if (loadAssetSuccessCallback == null)
            {
                throw new GameFrameworkException("Load asset success callback is invalid.");
            }

            m_LoadAssetSuccessCallback = loadAssetSuccessCallback;
            m_LoadAssetFailureCallback = loadAssetFailureCallback;
            m_LoadAssetUpdateCallback = loadAssetUpdateCallback;
            m_LoadAssetDependencyAssetCallback = loadAssetDependencyAssetCallback;
        }

        /// <summary>
        /// Gets the load asset success callback method.
        /// </summary>
        public LoadAssetSuccessCallback LoadAssetSuccessCallback
        {
            get
            {
                return m_LoadAssetSuccessCallback;
            }
        }

        /// <summary>
        /// Gets the load asset failure callback method.
        /// </summary>
        public LoadAssetFailureCallback LoadAssetFailureCallback
        {
            get
            {
                return m_LoadAssetFailureCallback;
            }
        }

        /// <summary>
        /// Gets the load asset update callback method.
        /// </summary>
        public LoadAssetUpdateCallback LoadAssetUpdateCallback
        {
            get
            {
                return m_LoadAssetUpdateCallback;
            }
        }

        /// <summary>
        /// Gets the load asset dependency asset callback method.
        /// </summary>
        public LoadAssetDependencyAssetCallback LoadAssetDependencyAssetCallback
        {
            get
            {
                return m_LoadAssetDependencyAssetCallback;
            }
        }
    }
}
