//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.WebRequest
{
    /// <summary>
    /// Web request agent helper interface.
    /// </summary>
    public interface IWebRequestAgentHelper
    {
        /// <summary>
        /// Web request agent helper complete event.
        /// </summary>
        event EventHandler<WebRequestAgentHelperCompleteEventArgs> WebRequestAgentHelperComplete;

        /// <summary>
        /// Web request agent helper error event.
        /// </summary>
        event EventHandler<WebRequestAgentHelperErrorEventArgs> WebRequestAgentHelperError;

        /// <summary>
        /// Sends a web request through the web request agent helper.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="userData">Custom user data.</param>
        void Request(string webRequestUri, object userData);

        /// <summary>
        /// Sends a web request through the web request agent helper.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="userData">Custom user data.</param>
        void Request(string webRequestUri, byte[] postData, object userData);

        /// <summary>
        /// Resets the web request agent helper.
        /// </summary>
        void Reset();
    }
}
