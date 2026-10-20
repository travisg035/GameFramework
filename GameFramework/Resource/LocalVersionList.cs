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
    /// Local version list.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public partial struct LocalVersionList
    {
        private static readonly Resource[] EmptyResourceArray = new Resource[] { };
        private static readonly FileSystem[] EmptyFileSystemArray = new FileSystem[] { };

        private readonly bool m_IsValid;
        private readonly Resource[] m_Resources;
        private readonly FileSystem[] m_FileSystems;

        /// <summary>
        /// Initializes a new instance of the LocalVersionList struct.
        /// </summary>
        /// <param name="resources">Included resources.</param>
        /// <param name="fileSystems">Included file systems.</param>
        public LocalVersionList(Resource[] resources, FileSystem[] fileSystems)
        {
            m_IsValid = true;
            m_Resources = resources ?? EmptyResourceArray;
            m_FileSystems = fileSystems ?? EmptyFileSystemArray;
        }

        /// <summary>
        /// Gets whether the local version list is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return m_IsValid;
            }
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
    }
}
