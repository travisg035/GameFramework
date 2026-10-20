//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Read data update event.
    /// </summary>
    public sealed class ReadDataUpdateEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ReadDataUpdateEventArgs class.
        /// </summary>
        public ReadDataUpdateEventArgs()
        {
            DataAssetName = null;
            Progress = 0f;
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
        /// Gets the read data progress.
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
        /// Creates a read data update event.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="progress">Read data progress.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created read data update event.</returns>
        public static ReadDataUpdateEventArgs Create(string dataAssetName, float progress, object userData)
        {
            ReadDataUpdateEventArgs loadDataUpdateEventArgs = ReferencePool.Acquire<ReadDataUpdateEventArgs>();
            loadDataUpdateEventArgs.DataAssetName = dataAssetName;
            loadDataUpdateEventArgs.Progress = progress;
            loadDataUpdateEventArgs.UserData = userData;
            return loadDataUpdateEventArgs;
        }

        /// <summary>
        /// Clears the read data update event.
        /// </summary>
        public override void Clear()
        {
            DataAssetName = null;
            Progress = 0f;
            UserData = null;
        }
    }
}
