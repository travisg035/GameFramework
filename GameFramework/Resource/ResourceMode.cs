//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource mode.
    /// </summary>
    public enum ResourceMode : byte
    {
        /// <summary>
        /// Unspecified.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Package mode.
        /// </summary>
        Package,

        /// <summary>
        /// Updatable mode (download before playing).
        /// </summary>
        Updatable,

        /// <summary>
        /// Updatable while playing mode (download on demand).
        /// </summary>
        UpdatableWhilePlaying
    }
}
