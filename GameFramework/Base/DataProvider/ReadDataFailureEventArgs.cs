//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Read data failure event.
    /// </summary>
    public sealed class ReadDataFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ReadDataFailureEventArgs class.
        /// </summary>
        public ReadDataFailureEventArgs()
        {
            DataAssetName = null;
            ErrorMessage = null;
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
        /// Creates a read data failure event.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created read data failure event.</returns>
        public static ReadDataFailureEventArgs Create(string dataAssetName, string errorMessage, object userData)
        {
            ReadDataFailureEventArgs loadDataFailureEventArgs = ReferencePool.Acquire<ReadDataFailureEventArgs>();
            loadDataFailureEventArgs.DataAssetName = dataAssetName;
            loadDataFailureEventArgs.ErrorMessage = errorMessage;
            loadDataFailureEventArgs.UserData = userData;
            return loadDataFailureEventArgs;
        }

        /// <summary>
        /// Clears the read data failure event.
        /// </summary>
        public override void Clear()
        {
            DataAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
