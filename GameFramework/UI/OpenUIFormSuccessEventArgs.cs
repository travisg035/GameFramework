//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.UI
{
    /// <summary>
    /// Open UI form success event.
    /// </summary>
    public sealed class OpenUIFormSuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the OpenUIFormSuccessEventArgs class.
        /// </summary>
        public OpenUIFormSuccessEventArgs()
        {
            UIForm = null;
            Duration = 0f;
            UserData = null;
        }

        /// <summary>
        /// Gets the opened UI form.
        /// </summary>
        public IUIForm UIForm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the loading duration.
        /// </summary>
        public float Duration
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user-defined data.
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates an open UI form success event.
        /// </summary>
        /// <param name="uiForm">The opened UI form.</param>
        /// <param name="duration">The loading duration.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The created open UI form success event.</returns>
        public static OpenUIFormSuccessEventArgs Create(IUIForm uiForm, float duration, object userData)
        {
            OpenUIFormSuccessEventArgs openUIFormSuccessEventArgs = ReferencePool.Acquire<OpenUIFormSuccessEventArgs>();
            openUIFormSuccessEventArgs.UIForm = uiForm;
            openUIFormSuccessEventArgs.Duration = duration;
            openUIFormSuccessEventArgs.UserData = userData;
            return openUIFormSuccessEventArgs;
        }

        /// <summary>
        /// Clears the open UI form success event.
        /// </summary>
        public override void Clear()
        {
            UIForm = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
