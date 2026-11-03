//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource extension methods providing asynchronous operations.
    /// </summary>
    public static class ResourceExtension
    {
        private const int DefaultPriority = 0;

        /// <summary>
        /// Loads an asset asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the asset to load.</typeparam>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="priority">The priority for loading the asset.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the asynchronous load operation.</returns>
        public static Task<T> LoadAssetAsync<T>(this IResourceManager resourceManager, string assetName, int priority = DefaultPriority, object userData = null)
        {
            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);

            LoadAssetCallbacks callbacks = new LoadAssetCallbacks(
                (loadedAssetName, asset, duration, customUserData) =>
                {
                    tcs.TrySetResult((T)asset);
                },
                (loadedAssetName, status, errorMessage, customUserData) =>
                {
                    tcs.TrySetException(new GameFrameworkException(errorMessage));
                });

            try
            {
                resourceManager.LoadAsset(assetName, typeof(T), priority, callbacks, userData);
            }
            catch (Exception exception)
            {
                tcs.TrySetException(exception);
            }

            return tcs.Task;
        }

        /// <summary>
        /// Loads an asset asynchronously.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="assetType">The type of the asset to load.</param>
        /// <param name="priority">The priority for loading the asset.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the asynchronous load operation.</returns>
        public static Task<object> LoadAssetAsync(this IResourceManager resourceManager, string assetName, Type assetType, int priority = DefaultPriority, object userData = null)
        {
            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            LoadAssetCallbacks callbacks = new LoadAssetCallbacks(
                (loadedAssetName, asset, duration, customUserData) =>
                {
                    tcs.TrySetResult(asset);
                },
                (loadedAssetName, status, errorMessage, customUserData) =>
                {
                    tcs.TrySetException(new GameFrameworkException(errorMessage));
                });

            try
            {
                resourceManager.LoadAsset(assetName, assetType, priority, callbacks, userData);
            }
            catch (Exception exception)
            {
                tcs.TrySetException(exception);
            }

            return tcs.Task;
        }

        /// <summary>
        /// Loads a binary asset asynchronously.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the asynchronous load operation.</returns>
        public static Task<byte[]> LoadBinaryAsync(this IResourceManager resourceManager, string binaryAssetName, object userData = null)
        {
            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>(TaskCreationOptions.RunContinuationsAsynchronously);

            LoadBinaryCallbacks callbacks = new LoadBinaryCallbacks(
                (name, bytes, duration, customUserData) =>
                {
                    tcs.TrySetResult(bytes);
                },
                (name, status, errorMessage, customUserData) =>
                {
                    tcs.TrySetException(new GameFrameworkException(errorMessage));
                });

            try
            {
                resourceManager.LoadBinary(binaryAssetName, callbacks, userData);
            }
            catch (Exception exception)
            {
                tcs.TrySetException(exception);
            }

            return tcs.Task;
        }

        /// <summary>
        /// Loads a scene asynchronously.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="sceneAssetName">The asset name of the scene to load.</param>
        /// <param name="priority">The priority for loading the scene.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the asynchronous load operation.</returns>
        public static Task<bool> LoadSceneAsync(this IResourceManager resourceManager, string sceneAssetName, int priority = DefaultPriority, object userData = null)
        {
            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            LoadSceneCallbacks callbacks = new LoadSceneCallbacks(
                (name, duration, customUserData) =>
                {
                    tcs.TrySetResult(true);
                },
                (name, status, errorMessage, customUserData) =>
                {
                    tcs.TrySetException(new GameFrameworkException(errorMessage));
                });

            try
            {
                resourceManager.LoadScene(sceneAssetName, priority, callbacks, userData);
            }
            catch (Exception exception)
            {
                tcs.TrySetException(exception);
            }

            return tcs.Task;
        }

        /// <summary>
        /// Unloads a scene asynchronously.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="sceneAssetName">The asset name of the scene to unload.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the asynchronous unload operation.</returns>
        public static Task<bool> UnloadSceneAsync(this IResourceManager resourceManager, string sceneAssetName, object userData = null)
        {
            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            UnloadSceneCallbacks callbacks = new UnloadSceneCallbacks(
                (name, customUserData) =>
                {
                    tcs.TrySetResult(true);
                },
                (name, customUserData) =>
                {
                    tcs.TrySetException(new GameFrameworkException(Utility.Text.Format("Unload scene '{0}' failed.", name)));
                });

            try
            {
                resourceManager.UnloadScene(sceneAssetName, callbacks, userData);
            }
            catch (Exception exception)
            {
                tcs.TrySetException(exception);
            }

            return tcs.Task;
        }
    }
}
