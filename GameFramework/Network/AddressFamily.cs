//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// Network address family.
    /// </summary>
    public enum AddressFamily : byte
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// IP version 4.
        /// </summary>
        IPv4,

        /// <summary>
        /// IP version 6.
        /// </summary>
        IPv6
    }
}
