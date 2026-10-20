//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.FileSystem;
using GameFramework.ObjectPool;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.Resource
{
    internal sealed partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        /// <summary>
        /// Resource loader.
        /// </summary>
        private sealed partial class ResourceLoader
        {
            private const int CachedHashBytesLength = 4;

            private readonly ResourceManager m_ResourceManager;
            private readonly TaskPool<LoadResourceTaskBase> m_TaskPool;
            private readonly Dictionary<object, int> m_AssetDependencyCount;
            private readonly Dictionary<object, int> m_ResourceDependencyCount;
            private readonly Dictionary<object, object> m_AssetToResourceMap;
            private readonly Dictionary<string, object> m_SceneToAssetMap;
            private readonly LoadBytesCallbacks m_LoadBytesCallbacks;
            private readonly byte[] m_CachedHashBytes;
            private IObjectPool<AssetObject> m_AssetPool;
            private IObjectPool<ResourceObject> m_ResourcePool;

            /// <summary>
            /// Initializes a new instance of the ResourceLoader class.
            /// </summary>
            /// <param name="resourceManager">The resource manager.</param>
            public ResourceLoader(ResourceManager resourceManager)
            {
                m_ResourceManager = resourceManager;
                m_TaskPool = new TaskPool<LoadResourceTaskBase>();
                m_AssetDependencyCount = new Dictionary<object, int>();
                m_ResourceDependencyCount = new Dictionary<object, int>();
                m_AssetToResourceMap = new Dictionary<object, object>();
                m_SceneToAssetMap = new Dictionary<string, object>(StringComparer.Ordinal);
                m_LoadBytesCallbacks = new LoadBytesCallbacks(OnLoadBinarySuccess, OnLoadBinaryFailure);
                m_CachedHashBytes = new byte[CachedHashBytesLength];
                m_AssetPool = null;
                m_ResourcePool = null;
            }

            /// <summary>
            /// Gets the total number of load resource agents.
            /// </summary>
            public int TotalAgentCount
            {
                get
                {
                    return m_TaskPool.TotalAgentCount;
                }
            }

            /// <summary>
            /// Gets the number of available load resource agents.
            /// </summary>
            public int FreeAgentCount
            {
                get
                {
                    return m_TaskPool.FreeAgentCount;
                }
            }

            /// <summary>
            /// Gets the number of working load resource agents.
            /// </summary>
            public int WorkingAgentCount
            {
                get
                {
                    return m_TaskPool.WorkingAgentCount;
                }
            }

            /// <summary>
            /// Gets the number of waiting load resource tasks.
            /// </summary>
            public int WaitingTaskCount
            {
                get
                {
                    return m_TaskPool.WaitingTaskCount;
                }
            }

            /// <summary>
            /// Gets or sets the interval in seconds for automatically releasing releasable objects in the asset object pool.
            /// </summary>
            public float AssetAutoReleaseInterval
            {
                get
                {
                    return m_AssetPool.AutoReleaseInterval;
                }
                set
                {
                    m_AssetPool.AutoReleaseInterval = value;
                }
            }

            /// <summary>
            /// Gets or sets the capacity of the asset object pool.
            /// </summary>
            public int AssetCapacity
            {
                get
                {
                    return m_AssetPool.Capacity;
                }
                set
                {
                    m_AssetPool.Capacity = value;
                }
            }

            /// <summary>
            /// Gets or sets the expiration time in seconds of objects in the asset object pool.
            /// </summary>
            public float AssetExpireTime
            {
                get
                {
                    return m_AssetPool.ExpireTime;
                }
                set
                {
                    m_AssetPool.ExpireTime = value;
                }
            }

            /// <summary>
            /// Gets or sets the priority of the asset object pool.
            /// </summary>
            public int AssetPriority
            {
                get
                {
                    return m_AssetPool.Priority;
                }
                set
                {
                    m_AssetPool.Priority = value;
                }
            }

            /// <summary>
            /// Gets or sets the interval in seconds for automatically releasing releasable objects in the resource object pool.
            /// </summary>
            public float ResourceAutoReleaseInterval
            {
                get
                {
                    return m_ResourcePool.AutoReleaseInterval;
                }
                set
                {
                    m_ResourcePool.AutoReleaseInterval = value;
                }
            }

            /// <summary>
            /// Gets or sets the capacity of the resource object pool.
            /// </summary>
            public int ResourceCapacity
            {
                get
                {
                    return m_ResourcePool.Capacity;
                }
                set
                {
                    m_ResourcePool.Capacity = value;
                }
            }

            /// <summary>
            /// Gets or sets the expiration time in seconds of objects in the resource object pool.
            /// </summary>
            public float ResourceExpireTime
            {
                get
                {
                    return m_ResourcePool.ExpireTime;
                }
                set
                {
                    m_ResourcePool.ExpireTime = value;
                }
            }

            /// <summary>
            /// Gets or sets the priority of the resource object pool.
            /// </summary>
            public int ResourcePriority
            {
                get
                {
                    return m_ResourcePool.Priority;
                }
                set
                {
                    m_ResourcePool.Priority = value;
                }
            }

            /// <summary>
            /// Resource loader update.
            /// </summary>
            /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
            /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                m_TaskPool.Update(elapseSeconds, realElapseSeconds);
            }

            /// <summary>
            /// Shuts down and cleans up the resource loader.
            /// </summary>
            public void Shutdown()
            {
                m_TaskPool.Shutdown();
                m_AssetDependencyCount.Clear();
                m_ResourceDependencyCount.Clear();
                m_AssetToResourceMap.Clear();
                m_SceneToAssetMap.Clear();
                LoadResourceAgent.Clear();
            }

            /// <summary>
            /// Sets the object pool manager.
            /// </summary>
            /// <param name="objectPoolManager">The object pool manager.</param>
            public void SetObjectPoolManager(IObjectPoolManager objectPoolManager)
            {
                m_AssetPool = objectPoolManager.CreateMultiSpawnObjectPool<AssetObject>("Asset Pool");
                m_ResourcePool = objectPoolManager.CreateMultiSpawnObjectPool<ResourceObject>("Resource Pool");
            }

            /// <summary>
            /// Adds a load resource agent helper.
            /// </summary>
            /// <param name="loadResourceAgentHelper">The load resource agent helper to add.</param>
            /// <param name="resourceHelper">The resource helper.</param>
            /// <param name="readOnlyPath">The resource read-only path.</param>
            /// <param name="readWritePath">The resource read-write path.</param>
            /// <param name="decryptResourceCallback">The decrypt resource callback method to set.</param>
            public void AddLoadResourceAgentHelper(ILoadResourceAgentHelper loadResourceAgentHelper, IResourceHelper resourceHelper, string readOnlyPath, string readWritePath, DecryptResourceCallback decryptResourceCallback)
            {
                if (m_AssetPool == null || m_ResourcePool == null)
                {
                    throw new GameFrameworkException("You must set object pool manager first.");
                }

                LoadResourceAgent agent = new LoadResourceAgent(loadResourceAgentHelper, resourceHelper, this, readOnlyPath, readWritePath, decryptResourceCallback ?? DefaultDecryptResourceCallback);
                m_TaskPool.AddAgent(agent);
            }

            /// <summary>
            /// Checks whether the asset exists.
            /// </summary>
            /// <param name="assetName">The name of the asset to check.</param>
            /// <returns>The result of checking whether the asset exists.</returns>
            public HasAssetResult HasAsset(string assetName)
            {
                ResourceInfo resourceInfo = GetResourceInfo(assetName);
                if (resourceInfo == null)
                {
                    return HasAssetResult.NotExist;
                }

                if (!resourceInfo.Ready && m_ResourceManager.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
                {
                    return HasAssetResult.NotReady;
                }

                if (resourceInfo.UseFileSystem)
                {
                    return resourceInfo.IsLoadFromBinary ? HasAssetResult.BinaryOnFileSystem : HasAssetResult.AssetOnFileSystem;
                }
                else
                {
                    return resourceInfo.IsLoadFromBinary ? HasAssetResult.BinaryOnDisk : HasAssetResult.AssetOnDisk;
                }
            }

            /// <summary>
            /// Asynchronously loads an asset.
            /// </summary>
            /// <param name="assetName">The name of the asset to load.</param>
            /// <param name="assetType">The type of the asset to load.</param>
            /// <param name="priority">The priority for loading the asset.</param>
            /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
            /// <param name="userData">The user-defined data.</param>
            public void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks loadAssetCallbacks, object userData)
            {
                ResourceInfo resourceInfo = null;
                string[] dependencyAssetNames = null;
                if (!CheckAsset(assetName, out resourceInfo, out dependencyAssetNames))
                {
                    string errorMessage = Utility.Text.Format("Can not load asset '{0}'.", assetName);
                    if (loadAssetCallbacks.LoadAssetFailureCallback != null)
                    {
                        loadAssetCallbacks.LoadAssetFailureCallback(assetName, resourceInfo != null && !resourceInfo.Ready ? LoadResourceStatus.NotReady : LoadResourceStatus.NotExist, errorMessage, userData);
                        return;
                    }

                    throw new GameFrameworkException(errorMessage);
                }

                if (resourceInfo.IsLoadFromBinary)
                {
                    string errorMessage = Utility.Text.Format("Can not load asset '{0}' which is a binary asset.", assetName);
                    if (loadAssetCallbacks.LoadAssetFailureCallback != null)
                    {
                        loadAssetCallbacks.LoadAssetFailureCallback(assetName, LoadResourceStatus.TypeError, errorMessage, userData);
                        return;
                    }

                    throw new GameFrameworkException(errorMessage);
                }

                LoadAssetTask mainTask = LoadAssetTask.Create(assetName, assetType, priority, resourceInfo, dependencyAssetNames, loadAssetCallbacks, userData);
                foreach (string dependencyAssetName in dependencyAssetNames)
                {
                    if (!LoadDependencyAsset(dependencyAssetName, priority, mainTask, userData))
                    {
                        string errorMessage = Utility.Text.Format("Can not load dependency asset '{0}' when load asset '{1}'.", dependencyAssetName, assetName);
                        if (loadAssetCallbacks.LoadAssetFailureCallback != null)
                        {
                            loadAssetCallbacks.LoadAssetFailureCallback(assetName, LoadResourceStatus.DependencyError, errorMessage, userData);
                            return;
                        }

                        throw new GameFrameworkException(errorMessage);
                    }
                }

                m_TaskPool.AddTask(mainTask);
                if (!resourceInfo.Ready)
                {
                    m_ResourceManager.UpdateResource(resourceInfo.ResourceName);
                }
            }

            /// <summary>
            /// Unloads the asset.
            /// </summary>
            /// <param name="asset">The asset to unload.</param>
            public void UnloadAsset(object asset)
            {
                m_AssetPool.Unspawn(asset);
            }

            /// <summary>
            /// Asynchronously loads a scene.
            /// </summary>
            /// <param name="sceneAssetName">The name of the scene asset to load.</param>
            /// <param name="priority">The priority for loading the scene asset.</param>
            /// <param name="loadSceneCallbacks">The load scene callbacks.</param>
            /// <param name="userData">The user-defined data.</param>
            public void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks loadSceneCallbacks, object userData)
            {
                ResourceInfo resourceInfo = null;
                string[] dependencyAssetNames = null;
                if (!CheckAsset(sceneAssetName, out resourceInfo, out dependencyAssetNames))
                {
                    string errorMessage = Utility.Text.Format("Can not load scene '{0}'.", sceneAssetName);
                    if (loadSceneCallbacks.LoadSceneFailureCallback != null)
                    {
                        loadSceneCallbacks.LoadSceneFailureCallback(sceneAssetName, resourceInfo != null && !resourceInfo.Ready ? LoadResourceStatus.NotReady : LoadResourceStatus.NotExist, errorMessage, userData);
                        return;
                    }

                    throw new GameFrameworkException(errorMessage);
                }

                if (resourceInfo.IsLoadFromBinary)
                {
                    string errorMessage = Utility.Text.Format("Can not load scene asset '{0}' which is a binary asset.", sceneAssetName);
                    if (loadSceneCallbacks.LoadSceneFailureCallback != null)
                    {
                        loadSceneCallbacks.LoadSceneFailureCallback(sceneAssetName, LoadResourceStatus.TypeError, errorMessage, userData);
                        return;
                    }

                    throw new GameFrameworkException(errorMessage);
                }

                LoadSceneTask mainTask = LoadSceneTask.Create(sceneAssetName, priority, resourceInfo, dependencyAssetNames, loadSceneCallbacks, userData);
                foreach (string dependencyAssetName in dependencyAssetNames)
                {
                    if (!LoadDependencyAsset(dependencyAssetName, priority, mainTask, userData))
                    {
                        string errorMessage = Utility.Text.Format("Can not load dependency asset '{0}' when load scene '{1}'.", dependencyAssetName, sceneAssetName);
                        if (loadSceneCallbacks.LoadSceneFailureCallback != null)
                        {
                            loadSceneCallbacks.LoadSceneFailureCallback(sceneAssetName, LoadResourceStatus.DependencyError, errorMessage, userData);
                            return;
                        }

                        throw new GameFrameworkException(errorMessage);
                    }
                }

                m_TaskPool.AddTask(mainTask);
                if (!resourceInfo.Ready)
                {
                    m_ResourceManager.UpdateResource(resourceInfo.ResourceName);
                }
            }

            /// <summary>
            /// Asynchronously unloads a scene.
            /// </summary>
            /// <param name="sceneAssetName">The name of the scene asset to unload.</param>
            /// <param name="unloadSceneCallbacks">The unload scene callbacks.</param>
            /// <param name="userData">The user-defined data.</param>
            public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData)
            {
                if (m_ResourceManager.m_ResourceHelper == null)
                {
                    throw new GameFrameworkException("You must set resource helper first.");
                }

                object asset = null;
                if (m_SceneToAssetMap.TryGetValue(sceneAssetName, out asset))
                {
                    m_SceneToAssetMap.Remove(sceneAssetName);
                    m_AssetPool.Unspawn(asset);
                    m_AssetPool.ReleaseObject(asset);
                }
                else
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not find asset of scene '{0}'.", sceneAssetName));
                }

                m_ResourceManager.m_ResourceHelper.UnloadScene(sceneAssetName, unloadSceneCallbacks, userData);
            }

            /// <summary>
            /// Gets the actual path of the binary asset.
            /// </summary>
            /// <param name="binaryAssetName">The name of the binary asset to get the actual path for.</param>
            /// <returns>The actual path of the binary asset.</returns>
            /// <remarks>This method applies only when the binary asset is stored on disk (not in a file system). If the binary asset is stored in a file system, the return value will always be null.</remarks>
            public string GetBinaryPath(string binaryAssetName)
            {
                ResourceInfo resourceInfo = GetResourceInfo(binaryAssetName);
                if (resourceInfo == null)
                {
                    return null;
                }

                if (!resourceInfo.Ready)
                {
                    return null;
                }

                if (!resourceInfo.IsLoadFromBinary)
                {
                    return null;
                }

                if (resourceInfo.UseFileSystem)
                {
                    return null;
                }

                return Utility.Path.GetRegularPath(Path.Combine(resourceInfo.StorageInReadOnly ? m_ResourceManager.m_ReadOnlyPath : m_ResourceManager.m_ReadWritePath, resourceInfo.ResourceName.FullName));
            }

            /// <summary>
            /// Gets the actual path of the binary asset.
            /// </summary>
            /// <param name="binaryAssetName">The name of the binary asset to get the actual path for.</param>
            /// <param name="storageInReadOnly">Whether the binary asset is stored in the read-only area.</param>
            /// <param name="storageInFileSystem">Whether the binary asset is stored in a file system.</param>
            /// <param name="relativePath">The relative path of the binary asset or the file system storing the binary asset, relative to the read-only or read-write area.</param>
            /// <param name="fileName">If the binary asset is stored in a file system, indicates the name of the binary asset in the file system; otherwise this parameter returns null.</param>
            /// <returns>Whether the actual path of the binary asset was retrieved successfully.</returns>
            public bool GetBinaryPath(string binaryAssetName, out bool storageInReadOnly, out bool storageInFileSystem, out string relativePath, out string fileName)
            {
                storageInReadOnly = false;
                storageInFileSystem = false;
                relativePath = null;
                fileName = null;

                ResourceInfo resourceInfo = GetResourceInfo(binaryAssetName);
                if (resourceInfo == null)
                {
                    return false;
                }

                if (!resourceInfo.Ready)
                {
                    return false;
                }

                if (!resourceInfo.IsLoadFromBinary)
                {
                    return false;
                }

                storageInReadOnly = resourceInfo.StorageInReadOnly;
                if (resourceInfo.UseFileSystem)
                {
                    storageInFileSystem = true;
                    relativePath = Utility.Text.Format("{0}.{1}", resourceInfo.FileSystemName, DefaultExtension);
                    fileName = resourceInfo.ResourceName.FullName;
                }
                else
                {
                    relativePath = resourceInfo.ResourceName.FullName;
                }

                return true;
            }

            /// <summary>
            /// Gets the length of the binary asset.
            /// </summary>
            /// <param name="binaryAssetName">The name of the binary asset to get the length for.</param>
            /// <returns>The length of the binary asset.</returns>
            public int GetBinaryLength(string binaryAssetName)
            {
                ResourceInfo resourceInfo = GetResourceInfo(binaryAssetName);
                if (resourceInfo == null)
                {
                    return -1;
                }

                if (!resourceInfo.Ready)
                {
                    return -1;
                }

                if (!resourceInfo.IsLoadFromBinary)
                {
                    return -1;
                }

                return resourceInfo.Length;
            }

            /// <summary>
            /// Asynchronously loads a binary asset.
            /// </summary>
            /// <param name="binaryAssetName">The name of the binary asset to load.</param>
            /// <param name="loadBinaryCallbacks">The load binary callbacks.</param>
            /// <param name="userData">The user-defined data.</param>
            public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks loadBinaryCallbacks, object userData)
            {
                ResourceInfo resourceInfo = GetResourceInfo(binaryAssetName);
                if (resourceInfo == null)
                {
                    string errorMessage = Utility.Text.Format("Can not load binary '{0}' which is not exist.", binaryAssetName);
                    if (loadBinaryCallbacks.LoadBinaryFailureCallback != null)
                    {
                        loadBinaryCallbacks.LoadBinaryFailureCallback(binaryAssetName, LoadResourceStatus.NotExist, errorMessage, userData);
                        return;
                    }

                    throw new GameFrameworkException(errorMessage);
                }

                if (!resourceInfo.Ready)
                {
                    string errorMessage = Utility.Text.Format("Can not load binary '{0}' which is not ready.", binaryAssetName);
                    if (loadBinaryCallbacks.LoadBinaryFailureCallback != null)
                    {
                        loadBinaryCallbacks.LoadBinaryFailureCallback(binaryAssetName, LoadResourceStatus.NotReady, errorMessage, userData);
                        return;
                    }

                    throw new GameFrameworkException(errorMessage);
                }

                if (!resourceInfo.IsLoadFromBinary)
                {
                    string errorMessage = Utility.Text.Format("Can not load binary '{0}' which is not a binary asset.", binaryAssetName);
                    if (loadBinaryCallbacks.LoadBinaryFailureCallback != null)
                    {
                        loadBinaryCallbacks.LoadBinaryFailureCallback(binaryAssetName, LoadResourceStatus.TypeError, errorMessage, userData);
                        return;
                    }

                    throw new GameFrameworkException(errorMessage);
                }

                if (resourceInfo.UseFileSystem)
                {
                    loadBinaryCallbacks.LoadBinarySuccessCallback(binaryAssetName, LoadBinaryFromFileSystem(binaryAssetName), 0f, userData);
                }
                else
                {
                    string path = Utility.Path.GetRemotePath(Path.Combine(resourceInfo.StorageInReadOnly ? m_ResourceManager.m_ReadOnlyPath : m_ResourceManager.m_ReadWritePath, resourceInfo.ResourceName.FullName));
                    m_ResourceManager.m_ResourceHelper.LoadBytes(path, m_LoadBytesCallbacks, LoadBinaryInfo.Create(binaryAssetName, resourceInfo, loadBinaryCallbacks, userData));
                }
            }

            /// <summary>
            /// Loads a binary asset from the file system.
            /// </summary>
            /// <param name="binaryAssetName">The name of the binary asset to load.</param>
            /// <returns>The binary bytes containing the loaded binary asset.</returns>
            public byte[] LoadBinaryFromFileSystem(string binaryAssetName)
            {
                ResourceInfo resourceInfo = GetResourceInfo(binaryAssetName);
                if (resourceInfo == null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not exist.", binaryAssetName));
                }

                if (!resourceInfo.Ready)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not ready.", binaryAssetName));
                }

                if (!resourceInfo.IsLoadFromBinary)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not a binary asset.", binaryAssetName));
                }

                if (!resourceInfo.UseFileSystem)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not use file system.", binaryAssetName));
                }

                IFileSystem fileSystem = m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly);
                byte[] bytes = fileSystem.ReadFile(resourceInfo.ResourceName.FullName);
                if (bytes == null)
                {
                    return null;
                }

                if (resourceInfo.LoadType == LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == LoadType.LoadFromBinaryAndDecrypt)
                {
                    DecryptResourceCallback decryptResourceCallback = m_ResourceManager.m_DecryptResourceCallback ?? DefaultDecryptResourceCallback;
                    decryptResourceCallback(bytes, 0, bytes.Length, resourceInfo.ResourceName.Name, resourceInfo.ResourceName.Variant, resourceInfo.ResourceName.Extension, resourceInfo.StorageInReadOnly, resourceInfo.FileSystemName, (byte)resourceInfo.LoadType, resourceInfo.Length, resourceInfo.HashCode);
                }

                return bytes;
            }

            /// <summary>
            /// Loads a binary asset from the file system.
            /// </summary>
            /// <param name="binaryAssetName">The name of the binary asset to load.</param>
            /// <param name="buffer">The buffer to store the loaded binary asset bytes.</param>
            /// <param name="startIndex">The start index in the buffer.</param>
            /// <param name="length">The length of bytes to read into the buffer.</param>
            /// <returns>The actual number of bytes loaded.</returns>
            public int LoadBinaryFromFileSystem(string binaryAssetName, byte[] buffer, int startIndex, int length)
            {
                ResourceInfo resourceInfo = GetResourceInfo(binaryAssetName);
                if (resourceInfo == null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not exist.", binaryAssetName));
                }

                if (!resourceInfo.Ready)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not ready.", binaryAssetName));
                }

                if (!resourceInfo.IsLoadFromBinary)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not a binary asset.", binaryAssetName));
                }

                if (!resourceInfo.UseFileSystem)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not use file system.", binaryAssetName));
                }

                IFileSystem fileSystem = m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly);
                int bytesRead = fileSystem.ReadFile(resourceInfo.ResourceName.FullName, buffer, startIndex, length);
                if (resourceInfo.LoadType == LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == LoadType.LoadFromBinaryAndDecrypt)
                {
                    DecryptResourceCallback decryptResourceCallback = m_ResourceManager.m_DecryptResourceCallback ?? DefaultDecryptResourceCallback;
                    decryptResourceCallback(buffer, startIndex, bytesRead, resourceInfo.ResourceName.Name, resourceInfo.ResourceName.Variant, resourceInfo.ResourceName.Extension, resourceInfo.StorageInReadOnly, resourceInfo.FileSystemName, (byte)resourceInfo.LoadType, resourceInfo.Length, resourceInfo.HashCode);
                }

                return bytesRead;
            }

            /// <summary>
            /// Loads a binary asset segment from the file system.
            /// </summary>
            /// <param name="binaryAssetName">The name of the binary asset to load the segment for.</param>
            /// <param name="offset">The offset of the segment to load.</param>
            /// <param name="length">The length of the segment to load.</param>
            /// <returns>The binary bytes containing the loaded binary asset segment.</returns>
            public byte[] LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, int length)
            {
                ResourceInfo resourceInfo = GetResourceInfo(binaryAssetName);
                if (resourceInfo == null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not exist.", binaryAssetName));
                }

                if (!resourceInfo.Ready)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not ready.", binaryAssetName));
                }

                if (!resourceInfo.IsLoadFromBinary)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not a binary asset.", binaryAssetName));
                }

                if (!resourceInfo.UseFileSystem)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not use file system.", binaryAssetName));
                }

                IFileSystem fileSystem = m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly);
                byte[] bytes = fileSystem.ReadFileSegment(resourceInfo.ResourceName.FullName, offset, length);
                if (bytes == null)
                {
                    return null;
                }

                if (resourceInfo.LoadType == LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == LoadType.LoadFromBinaryAndDecrypt)
                {
                    DecryptResourceCallback decryptResourceCallback = m_ResourceManager.m_DecryptResourceCallback ?? DefaultDecryptResourceCallback;
                    decryptResourceCallback(bytes, 0, bytes.Length, resourceInfo.ResourceName.Name, resourceInfo.ResourceName.Variant, resourceInfo.ResourceName.Extension, resourceInfo.StorageInReadOnly, resourceInfo.FileSystemName, (byte)resourceInfo.LoadType, resourceInfo.Length, resourceInfo.HashCode);
                }

                return bytes;
            }

            /// <summary>
            /// Loads a binary asset segment from the file system.
            /// </summary>
            /// <param name="binaryAssetName">The name of the binary asset to load the segment for.</param>
            /// <param name="offset">The offset of the segment to load.</param>
            /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
            /// <param name="startIndex">The start index in the buffer.</param>
            /// <param name="length">The length of the segment to load.</param>
            /// <returns>The actual number of bytes loaded.</returns>
            public int LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, byte[] buffer, int startIndex, int length)
            {
                ResourceInfo resourceInfo = GetResourceInfo(binaryAssetName);
                if (resourceInfo == null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not exist.", binaryAssetName));
                }

                if (!resourceInfo.Ready)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not ready.", binaryAssetName));
                }

                if (!resourceInfo.IsLoadFromBinary)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not a binary asset.", binaryAssetName));
                }

                if (!resourceInfo.UseFileSystem)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not load binary '{0}' from file system which is not use file system.", binaryAssetName));
                }

                IFileSystem fileSystem = m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly);
                int bytesRead = fileSystem.ReadFileSegment(resourceInfo.ResourceName.FullName, offset, buffer, startIndex, length);
                if (resourceInfo.LoadType == LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == LoadType.LoadFromBinaryAndDecrypt)
                {
                    DecryptResourceCallback decryptResourceCallback = m_ResourceManager.m_DecryptResourceCallback ?? DefaultDecryptResourceCallback;
                    decryptResourceCallback(buffer, startIndex, bytesRead, resourceInfo.ResourceName.Name, resourceInfo.ResourceName.Variant, resourceInfo.ResourceName.Extension, resourceInfo.StorageInReadOnly, resourceInfo.FileSystemName, (byte)resourceInfo.LoadType, resourceInfo.Length, resourceInfo.HashCode);
                }

                return bytesRead;
            }

            /// <summary>
            /// Gets the information of all load resource tasks.
            /// </summary>
            /// <returns>The information of all load resource tasks.</returns>
            public TaskInfo[] GetAllLoadAssetInfos()
            {
                return m_TaskPool.GetAllTaskInfos();
            }

            /// <summary>
            /// Gets the information of all load resource tasks.
            /// </summary>
            /// <param name="results">The information of all load resource tasks.</param>
            public void GetAllLoadAssetInfos(List<TaskInfo> results)
            {
                m_TaskPool.GetAllTaskInfos(results);
            }

            private bool LoadDependencyAsset(string assetName, int priority, LoadResourceTaskBase mainTask, object userData)
            {
                if (mainTask == null)
                {
                    throw new GameFrameworkException("Main task is invalid.");
                }

                ResourceInfo resourceInfo = null;
                string[] dependencyAssetNames = null;
                if (!CheckAsset(assetName, out resourceInfo, out dependencyAssetNames))
                {
                    return false;
                }

                if (resourceInfo.IsLoadFromBinary)
                {
                    return false;
                }

                LoadDependencyAssetTask dependencyTask = LoadDependencyAssetTask.Create(assetName, priority, resourceInfo, dependencyAssetNames, mainTask, userData);
                foreach (string dependencyAssetName in dependencyAssetNames)
                {
                    if (!LoadDependencyAsset(dependencyAssetName, priority, dependencyTask, userData))
                    {
                        return false;
                    }
                }

                m_TaskPool.AddTask(dependencyTask);
                if (!resourceInfo.Ready)
                {
                    m_ResourceManager.UpdateResource(resourceInfo.ResourceName);
                }

                return true;
            }

            private ResourceInfo GetResourceInfo(string assetName)
            {
                if (string.IsNullOrEmpty(assetName))
                {
                    return null;
                }

                AssetInfo assetInfo = m_ResourceManager.GetAssetInfo(assetName);
                if (assetInfo == null)
                {
                    return null;
                }

                return m_ResourceManager.GetResourceInfo(assetInfo.ResourceName);
            }

            private bool CheckAsset(string assetName, out ResourceInfo resourceInfo, out string[] dependencyAssetNames)
            {
                resourceInfo = null;
                dependencyAssetNames = null;

                if (string.IsNullOrEmpty(assetName))
                {
                    return false;
                }

                AssetInfo assetInfo = m_ResourceManager.GetAssetInfo(assetName);
                if (assetInfo == null)
                {
                    return false;
                }

                resourceInfo = m_ResourceManager.GetResourceInfo(assetInfo.ResourceName);
                if (resourceInfo == null)
                {
                    return false;
                }

                dependencyAssetNames = assetInfo.GetDependencyAssetNames();
                return m_ResourceManager.m_ResourceMode == ResourceMode.UpdatableWhilePlaying ? true : resourceInfo.Ready;
            }

            private void DefaultDecryptResourceCallback(byte[] bytes, int startIndex, int count, string name, string variant, string extension, bool storageInReadOnly, string fileSystem, byte loadType, int length, int hashCode)
            {
                Utility.Converter.GetBytes(hashCode, m_CachedHashBytes);
                switch ((LoadType)loadType)
                {
                    case LoadType.LoadFromMemoryAndQuickDecrypt:
                    case LoadType.LoadFromBinaryAndQuickDecrypt:
                        Utility.Encryption.GetQuickSelfXorBytes(bytes, m_CachedHashBytes);
                        break;

                    case LoadType.LoadFromMemoryAndDecrypt:
                    case LoadType.LoadFromBinaryAndDecrypt:
                        Utility.Encryption.GetSelfXorBytes(bytes, m_CachedHashBytes);
                        break;

                    default:
                        throw new GameFrameworkException("Not supported load type when decrypt resource.");
                }

                Array.Clear(m_CachedHashBytes, 0, CachedHashBytesLength);
            }

            private void OnLoadBinarySuccess(string fileUri, byte[] bytes, float duration, object userData)
            {
                LoadBinaryInfo loadBinaryInfo = (LoadBinaryInfo)userData;
                if (loadBinaryInfo == null)
                {
                    throw new GameFrameworkException("Load binary info is invalid.");
                }

                ResourceInfo resourceInfo = loadBinaryInfo.ResourceInfo;
                if (resourceInfo.LoadType == LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == LoadType.LoadFromBinaryAndDecrypt)
                {
                    DecryptResourceCallback decryptResourceCallback = m_ResourceManager.m_DecryptResourceCallback ?? DefaultDecryptResourceCallback;
                    decryptResourceCallback(bytes, 0, bytes.Length, resourceInfo.ResourceName.Name, resourceInfo.ResourceName.Variant, resourceInfo.ResourceName.Extension, resourceInfo.StorageInReadOnly, resourceInfo.FileSystemName, (byte)resourceInfo.LoadType, resourceInfo.Length, resourceInfo.HashCode);
                }

                loadBinaryInfo.LoadBinaryCallbacks.LoadBinarySuccessCallback(loadBinaryInfo.BinaryAssetName, bytes, duration, loadBinaryInfo.UserData);
                ReferencePool.Release(loadBinaryInfo);
            }

            private void OnLoadBinaryFailure(string fileUri, string errorMessage, object userData)
            {
                LoadBinaryInfo loadBinaryInfo = (LoadBinaryInfo)userData;
                if (loadBinaryInfo == null)
                {
                    throw new GameFrameworkException("Load binary info is invalid.");
                }

                if (loadBinaryInfo.LoadBinaryCallbacks.LoadBinaryFailureCallback != null)
                {
                    loadBinaryInfo.LoadBinaryCallbacks.LoadBinaryFailureCallback(loadBinaryInfo.BinaryAssetName, LoadResourceStatus.AssetError, errorMessage, loadBinaryInfo.UserData);
                }

                ReferencePool.Release(loadBinaryInfo);
            }
        }
    }
}
