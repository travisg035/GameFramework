//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Package version list serializer.
    /// </summary>
    public sealed class PackageVersionListSerializer : GameFrameworkSerializer<PackageVersionList>
    {
        private static readonly byte[] Header = new byte[] { (byte)'G', (byte)'F', (byte)'P' };

        /// <summary>
        /// Initializes a new instance of the PackageVersionListSerializer class.
        /// </summary>
        public PackageVersionListSerializer()
        {
        }

        /// <summary>
        /// Gets the package version list header.
        /// </summary>
        /// <returns>Package version list header.</returns>
        protected override byte[] GetHeader()
        {
            return Header;
        }
    }
}
