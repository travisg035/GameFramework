//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Scene
{
    /// <summary>
    /// Load scene update event.
    /// </summary>
    public sealed class LoadSceneUpdateEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the LoadSceneUpdateEventArgs class.
        /// </summary>
        public LoadSceneUpdateEventArgs()
        {
            SceneAssetName = null;
            Progress = 0f;
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
        /// Gets the load scene progress.
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
        /// Creates a load scene update event.
        /// </summary>
        /// <param name="sceneAssetName">Scene asset name.</param>
        /// <param name="progress">Load scene progress.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created load scene update event.</returns>
        public static LoadSceneUpdateEventArgs Create(string sceneAssetName, float progress, object userData)
        {
            LoadSceneUpdateEventArgs loadSceneUpdateEventArgs = ReferencePool.Acquire<LoadSceneUpdateEventArgs>();
            loadSceneUpdateEventArgs.SceneAssetName = sceneAssetName;
            loadSceneUpdateEventArgs.Progress = progress;
            loadSceneUpdateEventArgs.UserData = userData;
            return loadSceneUpdateEventArgs;
        }

        /// <summary>
        /// Clears the load scene update event.
        /// </summary>
        public override void Clear()
        {
            SceneAssetName = null;
            Progress = 0f;
            UserData = null;
        }
    }
}
