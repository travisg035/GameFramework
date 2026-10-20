//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Updatable version list serializer.
    /// </summary>
    public sealed class UpdatableVersionListSerializer : GameFrameworkSerializer<UpdatableVersionList>
    {
        private static readonly byte[] Header = new byte[] { (byte)'G', (byte)'F', (byte)'U' };

        /// <summary>
        /// Initializes a new instance of the UpdatableVersionListSerializer class.
        /// </summary>
        public UpdatableVersionListSerializer()
        {
        }

        /// <summary>
        /// Gets the updatable version list header.
        /// </summary>
        /// <returns>Updatable version list header.</returns>
        protected override byte[] GetHeader()
        {
            return Header;
        }
    }
}
