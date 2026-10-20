//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Read-write version list serializer.
    /// </summary>
    public sealed class ReadWriteVersionListSerializer : GameFrameworkSerializer<LocalVersionList>
    {
        private static readonly byte[] Header = new byte[] { (byte)'G', (byte)'F', (byte)'W' };

        /// <summary>
        /// Initializes a new instance of the ReadWriteVersionListSerializer class.
        /// </summary>
        public ReadWriteVersionListSerializer()
        {
        }

        /// <summary>
        /// Gets the read-write version list header.
        /// </summary>
        /// <returns>Read-write version list header.</returns>
        protected override byte[] GetHeader()
        {
            return Header;
        }
    }
}
