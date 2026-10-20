//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Read-only version list serializer.
    /// </summary>
    public sealed class ReadOnlyVersionListSerializer : GameFrameworkSerializer<LocalVersionList>
    {
        private static readonly byte[] Header = new byte[] { (byte)'G', (byte)'F', (byte)'R' };

        /// <summary>
        /// Initializes a new instance of the ReadOnlyVersionListSerializer class.
        /// </summary>
        public ReadOnlyVersionListSerializer()
        {
        }

        /// <summary>
        /// Gets the read-only version list header.
        /// </summary>
        /// <returns>Read-only version list header.</returns>
        protected override byte[] GetHeader()
        {
            return Header;
        }
    }
}
