//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Update version list callbacks.
    /// </summary>
    public sealed class UpdateVersionListCallbacks
    {
        private readonly UpdateVersionListSuccessCallback m_UpdateVersionListSuccessCallback;
        private readonly UpdateVersionListFailureCallback m_UpdateVersionListFailureCallback;

        /// <summary>
        /// Initializes a new instance of the UpdateVersionListCallbacks class.
        /// </summary>
        /// <param name="updateVersionListSuccessCallback">Update version list success callback.</param>
        public UpdateVersionListCallbacks(UpdateVersionListSuccessCallback updateVersionListSuccessCallback)
            : this(updateVersionListSuccessCallback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UpdateVersionListCallbacks class.
        /// </summary>
        /// <param name="updateVersionListSuccessCallback">Update version list success callback.</param>
        /// <param name="updateVersionListFailureCallback">Update version list failure callback.</param>
        public UpdateVersionListCallbacks(UpdateVersionListSuccessCallback updateVersionListSuccessCallback, UpdateVersionListFailureCallback updateVersionListFailureCallback)
        {
            if (updateVersionListSuccessCallback == null)
            {
                throw new GameFrameworkException("Update version list success callback is invalid.");
            }

            m_UpdateVersionListSuccessCallback = updateVersionListSuccessCallback;
            m_UpdateVersionListFailureCallback = updateVersionListFailureCallback;
        }

        /// <summary>
        /// Gets the update version list success callback.
        /// </summary>
        public UpdateVersionListSuccessCallback UpdateVersionListSuccessCallback
        {
            get
            {
                return m_UpdateVersionListSuccessCallback;
            }
        }

        /// <summary>
        /// Gets the update version list failure callback.
        /// </summary>
        public UpdateVersionListFailureCallback UpdateVersionListFailureCallback
        {
            get
            {
                return m_UpdateVersionListFailureCallback;
            }
        }
    }
}
