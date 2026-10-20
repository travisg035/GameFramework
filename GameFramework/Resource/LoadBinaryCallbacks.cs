//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load binary callback set.
    /// </summary>
    public sealed class LoadBinaryCallbacks
    {
        private readonly LoadBinarySuccessCallback m_LoadBinarySuccessCallback;
        private readonly LoadBinaryFailureCallback m_LoadBinaryFailureCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadBinaryCallbacks"/> class.
        /// </summary>
        /// <param name="loadBinarySuccessCallback">The load binary success callback method.</param>
        public LoadBinaryCallbacks(LoadBinarySuccessCallback loadBinarySuccessCallback)
            : this(loadBinarySuccessCallback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadBinaryCallbacks"/> class.
        /// </summary>
        /// <param name="loadBinarySuccessCallback">The load binary success callback method.</param>
        /// <param name="loadBinaryFailureCallback">The load binary failure callback method.</param>
        public LoadBinaryCallbacks(LoadBinarySuccessCallback loadBinarySuccessCallback, LoadBinaryFailureCallback loadBinaryFailureCallback)
        {
            if (loadBinarySuccessCallback == null)
            {
                throw new GameFrameworkException("Load binary success callback is invalid.");
            }

            m_LoadBinarySuccessCallback = loadBinarySuccessCallback;
            m_LoadBinaryFailureCallback = loadBinaryFailureCallback;
        }

        /// <summary>
        /// Gets the load binary success callback method.
        /// </summary>
        public LoadBinarySuccessCallback LoadBinarySuccessCallback
        {
            get
            {
                return m_LoadBinarySuccessCallback;
            }
        }

        /// <summary>
        /// Gets the load binary failure callback method.
        /// </summary>
        public LoadBinaryFailureCallback LoadBinaryFailureCallback
        {
            get
            {
                return m_LoadBinaryFailureCallback;
            }
        }
    }
}
