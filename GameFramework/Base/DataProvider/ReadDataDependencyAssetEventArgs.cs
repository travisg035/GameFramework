//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Event fired when loading a dependency asset while reading data.
    /// </summary>
    public sealed class ReadDataDependencyAssetEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ReadDataDependencyAssetEventArgs class.
        /// </summary>
        public ReadDataDependencyAssetEventArgs()
        {
            DataAssetName = null;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }

        /// <summary>
        /// Gets the data asset name.
        /// </summary>
        public string DataAssetName
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
        /// Creates a read data dependency asset event.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="dependencyAssetName">Name of the dependency asset being loaded.</param>
        /// <param name="loadedCount">Number of dependency assets currently loaded.</param>
        /// <param name="totalCount">Total number of dependency assets to load.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created read data dependency asset event.</returns>
        public static ReadDataDependencyAssetEventArgs Create(string dataAssetName, string dependencyAssetName, int loadedCount, int totalCount, object userData)
        {
            ReadDataDependencyAssetEventArgs loadDataDependencyAssetEventArgs = ReferencePool.Acquire<ReadDataDependencyAssetEventArgs>();
            loadDataDependencyAssetEventArgs.DataAssetName = dataAssetName;
            loadDataDependencyAssetEventArgs.DependencyAssetName = dependencyAssetName;
            loadDataDependencyAssetEventArgs.LoadedCount = loadedCount;
            loadDataDependencyAssetEventArgs.TotalCount = totalCount;
            loadDataDependencyAssetEventArgs.UserData = userData;
            return loadDataDependencyAssetEventArgs;
        }

        /// <summary>
        /// Clears the read data dependency asset event.
        /// </summary>
        public override void Clear()
        {
            DataAssetName = null;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }
    }
}
