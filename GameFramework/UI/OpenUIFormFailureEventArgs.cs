//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.UI
{
    /// <summary>
    /// Open UI form failure event.
    /// </summary>
    public sealed class OpenUIFormFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the OpenUIFormFailureEventArgs class.
        /// </summary>
        public OpenUIFormFailureEventArgs()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            ErrorMessage = null;
            UserData = null;
        }

        /// <summary>
        /// Gets the serial ID of the UI form.
        /// </summary>
        public int SerialId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the UI form asset name.
        /// </summary>
        public string UIFormAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the UI group name.
        /// </summary>
        public string UIGroupName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets whether to pause the covered UI form.
        /// </summary>
        public bool PauseCoveredUIForm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage
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
        /// Creates an open UI form failure event.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="pauseCoveredUIForm">Whether to pause the covered UI form.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The created open UI form failure event.</returns>
        public static OpenUIFormFailureEventArgs Create(int serialId, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, string errorMessage, object userData)
        {
            OpenUIFormFailureEventArgs openUIFormFailureEventArgs = ReferencePool.Acquire<OpenUIFormFailureEventArgs>();
            openUIFormFailureEventArgs.SerialId = serialId;
            openUIFormFailureEventArgs.UIFormAssetName = uiFormAssetName;
            openUIFormFailureEventArgs.UIGroupName = uiGroupName;
            openUIFormFailureEventArgs.PauseCoveredUIForm = pauseCoveredUIForm;
            openUIFormFailureEventArgs.ErrorMessage = errorMessage;
            openUIFormFailureEventArgs.UserData = userData;
            return openUIFormFailureEventArgs;
        }

        /// <summary>
        /// Clears the open UI form failure event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
