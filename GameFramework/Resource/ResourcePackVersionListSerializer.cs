//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource pack version list serializer.
    /// </summary>
    public sealed class ResourcePackVersionListSerializer : GameFrameworkSerializer<ResourcePackVersionList>
    {
        private static readonly byte[] Header = new byte[] { (byte)'G', (byte)'F', (byte)'K' };

        /// <summary>
        /// Initializes a new instance of the ResourcePackVersionListSerializer class.
        /// </summary>
        public ResourcePackVersionListSerializer()
        {
        }

        /// <summary>
        /// Gets the resource pack version list header.
        /// </summary>
        /// <returns>Resource pack version list header.</returns>
        protected override byte[] GetHeader()
        {
            return Header;
        }
    }
}
