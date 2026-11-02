//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// KCP segment.
    /// </summary>
    internal sealed class KcpSegment : IReference
    {
        public const int HeaderSize = 24;

        public uint Conv;
        public byte Cmd;
        public byte Frg;
        public ushort Wnd;
        public uint Ts;
        public uint Sn;
        public uint Una;
        public uint ResendTs;
        public uint Rto;
        public uint FastAck;
        public uint Xmit;
        public byte[] Data;
        public int DataLength;

        public KcpSegment()
        {
            Conv = 0;
            Cmd = 0;
            Frg = 0;
            Wnd = 0;
            Ts = 0;
            Sn = 0;
            Una = 0;
            ResendTs = 0;
            Rto = 0;
            FastAck = 0;
            Xmit = 0;
            Data = null;
            DataLength = 0;
        }

        public static KcpSegment Create(int size)
        {
            KcpSegment segment = ReferencePool.Acquire<KcpSegment>();
            if (size > 0)
            {
                if (segment.Data == null || segment.Data.Length < size)
                {
                    segment.Data = new byte[size];
                }

                segment.DataLength = size;
            }

            return segment;
        }

        public void Encode(byte[] buffer, ref int offset)
        {
            buffer[offset++] = (byte)(Conv & 0xFF);
            buffer[offset++] = (byte)((Conv >> 8) & 0xFF);
            buffer[offset++] = (byte)((Conv >> 16) & 0xFF);
            buffer[offset++] = (byte)((Conv >> 24) & 0xFF);

            buffer[offset++] = Cmd;
            buffer[offset++] = Frg;

            buffer[offset++] = (byte)(Wnd & 0xFF);
            buffer[offset++] = (byte)((Wnd >> 8) & 0xFF);

            buffer[offset++] = (byte)(Ts & 0xFF);
            buffer[offset++] = (byte)((Ts >> 8) & 0xFF);
            buffer[offset++] = (byte)((Ts >> 16) & 0xFF);
            buffer[offset++] = (byte)((Ts >> 24) & 0xFF);

            buffer[offset++] = (byte)(Sn & 0xFF);
            buffer[offset++] = (byte)((Sn >> 8) & 0xFF);
            buffer[offset++] = (byte)((Sn >> 16) & 0xFF);
            buffer[offset++] = (byte)((Sn >> 24) & 0xFF);

            buffer[offset++] = (byte)(Una & 0xFF);
            buffer[offset++] = (byte)((Una >> 8) & 0xFF);
            buffer[offset++] = (byte)((Una >> 16) & 0xFF);
            buffer[offset++] = (byte)((Una >> 24) & 0xFF);

            buffer[offset++] = (byte)(DataLength & 0xFF);
            buffer[offset++] = (byte)((DataLength >> 8) & 0xFF);
            buffer[offset++] = (byte)((DataLength >> 16) & 0xFF);
            buffer[offset++] = (byte)((DataLength >> 24) & 0xFF);
        }

        public void Clear()
        {
            Conv = 0;
            Cmd = 0;
            Frg = 0;
            Wnd = 0;
            Ts = 0;
            Sn = 0;
            Una = 0;
            ResendTs = 0;
            Rto = 0;
            FastAck = 0;
            Xmit = 0;
            DataLength = 0;
        }
    }
}
