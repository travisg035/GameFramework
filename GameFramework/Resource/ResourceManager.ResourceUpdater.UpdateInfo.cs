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
        private sealed partial class ResourceUpdater
        {
            /// <summary>
            /// Resource update information.
            /// </summary>
            private sealed class UpdateInfo
            {
                private readonly ResourceName m_ResourceName;
                private readonly string m_FileSystemName;
                private readonly LoadType m_LoadType;
                private readonly int m_Length;
                private readonly int m_HashCode;
                private readonly int m_CompressedLength;
                private readonly int m_CompressedHashCode;
                private readonly string m_ResourcePath;
                private bool m_Downloading;
                private int m_RetryCount;

                /// <summary>
                /// Initializes a new instance of the UpdateInfo class.
                /// </summary>
                /// <param name="resourceName">The resource name.</param>
                /// <param name="fileSystemName">The file system name containing the resource.</param>
                /// <param name="loadType">The resource load type.</param>
                /// <param name="length">The resource length.</param>
                /// <param name="hashCode">The resource hash code.</param>
                /// <param name="compressedLength">The compressed resource length.</param>
                /// <param name="compressedHashCode">The compressed resource hash code.</param>
                /// <param name="resourcePath">The resource path.</param>
                public UpdateInfo(ResourceName resourceName, string fileSystemName, LoadType loadType, int length, int hashCode, int compressedLength, int compressedHashCode, string resourcePath)
                {
                    m_ResourceName = resourceName;
                    m_FileSystemName = fileSystemName;
                    m_LoadType = loadType;
                    m_Length = length;
                    m_HashCode = hashCode;
                    m_CompressedLength = compressedLength;
                    m_CompressedHashCode = compressedHashCode;
                    m_ResourcePath = resourcePath;
                    m_Downloading = false;
                    m_RetryCount = 0;
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
                /// Gets a value indicating whether the resource uses the file system.
                /// </summary>
                public bool UseFileSystem
                {
                    get
                    {
                        return !string.IsNullOrEmpty(m_FileSystemName);
                    }
                }

                /// <summary>
                /// Gets the file system name containing the resource.
                /// </summary>
                public string FileSystemName
                {
                    get
                    {
                        return m_FileSystemName;
                    }
                }

                /// <summary>
                /// Gets the resource load type.
                /// </summary>
                public LoadType LoadType
                {
                    get
                    {
                        return m_LoadType;
                    }
                }

                /// <summary>
                /// Gets the resource length.
                /// </summary>
                public int Length
                {
                    get
                    {
                        return m_Length;
                    }
                }

                /// <summary>
                /// Gets the resource hash code.
                /// </summary>
                public int HashCode
                {
                    get
                    {
                        return m_HashCode;
                    }
                }

                /// <summary>
                /// Gets the compressed resource length.
                /// </summary>
                public int CompressedLength
                {
                    get
                    {
                        return m_CompressedLength;
                    }
                }

                /// <summary>
                /// Gets the compressed resource hash code.
                /// </summary>
                public int CompressedHashCode
                {
                    get
                    {
                        return m_CompressedHashCode;
                    }
                }

                /// <summary>
                /// Gets the resource path.
                /// </summary>
                public string ResourcePath
                {
                    get
                    {
                        return m_ResourcePath;
                    }
                }

                /// <summary>
                /// Gets or sets a value indicating whether downloading is in progress.
                /// </summary>
                public bool Downloading
                {
                    get
                    {
                        return m_Downloading;
                    }
                    set
                    {
                        m_Downloading = value;
                    }
                }

                /// <summary>
                /// Gets or sets the retry count.
                /// </summary>
                public int RetryCount
                {
                    get
                    {
                        return m_RetryCount;
                    }
                    set
                    {
                        m_RetryCount = value;
                    }
                }
            }
        }
    }
}
