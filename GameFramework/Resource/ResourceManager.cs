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
using System.IO;

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource manager.
    /// </summary>
    internal sealed partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        private const string RemoteVersionListFileName = "GameFrameworkVersion.dat";
        private const string LocalVersionListFileName = "GameFrameworkList.dat";
        private const string DefaultExtension = "dat";
        private const string TempExtension = "tmp";
        private const int FileSystemMaxFileCount = 1024 * 16;
        private const int FileSystemMaxBlockCount = 1024 * 256;

        private Dictionary<string, AssetInfo> m_AssetInfos;
        private Dictionary<ResourceName, ResourceInfo> m_ResourceInfos;
        private SortedDictionary<ResourceName, ReadWriteResourceInfo> m_ReadWriteResourceInfos;
        private readonly Dictionary<string, IFileSystem> m_ReadOnlyFileSystems;
        private readonly Dictionary<string, IFileSystem> m_ReadWriteFileSystems;
        private readonly Dictionary<string, ResourceGroup> m_ResourceGroups;

        private PackageVersionListSerializer m_PackageVersionListSerializer;
        private UpdatableVersionListSerializer m_UpdatableVersionListSerializer;
        private ReadOnlyVersionListSerializer m_ReadOnlyVersionListSerializer;
        private ReadWriteVersionListSerializer m_ReadWriteVersionListSerializer;
        private ResourcePackVersionListSerializer m_ResourcePackVersionListSerializer;

        private IFileSystemManager m_FileSystemManager;
        private ResourceIniter m_ResourceIniter;
        private VersionListProcessor m_VersionListProcessor;
        private ResourceVerifier m_ResourceVerifier;
        private ResourceChecker m_ResourceChecker;
        private ResourceUpdater m_ResourceUpdater;
        private ResourceLoader m_ResourceLoader;
        private IResourceHelper m_ResourceHelper;

        private string m_ReadOnlyPath;
        private string m_ReadWritePath;
        private ResourceMode m_ResourceMode;
        private bool m_RefuseSetFlag;
        private string m_CurrentVariant;
        private string m_UpdatePrefixUri;
        private string m_ApplicableGameVersion;
        private int m_InternalResourceVersion;
        private MemoryStream m_CachedStream;
        private DecryptResourceCallback m_DecryptResourceCallback;
        private InitResourcesCompleteCallback m_InitResourcesCompleteCallback;
        private UpdateVersionListCallbacks m_UpdateVersionListCallbacks;
        private VerifyResourcesCompleteCallback m_VerifyResourcesCompleteCallback;
        private CheckResourcesCompleteCallback m_CheckResourcesCompleteCallback;
        private ApplyResourcesCompleteCallback m_ApplyResourcesCompleteCallback;
        private UpdateResourcesCompleteCallback m_UpdateResourcesCompleteCallback;
        private EventHandler<ResourceVerifyStartEventArgs> m_ResourceVerifyStartEventHandler;
        private EventHandler<ResourceVerifySuccessEventArgs> m_ResourceVerifySuccessEventHandler;
        private EventHandler<ResourceVerifyFailureEventArgs> m_ResourceVerifyFailureEventHandler;
        private EventHandler<ResourceApplyStartEventArgs> m_ResourceApplyStartEventHandler;
        private EventHandler<ResourceApplySuccessEventArgs> m_ResourceApplySuccessEventHandler;
        private EventHandler<ResourceApplyFailureEventArgs> m_ResourceApplyFailureEventHandler;
        private EventHandler<ResourceUpdateStartEventArgs> m_ResourceUpdateStartEventHandler;
        private EventHandler<ResourceUpdateChangedEventArgs> m_ResourceUpdateChangedEventHandler;
        private EventHandler<ResourceUpdateSuccessEventArgs> m_ResourceUpdateSuccessEventHandler;
        private EventHandler<ResourceUpdateFailureEventArgs> m_ResourceUpdateFailureEventHandler;
        private EventHandler<ResourceUpdateAllCompleteEventArgs> m_ResourceUpdateAllCompleteEventHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceManager"/> class.
        /// </summary>
        public ResourceManager()
        {
            m_AssetInfos = null;
            m_ResourceInfos = null;
            m_ReadWriteResourceInfos = null;
            m_ReadOnlyFileSystems = new Dictionary<string, IFileSystem>(StringComparer.Ordinal);
            m_ReadWriteFileSystems = new Dictionary<string, IFileSystem>(StringComparer.Ordinal);
            m_ResourceGroups = new Dictionary<string, ResourceGroup>(StringComparer.Ordinal);

            m_PackageVersionListSerializer = null;
            m_UpdatableVersionListSerializer = null;
            m_ReadOnlyVersionListSerializer = null;
            m_ReadWriteVersionListSerializer = null;
            m_ResourcePackVersionListSerializer = null;

            m_ResourceIniter = null;
            m_VersionListProcessor = null;
            m_ResourceVerifier = null;
            m_ResourceChecker = null;
            m_ResourceUpdater = null;
            m_ResourceLoader = new ResourceLoader(this);

            m_ResourceHelper = null;
            m_ReadOnlyPath = null;
            m_ReadWritePath = null;
            m_ResourceMode = ResourceMode.Unspecified;
            m_RefuseSetFlag = false;
            m_CurrentVariant = null;
            m_UpdatePrefixUri = null;
            m_ApplicableGameVersion = null;
            m_InternalResourceVersion = 0;
            m_CachedStream = null;
            m_DecryptResourceCallback = null;
            m_InitResourcesCompleteCallback = null;
            m_UpdateVersionListCallbacks = null;
            m_VerifyResourcesCompleteCallback = null;
            m_CheckResourcesCompleteCallback = null;
            m_ApplyResourcesCompleteCallback = null;
            m_UpdateResourcesCompleteCallback = null;
            m_ResourceVerifySuccessEventHandler = null;
            m_ResourceVerifyFailureEventHandler = null;
            m_ResourceApplySuccessEventHandler = null;
            m_ResourceApplyFailureEventHandler = null;
            m_ResourceUpdateStartEventHandler = null;
            m_ResourceUpdateChangedEventHandler = null;
            m_ResourceUpdateSuccessEventHandler = null;
            m_ResourceUpdateFailureEventHandler = null;
            m_ResourceUpdateAllCompleteEventHandler = null;
        }

        /// <summary>
        /// Gets the game framework module priority.
        /// </summary>
        /// <remarks>Modules with higher priority will be polled first and shut down last.</remarks>
        internal override int Priority
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Gets the read-only resource path.
        /// </summary>
        public string ReadOnlyPath
        {
            get
            {
                return m_ReadOnlyPath;
            }
        }

        /// <summary>
        /// Gets the read-write resource path.
        /// </summary>
        public string ReadWritePath
        {
            get
            {
                return m_ReadWritePath;
            }
        }

        /// <summary>
        /// Gets the resource mode.
        /// </summary>
        public ResourceMode ResourceMode
        {
            get
            {
                return m_ResourceMode;
            }
        }

        /// <summary>
        /// Gets the current variant.
        /// </summary>
        public string CurrentVariant
        {
            get
            {
                return m_CurrentVariant;
            }
        }

        /// <summary>
        /// Gets the package version list serializer.
        /// </summary>
        public PackageVersionListSerializer PackageVersionListSerializer
        {
            get
            {
                return m_PackageVersionListSerializer;
            }
        }

        /// <summary>
        /// Gets the updatable version list serializer.
        /// </summary>
        public UpdatableVersionListSerializer UpdatableVersionListSerializer
        {
            get
            {
                return m_UpdatableVersionListSerializer;
            }
        }

        /// <summary>
        /// Gets the local read-only version list serializer.
        /// </summary>
        public ReadOnlyVersionListSerializer ReadOnlyVersionListSerializer
        {
            get
            {
                return m_ReadOnlyVersionListSerializer;
            }
        }

        /// <summary>
        /// Gets the local read-write version list serializer.
        /// </summary>
        public ReadWriteVersionListSerializer ReadWriteVersionListSerializer
        {
            get
            {
                return m_ReadWriteVersionListSerializer;
            }
        }

        /// <summary>
        /// Gets the resource pack version list serializer.
        /// </summary>
        public ResourcePackVersionListSerializer ResourcePackVersionListSerializer
        {
            get
            {
                return m_ResourcePackVersionListSerializer;
            }
        }

        /// <summary>
        /// Gets the applicable game version for current resources.
        /// </summary>
        public string ApplicableGameVersion
        {
            get
            {
                return m_ApplicableGameVersion;
            }
        }

        /// <summary>
        /// Gets the current internal resource version.
        /// </summary>
        public int InternalResourceVersion
        {
            get
            {
                return m_InternalResourceVersion;
            }
        }

        /// <summary>
        /// Gets the asset count.
        /// </summary>
        public int AssetCount
        {
            get
            {
                return m_AssetInfos != null ? m_AssetInfos.Count : 0;
            }
        }

        /// <summary>
        /// Gets the resource count.
        /// </summary>
        public int ResourceCount
        {
            get
            {
                return m_ResourceInfos != null ? m_ResourceInfos.Count : 0;
            }
        }

        /// <summary>
        /// Gets the resource group count.
        /// </summary>
        public int ResourceGroupCount
        {
            get
            {
                return m_ResourceGroups.Count;
            }
        }

        /// <summary>
        /// Gets or sets the URI prefix for updating resources.
        /// </summary>
        public string UpdatePrefixUri
        {
            get
            {
                return m_UpdatePrefixUri;
            }
            set
            {
                m_UpdatePrefixUri = value;
            }
        }

        /// <summary>
        /// Gets or sets the length in bytes of updated resources after which the version list is regenerated.
        /// </summary>
        public int GenerateReadWriteVersionListLength
        {
            get
            {
                return m_ResourceUpdater != null ? m_ResourceUpdater.GenerateReadWriteVersionListLength : 0;
            }
            set
            {
                if (m_ResourceUpdater == null)
                {
                    throw new GameFrameworkException("You can not use GenerateReadWriteVersionListLength at this time.");
                }

                m_ResourceUpdater.GenerateReadWriteVersionListLength = value;
            }
        }

        /// <summary>
        /// Gets the path of the resource pack currently being applied.
        /// </summary>
        public string ApplyingResourcePackPath
        {
            get
            {
                return m_ResourceUpdater != null ? m_ResourceUpdater.ApplyingResourcePackPath : null;
            }
        }

        /// <summary>
        /// Gets the number of resources waiting to be applied.
        /// </summary>
        public int ApplyWaitingCount
        {
            get
            {
                return m_ResourceUpdater != null ? m_ResourceUpdater.ApplyWaitingCount : 0;
            }
        }

        /// <summary>
        /// Gets or sets the resource update retry count.
        /// </summary>
        public int UpdateRetryCount
        {
            get
            {
                return m_ResourceUpdater != null ? m_ResourceUpdater.UpdateRetryCount : 0;
            }
            set
            {
                if (m_ResourceUpdater == null)
                {
                    throw new GameFrameworkException("You can not use UpdateRetryCount at this time.");
                }

                m_ResourceUpdater.UpdateRetryCount = value;
            }
        }

        /// <summary>
        /// Gets the resource group currently being updated.
        /// </summary>
        public IResourceGroup UpdatingResourceGroup
        {
            get
            {
                return m_ResourceUpdater != null ? m_ResourceUpdater.UpdatingResourceGroup : null;
            }
        }

        /// <summary>
        /// Gets the number of resources waiting to be updated.
        /// </summary>
        public int UpdateWaitingCount
        {
            get
            {
                return m_ResourceUpdater != null ? m_ResourceUpdater.UpdateWaitingCount : 0;
            }
        }

        /// <summary>
        /// Gets the number of resources waiting to be updated while playing.
        /// </summary>
        public int UpdateWaitingWhilePlayingCount
        {
            get
            {
                return m_ResourceUpdater != null ? m_ResourceUpdater.UpdateWaitingWhilePlayingCount : 0;
            }
        }

        /// <summary>
        /// Gets the number of candidate resources for update.
        /// </summary>
        public int UpdateCandidateCount
        {
            get
            {
                return m_ResourceUpdater != null ? m_ResourceUpdater.UpdateCandidateCount : 0;
            }
        }

        /// <summary>
        /// Gets the total number of load resource agents.
        /// </summary>
        public int LoadTotalAgentCount
        {
            get
            {
                return m_ResourceLoader.TotalAgentCount;
            }
        }

        /// <summary>
        /// Gets the number of available load resource agents.
        /// </summary>
        public int LoadFreeAgentCount
        {
            get
            {
                return m_ResourceLoader.FreeAgentCount;
            }
        }

        /// <summary>
        /// Gets the number of working load resource agents.
        /// </summary>
        public int LoadWorkingAgentCount
        {
            get
            {
                return m_ResourceLoader.WorkingAgentCount;
            }
        }

        /// <summary>
        /// Gets the number of waiting tasks for loading resources.
        /// </summary>
        public int LoadWaitingTaskCount
        {
            get
            {
                return m_ResourceLoader.WaitingTaskCount;
            }
        }

        /// <summary>
        /// Gets or sets the interval in seconds for automatically releasing releasable objects in the asset object pool.
        /// </summary>
        public float AssetAutoReleaseInterval
        {
            get
            {
                return m_ResourceLoader.AssetAutoReleaseInterval;
            }
            set
            {
                m_ResourceLoader.AssetAutoReleaseInterval = value;
            }
        }

        /// <summary>
        /// Gets or sets the capacity of the asset object pool.
        /// </summary>
        public int AssetCapacity
        {
            get
            {
                return m_ResourceLoader.AssetCapacity;
            }
            set
            {
                m_ResourceLoader.AssetCapacity = value;
            }
        }

        /// <summary>
        /// Gets or sets the expiration time in seconds for objects in the asset object pool.
        /// </summary>
        public float AssetExpireTime
        {
            get
            {
                return m_ResourceLoader.AssetExpireTime;
            }
            set
            {
                m_ResourceLoader.AssetExpireTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the priority of the asset object pool.
        /// </summary>
        public int AssetPriority
        {
            get
            {
                return m_ResourceLoader.AssetPriority;
            }
            set
            {
                m_ResourceLoader.AssetPriority = value;
            }
        }

        /// <summary>
        /// Gets or sets the interval in seconds for automatically releasing releasable objects in the resource object pool.
        /// </summary>
        public float ResourceAutoReleaseInterval
        {
            get
            {
                return m_ResourceLoader.ResourceAutoReleaseInterval;
            }
            set
            {
                m_ResourceLoader.ResourceAutoReleaseInterval = value;
            }
        }

        /// <summary>
        /// Gets or sets the capacity of the resource object pool.
        /// </summary>
        public int ResourceCapacity
        {
            get
            {
                return m_ResourceLoader.ResourceCapacity;
            }
            set
            {
                m_ResourceLoader.ResourceCapacity = value;
            }
        }

        /// <summary>
        /// Gets or sets the expiration time in seconds for objects in the resource object pool.
        /// </summary>
        public float ResourceExpireTime
        {
            get
            {
                return m_ResourceLoader.ResourceExpireTime;
            }
            set
            {
                m_ResourceLoader.ResourceExpireTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the priority of the resource object pool.
        /// </summary>
        public int ResourcePriority
        {
            get
            {
                return m_ResourceLoader.ResourcePriority;
            }
            set
            {
                m_ResourceLoader.ResourcePriority = value;
            }
        }

        /// <summary>
        /// Resource verify start event.
        /// </summary>
        public event EventHandler<ResourceVerifyStartEventArgs> ResourceVerifyStart
        {
            add
            {
                m_ResourceVerifyStartEventHandler += value;
            }
            remove
            {
                m_ResourceVerifyStartEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource verify success event.
        /// </summary>
        public event EventHandler<ResourceVerifySuccessEventArgs> ResourceVerifySuccess
        {
            add
            {
                m_ResourceVerifySuccessEventHandler += value;
            }
            remove
            {
                m_ResourceVerifySuccessEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource verify failure event.
        /// </summary>
        public event EventHandler<ResourceVerifyFailureEventArgs> ResourceVerifyFailure
        {
            add
            {
                m_ResourceVerifyFailureEventHandler += value;
            }
            remove
            {
                m_ResourceVerifyFailureEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource apply start event.
        /// </summary>
        public event EventHandler<ResourceApplyStartEventArgs> ResourceApplyStart
        {
            add
            {
                m_ResourceApplyStartEventHandler += value;
            }
            remove
            {
                m_ResourceApplyStartEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource apply success event.
        /// </summary>
        public event EventHandler<ResourceApplySuccessEventArgs> ResourceApplySuccess
        {
            add
            {
                m_ResourceApplySuccessEventHandler += value;
            }
            remove
            {
                m_ResourceApplySuccessEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource apply failure event.
        /// </summary>
        public event EventHandler<ResourceApplyFailureEventArgs> ResourceApplyFailure
        {
            add
            {
                m_ResourceApplyFailureEventHandler += value;
            }
            remove
            {
                m_ResourceApplyFailureEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource update start event.
        /// </summary>
        public event EventHandler<ResourceUpdateStartEventArgs> ResourceUpdateStart
        {
            add
            {
                m_ResourceUpdateStartEventHandler += value;
            }
            remove
            {
                m_ResourceUpdateStartEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource update changed event.
        /// </summary>
        public event EventHandler<ResourceUpdateChangedEventArgs> ResourceUpdateChanged
        {
            add
            {
                m_ResourceUpdateChangedEventHandler += value;
            }
            remove
            {
                m_ResourceUpdateChangedEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource update success event.
        /// </summary>
        public event EventHandler<ResourceUpdateSuccessEventArgs> ResourceUpdateSuccess
        {
            add
            {
                m_ResourceUpdateSuccessEventHandler += value;
            }
            remove
            {
                m_ResourceUpdateSuccessEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource update failure event.
        /// </summary>
        public event EventHandler<ResourceUpdateFailureEventArgs> ResourceUpdateFailure
        {
            add
            {
                m_ResourceUpdateFailureEventHandler += value;
            }
            remove
            {
                m_ResourceUpdateFailureEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource update all complete event.
        /// </summary>
        public event EventHandler<ResourceUpdateAllCompleteEventArgs> ResourceUpdateAllComplete
        {
            add
            {
                m_ResourceUpdateAllCompleteEventHandler += value;
            }
            remove
            {
                m_ResourceUpdateAllCompleteEventHandler -= value;
            }
        }

        /// <summary>
        /// Resource manager update.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_ResourceVerifier != null)
            {
                m_ResourceVerifier.Update(elapseSeconds, realElapseSeconds);
                return;
            }

            if (m_ResourceUpdater != null)
            {
                m_ResourceUpdater.Update(elapseSeconds, realElapseSeconds);
            }

            m_ResourceLoader.Update(elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// Shutdowns and cleans up the resource manager.
        /// </summary>
        internal override void Shutdown()
        {
            if (m_ResourceIniter != null)
            {
                m_ResourceIniter.Shutdown();
                m_ResourceIniter = null;
            }

            if (m_VersionListProcessor != null)
            {
                m_VersionListProcessor.VersionListUpdateSuccess -= OnVersionListProcessorUpdateSuccess;
                m_VersionListProcessor.VersionListUpdateFailure -= OnVersionListProcessorUpdateFailure;
                m_VersionListProcessor.Shutdown();
                m_VersionListProcessor = null;
            }

            if (m_ResourceVerifier != null)
            {
                m_ResourceVerifier.ResourceVerifyStart -= OnVerifierResourceVerifyStart;
                m_ResourceVerifier.ResourceVerifySuccess -= OnVerifierResourceVerifySuccess;
                m_ResourceVerifier.ResourceVerifyFailure -= OnVerifierResourceVerifyFailure;
                m_ResourceVerifier.ResourceVerifyComplete -= OnVerifierResourceVerifyComplete;
                m_ResourceVerifier.Shutdown();
                m_ResourceVerifier = null;
            }

            if (m_ResourceChecker != null)
            {
                m_ResourceChecker.ResourceNeedUpdate -= OnCheckerResourceNeedUpdate;
                m_ResourceChecker.ResourceCheckComplete -= OnCheckerResourceCheckComplete;
                m_ResourceChecker.Shutdown();
                m_ResourceChecker = null;
            }

            if (m_ResourceUpdater != null)
            {
                m_ResourceUpdater.ResourceApplyStart -= OnUpdaterResourceApplyStart;
                m_ResourceUpdater.ResourceApplySuccess -= OnUpdaterResourceApplySuccess;
                m_ResourceUpdater.ResourceApplyFailure -= OnUpdaterResourceApplyFailure;
                m_ResourceUpdater.ResourceApplyComplete -= OnUpdaterResourceApplyComplete;
                m_ResourceUpdater.ResourceUpdateStart -= OnUpdaterResourceUpdateStart;
                m_ResourceUpdater.ResourceUpdateChanged -= OnUpdaterResourceUpdateChanged;
                m_ResourceUpdater.ResourceUpdateSuccess -= OnUpdaterResourceUpdateSuccess;
                m_ResourceUpdater.ResourceUpdateFailure -= OnUpdaterResourceUpdateFailure;
                m_ResourceUpdater.ResourceUpdateComplete -= OnUpdaterResourceUpdateComplete;
                m_ResourceUpdater.ResourceUpdateAllComplete -= OnUpdaterResourceUpdateAllComplete;
                m_ResourceUpdater.Shutdown();
                m_ResourceUpdater = null;

                if (m_ReadWriteResourceInfos != null)
                {
                    m_ReadWriteResourceInfos.Clear();
                    m_ReadWriteResourceInfos = null;
                }

                FreeCachedStream();
            }

            if (m_ResourceLoader != null)
            {
                m_ResourceLoader.Shutdown();
                m_ResourceLoader = null;
            }

            if (m_AssetInfos != null)
            {
                m_AssetInfos.Clear();
                m_AssetInfos = null;
            }

            if (m_ResourceInfos != null)
            {
                m_ResourceInfos.Clear();
                m_ResourceInfos = null;
            }

            m_ReadOnlyFileSystems.Clear();
            m_ReadWriteFileSystems.Clear();
            m_ResourceGroups.Clear();
        }

        /// <summary>
        /// Sets the read-only resource path.
        /// </summary>
        /// <param name="readOnlyPath">The read-only resource path.</param>
        public void SetReadOnlyPath(string readOnlyPath)
        {
            if (string.IsNullOrEmpty(readOnlyPath))
            {
                throw new GameFrameworkException("Read-only path is invalid.");
            }

            if (m_RefuseSetFlag)
            {
                throw new GameFrameworkException("You can not set read-only path at this time.");
            }

            if (m_ResourceLoader.TotalAgentCount > 0)
            {
                throw new GameFrameworkException("You must set read-only path before add load resource agent helper.");
            }

            m_ReadOnlyPath = readOnlyPath;
        }

        /// <summary>
        /// Sets the read-write resource path.
        /// </summary>
        /// <param name="readWritePath">The read-write resource path.</param>
        public void SetReadWritePath(string readWritePath)
        {
            if (string.IsNullOrEmpty(readWritePath))
            {
                throw new GameFrameworkException("Read-write path is invalid.");
            }

            if (m_RefuseSetFlag)
            {
                throw new GameFrameworkException("You can not set read-write path at this time.");
            }

            if (m_ResourceLoader.TotalAgentCount > 0)
            {
                throw new GameFrameworkException("You must set read-write path before add load resource agent helper.");
            }

            m_ReadWritePath = readWritePath;
        }

        /// <summary>
        /// Sets the resource mode.
        /// </summary>
        /// <param name="resourceMode">The resource mode.</param>
        public void SetResourceMode(ResourceMode resourceMode)
        {
            if (resourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("Resource mode is invalid.");
            }

            if (m_RefuseSetFlag)
            {
                throw new GameFrameworkException("You can not set resource mode at this time.");
            }

            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                m_ResourceMode = resourceMode;

                if (m_ResourceMode == ResourceMode.Package)
                {
                    m_PackageVersionListSerializer = new PackageVersionListSerializer();

                    m_ResourceIniter = new ResourceIniter(this);
                    m_ResourceIniter.ResourceInitComplete += OnIniterResourceInitComplete;
                }
                else if (m_ResourceMode == ResourceMode.Updatable || m_ResourceMode == ResourceMode.UpdatableWhilePlaying)
                {
                    m_UpdatableVersionListSerializer = new UpdatableVersionListSerializer();
                    m_ReadOnlyVersionListSerializer = new ReadOnlyVersionListSerializer();
                    m_ReadWriteVersionListSerializer = new ReadWriteVersionListSerializer();
                    m_ResourcePackVersionListSerializer = new ResourcePackVersionListSerializer();

                    m_VersionListProcessor = new VersionListProcessor(this);
                    m_VersionListProcessor.VersionListUpdateSuccess += OnVersionListProcessorUpdateSuccess;
                    m_VersionListProcessor.VersionListUpdateFailure += OnVersionListProcessorUpdateFailure;

                    m_ResourceChecker = new ResourceChecker(this);
                    m_ResourceChecker.ResourceNeedUpdate += OnCheckerResourceNeedUpdate;
                    m_ResourceChecker.ResourceCheckComplete += OnCheckerResourceCheckComplete;

                    m_ResourceUpdater = new ResourceUpdater(this);
                    m_ResourceUpdater.ResourceApplyStart += OnUpdaterResourceApplyStart;
                    m_ResourceUpdater.ResourceApplySuccess += OnUpdaterResourceApplySuccess;
                    m_ResourceUpdater.ResourceApplyFailure += OnUpdaterResourceApplyFailure;
                    m_ResourceUpdater.ResourceApplyComplete += OnUpdaterResourceApplyComplete;
                    m_ResourceUpdater.ResourceUpdateStart += OnUpdaterResourceUpdateStart;
                    m_ResourceUpdater.ResourceUpdateChanged += OnUpdaterResourceUpdateChanged;
                    m_ResourceUpdater.ResourceUpdateSuccess += OnUpdaterResourceUpdateSuccess;
                    m_ResourceUpdater.ResourceUpdateFailure += OnUpdaterResourceUpdateFailure;
                    m_ResourceUpdater.ResourceUpdateComplete += OnUpdaterResourceUpdateComplete;
                    m_ResourceUpdater.ResourceUpdateAllComplete += OnUpdaterResourceUpdateAllComplete;
                }
            }
            else if (m_ResourceMode != resourceMode)
            {
                throw new GameFrameworkException("You can not change resource mode at this time.");
            }
        }

        /// <summary>
        /// Sets the current variant.
        /// </summary>
        /// <param name="currentVariant">The current variant.</param>
        public void SetCurrentVariant(string currentVariant)
        {
            if (m_RefuseSetFlag)
            {
                throw new GameFrameworkException("You can not set current variant at this time.");
            }

            m_CurrentVariant = currentVariant;
        }

        /// <summary>
        /// Sets the object pool manager.
        /// </summary>
        /// <param name="objectPoolManager">The object pool manager.</param>
        public void SetObjectPoolManager(IObjectPoolManager objectPoolManager)
        {
            if (objectPoolManager == null)
            {
                throw new GameFrameworkException("Object pool manager is invalid.");
            }

            m_ResourceLoader.SetObjectPoolManager(objectPoolManager);
        }

        /// <summary>
        /// Sets the file system manager.
        /// </summary>
        /// <param name="fileSystemManager">The file system manager.</param>
        public void SetFileSystemManager(IFileSystemManager fileSystemManager)
        {
            if (fileSystemManager == null)
            {
                throw new GameFrameworkException("File system manager is invalid.");
            }

            m_FileSystemManager = fileSystemManager;
        }

        /// <summary>
        /// Sets the download manager.
        /// </summary>
        /// <param name="downloadManager">The download manager.</param>
        public void SetDownloadManager(IDownloadManager downloadManager)
        {
            if (downloadManager == null)
            {
                throw new GameFrameworkException("Download manager is invalid.");
            }

            if (m_VersionListProcessor != null)
            {
                m_VersionListProcessor.SetDownloadManager(downloadManager);
            }

            if (m_ResourceUpdater != null)
            {
                m_ResourceUpdater.SetDownloadManager(downloadManager);
            }
        }

        /// <summary>
        /// Sets the decrypt resource callback.
        /// </summary>
        /// <param name="decryptResourceCallback">The decrypt resource callback to set.</param>
        /// <remarks>If not set, the default decrypt resource callback will be used.</remarks>
        public void SetDecryptResourceCallback(DecryptResourceCallback decryptResourceCallback)
        {
            if (m_ResourceLoader.TotalAgentCount > 0)
            {
                throw new GameFrameworkException("You must set decrypt resource callback before add load resource agent helper.");
            }

            m_DecryptResourceCallback = decryptResourceCallback;
        }

        /// <summary>
        /// Sets the resource helper.
        /// </summary>
        /// <param name="resourceHelper">The resource helper.</param>
        public void SetResourceHelper(IResourceHelper resourceHelper)
        {
            if (resourceHelper == null)
            {
                throw new GameFrameworkException("Resource helper is invalid.");
            }

            if (m_ResourceLoader.TotalAgentCount > 0)
            {
                throw new GameFrameworkException("You must set resource helper before add load resource agent helper.");
            }

            m_ResourceHelper = resourceHelper;
        }

        /// <summary>
        /// Adds a load resource agent helper.
        /// </summary>
        /// <param name="loadResourceAgentHelper">The load resource agent helper to add.</param>
        public void AddLoadResourceAgentHelper(ILoadResourceAgentHelper loadResourceAgentHelper)
        {
            if (m_ResourceHelper == null)
            {
                throw new GameFrameworkException("Resource helper is invalid.");
            }

            if (string.IsNullOrEmpty(m_ReadOnlyPath))
            {
                throw new GameFrameworkException("Read-only path is invalid.");
            }

            if (string.IsNullOrEmpty(m_ReadWritePath))
            {
                throw new GameFrameworkException("Read-write path is invalid.");
            }

            m_ResourceLoader.AddLoadResourceAgentHelper(loadResourceAgentHelper, m_ResourceHelper, m_ReadOnlyPath, m_ReadWritePath, m_DecryptResourceCallback);
        }

        /// <summary>
        /// Initializes resources in package mode.
        /// </summary>
        /// <param name="initResourcesCompleteCallback">The callback invoked when initializing resources in package mode completes.</param>
        public void InitResources(InitResourcesCompleteCallback initResourcesCompleteCallback)
        {
            if (initResourcesCompleteCallback == null)
            {
                throw new GameFrameworkException("Init resources complete callback is invalid.");
            }

            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("You must set resource mode first.");
            }

            if (m_ResourceMode != ResourceMode.Package)
            {
                throw new GameFrameworkException("You can not use InitResources without package resource mode.");
            }

            if (m_ResourceIniter == null)
            {
                throw new GameFrameworkException("You can not use InitResources at this time.");
            }

            m_RefuseSetFlag = true;
            m_InitResourcesCompleteCallback = initResourcesCompleteCallback;
            m_ResourceIniter.InitResources(m_CurrentVariant);
        }

        /// <summary>
        /// Checks the version list in updatable mode.
        /// </summary>
        /// <param name="latestInternalResourceVersion">The latest internal resource version.</param>
        /// <returns>The check version list result.</returns>
        public CheckVersionListResult CheckVersionList(int latestInternalResourceVersion)
        {
            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("You must set resource mode first.");
            }

            if (m_ResourceMode != ResourceMode.Updatable && m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
            {
                throw new GameFrameworkException("You can not use CheckVersionList without updatable resource mode.");
            }

            if (m_VersionListProcessor == null)
            {
                throw new GameFrameworkException("You can not use CheckVersionList at this time.");
            }

            return m_VersionListProcessor.CheckVersionList(latestInternalResourceVersion);
        }

        /// <summary>
        /// Updates the version list in updatable mode.
        /// </summary>
        /// <param name="versionListLength">The version list length.</param>
        /// <param name="versionListHashCode">The version list hash code.</param>
        /// <param name="versionListCompressedLength">The compressed version list length.</param>
        /// <param name="versionListCompressedHashCode">The compressed version list hash code.</param>
        /// <param name="updateVersionListCallbacks">The update version list callbacks.</param>
        public void UpdateVersionList(int versionListLength, int versionListHashCode, int versionListCompressedLength, int versionListCompressedHashCode, UpdateVersionListCallbacks updateVersionListCallbacks)
        {
            if (updateVersionListCallbacks == null)
            {
                throw new GameFrameworkException("Update version list callbacks is invalid.");
            }

            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("You must set resource mode first.");
            }

            if (m_ResourceMode != ResourceMode.Updatable && m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
            {
                throw new GameFrameworkException("You can not use UpdateVersionList without updatable resource mode.");
            }

            if (m_VersionListProcessor == null)
            {
                throw new GameFrameworkException("You can not use UpdateVersionList at this time.");
            }

            m_UpdateVersionListCallbacks = updateVersionListCallbacks;
            m_VersionListProcessor.UpdateVersionList(versionListLength, versionListHashCode, versionListCompressedLength, versionListCompressedHashCode);
        }

        /// <summary>
        /// Verifies resources in updatable mode.
        /// </summary>
        /// <param name="verifyResourceLengthPerFrame">The minimum length in bytes of resources to verify per frame.</param>
        /// <param name="verifyResourcesCompleteCallback">The callback invoked when verifying resources in updatable mode completes.</param>
        public void VerifyResources(int verifyResourceLengthPerFrame, VerifyResourcesCompleteCallback verifyResourcesCompleteCallback)
        {
            if (verifyResourcesCompleteCallback == null)
            {
                throw new GameFrameworkException("Verify resources complete callback is invalid.");
            }

            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("You must set resource mode first.");
            }

            if (m_ResourceMode != ResourceMode.Updatable && m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
            {
                throw new GameFrameworkException("You can not use VerifyResources without updatable resource mode.");
            }

            if (m_RefuseSetFlag)
            {
                throw new GameFrameworkException("You can not verify resources at this time.");
            }

            m_ResourceVerifier = new ResourceVerifier(this);
            m_ResourceVerifier.ResourceVerifyStart += OnVerifierResourceVerifyStart;
            m_ResourceVerifier.ResourceVerifySuccess += OnVerifierResourceVerifySuccess;
            m_ResourceVerifier.ResourceVerifyFailure += OnVerifierResourceVerifyFailure;
            m_ResourceVerifier.ResourceVerifyComplete += OnVerifierResourceVerifyComplete;
            m_VerifyResourcesCompleteCallback = verifyResourcesCompleteCallback;
            m_ResourceVerifier.VerifyResources(verifyResourceLengthPerFrame);
        }

        /// <summary>
        /// Checks resources in updatable mode.
        /// </summary>
        /// <param name="ignoreOtherVariant">Whether to ignore resources of other variants. If false, resources of other variants will be removed.</param>
        /// <param name="checkResourcesCompleteCallback">The callback invoked when checking resources in updatable mode completes.</param>
        public void CheckResources(bool ignoreOtherVariant, CheckResourcesCompleteCallback checkResourcesCompleteCallback)
        {
            if (checkResourcesCompleteCallback == null)
            {
                throw new GameFrameworkException("Check resources complete callback is invalid.");
            }

            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("You must set resource mode first.");
            }

            if (m_ResourceMode != ResourceMode.Updatable && m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
            {
                throw new GameFrameworkException("You can not use CheckResources without updatable resource mode.");
            }

            if (m_ResourceChecker == null)
            {
                throw new GameFrameworkException("You can not use CheckResources at this time.");
            }

            m_RefuseSetFlag = true;
            m_CheckResourcesCompleteCallback = checkResourcesCompleteCallback;
            m_ResourceChecker.CheckResources(m_CurrentVariant, ignoreOtherVariant);
        }

        /// <summary>
        /// Applies resources from a resource pack in updatable mode.
        /// </summary>
        /// <param name="resourcePackPath">The path of the resource pack to apply.</param>
        /// <param name="applyResourcesCompleteCallback">The callback invoked when applying resources from a resource pack in updatable mode completes.</param>
        public void ApplyResources(string resourcePackPath, ApplyResourcesCompleteCallback applyResourcesCompleteCallback)
        {
            if (string.IsNullOrEmpty(resourcePackPath))
            {
                throw new GameFrameworkException("Resource pack path is invalid.");
            }

            if (!File.Exists(resourcePackPath))
            {
                throw new GameFrameworkException(Utility.Text.Format("Resource pack '{0}' is not exist.", resourcePackPath));
            }

            if (applyResourcesCompleteCallback == null)
            {
                throw new GameFrameworkException("Apply resources complete callback is invalid.");
            }

            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("You must set resource mode first.");
            }

            if (m_ResourceMode != ResourceMode.Updatable && m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
            {
                throw new GameFrameworkException("You can not use ApplyResources without updatable resource mode.");
            }

            if (m_ResourceUpdater == null)
            {
                throw new GameFrameworkException("You can not use ApplyResources at this time.");
            }

            m_ApplyResourcesCompleteCallback = applyResourcesCompleteCallback;
            m_ResourceUpdater.ApplyResources(resourcePackPath);
        }

        /// <summary>
        /// Updates all resources in updatable mode.
        /// </summary>
        /// <param name="updateResourcesCompleteCallback">The callback invoked when updating default resource group in updatable mode completes.</param>
        public void UpdateResources(UpdateResourcesCompleteCallback updateResourcesCompleteCallback)
        {
            UpdateResources(string.Empty, updateResourcesCompleteCallback);
        }

        /// <summary>
        /// Updates resources in the specified resource group in updatable mode.
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group to update.</param>
        /// <param name="updateResourcesCompleteCallback">The callback invoked when updating the specified resource group in updatable mode completes.</param>
        public void UpdateResources(string resourceGroupName, UpdateResourcesCompleteCallback updateResourcesCompleteCallback)
        {
            if (updateResourcesCompleteCallback == null)
            {
                throw new GameFrameworkException("Update resources complete callback is invalid.");
            }

            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("You must set resource mode first.");
            }

            if (m_ResourceMode != ResourceMode.Updatable && m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
            {
                throw new GameFrameworkException("You can not use UpdateResources without updatable resource mode.");
            }

            if (m_ResourceUpdater == null)
            {
                throw new GameFrameworkException("You can not use UpdateResources at this time.");
            }

            ResourceGroup resourceGroup = (ResourceGroup)GetResourceGroup(resourceGroupName);
            if (resourceGroup == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not find resource group '{0}'.", resourceGroupName));
            }

            m_UpdateResourcesCompleteCallback = updateResourcesCompleteCallback;
            m_ResourceUpdater.UpdateResources(resourceGroup);
        }

        /// <summary>
        /// Stops updating resources.
        /// </summary>
        public void StopUpdateResources()
        {
            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("You must set resource mode first.");
            }

            if (m_ResourceMode != ResourceMode.Updatable && m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
            {
                throw new GameFrameworkException("You can not use StopUpdateResources without updatable resource mode.");
            }

            if (m_ResourceUpdater == null)
            {
                throw new GameFrameworkException("You can not use StopUpdateResources at this time.");
            }

            m_ResourceUpdater.StopUpdateResources();
            m_UpdateResourcesCompleteCallback = null;
        }

        /// <summary>
        /// Verifies a resource pack.
        /// </summary>
        /// <param name="resourcePackPath">The path of the resource pack to verify.</param>
        /// <returns>Whether the resource pack verification succeeded.</returns>
        public bool VerifyResourcePack(string resourcePackPath)
        {
            if (string.IsNullOrEmpty(resourcePackPath))
            {
                throw new GameFrameworkException("Resource pack path is invalid.");
            }

            if (!File.Exists(resourcePackPath))
            {
                throw new GameFrameworkException(Utility.Text.Format("Resource pack '{0}' is not exist.", resourcePackPath));
            }

            if (m_ResourceMode == ResourceMode.Unspecified)
            {
                throw new GameFrameworkException("You must set resource mode first.");
            }

            if (m_ResourceMode != ResourceMode.Updatable && m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
            {
                throw new GameFrameworkException("You can not use VerifyResourcePack without updatable resource mode.");
            }

            if (m_ResourcePackVersionListSerializer == null)
            {
                throw new GameFrameworkException("You can not use VerifyResourcePack at this time.");
            }

            try
            {
                long length = 0L;
                ResourcePackVersionList versionList = default(ResourcePackVersionList);
                using (FileStream fileStream = new FileStream(resourcePackPath, FileMode.Open, FileAccess.Read))
                {
                    length = fileStream.Length;
                    versionList = m_ResourcePackVersionListSerializer.Deserialize(fileStream);
                }

                if (!versionList.IsValid)
                {
                    return false;
                }

                if (versionList.Offset + versionList.Length != length)
                {
                    return false;
                }

                int hashCode = 0;
                using (FileStream fileStream = new FileStream(resourcePackPath, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Position = versionList.Offset;
                    hashCode = Utility.Verifier.GetCrc32(fileStream);
                }

                if (versionList.HashCode != hashCode)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets information of all load asset tasks.
        /// </summary>
        /// <returns>Information of all load asset tasks.</returns>
        public TaskInfo[] GetAllLoadAssetInfos()
        {
            return m_ResourceLoader.GetAllLoadAssetInfos();
        }

        /// <summary>
        /// Gets information of all load asset tasks.
        /// </summary>
        /// <param name="results">Information of all load asset tasks.</param>
        public void GetAllLoadAssetInfos(List<TaskInfo> results)
        {
            m_ResourceLoader.GetAllLoadAssetInfos(results);
        }

        /// <summary>
        /// Checks whether an asset exists.
        /// </summary>
        /// <param name="assetName">The name of the asset to check.</param>
        /// <returns>The result of checking whether the asset exists.</returns>
        public HasAssetResult HasAsset(string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            return m_ResourceLoader.HasAsset(assetName);
        }

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        public void LoadAsset(string assetName, LoadAssetCallbacks loadAssetCallbacks)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            if (loadAssetCallbacks == null)
            {
                throw new GameFrameworkException("Load asset callbacks is invalid.");
            }

            m_ResourceLoader.LoadAsset(assetName, null, Constant.DefaultPriority, loadAssetCallbacks, null);
        }

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="assetType">The type of the asset to load.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        public void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks loadAssetCallbacks)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            if (loadAssetCallbacks == null)
            {
                throw new GameFrameworkException("Load asset callbacks is invalid.");
            }

            m_ResourceLoader.LoadAsset(assetName, assetType, Constant.DefaultPriority, loadAssetCallbacks, null);
        }

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="priority">The priority for loading the asset.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        public void LoadAsset(string assetName, int priority, LoadAssetCallbacks loadAssetCallbacks)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            if (loadAssetCallbacks == null)
            {
                throw new GameFrameworkException("Load asset callbacks is invalid.");
            }

            m_ResourceLoader.LoadAsset(assetName, null, priority, loadAssetCallbacks, null);
        }

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        public void LoadAsset(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            if (loadAssetCallbacks == null)
            {
                throw new GameFrameworkException("Load asset callbacks is invalid.");
            }

            m_ResourceLoader.LoadAsset(assetName, null, Constant.DefaultPriority, loadAssetCallbacks, userData);
        }

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="assetType">The type of the asset to load.</param>
        /// <param name="priority">The priority for loading the asset.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        public void LoadAsset(string assetName, Type assetType, int priority, LoadAssetCallbacks loadAssetCallbacks)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            if (loadAssetCallbacks == null)
            {
                throw new GameFrameworkException("Load asset callbacks is invalid.");
            }

            m_ResourceLoader.LoadAsset(assetName, assetType, priority, loadAssetCallbacks, null);
        }

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="assetType">The type of the asset to load.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        public void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks loadAssetCallbacks, object userData)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            if (loadAssetCallbacks == null)
            {
                throw new GameFrameworkException("Load asset callbacks is invalid.");
            }

            m_ResourceLoader.LoadAsset(assetName, assetType, Constant.DefaultPriority, loadAssetCallbacks, userData);
        }

        /// <summary>
        /// Asynchronously loads an asset.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="priority">The priority for loading the asset.</param>
        /// <param name="loadAssetCallbacks">The load asset callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        public void LoadAsset(string assetName, int priority, LoadAssetCallbacks loadAssetCallbacks, object userData)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            if (loadAssetCallbacks == null)
            {
                throw new GameFrameworkException("Load asset callbacks is invalid.");
            }

            m_ResourceLoader.LoadAsset(assetName, null, priority, loadAssetCallbacks, userData);
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
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            if (loadAssetCallbacks == null)
            {
                throw new GameFrameworkException("Load asset callbacks is invalid.");
            }

            m_ResourceLoader.LoadAsset(assetName, assetType, priority, loadAssetCallbacks, userData);
        }

        /// <summary>
        /// Unloads an asset.
        /// </summary>
        /// <param name="asset">The asset to unload.</param>
        public void UnloadAsset(object asset)
        {
            if (asset == null)
            {
                throw new GameFrameworkException("Asset is invalid.");
            }

            if (m_ResourceLoader == null)
            {
                return;
            }

            m_ResourceLoader.UnloadAsset(asset);
        }

        /// <summary>
        /// Asynchronously loads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to load.</param>
        /// <param name="loadSceneCallbacks">The load scene callbacks.</param>
        public void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            if (loadSceneCallbacks == null)
            {
                throw new GameFrameworkException("Load scene callbacks is invalid.");
            }

            m_ResourceLoader.LoadScene(sceneAssetName, Constant.DefaultPriority, loadSceneCallbacks, null);
        }

        /// <summary>
        /// Asynchronously loads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to load.</param>
        /// <param name="priority">The priority for loading the scene asset.</param>
        /// <param name="loadSceneCallbacks">The load scene callbacks.</param>
        public void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks loadSceneCallbacks)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            if (loadSceneCallbacks == null)
            {
                throw new GameFrameworkException("Load scene callbacks is invalid.");
            }

            m_ResourceLoader.LoadScene(sceneAssetName, priority, loadSceneCallbacks, null);
        }

        /// <summary>
        /// Asynchronously loads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to load.</param>
        /// <param name="loadSceneCallbacks">The load scene callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        public void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            if (loadSceneCallbacks == null)
            {
                throw new GameFrameworkException("Load scene callbacks is invalid.");
            }

            m_ResourceLoader.LoadScene(sceneAssetName, Constant.DefaultPriority, loadSceneCallbacks, userData);
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
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            if (loadSceneCallbacks == null)
            {
                throw new GameFrameworkException("Load scene callbacks is invalid.");
            }

            m_ResourceLoader.LoadScene(sceneAssetName, priority, loadSceneCallbacks, userData);
        }

        /// <summary>
        /// Asynchronously unloads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to unload.</param>
        /// <param name="unloadSceneCallbacks">The unload scene callbacks.</param>
        public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            if (unloadSceneCallbacks == null)
            {
                throw new GameFrameworkException("Unload scene callbacks is invalid.");
            }

            m_ResourceLoader.UnloadScene(sceneAssetName, unloadSceneCallbacks, null);
        }

        /// <summary>
        /// Asynchronously unloads a scene.
        /// </summary>
        /// <param name="sceneAssetName">The name of the scene asset to unload.</param>
        /// <param name="unloadSceneCallbacks">The unload scene callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameFrameworkException("Scene asset name is invalid.");
            }

            if (unloadSceneCallbacks == null)
            {
                throw new GameFrameworkException("Unload scene callbacks is invalid.");
            }

            m_ResourceLoader.UnloadScene(sceneAssetName, unloadSceneCallbacks, userData);
        }

        /// <summary>
        /// Gets the actual path of a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to get the actual path for.</param>
        /// <returns>The actual path of the binary asset.</returns>
        /// <remarks>This method is only applicable when the binary asset is stored on disk (not in a file system). If the binary asset is stored in a file system, the return value will always be null.</remarks>
        public string GetBinaryPath(string binaryAssetName)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            return m_ResourceLoader.GetBinaryPath(binaryAssetName);
        }

        /// <summary>
        /// Gets the actual path of a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to get the actual path for.</param>
        /// <param name="storageInReadOnly">Whether the binary asset is stored in the read-only path.</param>
        /// <param name="storageInFileSystem">Whether the binary asset is stored in a file system.</param>
        /// <param name="relativePath">The relative path of the binary asset or the file system storing the binary asset, relative to the read-only or read-write path.</param>
        /// <param name="fileName">If the binary asset is stored in a file system, indicates the name of the binary asset in the file system; otherwise, returns null.</param>
        /// <returns>Whether getting the actual path of the binary asset succeeded.</returns>
        public bool GetBinaryPath(string binaryAssetName, out bool storageInReadOnly, out bool storageInFileSystem, out string relativePath, out string fileName)
        {
            return m_ResourceLoader.GetBinaryPath(binaryAssetName, out storageInReadOnly, out storageInFileSystem, out relativePath, out fileName);
        }

        /// <summary>
        /// Gets the length of a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to get the length for.</param>
        /// <returns>The length of the binary asset.</returns>
        public int GetBinaryLength(string binaryAssetName)
        {
            return m_ResourceLoader.GetBinaryLength(binaryAssetName);
        }

        /// <summary>
        /// Asynchronously loads a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="loadBinaryCallbacks">The load binary callbacks.</param>
        public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks loadBinaryCallbacks)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (loadBinaryCallbacks == null)
            {
                throw new GameFrameworkException("Load binary callbacks is invalid.");
            }

            m_ResourceLoader.LoadBinary(binaryAssetName, loadBinaryCallbacks, null);
        }

        /// <summary>
        /// Asynchronously loads a binary asset.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="loadBinaryCallbacks">The load binary callbacks.</param>
        /// <param name="userData">The user-defined data.</param>
        public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks loadBinaryCallbacks, object userData)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (loadBinaryCallbacks == null)
            {
                throw new GameFrameworkException("Load binary callbacks is invalid.");
            }

            m_ResourceLoader.LoadBinary(binaryAssetName, loadBinaryCallbacks, userData);
        }

        /// <summary>
        /// Loads a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <returns>The loaded binary stream.</returns>
        public byte[] LoadBinaryFromFileSystem(string binaryAssetName)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            return m_ResourceLoader.LoadBinaryFromFileSystem(binaryAssetName);
        }

        /// <summary>
        /// Loads a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset bytes.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        public int LoadBinaryFromFileSystem(string binaryAssetName, byte[] buffer)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (buffer == null)
            {
                throw new GameFrameworkException("Buffer is invalid.");
            }

            return m_ResourceLoader.LoadBinaryFromFileSystem(binaryAssetName, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Loads a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset bytes.</param>
        /// <param name="startIndex">The start index in the buffer to store the loaded binary asset bytes.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        public int LoadBinaryFromFileSystem(string binaryAssetName, byte[] buffer, int startIndex)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (buffer == null)
            {
                throw new GameFrameworkException("Buffer is invalid.");
            }

            return m_ResourceLoader.LoadBinaryFromFileSystem(binaryAssetName, buffer, startIndex, buffer.Length - startIndex);
        }

        /// <summary>
        /// Loads a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset bytes.</param>
        /// <param name="startIndex">The start index in the buffer to store the loaded binary asset bytes.</param>
        /// <param name="length">The length of the binary asset bytes to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        public int LoadBinaryFromFileSystem(string binaryAssetName, byte[] buffer, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (buffer == null)
            {
                throw new GameFrameworkException("Buffer is invalid.");
            }

            return m_ResourceLoader.LoadBinaryFromFileSystem(binaryAssetName, buffer, startIndex, length);
        }

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The loaded binary segment stream.</returns>
        public byte[] LoadBinarySegmentFromFileSystem(string binaryAssetName, int length)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            return m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, 0, length);
        }

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="offset">The offset of the segment to load.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The loaded binary segment stream.</returns>
        public byte[] LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, int length)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            return m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, offset, length);
        }

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        public int LoadBinarySegmentFromFileSystem(string binaryAssetName, byte[] buffer)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (buffer == null)
            {
                throw new GameFrameworkException("Buffer is invalid.");
            }

            return m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, 0, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        public int LoadBinarySegmentFromFileSystem(string binaryAssetName, byte[] buffer, int length)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (buffer == null)
            {
                throw new GameFrameworkException("Buffer is invalid.");
            }

            return m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, 0, buffer, 0, length);
        }

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="startIndex">The start index in the buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        public int LoadBinarySegmentFromFileSystem(string binaryAssetName, byte[] buffer, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (buffer == null)
            {
                throw new GameFrameworkException("Buffer is invalid.");
            }

            return m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, 0, buffer, startIndex, length);
        }

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="offset">The offset of the segment to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        public int LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, byte[] buffer)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (buffer == null)
            {
                throw new GameFrameworkException("Buffer is invalid.");
            }

            return m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, offset, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="offset">The offset of the segment to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        public int LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, byte[] buffer, int length)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (buffer == null)
            {
                throw new GameFrameworkException("Buffer is invalid.");
            }

            return m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, offset, buffer, 0, length);
        }

        /// <summary>
        /// Loads a segment of a binary asset from a file system.
        /// </summary>
        /// <param name="binaryAssetName">The name of the binary asset to load the segment from.</param>
        /// <param name="offset">The offset of the segment to load.</param>
        /// <param name="buffer">The buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="startIndex">The start index in the buffer to store the loaded binary asset segment bytes.</param>
        /// <param name="length">The length of the segment to load.</param>
        /// <returns>The actual number of bytes loaded.</returns>
        public int LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, byte[] buffer, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(binaryAssetName))
            {
                throw new GameFrameworkException("Binary asset name is invalid.");
            }

            if (buffer == null)
            {
                throw new GameFrameworkException("Buffer is invalid.");
            }

            return m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, offset, buffer, startIndex, length);
        }

        /// <summary>
        /// Checks whether a resource group exists.
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group to check.</param>
        /// <returns>Whether the resource group exists.</returns>
        public bool HasResourceGroup(string resourceGroupName)
        {
            return m_ResourceGroups.ContainsKey(resourceGroupName ?? string.Empty);
        }

        /// <summary>
        /// Gets the default resource group.
        /// </summary>
        /// <returns>The default resource group.</returns>
        public IResourceGroup GetResourceGroup()
        {
            return GetResourceGroup(string.Empty);
        }

        /// <summary>
        /// Gets a resource group.
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group to get.</param>
        /// <returns>The resource group to get.</returns>
        public IResourceGroup GetResourceGroup(string resourceGroupName)
        {
            ResourceGroup resourceGroup = null;
            if (m_ResourceGroups.TryGetValue(resourceGroupName ?? string.Empty, out resourceGroup))
            {
                return resourceGroup;
            }

            return null;
        }

        /// <summary>
        /// Gets all resource groups.
        /// </summary>
        /// <returns>All resource groups.</returns>
        public IResourceGroup[] GetAllResourceGroups()
        {
            int index = 0;
            IResourceGroup[] results = new IResourceGroup[m_ResourceGroups.Count];
            foreach (KeyValuePair<string, ResourceGroup> resourceGroup in m_ResourceGroups)
            {
                results[index++] = resourceGroup.Value;
            }

            return results;
        }

        /// <summary>
        /// Gets all resource groups.
        /// </summary>
        /// <param name="results">All resource groups.</param>
        public void GetAllResourceGroups(List<IResourceGroup> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<string, ResourceGroup> resourceGroup in m_ResourceGroups)
            {
                results.Add(resourceGroup.Value);
            }
        }

        /// <summary>
        /// Gets a resource group collection.
        /// </summary>
        /// <param name="resourceGroupNames">The collection of resource group names to get.</param>
        /// <returns>The resource group collection to get.</returns>
        public IResourceGroupCollection GetResourceGroupCollection(params string[] resourceGroupNames)
        {
            if (resourceGroupNames == null || resourceGroupNames.Length < 1)
            {
                throw new GameFrameworkException("Resource group names is invalid.");
            }

            ResourceGroup[] resourceGroups = new ResourceGroup[resourceGroupNames.Length];
            for (int i = 0; i < resourceGroupNames.Length; i++)
            {
                if (string.IsNullOrEmpty(resourceGroupNames[i]))
                {
                    throw new GameFrameworkException("Resource group name is invalid.");
                }

                resourceGroups[i] = (ResourceGroup)GetResourceGroup(resourceGroupNames[i]);
                if (resourceGroups[i] == null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Resource group '{0}' is not exist.", resourceGroupNames[i]));
                }
            }

            return new ResourceGroupCollection(resourceGroups, m_ResourceInfos);
        }

        /// <summary>
        /// Gets a resource group collection.
        /// </summary>
        /// <param name="resourceGroupNames">The collection of resource group names to get.</param>
        /// <returns>The resource group collection to get.</returns>
        public IResourceGroupCollection GetResourceGroupCollection(List<string> resourceGroupNames)
        {
            if (resourceGroupNames == null || resourceGroupNames.Count < 1)
            {
                throw new GameFrameworkException("Resource group names is invalid.");
            }

            ResourceGroup[] resourceGroups = new ResourceGroup[resourceGroupNames.Count];
            for (int i = 0; i < resourceGroupNames.Count; i++)
            {
                if (string.IsNullOrEmpty(resourceGroupNames[i]))
                {
                    throw new GameFrameworkException("Resource group name is invalid.");
                }

                resourceGroups[i] = (ResourceGroup)GetResourceGroup(resourceGroupNames[i]);
                if (resourceGroups[i] == null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Resource group '{0}' is not exist.", resourceGroupNames[i]));
                }
            }

            return new ResourceGroupCollection(resourceGroups, m_ResourceInfos);
        }

        private void UpdateResource(ResourceName resourceName)
        {
            m_ResourceUpdater.UpdateResource(resourceName);
        }

        private ResourceGroup GetOrAddResourceGroup(string resourceGroupName)
        {
            if (resourceGroupName == null)
            {
                resourceGroupName = string.Empty;
            }

            ResourceGroup resourceGroup = null;
            if (!m_ResourceGroups.TryGetValue(resourceGroupName, out resourceGroup))
            {
                resourceGroup = new ResourceGroup(resourceGroupName, m_ResourceInfos);
                m_ResourceGroups.Add(resourceGroupName, resourceGroup);
            }

            return resourceGroup;
        }

        private AssetInfo GetAssetInfo(string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                throw new GameFrameworkException("Asset name is invalid.");
            }

            if (m_AssetInfos == null)
            {
                return null;
            }

            AssetInfo assetInfo = null;
            if (m_AssetInfos.TryGetValue(assetName, out assetInfo))
            {
                return assetInfo;
            }

            return null;
        }

        private ResourceInfo GetResourceInfo(ResourceName resourceName)
        {
            if (m_ResourceInfos == null)
            {
                return null;
            }

            ResourceInfo resourceInfo = null;
            if (m_ResourceInfos.TryGetValue(resourceName, out resourceInfo))
            {
                return resourceInfo;
            }

            return null;
        }

        private IFileSystem GetFileSystem(string fileSystemName, bool storageInReadOnly)
        {
            if (string.IsNullOrEmpty(fileSystemName))
            {
                throw new GameFrameworkException("File system name is invalid.");
            }

            IFileSystem fileSystem = null;
            if (storageInReadOnly)
            {
                if (!m_ReadOnlyFileSystems.TryGetValue(fileSystemName, out fileSystem))
                {
                    string fullPath = Utility.Path.GetRegularPath(Path.Combine(m_ReadOnlyPath, Utility.Text.Format("{0}.{1}", fileSystemName, DefaultExtension)));
                    fileSystem = m_FileSystemManager.GetFileSystem(fullPath);
                    if (fileSystem == null)
                    {
                        fileSystem = m_FileSystemManager.LoadFileSystem(fullPath, FileSystemAccess.Read);
                        m_ReadOnlyFileSystems.Add(fileSystemName, fileSystem);
                    }
                }
            }
            else
            {
                if (!m_ReadWriteFileSystems.TryGetValue(fileSystemName, out fileSystem))
                {
                    string fullPath = Utility.Path.GetRegularPath(Path.Combine(m_ReadWritePath, Utility.Text.Format("{0}.{1}", fileSystemName, DefaultExtension)));
                    fileSystem = m_FileSystemManager.GetFileSystem(fullPath);
                    if (fileSystem == null)
                    {
                        if (File.Exists(fullPath))
                        {
                            fileSystem = m_FileSystemManager.LoadFileSystem(fullPath, FileSystemAccess.ReadWrite);
                        }
                        else
                        {
                            string directory = Path.GetDirectoryName(fullPath);
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }

                            fileSystem = m_FileSystemManager.CreateFileSystem(fullPath, FileSystemAccess.ReadWrite, FileSystemMaxFileCount, FileSystemMaxBlockCount);
                        }

                        m_ReadWriteFileSystems.Add(fileSystemName, fileSystem);
                    }
                }
            }

            return fileSystem;
        }

        private void PrepareCachedStream()
        {
            if (m_CachedStream == null)
            {
                m_CachedStream = new MemoryStream();
            }

            m_CachedStream.Position = 0L;
            m_CachedStream.SetLength(0L);
        }

        private void FreeCachedStream()
        {
            if (m_CachedStream != null)
            {
                m_CachedStream.Dispose();
                m_CachedStream = null;
            }
        }

        private void OnIniterResourceInitComplete()
        {
            m_ResourceIniter.ResourceInitComplete -= OnIniterResourceInitComplete;
            m_ResourceIniter.Shutdown();
            m_ResourceIniter = null;

            m_InitResourcesCompleteCallback();
            m_InitResourcesCompleteCallback = null;
        }

        private void OnVersionListProcessorUpdateSuccess(string downloadPath, string downloadUri)
        {
            m_UpdateVersionListCallbacks.UpdateVersionListSuccessCallback(downloadPath, downloadUri);
        }

        private void OnVersionListProcessorUpdateFailure(string downloadUri, string errorMessage)
        {
            if (m_UpdateVersionListCallbacks.UpdateVersionListFailureCallback != null)
            {
                m_UpdateVersionListCallbacks.UpdateVersionListFailureCallback(downloadUri, errorMessage);
            }
        }

        private void OnVerifierResourceVerifyStart(int count, long totalLength)
        {
            if (m_ResourceVerifyStartEventHandler != null)
            {
                ResourceVerifyStartEventArgs resourceVerifyStartEventArgs = ResourceVerifyStartEventArgs.Create(count, totalLength);
                m_ResourceVerifyStartEventHandler(this, resourceVerifyStartEventArgs);
                ReferencePool.Release(resourceVerifyStartEventArgs);
            }
        }

        private void OnVerifierResourceVerifySuccess(ResourceName resourceName, int length)
        {
            if (m_ResourceVerifySuccessEventHandler != null)
            {
                ResourceVerifySuccessEventArgs resourceVerifySuccessEventArgs = ResourceVerifySuccessEventArgs.Create(resourceName.FullName, length);
                m_ResourceVerifySuccessEventHandler(this, resourceVerifySuccessEventArgs);
                ReferencePool.Release(resourceVerifySuccessEventArgs);
            }
        }

        private void OnVerifierResourceVerifyFailure(ResourceName resourceName)
        {
            if (m_ResourceVerifyFailureEventHandler != null)
            {
                ResourceVerifyFailureEventArgs resourceVerifyFailureEventArgs = ResourceVerifyFailureEventArgs.Create(resourceName.FullName);
                m_ResourceVerifyFailureEventHandler(this, resourceVerifyFailureEventArgs);
                ReferencePool.Release(resourceVerifyFailureEventArgs);
            }
        }

        private void OnVerifierResourceVerifyComplete(bool result)
        {
            m_VerifyResourcesCompleteCallback(result);
            m_ResourceVerifier.ResourceVerifyStart -= OnVerifierResourceVerifyStart;
            m_ResourceVerifier.ResourceVerifySuccess -= OnVerifierResourceVerifySuccess;
            m_ResourceVerifier.ResourceVerifyFailure -= OnVerifierResourceVerifyFailure;
            m_ResourceVerifier.ResourceVerifyComplete -= OnVerifierResourceVerifyComplete;
            m_ResourceVerifier.Shutdown();
            m_ResourceVerifier = null;
        }

        private void OnCheckerResourceNeedUpdate(ResourceName resourceName, string fileSystemName, LoadType loadType, int length, int hashCode, int compressedLength, int compressedHashCode)
        {
            m_ResourceUpdater.AddResourceUpdate(resourceName, fileSystemName, loadType, length, hashCode, compressedLength, compressedHashCode, Utility.Path.GetRegularPath(Path.Combine(m_ReadWritePath, resourceName.FullName)));
        }

        private void OnCheckerResourceCheckComplete(int movedCount, int removedCount, int updateCount, long updateTotalLength, long updateTotalCompressedLength)
        {
            m_VersionListProcessor.VersionListUpdateSuccess -= OnVersionListProcessorUpdateSuccess;
            m_VersionListProcessor.VersionListUpdateFailure -= OnVersionListProcessorUpdateFailure;
            m_VersionListProcessor.Shutdown();
            m_VersionListProcessor = null;
            m_UpdateVersionListCallbacks = null;

            m_ResourceChecker.ResourceNeedUpdate -= OnCheckerResourceNeedUpdate;
            m_ResourceChecker.ResourceCheckComplete -= OnCheckerResourceCheckComplete;
            m_ResourceChecker.Shutdown();
            m_ResourceChecker = null;

            m_ResourceUpdater.CheckResourceComplete(movedCount > 0 || removedCount > 0);

            if (updateCount <= 0)
            {
                m_ResourceUpdater.ResourceApplyStart -= OnUpdaterResourceApplyStart;
                m_ResourceUpdater.ResourceApplySuccess -= OnUpdaterResourceApplySuccess;
                m_ResourceUpdater.ResourceApplyFailure -= OnUpdaterResourceApplyFailure;
                m_ResourceUpdater.ResourceApplyComplete -= OnUpdaterResourceApplyComplete;
                m_ResourceUpdater.ResourceUpdateStart -= OnUpdaterResourceUpdateStart;
                m_ResourceUpdater.ResourceUpdateChanged -= OnUpdaterResourceUpdateChanged;
                m_ResourceUpdater.ResourceUpdateSuccess -= OnUpdaterResourceUpdateSuccess;
                m_ResourceUpdater.ResourceUpdateFailure -= OnUpdaterResourceUpdateFailure;
                m_ResourceUpdater.ResourceUpdateComplete -= OnUpdaterResourceUpdateComplete;
                m_ResourceUpdater.ResourceUpdateAllComplete -= OnUpdaterResourceUpdateAllComplete;
                m_ResourceUpdater.Shutdown();
                m_ResourceUpdater = null;

                m_ReadWriteResourceInfos.Clear();
                m_ReadWriteResourceInfos = null;

                FreeCachedStream();
            }

            m_CheckResourcesCompleteCallback(movedCount, removedCount, updateCount, updateTotalLength, updateTotalCompressedLength);
            m_CheckResourcesCompleteCallback = null;
        }

        private void OnUpdaterResourceApplyStart(string resourcePackPath, int count, long totalLength)
        {
            if (m_ResourceApplyStartEventHandler != null)
            {
                ResourceApplyStartEventArgs resourceApplyStartEventArgs = ResourceApplyStartEventArgs.Create(resourcePackPath, count, totalLength);
                m_ResourceApplyStartEventHandler(this, resourceApplyStartEventArgs);
                ReferencePool.Release(resourceApplyStartEventArgs);
            }
        }

        private void OnUpdaterResourceApplySuccess(ResourceName resourceName, string applyPath, string resourcePackPath, int length, int compressedLength)
        {
            if (m_ResourceApplySuccessEventHandler != null)
            {
                ResourceApplySuccessEventArgs resourceApplySuccessEventArgs = ResourceApplySuccessEventArgs.Create(resourceName.FullName, applyPath, resourcePackPath, length, compressedLength);
                m_ResourceApplySuccessEventHandler(this, resourceApplySuccessEventArgs);
                ReferencePool.Release(resourceApplySuccessEventArgs);
            }
        }

        private void OnUpdaterResourceApplyFailure(ResourceName resourceName, string resourcePackPath, string errorMessage)
        {
            if (m_ResourceApplyFailureEventHandler != null)
            {
                ResourceApplyFailureEventArgs resourceApplyFailureEventArgs = ResourceApplyFailureEventArgs.Create(resourceName.FullName, resourcePackPath, errorMessage);
                m_ResourceApplyFailureEventHandler(this, resourceApplyFailureEventArgs);
                ReferencePool.Release(resourceApplyFailureEventArgs);
            }
        }

        private void OnUpdaterResourceApplyComplete(string resourcePackPath, bool result)
        {
            ApplyResourcesCompleteCallback applyResourcesCompleteCallback = m_ApplyResourcesCompleteCallback;
            m_ApplyResourcesCompleteCallback = null;
            applyResourcesCompleteCallback(resourcePackPath, result);
        }

        private void OnUpdaterResourceUpdateStart(ResourceName resourceName, string downloadPath, string downloadUri, int currentLength, int compressedLength, int retryCount)
        {
            if (m_ResourceUpdateStartEventHandler != null)
            {
                ResourceUpdateStartEventArgs resourceUpdateStartEventArgs = ResourceUpdateStartEventArgs.Create(resourceName.FullName, downloadPath, downloadUri, currentLength, compressedLength, retryCount);
                m_ResourceUpdateStartEventHandler(this, resourceUpdateStartEventArgs);
                ReferencePool.Release(resourceUpdateStartEventArgs);
            }
        }

        private void OnUpdaterResourceUpdateChanged(ResourceName resourceName, string downloadPath, string downloadUri, int currentLength, int compressedLength)
        {
            if (m_ResourceUpdateChangedEventHandler != null)
            {
                ResourceUpdateChangedEventArgs resourceUpdateChangedEventArgs = ResourceUpdateChangedEventArgs.Create(resourceName.FullName, downloadPath, downloadUri, currentLength, compressedLength);
                m_ResourceUpdateChangedEventHandler(this, resourceUpdateChangedEventArgs);
                ReferencePool.Release(resourceUpdateChangedEventArgs);
            }
        }

        private void OnUpdaterResourceUpdateSuccess(ResourceName resourceName, string downloadPath, string downloadUri, int length, int compressedLength)
        {
            if (m_ResourceUpdateSuccessEventHandler != null)
            {
                ResourceUpdateSuccessEventArgs resourceUpdateSuccessEventArgs = ResourceUpdateSuccessEventArgs.Create(resourceName.FullName, downloadPath, downloadUri, length, compressedLength);
                m_ResourceUpdateSuccessEventHandler(this, resourceUpdateSuccessEventArgs);
                ReferencePool.Release(resourceUpdateSuccessEventArgs);
            }
        }

        private void OnUpdaterResourceUpdateFailure(ResourceName resourceName, string downloadUri, int retryCount, int totalRetryCount, string errorMessage)
        {
            if (m_ResourceUpdateFailureEventHandler != null)
            {
                ResourceUpdateFailureEventArgs resourceUpdateFailureEventArgs = ResourceUpdateFailureEventArgs.Create(resourceName.FullName, downloadUri, retryCount, totalRetryCount, errorMessage);
                m_ResourceUpdateFailureEventHandler(this, resourceUpdateFailureEventArgs);
                ReferencePool.Release(resourceUpdateFailureEventArgs);
            }
        }

        private void OnUpdaterResourceUpdateComplete(ResourceGroup resourceGroup, bool result)
        {
            Utility.Path.RemoveEmptyDirectory(m_ReadWritePath);
            UpdateResourcesCompleteCallback updateResourcesCompleteCallback = m_UpdateResourcesCompleteCallback;
            m_UpdateResourcesCompleteCallback = null;
            updateResourcesCompleteCallback(resourceGroup, result);
        }

        private void OnUpdaterResourceUpdateAllComplete()
        {
            m_ResourceUpdater.ResourceApplyStart -= OnUpdaterResourceApplyStart;
            m_ResourceUpdater.ResourceApplySuccess -= OnUpdaterResourceApplySuccess;
            m_ResourceUpdater.ResourceApplyFailure -= OnUpdaterResourceApplyFailure;
            m_ResourceUpdater.ResourceApplyComplete -= OnUpdaterResourceApplyComplete;
            m_ResourceUpdater.ResourceUpdateStart -= OnUpdaterResourceUpdateStart;
            m_ResourceUpdater.ResourceUpdateChanged -= OnUpdaterResourceUpdateChanged;
            m_ResourceUpdater.ResourceUpdateSuccess -= OnUpdaterResourceUpdateSuccess;
            m_ResourceUpdater.ResourceUpdateFailure -= OnUpdaterResourceUpdateFailure;
            m_ResourceUpdater.ResourceUpdateComplete -= OnUpdaterResourceUpdateComplete;
            m_ResourceUpdater.ResourceUpdateAllComplete -= OnUpdaterResourceUpdateAllComplete;
            m_ResourceUpdater.Shutdown();
            m_ResourceUpdater = null;

            m_ReadWriteResourceInfos.Clear();
            m_ReadWriteResourceInfos = null;

            FreeCachedStream();
            Utility.Path.RemoveEmptyDirectory(m_ReadWritePath);

            if (m_ResourceUpdateAllCompleteEventHandler != null)
            {
                ResourceUpdateAllCompleteEventArgs resourceUpdateAllCompleteEventArgs = ResourceUpdateAllCompleteEventArgs.Create();
                m_ResourceUpdateAllCompleteEventHandler(this, resourceUpdateAllCompleteEventArgs);
                ReferencePool.Release(resourceUpdateAllCompleteEventArgs);
            }
        }
    }
}
