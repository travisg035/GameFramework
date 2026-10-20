//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Scene
{
    /// <summary>
    /// Event fired when loading a dependency asset while loading a scene.
    /// </summary>
    public sealed class LoadSceneDependencyAssetEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the LoadSceneDependencyAssetEventArgs class.
        /// </summary>
        public LoadSceneDependencyAssetEventArgs()
        {
            SceneAssetName = null;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }

        /// <summary>
        /// Gets the scene asset name.
        /// </summary>
        public string SceneAssetName
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
        /// Creates a load scene dependency asset event.
        /// </summary>
        /// <param name="sceneAssetName">Scene asset name.</param>
        /// <param name="dependencyAssetName">Name of the dependency asset being loaded.</param>
        /// <param name="loadedCount">Number of dependency assets currently loaded.</param>
        /// <param name="totalCount">Total number of dependency assets to load.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created load scene dependency asset event.</returns>
        public static LoadSceneDependencyAssetEventArgs Create(string sceneAssetName, string dependencyAssetName, int loadedCount, int totalCount, object userData)
        {
            LoadSceneDependencyAssetEventArgs loadSceneDependencyAssetEventArgs = ReferencePool.Acquire<LoadSceneDependencyAssetEventArgs>();
            loadSceneDependencyAssetEventArgs.SceneAssetName = sceneAssetName;
            loadSceneDependencyAssetEventArgs.DependencyAssetName = dependencyAssetName;
            loadSceneDependencyAssetEventArgs.LoadedCount = loadedCount;
            loadSceneDependencyAssetEventArgs.TotalCount = totalCount;
            loadSceneDependencyAssetEventArgs.UserData = userData;
            return loadSceneDependencyAssetEventArgs;
        }

        /// <summary>
        /// Clears the load scene dependency asset event.
        /// </summary>
        public override void Clear()
        {
            SceneAssetName = null;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }
    }
}
