//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.Download
{
    /// <summary>
    /// Download agent helper interface.
    /// </summary>
    public interface IDownloadAgentHelper
    {
        /// <summary>
        /// Download agent helper update bytes event.
        /// </summary>
        event EventHandler<DownloadAgentHelperUpdateBytesEventArgs> DownloadAgentHelperUpdateBytes;

        /// <summary>
        /// Download agent helper update length event.
        /// </summary>
        event EventHandler<DownloadAgentHelperUpdateLengthEventArgs> DownloadAgentHelperUpdateLength;

        /// <summary>
        /// Download agent helper complete event.
        /// </summary>
        event EventHandler<DownloadAgentHelperCompleteEventArgs> DownloadAgentHelperComplete;

        /// <summary>
        /// Download agent helper error event.
        /// </summary>
        event EventHandler<DownloadAgentHelperErrorEventArgs> DownloadAgentHelperError;

        /// <summary>
        /// Downloads data from the specified URI through the download agent helper.
        /// </summary>
        /// <param name="downloadUri">Download URI.</param>
        /// <param name="userData">User-defined data.</param>
        void Download(string downloadUri, object userData);

        /// <summary>
        /// Downloads data from the specified URI through the download agent helper.
        /// </summary>
        /// <param name="downloadUri">Download URI.</param>
        /// <param name="fromPosition">The start position of the download data.</param>
        /// <param name="userData">User-defined data.</param>
        void Download(string downloadUri, long fromPosition, object userData);

        /// <summary>
        /// Downloads data from the specified URI through the download agent helper.
        /// </summary>
        /// <param name="downloadUri">Download URI.</param>
        /// <param name="fromPosition">The start position of the download data.</param>
        /// <param name="toPosition">The end position of the download data.</param>
        /// <param name="userData">User-defined data.</param>
        void Download(string downloadUri, long fromPosition, long toPosition, object userData);

        /// <summary>
        /// Resets the download agent helper.
        /// </summary>
        void Reset();
    }
}
