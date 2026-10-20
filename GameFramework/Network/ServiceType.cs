//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// Network service type.
    /// </summary>
    public enum ServiceType : byte
    {
        /// <summary>
        /// TCP network service.
        /// </summary>
        Tcp = 0,

        /// <summary>
        /// TCP network service with synchronous receive.
        /// </summary>
        TcpWithSyncReceive
    }
}
