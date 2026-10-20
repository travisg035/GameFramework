//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.Resource
{
    internal sealed partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        /// <summary>
        /// Resource group.
        /// </summary>
        private sealed class ResourceGroup : IResourceGroup
        {
            private readonly string m_Name;
            private readonly Dictionary<ResourceName, ResourceInfo> m_ResourceInfos;
            private readonly HashSet<ResourceName> m_ResourceNames;
            private long m_TotalLength;
            private long m_TotalCompressedLength;

            /// <summary>
            /// Initializes a new instance of the ResourceGroup class.
            /// </summary>
            /// <param name="name">The resource group name.</param>
            /// <param name="resourceInfos">Resource information reference.</param>
            public ResourceGroup(string name, Dictionary<ResourceName, ResourceInfo> resourceInfos)
            {
                if (name == null)
                {
                    throw new GameFrameworkException("Name is invalid.");
                }

                if (resourceInfos == null)
                {
                    throw new GameFrameworkException("Resource infos is invalid.");
                }

                m_Name = name;
                m_ResourceInfos = resourceInfos;
                m_ResourceNames = new HashSet<ResourceName>();
            }

            /// <summary>
            /// Gets the resource group name.
            /// </summary>
            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            /// <summary>
            /// Gets a value indicating whether the resource group is ready.
            /// </summary>
            public bool Ready
            {
                get
                {
                    return ReadyCount >= TotalCount;
                }
            }

            /// <summary>
            /// Gets the total number of resources in the resource group.
            /// </summary>
            public int TotalCount
            {
                get
                {
                    return m_ResourceNames.Count;
                }
            }

            /// <summary>
            /// Gets the number of ready resources in the resource group.
            /// </summary>
            public int ReadyCount
            {
                get
                {
                    int readyCount = 0;
                    foreach (ResourceName resourceName in m_ResourceNames)
                    {
                        ResourceInfo resourceInfo = null;
                        if (m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) && resourceInfo.Ready)
                        {
                            readyCount++;
                        }
                    }

                    return readyCount;
                }
            }

            /// <summary>
            /// Gets the total length of resources in the resource group.
            /// </summary>
            public long TotalLength
            {
                get
                {
                    return m_TotalLength;
                }
            }

            /// <summary>
            /// Gets the total compressed length of resources in the resource group.
            /// </summary>
            public long TotalCompressedLength
            {
                get
                {
                    return m_TotalCompressedLength;
                }
            }

            /// <summary>
            /// Gets the total length of ready resources in the resource group.
            /// </summary>
            public long ReadyLength
            {
                get
                {
                    long readyLength = 0L;
                    foreach (ResourceName resourceName in m_ResourceNames)
                    {
                        ResourceInfo resourceInfo = null;
                        if (m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) && resourceInfo.Ready)
                        {
                            readyLength += resourceInfo.Length;
                        }
                    }

                    return readyLength;
                }
            }

            /// <summary>
            /// Gets the total compressed length of ready resources in the resource group.
            /// </summary>
            public long ReadyCompressedLength
            {
                get
                {
                    long readyCompressedLength = 0L;
                    foreach (ResourceName resourceName in m_ResourceNames)
                    {
                        ResourceInfo resourceInfo = null;
                        if (m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) && resourceInfo.Ready)
                        {
                            readyCompressedLength += resourceInfo.CompressedLength;
                        }
                    }

                    return readyCompressedLength;
                }
            }

            /// <summary>
            /// Gets the progress of the resource group.
            /// </summary>
            public float Progress
            {
                get
                {
                    return m_TotalLength > 0L ? (float)ReadyLength / m_TotalLength : 1f;
                }
            }

            /// <summary>
            /// Gets the resource names in the resource group.
            /// </summary>
            /// <returns>The resource names in the resource group.</returns>
            public string[] GetResourceNames()
            {
                int index = 0;
                string[] resourceNames = new string[m_ResourceNames.Count];
                foreach (ResourceName resourceName in m_ResourceNames)
                {
                    resourceNames[index++] = resourceName.FullName;
                }

                return resourceNames;
            }

            /// <summary>
            /// Gets the resource names in the resource group.
            /// </summary>
            /// <param name="results">The resource names in the resource group.</param>
            public void GetResourceNames(List<string> results)
            {
                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (ResourceName resourceName in m_ResourceNames)
                {
                    results.Add(resourceName.FullName);
                }
            }

            /// <summary>
            /// Gets the resource names in the resource group.
            /// </summary>
            /// <returns>The resource names in the resource group.</returns>
            public ResourceName[] InternalGetResourceNames()
            {
                int index = 0;
                ResourceName[] resourceNames = new ResourceName[m_ResourceNames.Count];
                foreach (ResourceName resourceName in m_ResourceNames)
                {
                    resourceNames[index++] = resourceName;
                }

                return resourceNames;
            }

            /// <summary>
            /// Gets the resource names in the resource group.
            /// </summary>
            /// <param name="results">The resource names in the resource group.</param>
            public void InternalGetResourceNames(List<ResourceName> results)
            {
                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (ResourceName resourceName in m_ResourceNames)
                {
                    results.Add(resourceName);
                }
            }

            /// <summary>
            /// Determines whether the specified resource belongs to the resource group.
            /// </summary>
            /// <param name="resourceName">The name of the resource to check.</param>
            /// <returns>Whether the specified resource belongs to the resource group.</returns>
            public bool HasResource(ResourceName resourceName)
            {
                return m_ResourceNames.Contains(resourceName);
            }

            /// <summary>
            /// Adds a resource to the resource group.
            /// </summary>
            /// <param name="resourceName">The resource name.</param>
            /// <param name="length">The resource length.</param>
            /// <param name="compressedLength">The compressed resource length.</param>
            public void AddResource(ResourceName resourceName, int length, int compressedLength)
            {
                m_ResourceNames.Add(resourceName);
                m_TotalLength += length;
                m_TotalCompressedLength += compressedLength;
            }
        }
    }
}
