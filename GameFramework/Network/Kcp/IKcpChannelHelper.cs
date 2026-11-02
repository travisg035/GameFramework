//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

namespace GameFramework.Network
{
    /// <summary>
    /// KCP network channel helper interface.
    /// </summary>
    public interface IKcpChannelHelper : INetworkChannelHelper
    {
        /// <summary>
        /// Gets the conversation ID.
        /// </summary>
        uint Conv
        {
            get;
        }

        /// <summary>
        /// Gets the maximum transmission unit (MTU).
        /// </summary>
        int Mtu
        {
            get;
        }

        /// <summary>
        /// Gets the send window size.
        /// </summary>
        int SendWindow
        {
            get;
        }

        /// <summary>
        /// Gets the receive window size.
        /// </summary>
        int ReceiveWindow
        {
            get;
        }

        /// <summary>
        /// Gets whether nodelay mode is enabled (0: regular, 1: nodelay).
        /// </summary>
        int NoDelay
        {
            get;
        }

        /// <summary>
        /// Gets the internal update interval in milliseconds.
        /// </summary>
        int Interval
        {
            get;
        }

        /// <summary>
        /// Gets the fast resend threshold.
        /// </summary>
        int FastResend
        {
            get;
        }

        /// <summary>
        /// Gets whether congestion control is enabled.
        /// </summary>
        bool CongestionControl
        {
            get;
        }
    }
}
