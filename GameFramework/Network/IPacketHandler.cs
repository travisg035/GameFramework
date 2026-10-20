//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// Packet handler interface.
    /// </summary>
    public interface IPacketHandler
    {
        /// <summary>
        /// Gets the packet protocol ID.
        /// </summary>
        int Id
        {
            get;
        }

        /// <summary>
        /// Packet handler method.
        /// </summary>
        /// <param name="sender">Packet source.</param>
        /// <param name="packet">Packet content.</param>
        void Handle(object sender, Packet packet);
    }
}
