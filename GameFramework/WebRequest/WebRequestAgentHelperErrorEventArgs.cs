//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.WebRequest
{
    /// <summary>
    /// Web request agent helper error event.
    /// </summary>
    public sealed class WebRequestAgentHelperErrorEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the WebRequestAgentHelperErrorEventArgs class.
        /// </summary>
        public WebRequestAgentHelperErrorEventArgs()
        {
            ErrorMessage = null;
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
        /// Creates a web request agent helper error event.
        /// </summary>
        /// <param name="errorMessage">Error message.</param>
        /// <returns>The created web request agent helper error event.</returns>
        public static WebRequestAgentHelperErrorEventArgs Create(string errorMessage)
        {
            WebRequestAgentHelperErrorEventArgs webRequestAgentHelperErrorEventArgs = ReferencePool.Acquire<WebRequestAgentHelperErrorEventArgs>();
            webRequestAgentHelperErrorEventArgs.ErrorMessage = errorMessage;
            return webRequestAgentHelperErrorEventArgs;
        }

        /// <summary>
        /// Clears the web request agent helper error event.
        /// </summary>
        public override void Clear()
        {
            ErrorMessage = null;
        }
    }
}
