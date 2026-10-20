//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.IO;

namespace GameFramework.Network
{
    /// <summary>
    /// Network channel helper interface.
    /// </summary>
    public interface INetworkChannelHelper
    {
        /// <summary>
        /// Gets the packet header length.
        /// </summary>
        int PacketHeaderLength
        {
            get;
        }

        /// <summary>
        /// Initializes the network channel helper.
        /// </summary>
        /// <param name="networkChannel">Network channel.</param>
        void Initialize(INetworkChannel networkChannel);

        /// <summary>
        /// Shuts down and cleans up the network channel helper.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Prepares for connecting.
        /// </summary>
        void PrepareForConnecting();

        /// <summary>
        /// Sends heartbeat packet.
        /// </summary>
        /// <returns>Whether sending the heartbeat packet was successful.</returns>
        bool SendHeartBeat();

        /// <summary>
        /// Serializes packet.
        /// </summary>
        /// <typeparam name="T">Packet type.</typeparam>
        /// <param name="packet">Packet to serialize.</param>
        /// <param name="destination">Target stream to serialize into.</param>
        /// <returns>Whether the packet was serialized successfully.</returns>
        bool Serialize<T>(T packet, Stream destination) where T : Packet;

        /// <summary>
        /// Deserializes packet header.
        /// </summary>
        /// <param name="source">Source stream to deserialize from.</param>
        /// <param name="customErrorData">User-defined error data.</param>
        /// <returns>Deserialized packet header.</returns>
        IPacketHeader DeserializePacketHeader(Stream source, out object customErrorData);

        /// <summary>
        /// Deserializes packet.
        /// </summary>
        /// <param name="packetHeader">Packet header.</param>
        /// <param name="source">Source stream to deserialize from.</param>
        /// <param name="customErrorData">User-defined error data.</param>
        /// <returns>Deserialized packet.</returns>
        Packet DeserializePacket(IPacketHeader packetHeader, Stream source, out object customErrorData);
    }
}
