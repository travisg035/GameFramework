//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load resource progress type.
    /// </summary>
    public enum LoadResourceProgress : byte
    {
        /// <summary>
        /// Unknown type.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Read resource.
        /// </summary>
        ReadResource,

        /// <summary>
        /// Load resource.
        /// </summary>
        LoadResource,

        /// <summary>
        /// Load asset.
        /// </summary>
        LoadAsset,

        /// <summary>
        /// Load scene.
        /// </summary>
        LoadScene
    }
}
