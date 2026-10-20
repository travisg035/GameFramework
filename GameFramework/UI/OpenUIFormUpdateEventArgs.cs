//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.UI
{
    /// <summary>
    /// Open UI form update event.
    /// </summary>
    public sealed class OpenUIFormUpdateEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the OpenUIFormUpdateEventArgs class.
        /// </summary>
        public OpenUIFormUpdateEventArgs()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            Progress = 0f;
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
        /// Gets the open UI form progress.
        /// </summary>
        public float Progress
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
        /// Creates an open UI form update event.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="pauseCoveredUIForm">Whether to pause the covered UI form.</param>
        /// <param name="progress">The open UI form progress.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The created open UI form update event.</returns>
        public static OpenUIFormUpdateEventArgs Create(int serialId, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, float progress, object userData)
        {
            OpenUIFormUpdateEventArgs openUIFormUpdateEventArgs = ReferencePool.Acquire<OpenUIFormUpdateEventArgs>();
            openUIFormUpdateEventArgs.SerialId = serialId;
            openUIFormUpdateEventArgs.UIFormAssetName = uiFormAssetName;
            openUIFormUpdateEventArgs.UIGroupName = uiGroupName;
            openUIFormUpdateEventArgs.PauseCoveredUIForm = pauseCoveredUIForm;
            openUIFormUpdateEventArgs.Progress = progress;
            openUIFormUpdateEventArgs.UserData = userData;
            return openUIFormUpdateEventArgs;
        }

        /// <summary>
        /// Clears the open UI form update event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            Progress = 0f;
            UserData = null;
        }
    }
}
