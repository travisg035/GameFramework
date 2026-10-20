//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.UI
{
    /// <summary>
    /// Close UI form complete event.
    /// </summary>
    public sealed class CloseUIFormCompleteEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CloseUIFormCompleteEventArgs class.
        /// </summary>
        public CloseUIFormCompleteEventArgs()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroup = null;
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
        /// Gets the UI group that the UI form belongs to.
        /// </summary>
        public IUIGroup UIGroup
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
        /// Creates a close UI form complete event.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroup">The UI group that the UI form belongs to.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The created close UI form complete event.</returns>
        public static CloseUIFormCompleteEventArgs Create(int serialId, string uiFormAssetName, IUIGroup uiGroup, object userData)
        {
            CloseUIFormCompleteEventArgs closeUIFormCompleteEventArgs = ReferencePool.Acquire<CloseUIFormCompleteEventArgs>();
            closeUIFormCompleteEventArgs.SerialId = serialId;
            closeUIFormCompleteEventArgs.UIFormAssetName = uiFormAssetName;
            closeUIFormCompleteEventArgs.UIGroup = uiGroup;
            closeUIFormCompleteEventArgs.UserData = userData;
            return closeUIFormCompleteEventArgs;
        }

        /// <summary>
        /// Clears the close UI form complete event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroup = null;
            UserData = null;
        }
    }
}
