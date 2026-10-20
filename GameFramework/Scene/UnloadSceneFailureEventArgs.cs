//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Scene
{
    /// <summary>
    /// Unload scene failure event.
    /// </summary>
    public sealed class UnloadSceneFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the UnloadSceneFailureEventArgs class.
        /// </summary>
        public UnloadSceneFailureEventArgs()
        {
            SceneAssetName = null;
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
        /// Gets the user-defined data.
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates an unload scene failure event.
        /// </summary>
        /// <param name="sceneAssetName">Scene asset name.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created unload scene failure event.</returns>
        public static UnloadSceneFailureEventArgs Create(string sceneAssetName, object userData)
        {
            UnloadSceneFailureEventArgs unloadSceneFailureEventArgs = ReferencePool.Acquire<UnloadSceneFailureEventArgs>();
            unloadSceneFailureEventArgs.SceneAssetName = sceneAssetName;
            unloadSceneFailureEventArgs.UserData = userData;
            return unloadSceneFailureEventArgs;
        }

        /// <summary>
        /// Clears the unload scene failure event.
        /// </summary>
        public override void Clear()
        {
            SceneAssetName = null;
            UserData = null;
        }
    }
}
