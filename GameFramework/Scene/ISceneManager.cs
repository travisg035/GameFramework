//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.Scene
{
    /// <summary>
    /// Scene manager interface.
    /// </summary>
    public interface ISceneManager
    {
        /// <summary>
        /// Load scene success event.
        /// </summary>
        event EventHandler<LoadSceneSuccessEventArgs> LoadSceneSuccess;

        /// <summary>
        /// Load scene failure event.
        /// </summary>
        event EventHandler<LoadSceneFailureEventArgs> LoadSceneFailure;

        /// <summary>
        /// Load scene update event.
        /// </summary>
        event EventHandler<LoadSceneUpdateEventArgs> LoadSceneUpdate;

        /// <summary>
        /// Load scene dependency asset event.
        /// </summary>
        event EventHandler<LoadSceneDependencyAssetEventArgs> LoadSceneDependencyAsset;

        /// <summary>
        /// Unload scene success event.
        /// </summary>
        event EventHandler<UnloadSceneSuccessEventArgs> UnloadSceneSuccess;

        /// <summary>
        /// Unload scene failure event.
        /// </summary>
        event EventHandler<UnloadSceneFailureEventArgs> UnloadSceneFailure;

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        void SetResourceManager(IResourceManager resourceManager);

        /// <summary>
        /// Gets whether the scene is loaded.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        /// <returns>Whether the scene is loaded.</returns>
        bool SceneIsLoaded(string sceneAssetName);

        /// <summary>
        /// Gets the loaded scene asset names.
        /// </summary>
        /// <returns>The loaded scene asset names.</returns>
        string[] GetLoadedSceneAssetNames();

        /// <summary>
        /// Gets the loaded scene asset names.
        /// </summary>
        /// <param name="results">The loaded scene asset names.</param>
        void GetLoadedSceneAssetNames(List<string> results);

        /// <summary>
        /// Gets whether the scene is loading.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        /// <returns>Whether the scene is loading.</returns>
        bool SceneIsLoading(string sceneAssetName);

        /// <summary>
        /// Gets the loading scene asset names.
        /// </summary>
        /// <returns>The loading scene asset names.</returns>
        string[] GetLoadingSceneAssetNames();

        /// <summary>
        /// Gets the loading scene asset names.
        /// </summary>
        /// <param name="results">The loading scene asset names.</param>
        void GetLoadingSceneAssetNames(List<string> results);

        /// <summary>
        /// Gets whether the scene is unloading.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        /// <returns>Whether the scene is unloading.</returns>
        bool SceneIsUnloading(string sceneAssetName);

        /// <summary>
        /// Gets the unloading scene asset names.
        /// </summary>
        /// <returns>The unloading scene asset names.</returns>
        string[] GetUnloadingSceneAssetNames();

        /// <summary>
        /// Gets the unloading scene asset names.
        /// </summary>
        /// <param name="results">The unloading scene asset names.</param>
        void GetUnloadingSceneAssetNames(List<string> results);

        /// <summary>
        /// Checks whether the scene asset exists.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to check.</param>
        /// <returns>Whether the scene asset exists.</returns>
        bool HasScene(string sceneAssetName);

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        void LoadScene(string sceneAssetName);

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        /// <param name="priority">The priority of loading the scene asset.</param>
        void LoadScene(string sceneAssetName, int priority);

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadScene(string sceneAssetName, object userData);

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        /// <param name="priority">The priority of loading the scene asset.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadScene(string sceneAssetName, int priority, object userData);

        /// <summary>
        /// Unloads the scene.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        void UnloadScene(string sceneAssetName);

        /// <summary>
        /// Unloads the scene.
        /// </summary>
        /// <param name="sceneAssetName">The scene asset name.</param>
        /// <param name="userData">The user-defined data.</param>
        void UnloadScene(string sceneAssetName, object userData);
    }
}
