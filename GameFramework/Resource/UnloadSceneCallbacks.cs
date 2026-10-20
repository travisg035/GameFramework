//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Unload scene callback set.
    /// </summary>
    public sealed class UnloadSceneCallbacks
    {
        private readonly UnloadSceneSuccessCallback m_UnloadSceneSuccessCallback;
        private readonly UnloadSceneFailureCallback m_UnloadSceneFailureCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnloadSceneCallbacks"/> class.
        /// </summary>
        /// <param name="unloadSceneSuccessCallback">The unload scene success callback method.</param>
        public UnloadSceneCallbacks(UnloadSceneSuccessCallback unloadSceneSuccessCallback)
            : this(unloadSceneSuccessCallback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnloadSceneCallbacks"/> class.
        /// </summary>
        /// <param name="unloadSceneSuccessCallback">The unload scene success callback method.</param>
        /// <param name="unloadSceneFailureCallback">The unload scene failure callback method.</param>
        public UnloadSceneCallbacks(UnloadSceneSuccessCallback unloadSceneSuccessCallback, UnloadSceneFailureCallback unloadSceneFailureCallback)
        {
            if (unloadSceneSuccessCallback == null)
            {
                throw new GameFrameworkException("Unload scene success callback is invalid.");
            }

            m_UnloadSceneSuccessCallback = unloadSceneSuccessCallback;
            m_UnloadSceneFailureCallback = unloadSceneFailureCallback;
        }

        /// <summary>
        /// Gets the unload scene success callback method.
        /// </summary>
        public UnloadSceneSuccessCallback UnloadSceneSuccessCallback
        {
            get
            {
                return m_UnloadSceneSuccessCallback;
            }
        }

        /// <summary>
        /// Gets the unload scene failure callback method.
        /// </summary>
        public UnloadSceneFailureCallback UnloadSceneFailureCallback
        {
            get
            {
                return m_UnloadSceneFailureCallback;
            }
        }
    }
}
