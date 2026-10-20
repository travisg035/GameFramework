//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Download;
using GameFramework.FileSystem;
using GameFramework.ObjectPool;
using System;
using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource manager interface.
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        /// Gets the read-only resource path.
        /// </summary>
        string ReadOnlyPath
        {
            get;
        }

        /// <summary>
        /// Gets the read-write resource path.
        /// </summary>
        string ReadWritePath
        {
            get;
        }

        /// <summary>
        /// Gets the resource mode.
        /// </summary>
        ResourceMode ResourceMode
        {
            get;
        }

        /// <summary>
        /// Gets the current variant.
        /// </summary>
        string CurrentVariant
        {
            get;
        }

        /// <summary>
        /// Gets the package version list serializer.
        /// </summary>
        PackageVersionListSerializer PackageVersionListSerializer
        {
            get;
        }

        /// <summary>
        /// Gets the updatable version list serializer.
        /// </summary>
        UpdatableVersionListSerializer UpdatableVersionListSerializer
        {
            get;
        }

        /// <summary>
        /// Gets the local read-only version list serializer.
        /// </summary>
        ReadOnlyVersionListSerializer ReadOnlyVersionListSerializer
        {
            get;
        }

        /// <summary>
        /// Gets the local read-write version list serializer.
        /// </summary>
        ReadWriteVersionListSerializer ReadWriteVersionListSerializer
        {
            get;
        }

        /// <summary>
        /// Gets the resource pack version list serializer.
        /// </summary>
        ResourcePackVersionListSerializer ResourcePackVersionListSerializer
        {
            get;
        }

        /// <summary>
        /// Gets the applicable game version for current resources.
        /// </summary>
        string ApplicableGameVersion
        {
            get;
        }

        /// <summary>
        /// Gets the current internal resource version.
        /// </summary>
        int InternalResourceVersion
        {
            get;
        }

        /// <summary>
        /// Gets the asset count.
        /// </summary>
        int AssetCount
        {
            get;
        }

        /// <summary>
        /// Gets the resource count.
        /// </summary>
        int ResourceCount
        {
            get;
        }

        /// <summary>
        /// Gets the resource group count.
        /// </summary>
        int ResourceGroupCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the URI prefix for updating resources.
        /// </summary>
        string UpdatePrefixUri
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the length in bytes of updated resources after which the version list is regenerated.
        /// </summary>
        int GenerateReadWriteVersionListLength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the path of the resource pack currently being applied.
        /// </summary>
        string ApplyingResourcePackPath
        {
            get;
        }

        /// <summary>
        /// Gets the number of resources waiting to be applied.
        /// </summary>
        int ApplyWaitingCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the resource update retry count.
        /// </summary>
        int UpdateRetryCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the resource group currently being updated.
        /// </summary>
        IResourceGroup UpdatingResourceGroup
        {
            get;
        }

        /// <summary>
        /// Gets the number of resources waiting to be updated.
        /// </summary>
        int UpdateWaitingCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of resources waiting to be updated while playing.
        /// </summary>
        int UpdateWaitingWhilePlayingCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of candidate resources for update.
        /// </summary>
        int UpdateCandidateCount
        {
            get;
        }

        /// <summary>
        /// Gets the total number of load resource agents.
        /// </summary>
        int LoadTotalAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of available load resource agents.
        /// </summary>
        int LoadFreeAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of working load resource agents.
        /// </summary>
        int LoadWorkingAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of waiting tasks for loading resources.
        /// </summary>
        int LoadWaitingTaskCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the interval in seconds for automatically releasing releasable objects in the asset object pool.
        /// </summary>
        float AssetAutoReleaseInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the capacity of the asset object pool.
        /// </summary>
        int AssetCapacity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the expiration time in seconds for objects in the asset object pool.
        /// </summary>
        float AssetExpireTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the priority of the asset object pool.
        /// </summary>
        int AssetPriority
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the interval in seconds for automatically releasing releasable objects in the resource object pool.
        /// </summary>
        float ResourceAutoReleaseInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the capacity of the resource object pool.
        /// </summary>
        int ResourceCapacity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the expiration time in seconds for objects in the resource object pool.
        /// </summary>
        float ResourceExpireTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the priority of the resource object pool.
        /// </summary>
        int ResourcePriority
        {
            get;
            set;
        }

        /// <summary>
        /// Resource verify start event.
        /// </summary>
        event EventHandler<ResourceVerifyStartEventArgs> ResourceVerifyStart;

        /// <summary>
        /// Resource verify success event.
        /// </summary>
        event EventHandler<ResourceVerifySuccessEventArgs> ResourceVerifySuccess;

        /// <summary>
        /// Resource verify failure event.
        /// </summary>
        event EventHandler<ResourceVerifyFailureEventArgs> ResourceVerifyFailure;

        /// <summary>
        /// Resource apply start event.
        /// </summary>
        event EventHandler<ResourceApplyStartEventArgs> ResourceApplyStart;

        /// <summary>
        /// Resource apply success event.
        /// </summary>
        event EventHandler<ResourceApplySuccessEventArgs> ResourceApplySuccess;

        /// <summary>
        /// Resource apply failure event.
        /// </summary>
        event EventHandler<ResourceApplyFailureEventArgs> ResourceApplyFailure;

        /// <summary>
        /// Resource update start event.
        /// </summary>
        event EventHandler<ResourceUpdateStartEventArgs> ResourceUpdateStart;

        /// <summary>
        /// Resource update changed event.
        /// </summary>
        event EventHandler<ResourceUpdateChangedEventArgs> ResourceUpdateChanged;

        /// <summary>
        /// Resource update success event.
        /// </summary>
        event EventHandler<ResourceUpdateSuccessEventArgs> ResourceUpdateSuccess;

        /// <summary>
        /// Resource update failure event.
        /// </summary>
        event EventHandler<ResourceUpdateFailureEventArgs> ResourceUpdateFailure;

        /// <summary>
        /// Resource update all complete event.
        /// </summary>
        event EventHandler<ResourceUpdateAllCompleteEventArgs> ResourceUpdateAllComplete;

        /// <summary>
        /// Sets the read-only resource path.
        /// </summary>
        /// <param name="readOnlyPath">The read-only resource path.</param>
        void SetReadOnlyPath(string readOnlyPath);

        /// <summary>
        /// Sets the read-write resource path.
        /// </summary>
        /// <param name="readWritePath">The read-write resource path.</param>
        void SetReadWritePath(string readWritePath);

        /// <summary>
        /// Sets the resource mode.
        /// </summary>
        /// <param name="resourceMode">The resource mode.</param>
        void SetResourceMode(ResourceMode resourceMode);

        /// <summary>
        /// Sets the current variant.
        /// </summary>
        /// <param name="currentVariant">The current variant.</param>
        void SetCurrentVariant(string currentVariant);

        /// <summary>
        /// Sets the object pool manager.
        /// </summary>
        /// <param name="objectPoolManager">The object pool manager.</param>
        void SetObjectPoolManager(IObjectPoolManager objectPoolManager);

        /// <summary>
        /// Sets the file system manager.
        /// </summary>
        /// <param name="fileSystemManager">The file system manager.</param>
        void SetFileSystemManager(IFileSystemManager fileSystemManager);

        /// <summary>
        /// Sets the download manager.
        /// </summary>
        /// <param name="downloadManager">The download manager.</param>
        void SetDownloadManager(IDownloadManager downloadManager);

        /// <summary>
        /// Sets the decrypt resource callback.
        /// </summary>
        /// <param name="decryptResourceCallback">The decrypt resource callback to set.</param>
        /// <remarks>If not set, the default decrypt resource callback will be used.</remarks>
        void SetDecryptResourceCallback(DecryptResourceCallback decryptResourceCallback);

        /// <summary>
        /// Sets the resource helper.
        /// </summary>
        /// <param name="resourceHelper">The resource helper.</param>
        void SetResourceHelper(IResourceHelper resourceHelper);

        /// <summary>
        /// Adds a load resource agent helper.
        /// </summary>
        /// <param name="loadResourceAgentHelper">The load resource agent helper to add.</param>
        void AddLoadResourceAgentHelper(ILoadResourceAgentHelper loadResourceAgentHelper);

        /// <summary>
        /// Initializes resources in package mode.
        /// </summary>
        /// <param name="initResourcesCompleteCallback">The callback invoked when initializing resources in package mode completes.</param>
        void InitResources(InitResourcesCompleteCallback initResourcesCompleteCallback);

        /// <summary>
        /// Checks the version list in updatable mode.
        /// </summary>
        /// <param name="latestInternalResourceVersion">The latest internal resource version.</param>
        /// <returns>The check version list result.</returns>
        CheckVersionListResult CheckVersionList(int latestInternalResourceVersion);

        /// <summary>
        /// Updates the version list in updatable mode.
        /// </summary>
        /// <param name="versionListLength">The version list length.</param>
        /// <param name="versionListHashCode">The version list hash code.</param>
        /// <param name="versionListCompressedLength">The compressed version list length.</param>
        /// <param name="versionListCompressedHashCode">The compressed version list hash code.</param>
        /// <param name="updateVersionListCallbacks">The update version list callbacks.</param>
        void UpdateVersionList(int versionListLength, int versionListHashCode, int versionListCompressedLength, int versionListCompressedHashCode, UpdateVersionListCallbacks updateVersionListCallbacks);

        /// <summary>
        /// Verifies resources in updatable mode.
        /// </summary>
        /// <param name="verifyResourceLengthPerFrame">The minimum length in bytes of resources to verify per frame.</param>
        /// <param name="verifyResourcesCompleteCallback">The callback invoked when verifying resources in updatable mode completes.</param>
        void VerifyResources(int verifyResourceLengthPerFrame, VerifyResourcesCompleteCallback verifyResourcesCompleteCallback);

        /// <summary>
        /// Checks resources in updatable mode.
        /// </summary>
        /// <param name="ignoreOtherVariant">Whether to ignore resources of other variants. If false, resources of other variants will be removed.</param>
        /// <param name="checkResourcesCompleteCallback">The callback invoked when checking resources in updatable mode completes.</param>
        void CheckResources(bool ignoreOtherVariant, CheckResourcesCompleteCallback checkResourcesCompleteCallback);

        /// <summary>
        /// Applies resources from a resource pack in updatable mode.
        /// </summary>
        /// <param name="resourcePackPath">The path of the resource pack to apply.</param>
        /// <param name="applyResourcesCompleteCallback">The callback invoked when applying resources from a resource pack in updatable mode completes.</param>
        void ApplyResources(string resourcePackPath, ApplyResourcesCompleteCallback applyResourcesCompleteCallback);

        /// <summary>
        /// Updates all resources in updatable mode.
        /// </summary>
        /// <param name="updateResourcesCompleteCallback">The callback invoked when updating default resource group in updatable mode completes.</param>
        void UpdateResources(UpdateResourcesCompleteCallback updateResourcesCompleteCallback);

        /// <summary>
        /// Updates resources in the specified resource group in updatable mode.
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group to update.</param>
        /// <param name="updateResourcesCompleteCallback">The callback invoked when updating the specified resource group in updatable mode completes.</param>
        void UpdateResources(string resourceGroupName, UpdateResourcesCompleteCallback updateResourcesCompleteCallback);

        /// <summary>
        /// Stops updating resources.
        /// </summary>
        void StopUpdateResources();

        /// <summary>
        /// Verifies a resource pack.
        /// </summary>
        /// <param name="resourcePackPath">The path of the resource pack to verify.</param>
        /// <returns>Whether the resource pack verification succeeded.</returns>
        bool VerifyResourcePack(string resourcePackPath);

        /// <summary>
        /// Gets information of all load asset tasks.
        /// </summary>
        /// <returns>Information of all load asset tasks.</returns>
        TaskInfo[] GetAllLoadAssetInfos();

        /// <summary>
        /// Gets information of all load asset tasks.
        /// </summary>
        /// <param name="results">Information of all load asset tasks.</param>
        void GetAllLoadAssetInfos(List<TaskInfo> results);

        /// <summary>
        /// Checks whether an asset exists.
        /// </summary>
        /// <param name="assetName">The name of the asset to check.</param>
        /// <returns>The result of checking whether the asset exists.</returns>
        HasAssetResult HasAsset(string assetName);

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        void LoadAsset(string assetName, LoadAssetCallbacks loadAssetCallbacks);

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="assetType">The type of the asset to load.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks loadAssetCallbacks);

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="priority">The priority for loading the asset.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        void LoadAsset(string assetName, int priority, LoadAssetCallbacks loadAssetCallbacks);

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadAsset(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData);

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="assetType">The type of the asset to load.</param>
        /// <param name="priority">The priority for loading the asset.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks loadAssetCallbacks);

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="assetType">The type of the asset to load.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks loadAssetCallbacks, object userData);

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="priority">The priority for loading the asset.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadAsset(string assetName, int priority, LoadAssetCallbacks loadAssetCallbacks, object userData);

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="assetType">The type of the asset to load.</param>
        /// <param name="priority">The priority for loading the asset.</param>
        /// <param name="loadAssetCallbacks">The loadAssetCallbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks loadAssetCallbacks, object userData);

        /// <summary>
        /// Unloads an asset.
        /// </summary>
        /// <param name="asset">The asset to unload.</param>
        void UnloadAsset(object asset);

        /// <summary>
        /// Asynchronously loads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to load.</param>
        /// <param name="loadSceneCallbacks">The load scene callbacks.</param>
        void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks);

        /// <summary>
        /// Asynchronously loads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to load.</param>
        /// <param name="priority">The priority for loading the scene asset.</param>
        /// <param name="loadSceneCallbacks">The load scene callbacks.</param>
        void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks loadSceneCallbacks);

        /// <summary>
        /// Asynchronously loads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to load.</param>
        /// <param name="loadSceneCallbacks">The load scene callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData);

        /// <summary>
        /// Asynchronously loads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to load.</param>
        /// <param name="priority">The priority for loading the scene asset.</param>
        /// <param name="loadSceneCallbacks">The load scene callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks loadSceneCallbacks, object userData);

        /// <summary>
        /// Asynchronously unloads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to unload.</param>
        /// <param name="unloadSceneCallbacks">The unload scene callbacks.</param>
        void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks);

        /// <summary>
        /// Asynchronously unloads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to unload.</param>
        /// <param name="unloadSceneCallbacks">The unload scene callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData);

        /// <summary>
        /// Gets the actual path of a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to get the actual path for.</param>
        /// <returns>The actual path of the binary asset.</returns>
        /// <remarks>This method is only applicable when the binary asset is stored on disk (not in a file system). If the binary asset is stored in a file system, the return value will always be null.</remarks>
        string GetBinaryPath(string binaryAssetName);

        /// <summary>
        /// Gets the actual path of a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to get the actual path for.</param>
        /// <param name="storageInReadOnly">Whether the binary asset is stored in the read-only path.</param>
        /// <param name="storageInFileSystem">Whether the binary asset is stored in a file system.</param>
        /// <param name="relativePath">The relative path of the binary asset or the file system storing the binary asset, relative to the read-only or read-write path.</param>
        /// <param name="fileName">If the binary asset is stored in a file system, indicates the name of the binary asset in the file system; otherwise, returns null.</param>
        /// <returns>Whether getting the actual path of the binary asset succeeded.</returns>
        bool GetBinaryPath(string binaryAssetName, out bool storageInReadOnly, out bool storageInFileSystem, out string relativePath, out string fileName);

        /// <summary>
        /// Gets the length of a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to get the length for.</param>
        /// <returns>The length of the binary asset.</returns>
        int GetBinaryLength(string binaryAssetName);

        /// <summary>
        /// Asynchronously loads a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="loadBinaryCallbacks">The load binary callbacks.</param>
        void LoadBinary(string binaryAssetName, LoadBinaryCallbacks loadBinaryCallbacks);

        /// <summary>
        /// Asynchronously loads a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="loadBinaryCallbacks">The load binary callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        void LoadBinary(string binaryAssetName, LoadBinaryCallbacks loadBinaryCallbacks, object userData);

        /// <summary>
        /// Loads a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <returns>The loaded binary stream.</returns>
        byte[] LoadBinaryFromFileSystem(string binaryAssetName);

        /// <summary>
        /// Loads a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset bytes.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        int LoadBinaryFromFileSystem(string binaryAssetName, byte[] buffer);

        /// <summary>
        /// Loads a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset bytes.</param>
        /// <param name="startIndex">The start index in the buffer to store the loaded binary asset bytes.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        int LoadBinaryFromFileSystem(string binaryAssetName, byte[] buffer, int startIndex);

        /// <summary>
        /// Loads a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset bytes.</param>
        /// <param name="startIndex">The start index in the buffer to store the loaded binary asset bytes.</param>
        /// <param name="length">The length of the binary asset bytes to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        int LoadBinaryFromFileSystem(string binaryAssetName, byte[] buffer, int startIndex, int length);

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The loaded binary segment stream.</returns>
        byte[] LoadBinarySegmentFromFileSystem(string binaryAssetName, int length);

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="offset">The offset of the segment to load.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The loaded binary segment stream.</returns>
        byte[] LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, int length);

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        int LoadBinarySegmentFromFileSystem(string binaryAssetName, byte[] buffer);

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        int LoadBinarySegmentFromFileSystem(string binaryAssetName, byte[] buffer, int length);

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="startIndex">The start index in the buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        int LoadBinarySegmentFromFileSystem(string binaryAssetName, byte[] buffer, int startIndex, int length);

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="offset">The offset of the segment to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        int LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, byte[] buffer);

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="offset">The offset of the segment to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        int LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, byte[] buffer, int length);

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="offset">The offset of the segment to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="startIndex">The start index in the buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        int LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, byte[] buffer, int startIndex, int length);

        /// <summary>
        /// Checks whether a resource group exists.
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group to check.</param>
        /// <returns>Whether the resource group exists.</returns>
        bool HasResourceGroup(string resourceGroupName);

        /// <summary>
        /// Gets the default resource group.
        /// </summary>
        /// <returns>The default resource group.</returns>
        IResourceGroup GetResourceGroup();

        /// <summary>
        /// Gets a resource group.
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group to get.</param>
        /// <returns>The resource group to get.</returns>
        IResourceGroup GetResourceGroup(string resourceGroupName);

        /// <summary>
        /// Gets all resource groups.
        /// </summary>
        /// <returns>All resource groups.</returns>
        IResourceGroup[] GetAllResourceGroups();

        /// <summary>
        /// Gets all resource groups.
        /// </summary>
        /// <param name="results">All resource groups.</param>
        void GetAllResourceGroups(List<IResourceGroup> results);

        /// <summary>
        /// Gets a resource group collection.
        /// </summary>
        /// <param name="resourceGroupNames">The collection of resource group names to get.</param>
        /// <returns>The resource group collection to get.</returns>
        IResourceGroupCollection GetResourceGroupCollection(params string[] resourceGroupNames);

        /// <summary>
        /// Gets a resource group collection.
        /// </summary>
        /// <param name="resourceGroupNames">The collection of resource group names to get.</param>
        /// <returns>The resource group collection to get.</returns>
        IResourceGroupCollection GetResourceGroupCollection(List<string> resourceGroupNames);
    }
}
