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
        /// Web request agent.
        /// </summary>
        private sealed class WebRequestAgent : ITaskAgent<WebRequestTask>
        {
            private readonly IWebRequestAgentHelper m_Helper;
            private WebRequestTask m_Task;
            private float m_WaitTime;

            public GameFrameworkAction<WebRequestAgent> WebRequestAgentStart;
            public GameFrameworkAction<WebRequestAgent, byte[]> WebRequestAgentSuccess;
            public GameFrameworkAction<WebRequestAgent, string> WebRequestAgentFailure;

            /// <summary>
            /// Initializes a new instance of the WebRequestAgent class.
            /// </summary>
            /// <param name="webRequestAgentHelper">Web request agent helper.</param>
            public WebRequestAgent(IWebRequestAgentHelper webRequestAgentHelper)
            {
                if (webRequestAgentHelper == null)
                {
                    throw new GameFrameworkException("Web request agent helper is invalid.");
                }

                m_Helper = webRequestAgentHelper;
                m_Task = null;
                m_WaitTime = 0f;

                WebRequestAgentStart = null;
                WebRequestAgentSuccess = null;
                WebRequestAgentFailure = null;
            }

            /// <summary>
            /// Gets the web request task.
            /// </summary>
            public WebRequestTask Task
            {
                get
                {
                    return m_Task;
                }
            }

            /// <summary>
            /// Gets the elapsed wait time.
            /// </summary>
            public float WaitTime
            {
                get
                {
                    return m_WaitTime;
                }
            }

            /// <summary>
            /// Initializes the web request agent.
            /// </summary>
            public void Initialize()
            {
                m_Helper.WebRequestAgentHelperComplete += OnWebRequestAgentHelperComplete;
                m_Helper.WebRequestAgentHelperError += OnWebRequestAgentHelperError;
            }

            /// <summary>
            /// Polls the web request agent.
            /// </summary>
            /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
            /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                if (m_Task.Status == WebRequestTaskStatus.Doing)
                {
                    m_WaitTime += realElapseSeconds;
                    if (m_WaitTime >= m_Task.Timeout)
                    {
                        WebRequestAgentHelperErrorEventArgs webRequestAgentHelperErrorEventArgs = WebRequestAgentHelperErrorEventArgs.Create("Timeout");
                        OnWebRequestAgentHelperError(this, webRequestAgentHelperErrorEventArgs);
                        ReferencePool.Release(webRequestAgentHelperErrorEventArgs);
                    }
                }
            }

            /// <summary>
            /// Shuts down and cleans up the web request agent.
            /// </summary>
            public void Shutdown()
            {
                Reset();
                m_Helper.WebRequestAgentHelperComplete -= OnWebRequestAgentHelperComplete;
                m_Helper.WebRequestAgentHelperError -= OnWebRequestAgentHelperError;
            }

            /// <summary>
            /// Starts processing the web request task.
            /// </summary>
            /// <param name="task">The web request task to process.</param>
            /// <returns>The start task status.</returns>
            public StartTaskStatus Start(WebRequestTask task)
            {
                if (task == null)
                {
                    throw new GameFrameworkException("Task is invalid.");
                }

                m_Task = task;
                m_Task.Status = WebRequestTaskStatus.Doing;

                if (WebRequestAgentStart != null)
                {
                    WebRequestAgentStart(this);
                }

                byte[] postData = m_Task.GetPostData();
                if (postData == null)
                {
                    m_Helper.Request(m_Task.WebRequestUri, m_Task.UserData);
                }
                else
                {
                    m_Helper.Request(m_Task.WebRequestUri, postData, m_Task.UserData);
                }

                m_WaitTime = 0f;
                return StartTaskStatus.CanResume;
            }

            /// <summary>
            /// Resets the web request agent.
            /// </summary>
            public void Reset()
            {
                m_Helper.Reset();
                m_Task = null;
                m_WaitTime = 0f;
            }

            private void OnWebRequestAgentHelperComplete(object sender, WebRequestAgentHelperCompleteEventArgs e)
            {
                m_Helper.Reset();
                m_Task.Status = WebRequestTaskStatus.Done;

                if (WebRequestAgentSuccess != null)
                {
                    WebRequestAgentSuccess(this, e.GetWebResponseBytes());
                }

                m_Task.Done = true;
            }

            private void OnWebRequestAgentHelperError(object sender, WebRequestAgentHelperErrorEventArgs e)
            {
                m_Helper.Reset();
                m_Task.Status = WebRequestTaskStatus.Error;

                if (WebRequestAgentFailure != null)
                {
                    WebRequestAgentFailure(this, e.ErrorMessage);
                }

                m_Task.Done = true;
            }
        }
    }
}
