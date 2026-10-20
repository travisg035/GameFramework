//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.WebRequest
{
    /// <summary>
    /// Web request failure event.
    /// </summary>
    public sealed class WebRequestFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the WebRequestFailureEventArgs class.
        /// </summary>
        public WebRequestFailureEventArgs()
        {
            SerialId = 0;
            WebRequestUri = null;
            ErrorMessage = null;
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
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage
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
        /// Creates a web request failure event.
        /// </summary>
        /// <param name="serialId">The serial id of the web request task.</param>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The created web request failure event.</returns>
        public static WebRequestFailureEventArgs Create(int serialId, string webRequestUri, string errorMessage, object userData)
        {
            WebRequestFailureEventArgs webRequestFailureEventArgs = ReferencePool.Acquire<WebRequestFailureEventArgs>();
            webRequestFailureEventArgs.SerialId = serialId;
            webRequestFailureEventArgs.WebRequestUri = webRequestUri;
            webRequestFailureEventArgs.ErrorMessage = errorMessage;
            webRequestFailureEventArgs.UserData = userData;
            return webRequestFailureEventArgs;
        }

        /// <summary>
        /// Clears the web request failure event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            WebRequestUri = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
