//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Check version list result.
    /// </summary>
    public enum CheckVersionListResult : byte
    {
        /// <summary>
        /// Already up-to-date.
        /// </summary>
        Updated = 0,

        /// <summary>
        /// Update needed.
        /// </summary>
        NeedUpdate
    }
}
