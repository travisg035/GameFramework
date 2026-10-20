//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    internal sealed partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        private sealed partial class ResourceChecker
        {
            /// <summary>
            /// Resource check information.
            /// </summary>
            private sealed partial class CheckInfo
            {
                private readonly ResourceName m_ResourceName;
                private CheckStatus m_Status;
                private bool m_NeedRemove;
                private bool m_NeedMoveToDisk;
                private bool m_NeedMoveToFileSystem;
                private RemoteVersionInfo m_VersionInfo;
                private LocalVersionInfo m_ReadOnlyInfo;
                private LocalVersionInfo m_ReadWriteInfo;
                private string m_CachedFileSystemName;

                /// <summary>
                /// Initializes a new instance of the CheckInfo class.
                /// </summary>
                /// <param name="resourceName">The resource name.</param>
                public CheckInfo(ResourceName resourceName)
                {
                    m_ResourceName = resourceName;
                    m_Status = CheckStatus.Unknown;
                    m_NeedRemove = false;
                    m_NeedMoveToDisk = false;
                    m_NeedMoveToFileSystem = false;
                    m_VersionInfo = default(RemoteVersionInfo);
                    m_ReadOnlyInfo = default(LocalVersionInfo);
                    m_ReadWriteInfo = default(LocalVersionInfo);
                    m_CachedFileSystemName = null;
                }

                /// <summary>
                /// Gets the resource name.
                /// </summary>
                public ResourceName ResourceName
                {
                    get
                    {
                        return m_ResourceName;
                    }
                }

                /// <summary>
                /// Gets the resource check status.
                /// </summary>
                public CheckStatus Status
                {
                    get
                    {
                        return m_Status;
                    }
                }

                /// <summary>
                /// Gets a value indicating whether the resource in the read-write area needs to be removed.
                /// </summary>
                public bool NeedRemove
                {
                    get
                    {
                        return m_NeedRemove;
                    }
                }

                /// <summary>
                /// Gets a value indicating whether the resource in the read-write area needs to be moved to disk.
                /// </summary>
                public bool NeedMoveToDisk
                {
                    get
                    {
                        return m_NeedMoveToDisk;
                    }
                }

                /// <summary>
                /// Gets a value indicating whether the resource in the read-write area needs to be moved to the file system.
                /// </summary>
                public bool NeedMoveToFileSystem
                {
                    get
                    {
                        return m_NeedMoveToFileSystem;
                    }
                }

                /// <summary>
                /// Gets the file system name containing the resource.
                /// </summary>
                public string FileSystemName
                {
                    get
                    {
                        return m_VersionInfo.FileSystemName;
                    }
                }

                /// <summary>
                /// Gets a value indicating whether the read-write resource uses the file system.
                /// </summary>
                public bool ReadWriteUseFileSystem
                {
                    get
                    {
                        return m_ReadWriteInfo.UseFileSystem;
                    }
                }

                /// <summary>
                /// Gets the file system name containing the read-write resource.
                /// </summary>
                public string ReadWriteFileSystemName
                {
                    get
                    {
                        return m_ReadWriteInfo.FileSystemName;
                    }
                }

                /// <summary>
                /// Gets the resource load type.
                /// </summary>
                public LoadType LoadType
                {
                    get
                    {
                        return m_VersionInfo.LoadType;
                    }
                }

                /// <summary>
                /// Gets the resource length.
                /// </summary>
                public int Length
                {
                    get
                    {
                        return m_VersionInfo.Length;
                    }
                }

                /// <summary>
                /// Gets the resource hash code.
                /// </summary>
                public int HashCode
                {
                    get
                    {
                        return m_VersionInfo.HashCode;
                    }
                }

                /// <summary>
                /// Gets the compressed resource length.
                /// </summary>
                public int CompressedLength
                {
                    get
                    {
                        return m_VersionInfo.CompressedLength;
                    }
                }

                /// <summary>
                /// Gets the compressed resource hash code.
                /// </summary>
                public int CompressedHashCode
                {
                    get
                    {
                        return m_VersionInfo.CompressedHashCode;
                    }
                }

                /// <summary>
                /// Temporarily caches the file system name containing the resource.
                /// </summary>
                /// <param name="fileSystemName">The file system name containing the resource.</param>
                public void SetCachedFileSystemName(string fileSystemName)
                {
                    m_CachedFileSystemName = fileSystemName;
                }

                /// <summary>
                /// Sets the resource version information.
                /// </summary>
                /// <param name="loadType">The resource load type.</param>
                /// <param name="length">The resource length.</param>
                /// <param name="hashCode">The resource hash code.</param>
                /// <param name="compressedLength">The compressed resource length.</param>
                /// <param name="compressedHashCode">The compressed resource hash code.</param>
                public void SetVersionInfo(LoadType loadType, int length, int hashCode, int compressedLength, int compressedHashCode)
                {
                    if (m_VersionInfo.Exist)
                    {
                        throw new GameFrameworkException(Utility.Text.Format("You must set version info of '{0}' only once.", m_ResourceName.FullName));
                    }

                    m_VersionInfo = new RemoteVersionInfo(m_CachedFileSystemName, loadType, length, hashCode, compressedLength, compressedHashCode);
                    m_CachedFileSystemName = null;
                }

                /// <summary>
                /// Sets the resource information in the read-only area.
                /// </summary>
                /// <param name="loadType">The resource load type.</param>
                /// <param name="length">The resource length.</param>
                /// <param name="hashCode">The resource hash code.</param>
                public void SetReadOnlyInfo(LoadType loadType, int length, int hashCode)
                {
                    if (m_ReadOnlyInfo.Exist)
                    {
                        throw new GameFrameworkException(Utility.Text.Format("You must set read-only info of '{0}' only once.", m_ResourceName.FullName));
                    }

                    m_ReadOnlyInfo = new LocalVersionInfo(m_CachedFileSystemName, loadType, length, hashCode);
                    m_CachedFileSystemName = null;
                }

                /// <summary>
                /// Sets the resource information in the read-write area.
                /// </summary>
                /// <param name="loadType">The resource load type.</param>
                /// <param name="length">The resource length.</param>
                /// <param name="hashCode">The resource hash code.</param>
                public void SetReadWriteInfo(LoadType loadType, int length, int hashCode)
                {
                    if (m_ReadWriteInfo.Exist)
                    {
                        throw new GameFrameworkException(Utility.Text.Format("You must set read-write info of '{0}' only once.", m_ResourceName.FullName));
                    }

                    m_ReadWriteInfo = new LocalVersionInfo(m_CachedFileSystemName, loadType, length, hashCode);
                    m_CachedFileSystemName = null;
                }

                /// <summary>
                /// Refreshes the resource status.
                /// </summary>
                /// <param name="currentVariant">The current variant.</param>
                /// <param name="ignoreOtherVariant">Whether to ignore resources of other variants; if not ignored, they will be removed.</param>
                public void RefreshStatus(string currentVariant, bool ignoreOtherVariant)
                {
                    if (!m_VersionInfo.Exist)
                    {
                        m_Status = CheckStatus.Disuse;
                        m_NeedRemove = m_ReadWriteInfo.Exist;
                        return;
                    }

                    if (m_ResourceName.Variant == null || m_ResourceName.Variant == currentVariant)
                    {
                        if (m_ReadOnlyInfo.Exist && m_ReadOnlyInfo.FileSystemName == m_VersionInfo.FileSystemName && m_ReadOnlyInfo.LoadType == m_VersionInfo.LoadType && m_ReadOnlyInfo.Length == m_VersionInfo.Length && m_ReadOnlyInfo.HashCode == m_VersionInfo.HashCode)
                        {
                            m_Status = CheckStatus.StorageInReadOnly;
                            m_NeedRemove = m_ReadWriteInfo.Exist;
                        }
                        else if (m_ReadWriteInfo.Exist && m_ReadWriteInfo.LoadType == m_VersionInfo.LoadType && m_ReadWriteInfo.Length == m_VersionInfo.Length && m_ReadWriteInfo.HashCode == m_VersionInfo.HashCode)
                        {
                            bool differentFileSystem = m_ReadWriteInfo.FileSystemName != m_VersionInfo.FileSystemName;
                            m_Status = CheckStatus.StorageInReadWrite;
                            m_NeedMoveToDisk = m_ReadWriteInfo.UseFileSystem && differentFileSystem;
                            m_NeedMoveToFileSystem = m_VersionInfo.UseFileSystem && differentFileSystem;
                        }
                        else
                        {
                            m_Status = CheckStatus.Update;
                            m_NeedRemove = m_ReadWriteInfo.Exist;
                        }
                    }
                    else
                    {
                        m_Status = CheckStatus.Unavailable;
                        m_NeedRemove = !ignoreOtherVariant && m_ReadWriteInfo.Exist;
                    }
                }
            }
        }
    }
}
