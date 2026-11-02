//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.Network
{
    /// <summary>
    /// Pure C# KCP ARQ protocol implementation.
    /// </summary>
    internal sealed class Kcp
    {
        public const int IKCP_RTO_NDL = 30; // no delay min rto
        public const int IKCP_RTO_MIN = 100; // normal min rto
        public const int IKCP_RTO_DEF = 200;
        public const int IKCP_RTO_MAX = 60000;
        public const byte IKCP_CMD_PUSH = 81; // cmd: push data
        public const byte IKCP_CMD_ACK = 82; // cmd: ack
        public const byte IKCP_CMD_WASK = 83; // cmd: window probe (ask)
        public const byte IKCP_CMD_WINS = 84; // cmd: window size (tell)
        public const int IKCP_ASK_SEND = 1; // need to send IKCP_CMD_WASK
        public const int IKCP_ASK_TELL = 2; // need to send IKCP_CMD_WINS
        public const int IKCP_WND_SND = 32;
        public const int IKCP_WND_RCV = 128;
        public const int IKCP_MTU_DEF = 1400;
        public const int IKCP_ACKFAST_LIMIT = 5;
        public const int IKCP_INTERVAL = 100;
        public const int IKCP_OVERHEAD = KcpSegment.HeaderSize;
        public const int IKCP_DEADLINK = 20;
        public const int IKCP_THRESH_INIT = 2;
        public const int IKCP_THRESH_MIN = 2;
        public const int IKCP_PROBE_INIT = 7000;
        public const int IKCP_PROBE_LIMIT = 120000;
        public const int IKCP_FASTACK_CONG = 5;

        private struct AckItem
        {
            public uint Sn;
            public uint Ts;
        }

        private readonly uint m_Conv;
        private int m_Mtu;
        private int m_Mss;
        private int m_State;
        private uint m_SndUna;
        private uint m_SndNxt;
        private uint m_RcvNxt;
        private uint m_Ssthresh;
        private int m_RxRttVal;
        private int m_RxSrtt;
        private int m_RxRto;
        private int m_RxMinRto;
        private uint m_SndWnd;
        private uint m_RcvWnd;
        private uint m_RmtWnd;
        private uint m_Cwnd;
        private int m_Probe;
        private uint m_Interval;
        private uint m_TsFlush;
        private uint m_Nodelay;
        private bool m_Updated;
        private uint m_TsProbe;
        private uint m_ProbeWait;
        private uint m_DeadLink;
        private uint m_Incr;
        private int m_FastResend;
        private bool m_Nocwnd;
        private uint m_Current;

        private readonly GameFrameworkLinkedList<KcpSegment> m_SndQueue;
        private readonly GameFrameworkLinkedList<KcpSegment> m_RcvQueue;
        private readonly GameFrameworkLinkedList<KcpSegment> m_SndBuf;
        private readonly GameFrameworkLinkedList<KcpSegment> m_RcvBuf;
        private readonly List<AckItem> m_AckList;
        private readonly byte[] m_Buffer;
        private readonly GameFrameworkAction<byte[], int, object> m_Output;
        private readonly object m_UserData;

        public Kcp(uint conv, GameFrameworkAction<byte[], int, object> output, object userData)
        {
            if (output == null)
            {
                throw new GameFrameworkException("Output callback is invalid.");
            }

            m_Conv = conv;
            m_Output = output;
            m_UserData = userData;
            m_Mtu = IKCP_MTU_DEF;
            m_Mss = m_Mtu - IKCP_OVERHEAD;
            m_State = 0;
            m_SndUna = 0;
            m_SndNxt = 0;
            m_RcvNxt = 0;
            m_Ssthresh = IKCP_THRESH_INIT;
            m_RxRttVal = 0;
            m_RxSrtt = 0;
            m_RxRto = IKCP_RTO_DEF;
            m_RxMinRto = IKCP_RTO_MIN;
            m_SndWnd = IKCP_WND_SND;
            m_RcvWnd = IKCP_WND_RCV;
            m_RmtWnd = IKCP_WND_RCV;
            m_Cwnd = 0;
            m_Probe = 0;
            m_Interval = IKCP_INTERVAL;
            m_TsFlush = IKCP_INTERVAL;
            m_Nodelay = 0;
            m_Updated = false;
            m_TsProbe = 0;
            m_ProbeWait = 0;
            m_DeadLink = IKCP_DEADLINK;
            m_Incr = 0;
            m_FastResend = 0;
            m_Nocwnd = false;
            m_Current = 0;

            m_SndQueue = new GameFrameworkLinkedList<KcpSegment>();
            m_RcvQueue = new GameFrameworkLinkedList<KcpSegment>();
            m_SndBuf = new GameFrameworkLinkedList<KcpSegment>();
            m_RcvBuf = new GameFrameworkLinkedList<KcpSegment>();
            m_AckList = new List<AckItem>();
            m_Buffer = new byte[(m_Mtu + IKCP_OVERHEAD) * 3];
        }

        public uint Conv
        {
            get
            {
                return m_Conv;
            }
        }

        public int State
        {
            get
            {
                return m_State;
            }
        }

        public int WaitSnd
        {
            get
            {
                return m_SndBuf.Count + m_SndQueue.Count;
            }
        }

        public int PeekSize()
        {
            if (m_RcvQueue.Count == 0)
            {
                return -1;
            }

            LinkedListNode<KcpSegment> firstNode = m_RcvQueue.First;
            KcpSegment firstSeg = firstNode.Value;
            if (firstSeg.Frg == 0)
            {
                return firstSeg.DataLength;
            }

            if (m_RcvQueue.Count < firstSeg.Frg + 1)
            {
                return -1;
            }

            int length = 0;
            for (LinkedListNode<KcpSegment> node = m_RcvQueue.First; node != null; node = node.Next)
            {
                KcpSegment seg = node.Value;
                length += seg.DataLength;
                if (seg.Frg == 0)
                {
                    break;
                }
            }

            return length;
        }

        public int Recv(byte[] buffer, int offset, int len)
        {
            if (m_RcvQueue.Count == 0)
            {
                return -1;
            }

            int peekSize = PeekSize();
            if (peekSize < 0)
            {
                return -2;
            }

            if (peekSize > len)
            {
                return -3;
            }

            bool recover = m_RcvQueue.Count >= m_RcvWnd;
            int writeOffset = offset;

            LinkedListNode<KcpSegment> nextNode = null;
            for (LinkedListNode<KcpSegment> node = m_RcvQueue.First; node != null; node = nextNode)
            {
                nextNode = node.Next;
                KcpSegment seg = node.Value;
                if (seg.DataLength > 0 && buffer != null)
                {
                    Array.Copy(seg.Data, 0, buffer, writeOffset, seg.DataLength);
                    writeOffset += seg.DataLength;
                }

                byte frg = seg.Frg;
                m_RcvQueue.Remove(node);
                ReferencePool.Release(seg);

                if (frg == 0)
                {
                    break;
                }
            }

            // Move available contiguous segments from rcv_buf to rcv_queue
            while (m_RcvBuf.Count > 0)
            {
                KcpSegment seg = m_RcvBuf.First.Value;
                if (seg.Sn == m_RcvNxt && m_RcvQueue.Count < m_RcvWnd)
                {
                    m_RcvBuf.Remove(m_RcvBuf.First);
                    m_RcvQueue.AddLast(seg);
                    m_RcvNxt++;
                }
                else
                {
                    break;
                }
            }

            if (m_RcvQueue.Count < m_RcvWnd && recover)
            {
                m_Probe |= IKCP_ASK_TELL;
            }

            return peekSize;
        }

        public int Send(byte[] buffer, int offset, int len)
        {
            if (len <= 0)
            {
                return -1;
            }

            int count = 0;
            if (len <= m_Mss)
            {
                count = 1;
            }
            else
            {
                count = (len + m_Mss - 1) / m_Mss;
            }

            if (count >= 255)
            {
                return -2;
            }

            if (count == 0)
            {
                count = 1;
            }

            int readOffset = offset;
            for (int i = 0; i < count; i++)
            {
                int size = len > m_Mss ? m_Mss : len;
                KcpSegment seg = KcpSegment.Create(size);
                if (buffer != null && size > 0)
                {
                    Array.Copy(buffer, readOffset, seg.Data, 0, size);
                }

                seg.DataLength = size;
                seg.Frg = (byte)(count - i - 1);
                m_SndQueue.AddLast(seg);

                readOffset += size;
                len -= size;
            }

            return 0;
        }

        public void Update(uint current)
        {
            m_Current = current;
            if (!m_Updated)
            {
                m_Updated = true;
                m_TsFlush = current;
            }

            int slap = TimeDiff(current, m_TsFlush);
            if (slap >= 10000 || slap < -10000)
            {
                m_TsFlush = current;
                slap = 0;
            }

            if (slap >= 0)
            {
                m_TsFlush += m_Interval;
                if (TimeDiff(current, m_TsFlush) >= 0)
                {
                    m_TsFlush = current + m_Interval;
                }

                Flush(current);
            }
        }

        public uint Check(uint current)
        {
            if (!m_Updated)
            {
                return current;
            }

            uint tsFlush = m_TsFlush;
            int tmPacket = 0x7fffffff;

            if (TimeDiff(current, tsFlush) >= 10000 || TimeDiff(current, tsFlush) < -10000)
            {
                tsFlush = current;
            }

            if (TimeDiff(current, tsFlush) >= 0)
            {
                return current;
            }

            int tmFlush = TimeDiff(tsFlush, current);
            for (LinkedListNode<KcpSegment> node = m_SndBuf.First; node != null; node = node.Next)
            {
                KcpSegment seg = node.Value;
                int diff = TimeDiff(seg.ResendTs, current);
                if (diff <= 0)
                {
                    return current;
                }

                if (diff < tmPacket)
                {
                    tmPacket = diff;
                }
            }

            uint minimal = (uint)(tmPacket < tmFlush ? tmPacket : tmFlush);
            if (minimal >= m_Interval)
            {
                minimal = m_Interval;
            }

            return current + minimal;
        }

        public int Input(byte[] data, int offset, int size)
        {
            uint prevUna = m_SndUna;
            uint maxAck = 0;
            uint latestTs = 0;
            bool flag = false;

            if (data == null || size < IKCP_OVERHEAD)
            {
                return -1;
            }

            int readOffset = offset;
            int remaining = size;

            while (remaining >= IKCP_OVERHEAD)
            {
                uint conv = ReadUInt32(data, ref readOffset);
                if (conv != m_Conv)
                {
                    return -1;
                }

                byte cmd = data[readOffset++];
                byte frg = data[readOffset++];
                ushort wnd = ReadUInt16(data, ref readOffset);
                uint ts = ReadUInt32(data, ref readOffset);
                uint sn = ReadUInt32(data, ref readOffset);
                uint una = ReadUInt32(data, ref readOffset);
                int len = (int)ReadUInt32(data, ref readOffset);
                remaining -= IKCP_OVERHEAD;

                if (remaining < len || len < 0)
                {
                    return -2;
                }

                if (cmd != IKCP_CMD_PUSH && cmd != IKCP_CMD_ACK && cmd != IKCP_CMD_WASK && cmd != IKCP_CMD_WINS)
                {
                    return -3;
                }

                m_RmtWnd = wnd;
                ParseUna(una);
                ShrinkBuf();

                if (cmd == IKCP_CMD_ACK)
                {
                    if (TimeDiff(m_Current, ts) >= 0)
                    {
                        UpdateAck(TimeDiff(m_Current, ts));
                    }

                    ParseAck(sn);
                    ShrinkBuf();

                    if (!flag)
                    {
                        flag = true;
                        maxAck = sn;
                        latestTs = ts;
                    }
                    else if (TimeDiff(sn, maxAck) > 0)
                    {
                        maxAck = sn;
                        latestTs = ts;
                    }
                }
                else if (cmd == IKCP_CMD_PUSH)
                {
                    if (TimeDiff(sn, m_RcvNxt + m_RcvWnd) < 0)
                    {
                        AckPush(sn, ts);
                        if (TimeDiff(sn, m_RcvNxt) >= 0)
                        {
                            KcpSegment seg = KcpSegment.Create(len);
                            seg.Conv = conv;
                            seg.Cmd = cmd;
                            seg.Frg = frg;
                            seg.Wnd = wnd;
                            seg.Ts = ts;
                            seg.Sn = sn;
                            seg.Una = una;
                            if (len > 0)
                            {
                                Array.Copy(data, readOffset, seg.Data, 0, len);
                            }

                            seg.DataLength = len;
                            ParseData(seg);
                        }
                    }
                }
                else if (cmd == IKCP_CMD_WASK)
                {
                    m_Probe |= IKCP_ASK_TELL;
                }
                else if (cmd == IKCP_CMD_WINS)
                {
                    // Do nothing
                }

                readOffset += len;
                remaining -= len;
            }

            if (flag)
            {
                ParseFastAck(maxAck, latestTs);
            }

            if (TimeDiff(m_SndUna, prevUna) > 0)
            {
                if (m_Cwnd < m_RmtWnd)
                {
                    uint mss = (uint)m_Mss;
                    if (m_Cwnd < m_Ssthresh)
                    {
                        m_Cwnd++;
                        m_Incr += mss;
                    }
                    else
                    {
                        if (m_Incr == 0)
                        {
                            m_Incr = mss;
                        }

                        m_Incr += (mss * mss) / m_Incr + (mss / 16);
                        if ((m_Cwnd + 1) * mss <= m_Incr)
                        {
                            m_Cwnd = (m_Incr + mss - 1) / ((mss > 0) ? mss : 1);
                        }
                    }

                    if (m_Cwnd > m_RmtWnd)
                    {
                        m_Cwnd = m_RmtWnd;
                        m_Incr = m_RmtWnd * mss;
                    }
                }
            }

            return 0;
        }

        public int SetNoDelay(int nodelay, int interval, int resend, int nc)
        {
            if (nodelay >= 0)
            {
                m_Nodelay = (uint)nodelay;
                if (nodelay > 0)
                {
                    m_RxMinRto = IKCP_RTO_NDL;
                }
                else
                {
                    m_RxMinRto = IKCP_RTO_MIN;
                }
            }

            if (interval >= 0)
            {
                if (interval > 5000)
                {
                    interval = 5000;
                }
                else if (interval < 10)
                {
                    interval = 10;
                }

                m_Interval = (uint)interval;
            }

            if (resend >= 0)
            {
                m_FastResend = resend;
            }

            if (nc >= 0)
            {
                m_Nocwnd = nc > 0;
            }

            return 0;
        }

        public int SetWindowSize(int sndwnd, int rcvwnd)
        {
            if (sndwnd > 0)
            {
                m_SndWnd = (uint)sndwnd;
            }

            if (rcvwnd > 0)
            {
                m_RcvWnd = (uint)rcvwnd;
            }

            return 0;
        }

        public int SetMtu(int mtu)
        {
            if (mtu < 50 || mtu < IKCP_OVERHEAD)
            {
                return -1;
            }

            m_Mtu = mtu;
            m_Mss = m_Mtu - IKCP_OVERHEAD;
            return 0;
        }

        private void Flush(uint current)
        {
            if (!m_Updated)
            {
                return;
            }

            KcpSegment seg = KcpSegment.Create(0);
            seg.Conv = m_Conv;
            seg.Cmd = IKCP_CMD_ACK;
            seg.Frg = 0;
            seg.Wnd = (ushort)WndUnused();
            seg.Una = m_RcvNxt;
            seg.DataLength = 0;
            seg.Sn = 0;
            seg.Ts = 0;

            int writeOffset = 0;

            // Flush ACKs
            for (int i = 0; i < m_AckList.Count; i++)
            {
                if (writeOffset + IKCP_OVERHEAD > m_Mtu)
                {
                    m_Output(m_Buffer, writeOffset, m_UserData);
                    writeOffset = 0;
                }

                seg.Sn = m_AckList[i].Sn;
                seg.Ts = m_AckList[i].Ts;
                seg.Encode(m_Buffer, ref writeOffset);
            }

            m_AckList.Clear();

            // Probe window size
            if (m_RmtWnd == 0)
            {
                if (m_ProbeWait == 0)
                {
                    m_ProbeWait = IKCP_PROBE_INIT;
                    m_TsProbe = current + m_ProbeWait;
                }
                else
                {
                    if (TimeDiff(current, m_TsProbe) >= 0)
                    {
                        if (m_ProbeWait < IKCP_PROBE_INIT)
                        {
                            m_ProbeWait = IKCP_PROBE_INIT;
                        }

                        m_ProbeWait += m_ProbeWait / 2;
                        if (m_ProbeWait > IKCP_PROBE_LIMIT)
                        {
                            m_ProbeWait = IKCP_PROBE_LIMIT;
                        }

                        m_TsProbe = current + m_ProbeWait;
                        m_Probe |= IKCP_ASK_SEND;
                    }
                }
            }
            else
            {
                m_TsProbe = 0;
                m_ProbeWait = 0;
            }

            // Flush window probe
            if ((m_Probe & IKCP_ASK_SEND) != 0)
            {
                seg.Cmd = IKCP_CMD_WASK;
                if (writeOffset + IKCP_OVERHEAD > m_Mtu)
                {
                    m_Output(m_Buffer, writeOffset, m_UserData);
                    writeOffset = 0;
                }

                seg.Encode(m_Buffer, ref writeOffset);
            }

            if ((m_Probe & IKCP_ASK_TELL) != 0)
            {
                seg.Cmd = IKCP_CMD_WINS;
                if (writeOffset + IKCP_OVERHEAD > m_Mtu)
                {
                    m_Output(m_Buffer, writeOffset, m_UserData);
                    writeOffset = 0;
                }

                seg.Encode(m_Buffer, ref writeOffset);
            }

            m_Probe = 0;

            // Calculate window
            uint cwnd = (m_SndWnd < m_RmtWnd) ? m_SndWnd : m_RmtWnd;
            if (!m_Nocwnd)
            {
                if (m_Cwnd < cwnd)
                {
                    cwnd = m_Cwnd;
                }
            }

            // Move data from snd_queue to snd_buf
            while (TimeDiff(m_SndNxt, m_SndUna + cwnd) < 0)
            {
                if (m_SndQueue.Count == 0)
                {
                    break;
                }

                KcpSegment newSeg = m_SndQueue.First.Value;
                m_SndQueue.Remove(m_SndQueue.First);

                newSeg.Conv = m_Conv;
                newSeg.Cmd = IKCP_CMD_PUSH;
                newSeg.Wnd = seg.Wnd;
                newSeg.Ts = current;
                newSeg.Sn = m_SndNxt++;
                newSeg.Una = m_RcvNxt;
                newSeg.ResendTs = current;
                newSeg.Rto = (uint)m_RxRto;
                newSeg.FastAck = 0;
                newSeg.Xmit = 0;
                m_SndBuf.AddLast(newSeg);
            }

            // Calculate resend
            uint resent = (m_FastResend > 0) ? (uint)m_FastResend : 0xffffffff;
            uint rtoMin = (m_Nodelay == 0) ? ((uint)m_RxRto >> 3) : 0;

            bool lost = false;
            bool change = false;

            for (LinkedListNode<KcpSegment> node = m_SndBuf.First; node != null; node = node.Next)
            {
                KcpSegment sndSeg = node.Value;
                bool needSend = false;

                if (sndSeg.Xmit == 0)
                {
                    needSend = true;
                    sndSeg.Xmit++;
                    sndSeg.Rto = (uint)m_RxRto;
                    sndSeg.ResendTs = current + sndSeg.Rto + rtoMin;
                }
                else if (TimeDiff(current, sndSeg.ResendTs) >= 0)
                {
                    needSend = true;
                    sndSeg.Xmit++;
                    if (m_Nodelay == 0)
                    {
                        sndSeg.Rto += (sndSeg.Rto > (uint)m_RxRto) ? sndSeg.Rto : (uint)m_RxRto;
                    }
                    else
                    {
                        uint step = (m_Nodelay < 2) ? sndSeg.Rto : (uint)m_RxRto;
                        sndSeg.Rto += step / 2;
                    }

                    sndSeg.ResendTs = current + sndSeg.Rto;
                    lost = true;
                }
                else if (sndSeg.FastAck >= resent)
                {
                    if (sndSeg.Xmit <= IKCP_DEADLINK)
                    {
                        needSend = true;
                        sndSeg.Xmit++;
                        sndSeg.FastAck = 0;
                        sndSeg.ResendTs = current + sndSeg.Rto;
                        change = true;
                    }
                }

                if (needSend)
                {
                    sndSeg.Ts = current;
                    sndSeg.Wnd = seg.Wnd;
                    sndSeg.Una = m_RcvNxt;

                    int need = IKCP_OVERHEAD + sndSeg.DataLength;
                    if (writeOffset + need > m_Mtu)
                    {
                        m_Output(m_Buffer, writeOffset, m_UserData);
                        writeOffset = 0;
                    }

                    sndSeg.Encode(m_Buffer, ref writeOffset);
                    if (sndSeg.DataLength > 0)
                    {
                        Array.Copy(sndSeg.Data, 0, m_Buffer, writeOffset, sndSeg.DataLength);
                        writeOffset += sndSeg.DataLength;
                    }

                    if (sndSeg.Xmit >= m_DeadLink)
                    {
                        m_State = -1;
                    }
                }
            }

            // Flush remaining data
            if (writeOffset > 0)
            {
                m_Output(m_Buffer, writeOffset, m_UserData);
            }

            // Update congestion control
            if (change)
            {
                uint inflight = m_SndNxt - m_SndUna;
                m_Ssthresh = inflight / 2;
                if (m_Ssthresh < IKCP_THRESH_MIN)
                {
                    m_Ssthresh = IKCP_THRESH_MIN;
                }

                m_Cwnd = m_Ssthresh + resent;
                m_Incr = m_Cwnd * (uint)m_Mss;
            }

            if (lost)
            {
                m_Ssthresh = cwnd / 2;
                if (m_Ssthresh < IKCP_THRESH_MIN)
                {
                    m_Ssthresh = IKCP_THRESH_MIN;
                }

                m_Cwnd = 1;
                m_Incr = (uint)m_Mss;
            }

            if (m_Cwnd < 1)
            {
                m_Cwnd = 1;
                m_Incr = (uint)m_Mss;
            }

            ReferencePool.Release(seg);
        }

        private void AckPush(uint sn, uint ts)
        {
            AckItem item = new AckItem();
            item.Sn = sn;
            item.Ts = ts;
            m_AckList.Add(item);
        }

        private void ParseData(KcpSegment newSeg)
        {
            uint sn = newSeg.Sn;
            if (TimeDiff(sn, m_RcvNxt + m_RcvWnd) >= 0 || TimeDiff(sn, m_RcvNxt) < 0)
            {
                ReferencePool.Release(newSeg);
                return;
            }

            bool repeat = false;
            LinkedListNode<KcpSegment> insertAfterNode = null;

            for (LinkedListNode<KcpSegment> node = m_RcvBuf.Last; node != null; node = node.Previous)
            {
                KcpSegment seg = node.Value;
                if (seg.Sn == sn)
                {
                    repeat = true;
                    break;
                }

                if (TimeDiff(sn, seg.Sn) > 0)
                {
                    insertAfterNode = node;
                    break;
                }
            }

            if (repeat)
            {
                ReferencePool.Release(newSeg);
                return;
            }

            if (insertAfterNode == null)
            {
                m_RcvBuf.AddFirst(newSeg);
            }
            else
            {
                m_RcvBuf.AddAfter(insertAfterNode, newSeg);
            }

            // Move contiguous segments from rcv_buf to rcv_queue
            while (m_RcvBuf.Count > 0)
            {
                KcpSegment seg = m_RcvBuf.First.Value;
                if (seg.Sn == m_RcvNxt && m_RcvQueue.Count < m_RcvWnd)
                {
                    m_RcvBuf.Remove(m_RcvBuf.First);
                    m_RcvQueue.AddLast(seg);
                    m_RcvNxt++;
                }
                else
                {
                    break;
                }
            }
        }

        private void ParseUna(uint una)
        {
            LinkedListNode<KcpSegment> nextNode = null;
            for (LinkedListNode<KcpSegment> node = m_SndBuf.First; node != null; node = nextNode)
            {
                nextNode = node.Next;
                KcpSegment seg = node.Value;
                if (TimeDiff(una, seg.Sn) > 0)
                {
                    m_SndBuf.Remove(node);
                    ReferencePool.Release(seg);
                }
                else
                {
                    break;
                }
            }
        }

        private void ParseAck(uint sn)
        {
            if (TimeDiff(sn, m_SndUna) < 0 || TimeDiff(sn, m_SndNxt) >= 0)
            {
                return;
            }

            for (LinkedListNode<KcpSegment> node = m_SndBuf.First; node != null; node = node.Next)
            {
                KcpSegment seg = node.Value;
                if (sn == seg.Sn)
                {
                    m_SndBuf.Remove(node);
                    ReferencePool.Release(seg);
                    break;
                }

                if (TimeDiff(sn, seg.Sn) < 0)
                {
                    break;
                }
            }
        }

        private void ParseFastAck(uint sn, uint ts)
        {
            if (TimeDiff(sn, m_SndUna) < 0 || TimeDiff(sn, m_SndNxt) >= 0)
            {
                return;
            }

            for (LinkedListNode<KcpSegment> node = m_SndBuf.First; node != null; node = node.Next)
            {
                KcpSegment seg = node.Value;
                if (TimeDiff(sn, seg.Sn) < 0)
                {
                    break;
                }
                else if (sn != seg.Sn)
                {
                    seg.FastAck++;
                }
            }
        }

        private void ShrinkBuf()
        {
            if (m_SndBuf.Count > 0)
            {
                m_SndUna = m_SndBuf.First.Value.Sn;
            }
            else
            {
                m_SndUna = m_SndNxt;
            }
        }

        private void UpdateAck(int rtt)
        {
            if (m_RxSrtt == 0)
            {
                m_RxSrtt = rtt;
                m_RxRttVal = rtt / 2;
            }
            else
            {
                int delta = rtt - m_RxSrtt;
                if (delta < 0)
                {
                    delta = -delta;
                }

                m_RxRttVal = (3 * m_RxRttVal + delta) / 4;
                m_RxSrtt = (7 * m_RxSrtt + rtt) / 8;
                if (m_RxSrtt < 1)
                {
                    m_RxSrtt = 1;
                }
            }

            int rto = m_RxSrtt + ((m_RxRttVal > (int)m_Interval) ? m_RxRttVal : (int)m_Interval);
            m_RxRto = (rto < m_RxMinRto) ? m_RxMinRto : ((rto > IKCP_RTO_MAX) ? IKCP_RTO_MAX : rto);
        }

        private int WndUnused()
        {
            if (m_RcvQueue.Count < m_RcvWnd)
            {
                return (int)(m_RcvWnd - m_RcvQueue.Count);
            }

            return 0;
        }

        private static int TimeDiff(uint later, uint earlier)
        {
            return (int)(later - earlier);
        }

        private static ushort ReadUInt16(byte[] data, ref int offset)
        {
            ushort value = (ushort)(data[offset] | (data[offset + 1] << 8));
            offset += 2;
            return value;
        }

        private static uint ReadUInt32(byte[] data, ref int offset)
        {
            uint value = (uint)(data[offset] | (data[offset + 1] << 8) | (data[offset + 2] << 16) | (data[offset + 3] << 24));
            offset += 4;
            return value;
        }
    }
}
