//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// Network error code.
    /// </summary>
    public enum NetworkErrorCode : byte
    {
        /// <summary>
        /// Unknown error.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Address family error.
        /// </summary>
        AddressFamilyError,

        /// <summary>
        /// Socket error.
        /// </summary>
        SocketError,

        /// <summary>
        /// Connect error.
        /// </summary>
        ConnectError,

        /// <summary>
        /// Send error.
        /// </summary>
        SendError,

        /// <summary>
        /// Receive error.
        /// </summary>
        ReceiveError,

        /// <summary>
        /// Serialize error.
        /// </summary>
        SerializeError,

        /// <summary>
        /// Deserialize packet header error.
        /// </summary>
        DeserializePacketHeaderError,

        /// <summary>
        /// Deserialize packet error.
        /// </summary>
        DeserializePacketError
    }
}
