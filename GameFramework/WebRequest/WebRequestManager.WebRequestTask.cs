//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.WebRequest
{
    internal sealed partial class WebRequestManager : GameFrameworkModule, IWebRequestManager
    {
        /// <summary>
        /// Web request task.
        /// </summary>
        private sealed class WebRequestTask : TaskBase
        {
            private static int s_Serial = 0;

            private WebRequestTaskStatus m_Status;
            private string m_WebRequestUri;
            private byte[] m_PostData;
            private float m_Timeout;

            public WebRequestTask()
            {
                m_Status = WebRequestTaskStatus.Todo;
                m_WebRequestUri = null;
                m_PostData = null;
                m_Timeout = 0f;
            }

            /// <summary>
            /// Gets or sets the status of the web request task.
            /// </summary>
            public WebRequestTaskStatus Status
            {
                get
                {
                    return m_Status;
                }
                set
                {
                    m_Status = value;
                }
            }

            /// <summary>
            /// Gets the remote web request URI.
            /// </summary>
            public string WebRequestUri
            {
                get
                {
                    return m_WebRequestUri;
                }
            }

            /// <summary>
            /// Gets the web request timeout in seconds.
            /// </summary>
            public float Timeout
            {
                get
                {
                    return m_Timeout;
                }
            }

            /// <summary>
            /// Gets the description of the web request task.
            /// </summary>
            public override string Description
            {
                get
                {
                    return m_WebRequestUri;
                }
            }

            /// <summary>
            /// Creates a web request task.
            /// </summary>
            /// <param name="webRequestUri">Remote web request URI.</param>
            /// <param name="postData">The post data to send.</param>
            /// <param name="tag">Web request task tag.</param>
            /// <param name="priority">Web request task priority.</param>
            /// <param name="timeout">Web request timeout in seconds.</param>
            /// <param name="userData">Custom user data.</param>
            /// <returns>The created web request task.</returns>
            public static WebRequestTask Create(string webRequestUri, byte[] postData, string tag, int priority, float timeout, object userData)
            {
                WebRequestTask webRequestTask = ReferencePool.Acquire<WebRequestTask>();
                webRequestTask.Initialize(++s_Serial, tag, priority, userData);
                webRequestTask.m_WebRequestUri = webRequestUri;
                webRequestTask.m_PostData = postData;
                webRequestTask.m_Timeout = timeout;
                return webRequestTask;
            }

            /// <summary>
            /// Clears the web request task.
            /// </summary>
            public override void Clear()
            {
                base.Clear();
                m_Status = WebRequestTaskStatus.Todo;
                m_WebRequestUri = null;
                m_PostData = null;
                m_Timeout = 0f;
            }

            /// <summary>
            /// Gets the post data to send.
            /// </summary>
            public byte[] GetPostData()
            {
                return m_PostData;
            }
        }
    }
}
