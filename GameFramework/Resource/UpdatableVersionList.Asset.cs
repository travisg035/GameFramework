//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Runtime.InteropServices;

namespace GameFramework.Resource
{
    public partial struct UpdatableVersionList
    {
        /// <summary>
        /// Asset.
        /// </summary>
        [StructLayout(LayoutKind.Auto)]
        public struct Asset
        {
            private static readonly int[] EmptyIntArray = new int[] { };

            private readonly string m_Name;
            private readonly int[] m_DependencyAssetIndexes;

            /// <summary>
            /// Initializes a new instance of the Asset struct.
            /// </summary>
            /// <param name="name">Asset name.</param>
            /// <param name="dependencyAssetIndexes">Dependency asset indexes included in the asset.</param>
            public Asset(string name, int[] dependencyAssetIndexes)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new GameFrameworkException("Name is invalid.");
                }

                m_Name = name;
                m_DependencyAssetIndexes = dependencyAssetIndexes ?? EmptyIntArray;
            }

            /// <summary>
            /// Gets the asset name.
            /// </summary>
            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            /// <summary>
            /// Gets the dependency asset indexes included in the asset.
            /// </summary>
            /// <returns>Dependency asset indexes included in the asset.</returns>
            public int[] GetDependencyAssetIndexes()
            {
                return m_DependencyAssetIndexes;
            }
        }
    }
}
