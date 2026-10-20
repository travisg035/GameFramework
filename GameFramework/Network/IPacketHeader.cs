//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// Packet header interface.
    /// </summary>
    public interface IPacketHeader
    {
        /// <summary>
        /// Gets the packet length.
        /// </summary>
        int PacketLength
        {
            get;
        }
    }
}
