//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Scene
{
    /// <summary>
    /// Load scene success event.
    /// </summary>
    public sealed class LoadSceneSuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the LoadSceneSuccessEventArgs class.
        /// </summary>
        public LoadSceneSuccessEventArgs()
        {
            SceneAssetName = null;
            Duration = 0f;
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
        /// Creates a load scene success event.
        /// </summary>
        /// <param name="sceneAssetName">Scene asset name.</param>
        /// <param name="duration">Loading duration in seconds.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created load scene success event.</returns>
        public static LoadSceneSuccessEventArgs Create(string sceneAssetName, float duration, object userData)
        {
            LoadSceneSuccessEventArgs loadSceneSuccessEventArgs = ReferencePool.Acquire<LoadSceneSuccessEventArgs>();
            loadSceneSuccessEventArgs.SceneAssetName = sceneAssetName;
            loadSceneSuccessEventArgs.Duration = duration;
            loadSceneSuccessEventArgs.UserData = userData;
            return loadSceneSuccessEventArgs;
        }

        /// <summary>
        /// Clears the load scene success event.
        /// </summary>
        public override void Clear()
        {
            SceneAssetName = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
