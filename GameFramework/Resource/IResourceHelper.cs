//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource helper interface.
    /// </summary>
    public interface IResourceHelper
    {
        /// <summary>
        /// Directly loads binary bytes from the specified file path.
        /// </summary>
        /// <param name="fileUri">The file path.</param>
        /// <param name="loadBytesCallbacks">The load bytes callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadBytes(string fileUri, LoadBytesCallbacks loadBytesCallbacks, object userData);

        /// <summary>
        /// Unloads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        /// <param name="unloadSceneCallbacks">The unload scene callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData);

        /// <summary>
        /// Releases a resource.
        /// </summary>
        /// <param name="objectToRelease">The resource object to release.</param>
        void Release(object objectToRelease);
    }
}
