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
        /// <summary>
        /// Resource information.
        /// </summary>
        private sealed class ResourceInfo
        {
            private readonly ResourceName m_ResourceName;
            private readonly string m_FileSystemName;
            private readonly LoadType m_LoadType;
            private readonly int m_Length;
            private readonly int m_HashCode;
            private readonly int m_CompressedLength;
            private readonly bool m_StorageInReadOnly;
            private bool m_Ready;

            /// <summary>
            /// Initializes a new instance of the ResourceInfo class.
            /// </summary>
            /// <param name="resourceName">The resource name.</param>
            /// <param name="fileSystemName">The file system name.</param>
            /// <param name="loadType">The resource load type.</param>
            /// <param name="length">The resource length.</param>
            /// <param name="hashCode">The resource hash code.</param>
            /// <param name="compressedLength">The compressed resource length.</param>
            /// <param name="storageInReadOnly">Whether the resource is stored in the read-only area.</param>
            /// <param name="ready">Whether the resource is ready.</param>
            public ResourceInfo(ResourceName resourceName, string fileSystemName, LoadType loadType, int length, int hashCode, int compressedLength, bool storageInReadOnly, bool ready)
            {
                m_ResourceName = resourceName;
                m_FileSystemName = fileSystemName;
                m_LoadType = loadType;
                m_Length = length;
                m_HashCode = hashCode;
                m_CompressedLength = compressedLength;
                m_StorageInReadOnly = storageInReadOnly;
                m_Ready = ready;
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
            /// Gets the file system name.
            /// </summary>
            public string FileSystemName
            {
                get
                {
                    return m_FileSystemName;
                }
            }

            /// <summary>
            /// Gets a value indicating whether the resource is loaded from binary.
            /// </summary>
            public bool IsLoadFromBinary
            {
                get
                {
                    return m_LoadType == LoadType.LoadFromBinary || m_LoadType == LoadType.LoadFromBinaryAndQuickDecrypt || m_LoadType == LoadType.LoadFromBinaryAndDecrypt;
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
            /// Gets a value indicating whether the resource is stored in the read-only area.
            /// </summary>
            public bool StorageInReadOnly
            {
                get
                {
                    return m_StorageInReadOnly;
                }
            }

            /// <summary>
            /// Gets a value indicating whether the resource is ready.
            /// </summary>
            public bool Ready
            {
                get
                {
                    return m_Ready;
                }
            }

            /// <summary>
            /// Marks the resource as ready.
            /// </summary>
            public void MarkReady()
            {
                m_Ready = true;
            }
        }
    }
}
