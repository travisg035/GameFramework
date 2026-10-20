//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Result of checking whether an asset exists.
    /// </summary>
    public enum HasAssetResult : byte
    {
        /// <summary>
        /// The asset does not exist.
        /// </summary>
        NotExist = 0,

        /// <summary>
        /// The asset is not ready yet.
        /// </summary>
        NotReady,

        /// <summary>
        /// The asset exists and is stored on disk.
        /// </summary>
        AssetOnDisk,

        /// <summary>
        /// The asset exists and is stored in a file system.
        /// </summary>
        AssetOnFileSystem,

        /// <summary>
        /// The binary asset exists and is stored on disk.
        /// </summary>
        BinaryOnDisk,

        /// <summary>
        /// The binary asset exists and is stored in a file system.
        /// </summary>
        BinaryOnFileSystem
    }
}
