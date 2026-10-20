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
        /// Resource load type.
        /// </summary>
        private enum LoadType : byte
        {
            /// <summary>
            /// Load from file.
            /// </summary>
            LoadFromFile = 0,

            /// <summary>
            /// Load from memory.
            /// </summary>
            LoadFromMemory,

            /// <summary>
            /// Load from memory with quick decryption.
            /// </summary>
            LoadFromMemoryAndQuickDecrypt,

            /// <summary>
            /// Load from memory with decryption.
            /// </summary>
            LoadFromMemoryAndDecrypt,

            /// <summary>
            /// Load from binary.
            /// </summary>
            LoadFromBinary,

            /// <summary>
            /// Load from binary with quick decryption.
            /// </summary>
            LoadFromBinaryAndQuickDecrypt,

            /// <summary>
            /// Load from binary with decryption.
            /// </summary>
            LoadFromBinaryAndDecrypt
        }
    }
}
