//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.WebRequest
{
    /// <summary>
    /// Web request start event.
    /// </summary>
    public sealed class WebRequestStartEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the WebRequestStartEventArgs class.
        /// </summary>
        public WebRequestStartEventArgs()
        {
            SerialId = 0;
            WebRequestUri = null;
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
        /// Creates a web request start event.
        /// </summary>
        /// <param name="serialId">The serial id of the web request task.</param>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The created web request start event.</returns>
        public static WebRequestStartEventArgs Create(int serialId, string webRequestUri, object userData)
        {
            WebRequestStartEventArgs webRequestStartEventArgs = ReferencePool.Acquire<WebRequestStartEventArgs>();
            webRequestStartEventArgs.SerialId = serialId;
            webRequestStartEventArgs.WebRequestUri = webRequestUri;
            webRequestStartEventArgs.UserData = userData;
            return webRequestStartEventArgs;
        }

        /// <summary>
        /// Clears the web request start event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            WebRequestUri = null;
            UserData = null;
        }
    }
}
