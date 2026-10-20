//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.WebRequest
{
    /// <summary>
    /// Web request success event.
    /// </summary>
    public sealed class WebRequestSuccessEventArgs : GameFrameworkEventArgs
    {
        private byte[] m_WebResponseBytes;

        /// <summary>
        /// Initializes a new instance of the WebRequestSuccessEventArgs class.
        /// </summary>
        public WebRequestSuccessEventArgs()
        {
            SerialId = 0;
            WebRequestUri = null;
            m_WebResponseBytes = null;
            UserData = null;
        }

        /// <summary>
        /// Gets the serial id of the web request task.
        /// </summary>
        public int SerialId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the web request URI.
        /// </summary>
        public string WebRequestUri
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the custom user data.
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a web request success event.
        /// </summary>
        /// <param name="serialId">The serial id of the web request task.</param>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="webResponseBytes">Web response bytes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The created web request success event.</returns>
        public static WebRequestSuccessEventArgs Create(int serialId, string webRequestUri, byte[] webResponseBytes, object userData)
        {
            WebRequestSuccessEventArgs webRequestSuccessEventArgs = ReferencePool.Acquire<WebRequestSuccessEventArgs>();
            webRequestSuccessEventArgs.SerialId = serialId;
            webRequestSuccessEventArgs.WebRequestUri = webRequestUri;
            webRequestSuccessEventArgs.m_WebResponseBytes = webResponseBytes;
            webRequestSuccessEventArgs.UserData = userData;
            return webRequestSuccessEventArgs;
        }

        /// <summary>
        /// Clears the web request success event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            WebRequestUri = null;
            m_WebResponseBytes = null;
            UserData = null;
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
