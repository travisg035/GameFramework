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
        /// Asset information.
        /// </summary>
        private sealed class AssetInfo
        {
            private readonly string m_AssetName;
            private readonly ResourceName m_ResourceName;
            private readonly string[] m_DependencyAssetNames;

            /// <summary>
            /// Initializes a new instance of the AssetInfo class.
            /// </summary>
            /// <param name="assetName">The asset name.</param>
            /// <param name="resourceName">The resource name containing the asset.</param>
            /// <param name="dependencyAssetNames">The dependency asset names.</param>
            public AssetInfo(string assetName, ResourceName resourceName, string[] dependencyAssetNames)
            {
                m_AssetName = assetName;
                m_ResourceName = resourceName;
                m_DependencyAssetNames = dependencyAssetNames;
            }

            /// <summary>
            /// Gets the asset name.
            /// </summary>
            public string AssetName
            {
                get
                {
                    return m_AssetName;
                }
            }

            /// <summary>
            /// Gets the resource name containing the asset.
            /// </summary>
            public ResourceName ResourceName
            {
                get
                {
                    return m_ResourceName;
                }
            }

            /// <summary>
            /// Gets the dependency asset names.
            /// </summary>
            /// <returns>The dependency asset names.</returns>
            public string[] GetDependencyAssetNames()
            {
                return m_DependencyAssetNames;
            }
        }
    }
}
