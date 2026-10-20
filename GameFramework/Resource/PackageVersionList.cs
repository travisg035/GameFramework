//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Runtime.InteropServices;

namespace GameFramework.Resource
{
    /// <summary>
    /// Package version list.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public partial struct PackageVersionList
    {
        private static readonly Asset[] EmptyAssetArray = new Asset[] { };
        private static readonly Resource[] EmptyResourceArray = new Resource[] { };
        private static readonly FileSystem[] EmptyFileSystemArray = new FileSystem[] { };
        private static readonly ResourceGroup[] EmptyResourceGroupArray = new ResourceGroup[] { };

        private readonly bool m_IsValid;
        private readonly string m_ApplicableGameVersion;
        private readonly int m_InternalResourceVersion;
        private readonly Asset[] m_Assets;
        private readonly Resource[] m_Resources;
        private readonly FileSystem[] m_FileSystems;
        private readonly ResourceGroup[] m_ResourceGroups;

        /// <summary>
        /// Initializes a new instance of the PackageVersionList struct.
        /// </summary>
        /// <param name="applicableGameVersion">Applicable game version.</param>
        /// <param name="internalResourceVersion">Internal resource version.</param>
        /// <param name="assets">Included assets.</param>
        /// <param name="resources">Included resources.</param>
        /// <param name="fileSystems">Included file systems.</param>
        /// <param name="resourceGroups">Included resource groups.</param>
        public PackageVersionList(string applicableGameVersion, int internalResourceVersion, Asset[] assets, Resource[] resources, FileSystem[] fileSystems, ResourceGroup[] resourceGroups)
        {
            m_IsValid = true;
            m_ApplicableGameVersion = applicableGameVersion;
            m_InternalResourceVersion = internalResourceVersion;
            m_Assets = assets ?? EmptyAssetArray;
            m_Resources = resources ?? EmptyResourceArray;
            m_FileSystems = fileSystems ?? EmptyFileSystemArray;
            m_ResourceGroups = resourceGroups ?? EmptyResourceGroupArray;
        }

        /// <summary>
        /// Gets whether the package version list is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return m_IsValid;
            }
        }

        /// <summary>
        /// Gets the applicable game version.
        /// </summary>
        public string ApplicableGameVersion
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_ApplicableGameVersion;
            }
        }

        /// <summary>
        /// Gets the internal resource version.
        /// </summary>
        public int InternalResourceVersion
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_InternalResourceVersion;
            }
        }

        /// <summary>
        /// Gets the included assets.
        /// </summary>
        /// <returns>Included assets.</returns>
        public Asset[] GetAssets()
        {
            if (!m_IsValid)
            {
                throw new GameFrameworkException("Data is invalid.");
            }

            return m_Assets;
        }

        /// <summary>
        /// Gets the included resources.
        /// </summary>
        /// <returns>Included resources.</returns>
        public Resource[] GetResources()
        {
            if (!m_IsValid)
            {
                throw new GameFrameworkException("Data is invalid.");
            }

            return m_Resources;
        }

        /// <summary>
        /// Gets the included file systems.
        /// </summary>
        /// <returns>Included file systems.</returns>
        public FileSystem[] GetFileSystems()
        {
            if (!m_IsValid)
            {
                throw new GameFrameworkException("Data is invalid.");
            }

            return m_FileSystems;
        }

        /// <summary>
        /// Gets the included resource groups.
        /// </summary>
        /// <returns>Included resource groups.</returns>
        public ResourceGroup[] GetResourceGroups()
        {
            if (!m_IsValid)
            {
                throw new GameFrameworkException("Data is invalid.");
            }

            return m_ResourceGroups;
        }
    }
}
