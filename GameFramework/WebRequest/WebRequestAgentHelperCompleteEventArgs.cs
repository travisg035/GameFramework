//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.WebRequest
{
    /// <summary>
    /// Web request agent helper complete event.
    /// </summary>
    public sealed class WebRequestAgentHelperCompleteEventArgs : GameFrameworkEventArgs
    {
        private byte[] m_WebResponseBytes;

        /// <summary>
        /// Initializes a new instance of the WebRequestAgentHelperCompleteEventArgs class.
        /// </summary>
        public WebRequestAgentHelperCompleteEventArgs()
        {
            m_WebResponseBytes = null;
        }

        /// <summary>
        /// Creates a web request agent helper complete event.
        /// </summary>
        /// <param name="webResponseBytes">Web response bytes.</param>
        /// <returns>The created web request agent helper complete event.</returns>
        public static WebRequestAgentHelperCompleteEventArgs Create(byte[] webResponseBytes)
        {
            WebRequestAgentHelperCompleteEventArgs webRequestAgentHelperCompleteEventArgs = ReferencePool.Acquire<WebRequestAgentHelperCompleteEventArgs>();
            webRequestAgentHelperCompleteEventArgs.m_WebResponseBytes = webResponseBytes;
            return webRequestAgentHelperCompleteEventArgs;
        }

        /// <summary>
        /// Clears the web request agent helper complete event.
        /// </summary>
        public override void Clear()
        {
            m_WebResponseBytes = null;
        }

        /// <summary>
        /// Gets the web response bytes.
        /// </summary>
        /// <returns>The web response bytes.</returns>
        public byte[] GetWebResponseBytes()
        {
            return m_WebResponseBytes;
        }
    }
}
