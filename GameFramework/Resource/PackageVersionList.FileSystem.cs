//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Runtime.InteropServices;

namespace GameFramework.Resource
{
    public partial struct PackageVersionList
    {
        /// <summary>
        /// File system.
        /// </summary>
        [StructLayout(LayoutKind.Auto)]
        public struct FileSystem
        {
            private static readonly int[] EmptyIntArray = new int[] { };

            private readonly string m_Name;
            private readonly int[] m_ResourceIndexes;

            /// <summary>
            /// Initializes a new instance of the FileSystem struct.
            /// </summary>
            /// <param name="name">File system name.</param>
            /// <param name="resourceIndexes">Resource indexes included in the file system.</param>
            public FileSystem(string name, int[] resourceIndexes)
            {
                if (name == null)
                {
                    throw new GameFrameworkException("Name is invalid.");
                }

                m_Name = name;
                m_ResourceIndexes = resourceIndexes ?? EmptyIntArray;
            }

            /// <summary>
            /// Gets the file system name.
            /// </summary>
            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            /// <summary>
            /// Gets the resource indexes included in the file system.
            /// </summary>
            /// <returns>Resource indexes included in the file system.</returns>
            public int[] GetResourceIndexes()
            {
                return m_ResourceIndexes;
            }
        }
    }
}
