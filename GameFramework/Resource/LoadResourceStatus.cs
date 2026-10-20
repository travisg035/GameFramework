//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load resource status.
    /// </summary>
    public enum LoadResourceStatus : byte
    {
        /// <summary>
        /// Load resource success.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Resource does not exist.
        /// </summary>
        NotExist,

        /// <summary>
        /// Resource is not ready yet.
        /// </summary>
        NotReady,

        /// <summary>
        /// Dependency asset error.
        /// </summary>
        DependencyError,

        /// <summary>
        /// Resource type error.
        /// </summary>
        TypeError,

        /// <summary>
        /// Load asset error.
        /// </summary>
        AssetError
    }
}
