//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Read data success event.
    /// </summary>
    public sealed class ReadDataSuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ReadDataSuccessEventArgs class.
        /// </summary>
        public ReadDataSuccessEventArgs()
        {
            DataAssetName = null;
            Duration = 0f;
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
        /// Gets the loading duration in seconds.
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
        /// Creates a read data success event.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="duration">Loading duration in seconds.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created read data success event.</returns>
        public static ReadDataSuccessEventArgs Create(string dataAssetName, float duration, object userData)
        {
            ReadDataSuccessEventArgs loadDataSuccessEventArgs = ReferencePool.Acquire<ReadDataSuccessEventArgs>();
            loadDataSuccessEventArgs.DataAssetName = dataAssetName;
            loadDataSuccessEventArgs.Duration = duration;
            loadDataSuccessEventArgs.UserData = userData;
            return loadDataSuccessEventArgs;
        }

        /// <summary>
        /// Clears the read data success event.
        /// </summary>
        public override void Clear()
        {
            DataAssetName = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
