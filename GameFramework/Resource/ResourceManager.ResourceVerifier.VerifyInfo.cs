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
        private sealed partial class ResourceVerifier
        {
            /// <summary>
            /// Resource verification information.
            /// </summary>
            private struct VerifyInfo
            {
                private readonly ResourceName m_ResourceName;
                private readonly string m_FileSystemName;
                private readonly LoadType m_LoadType;
                private readonly int m_Length;
                private readonly int m_HashCode;

                /// <summary>
                /// Initializes a new instance of the VerifyInfo struct.
                /// </summary>
                /// <param name="resourceName">The resource name.</param>
                /// <param name="fileSystemName">The file system name containing the resource.</param>
                /// <param name="loadType">The resource load type.</param>
                /// <param name="length">The resource length.</param>
                /// <param name="hashCode">The resource hash code.</param>
                public VerifyInfo(ResourceName resourceName, string fileSystemName, LoadType loadType, int length, int hashCode)
                {
                    m_ResourceName = resourceName;
                    m_FileSystemName = fileSystemName;
                    m_LoadType = loadType;
                    m_Length = length;
                    m_HashCode = hashCode;
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
            }
        }
    }
}
