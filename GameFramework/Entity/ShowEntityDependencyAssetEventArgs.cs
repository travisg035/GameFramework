//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Entity
{
    /// <summary>
    /// Event fired when loading a dependency asset while showing an entity.
    /// </summary>
    public sealed class ShowEntityDependencyAssetEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ShowEntityDependencyAssetEventArgs class.
        /// </summary>
        public ShowEntityDependencyAssetEventArgs()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroupName = null;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }

        /// <summary>
        /// Gets the entity id.
        /// </summary>
        public int EntityId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the entity asset name.
        /// </summary>
        public string EntityAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the entity group name.
        /// </summary>
        public string EntityGroupName
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
        /// Creates a show entity dependency asset event.
        /// </summary>
        /// <param name="entityId">Entity id.</param>
        /// <param name="entityAssetName">Entity asset name.</param>
        /// <param name="entityGroupName">Entity group name.</param>
        /// <param name="dependencyAssetName">Name of the dependency asset being loaded.</param>
        /// <param name="loadedCount">Number of dependency assets currently loaded.</param>
        /// <param name="totalCount">Total number of dependency assets to load.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created show entity dependency asset event.</returns>
        public static ShowEntityDependencyAssetEventArgs Create(int entityId, string entityAssetName, string entityGroupName, string dependencyAssetName, int loadedCount, int totalCount, object userData)
        {
            ShowEntityDependencyAssetEventArgs showEntityDependencyAssetEventArgs = ReferencePool.Acquire<ShowEntityDependencyAssetEventArgs>();
            showEntityDependencyAssetEventArgs.EntityId = entityId;
            showEntityDependencyAssetEventArgs.EntityAssetName = entityAssetName;
            showEntityDependencyAssetEventArgs.EntityGroupName = entityGroupName;
            showEntityDependencyAssetEventArgs.DependencyAssetName = dependencyAssetName;
            showEntityDependencyAssetEventArgs.LoadedCount = loadedCount;
            showEntityDependencyAssetEventArgs.TotalCount = totalCount;
            showEntityDependencyAssetEventArgs.UserData = userData;
            return showEntityDependencyAssetEventArgs;
        }

        /// <summary>
        /// Clears the show entity dependency asset event.
        /// </summary>
        public override void Clear()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroupName = null;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }
    }
}
