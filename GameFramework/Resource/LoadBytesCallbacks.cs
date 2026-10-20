//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load bytes callback set.
    /// </summary>
    public sealed class LoadBytesCallbacks
    {
        private readonly LoadBytesSuccessCallback m_LoadBytesSuccessCallback;
        private readonly LoadBytesFailureCallback m_LoadBytesFailureCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadBytesCallbacks"/> class.
        /// </summary>
        /// <param name="loadBytesSuccessCallback">The load bytes success callback method.</param>
        public LoadBytesCallbacks(LoadBytesSuccessCallback loadBytesSuccessCallback)
            : this(loadBytesSuccessCallback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadBytesCallbacks"/> class.
        /// </summary>
        /// <param name="loadBytesSuccessCallback">The load bytes success callback method.</param>
        /// <param name="loadBytesFailureCallback">The load bytes failure callback method.</param>
        public LoadBytesCallbacks(LoadBytesSuccessCallback loadBytesSuccessCallback, LoadBytesFailureCallback loadBytesFailureCallback)
        {
            if (loadBytesSuccessCallback == null)
            {
                throw new GameFrameworkException("Load bytes success callback is invalid.");
            }

            m_LoadBytesSuccessCallback = loadBytesSuccessCallback;
            m_LoadBytesFailureCallback = loadBytesFailureCallback;
        }

        /// <summary>
        /// Gets the load bytes success callback method.
        /// </summary>
        public LoadBytesSuccessCallback LoadBytesSuccessCallback
        {
            get
            {
                return m_LoadBytesSuccessCallback;
            }
        }

        /// <summary>
        /// Gets the load bytes failure callback method.
        /// </summary>
        public LoadBytesFailureCallback LoadBytesFailureCallback
        {
            get
            {
                return m_LoadBytesFailureCallback;
            }
        }
    }
}
