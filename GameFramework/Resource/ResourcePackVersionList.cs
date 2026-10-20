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
    /// Resource pack version list.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public partial struct ResourcePackVersionList
    {
        private static readonly Resource[] EmptyResourceArray = new Resource[] { };

        private readonly bool m_IsValid;
        private readonly int m_Offset;
        private readonly long m_Length;
        private readonly int m_HashCode;
        private readonly Resource[] m_Resources;

        /// <summary>
        /// Initializes a new instance of the ResourcePackVersionList struct.
        /// </summary>
        /// <param name="offset">Resource data offset.</param>
        /// <param name="length">Resource data length.</param>
        /// <param name="hashCode">Resource data hash code.</param>
        /// <param name="resources">Included resources.</param>
        public ResourcePackVersionList(int offset, long length, int hashCode, Resource[] resources)
        {
            m_IsValid = true;
            m_Offset = offset;
            m_Length = length;
            m_HashCode = hashCode;
            m_Resources = resources ?? EmptyResourceArray;
        }

        /// <summary>
        /// Gets whether the resource pack version list is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return m_IsValid;
            }
        }

        /// <summary>
        /// Gets the resource data offset.
        /// </summary>
        public int Offset
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Offset;
            }
        }

        /// <summary>
        /// Gets the resource data length.
        /// </summary>
        public long Length
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Length;
            }
        }

        /// <summary>
        /// Gets the resource data hash code.
        /// </summary>
        public int HashCode
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_HashCode;
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
    }
}
