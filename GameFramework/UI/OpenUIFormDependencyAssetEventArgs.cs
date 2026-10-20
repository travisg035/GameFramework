//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.UI
{
    /// <summary>
    /// Open UI form dependency asset event.
    /// </summary>
    public sealed class OpenUIFormDependencyAssetEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the OpenUIFormDependencyAssetEventArgs class.
        /// </summary>
        public OpenUIFormDependencyAssetEventArgs()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
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
        /// Gets the name of the dependency asset being loaded.
        /// </summary>
        public string DependencyAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of dependency assets currently loaded.
        /// </summary>
        public int LoadedCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total number of dependency assets to load.
        /// </summary>
        public int TotalCount
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
        /// Creates an open UI form dependency asset event.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="pauseCoveredUIForm">Whether to pause the covered UI form.</param>
        /// <param name="dependencyAssetName">The name of the dependency asset being loaded.</param>
        /// <param name="loadedCount">The number of dependency assets currently loaded.</param>
        /// <param name="totalCount">The total number of dependency assets to load.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The created open UI form dependency asset event.</returns>
        public static OpenUIFormDependencyAssetEventArgs Create(int serialId, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, string dependencyAssetName, int loadedCount, int totalCount, object userData)
        {
            OpenUIFormDependencyAssetEventArgs openUIFormDependencyAssetEventArgs = ReferencePool.Acquire<OpenUIFormDependencyAssetEventArgs>();
            openUIFormDependencyAssetEventArgs.SerialId = serialId;
            openUIFormDependencyAssetEventArgs.UIFormAssetName = uiFormAssetName;
            openUIFormDependencyAssetEventArgs.UIGroupName = uiGroupName;
            openUIFormDependencyAssetEventArgs.PauseCoveredUIForm = pauseCoveredUIForm;
            openUIFormDependencyAssetEventArgs.DependencyAssetName = dependencyAssetName;
            openUIFormDependencyAssetEventArgs.LoadedCount = loadedCount;
            openUIFormDependencyAssetEventArgs.TotalCount = totalCount;
            openUIFormDependencyAssetEventArgs.UserData = userData;
            return openUIFormDependencyAssetEventArgs;
        }

        /// <summary>
        /// Clears the open UI form dependency asset event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }
    }
}
