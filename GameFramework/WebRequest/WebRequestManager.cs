//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.WebRequest
{
    /// <summary>
    /// Web request manager.
    /// </summary>
    internal sealed partial class WebRequestManager : GameFrameworkModule, IWebRequestManager
    {
        private readonly TaskPool<WebRequestTask> m_TaskPool;
        private float m_Timeout;
        private EventHandler<WebRequestStartEventArgs> m_WebRequestStartEventHandler;
        private EventHandler<WebRequestSuccessEventArgs> m_WebRequestSuccessEventHandler;
        private EventHandler<WebRequestFailureEventArgs> m_WebRequestFailureEventHandler;

        /// <summary>
        /// Initializes a new instance of the WebRequestManager class.
        /// </summary>
        public WebRequestManager()
        {
            m_TaskPool = new TaskPool<WebRequestTask>();
            m_Timeout = 30f;
            m_WebRequestStartEventHandler = null;
            m_WebRequestSuccessEventHandler = null;
            m_WebRequestFailureEventHandler = null;
        }

        /// <summary>
        /// Gets the total count of web request agents.
        /// </summary>
        public int TotalAgentCount
        {
            get
            {
                return m_TaskPool.TotalAgentCount;
            }
        }

        /// <summary>
        /// Gets the count of free web request agents.
        /// </summary>
        public int FreeAgentCount
        {
            get
            {
                return m_TaskPool.FreeAgentCount;
            }
        }

        /// <summary>
        /// Gets the count of working web request agents.
        /// </summary>
        public int WorkingAgentCount
        {
            get
            {
                return m_TaskPool.WorkingAgentCount;
            }
        }

        /// <summary>
        /// Gets the count of waiting web request tasks.
        /// </summary>
        public int WaitingTaskCount
        {
            get
            {
                return m_TaskPool.WaitingTaskCount;
            }
        }

        /// <summary>
        /// Gets or sets the web request timeout in seconds.
        /// </summary>
        public float Timeout
        {
            get
            {
                return m_Timeout;
            }
            set
            {
                m_Timeout = value;
            }
        }

        /// <summary>
        /// Web request start event.
        /// </summary>
        public event EventHandler<WebRequestStartEventArgs> WebRequestStart
        {
            add
            {
                m_WebRequestStartEventHandler += value;
            }
            remove
            {
                m_WebRequestStartEventHandler -= value;
            }
        }

        /// <summary>
        /// Web request success event.
        /// </summary>
        public event EventHandler<WebRequestSuccessEventArgs> WebRequestSuccess
        {
            add
            {
                m_WebRequestSuccessEventHandler += value;
            }
            remove
            {
                m_WebRequestSuccessEventHandler -= value;
            }
        }

        /// <summary>
        /// Web request failure event.
        /// </summary>
        public event EventHandler<WebRequestFailureEventArgs> WebRequestFailure
        {
            add
            {
                m_WebRequestFailureEventHandler += value;
            }
            remove
            {
                m_WebRequestFailureEventHandler -= value;
            }
        }

        /// <summary>
        /// Polls the web request manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_TaskPool.Update(elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// Shuts down and cleans up the web request manager.
        /// </summary>
        internal override void Shutdown()
        {
            m_TaskPool.Shutdown();
        }

        /// <summary>
        /// Adds a web request agent helper.
        /// </summary>
        /// <param name="webRequestAgentHelper">The web request agent helper to add.</param>
        public void AddWebRequestAgentHelper(IWebRequestAgentHelper webRequestAgentHelper)
        {
            WebRequestAgent agent = new WebRequestAgent(webRequestAgentHelper);
            agent.WebRequestAgentStart += OnWebRequestAgentStart;
            agent.WebRequestAgentSuccess += OnWebRequestAgentSuccess;
            agent.WebRequestAgentFailure += OnWebRequestAgentFailure;

            m_TaskPool.AddAgent(agent);
        }

        /// <summary>
        /// Gets web request task info by serial id.
        /// </summary>
        /// <param name="serialId">The serial id of the web request task to get.</param>
        /// <returns>The web request task info.</returns>
        public TaskInfo GetWebRequestInfo(int serialId)
        {
            return m_TaskPool.GetTaskInfo(serialId);
        }

        /// <summary>
        /// Gets web request task infos by tag.
        /// </summary>
        /// <param name="tag">The tag of the web request task to get.</param>
        /// <returns>The web request task infos.</returns>
        public TaskInfo[] GetWebRequestInfos(string tag)
        {
            return m_TaskPool.GetTaskInfos(tag);
        }

        /// <summary>
        /// Gets web request task infos by tag.
        /// </summary>
        /// <param name="tag">The tag of the web request task to get.</param>
        /// <param name="results">The web request task infos.</param>
        public void GetAllWebRequestInfos(string tag, List<TaskInfo> results)
        {
            m_TaskPool.GetTaskInfos(tag, results);
        }

        /// <summary>
        /// Gets all web request task infos.
        /// </summary>
        /// <returns>All web request task infos.</returns>
        public TaskInfo[] GetAllWebRequestInfos()
        {
            return m_TaskPool.GetAllTaskInfos();
        }

        /// <summary>
        /// Gets all web request task infos.
        /// </summary>
        /// <param name="results">All web request task infos.</param>
        public void GetAllWebRequestInfos(List<TaskInfo> results)
        {
            m_TaskPool.GetAllTaskInfos(results);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri)
        {
            return AddWebRequest(webRequestUri, null, null, Constant.DefaultPriority, null);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, byte[] postData)
        {
            return AddWebRequest(webRequestUri, postData, null, Constant.DefaultPriority, null);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, string tag)
        {
            return AddWebRequest(webRequestUri, null, tag, Constant.DefaultPriority, null);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, int priority)
        {
            return AddWebRequest(webRequestUri, null, null, priority, null);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, object userData)
        {
            return AddWebRequest(webRequestUri, null, null, Constant.DefaultPriority, userData);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, byte[] postData, string tag)
        {
            return AddWebRequest(webRequestUri, postData, tag, Constant.DefaultPriority, null);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, byte[] postData, int priority)
        {
            return AddWebRequest(webRequestUri, postData, null, priority, null);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, byte[] postData, object userData)
        {
            return AddWebRequest(webRequestUri, postData, null, Constant.DefaultPriority, userData);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, string tag, int priority)
        {
            return AddWebRequest(webRequestUri, null, tag, priority, null);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, string tag, object userData)
        {
            return AddWebRequest(webRequestUri, null, tag, Constant.DefaultPriority, userData);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, int priority, object userData)
        {
            return AddWebRequest(webRequestUri, null, null, priority, userData);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, byte[] postData, string tag, int priority)
        {
            return AddWebRequest(webRequestUri, postData, tag, priority, null);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, byte[] postData, string tag, object userData)
        {
            return AddWebRequest(webRequestUri, postData, tag, Constant.DefaultPriority, userData);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, byte[] postData, int priority, object userData)
        {
            return AddWebRequest(webRequestUri, postData, null, priority, userData);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, string tag, int priority, object userData)
        {
            return AddWebRequest(webRequestUri, null, tag, priority, userData);
        }

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        public int AddWebRequest(string webRequestUri, byte[] postData, string tag, int priority, object userData)
        {
            if (string.IsNullOrEmpty(webRequestUri))
            {
                throw new GameFrameworkException("Web request uri is invalid.");
            }

            if (TotalAgentCount <= 0)
            {
                throw new GameFrameworkException("You must add web request agent first.");
            }

            WebRequestTask webRequestTask = WebRequestTask.Create(webRequestUri, postData, tag, priority, m_Timeout, userData);
            m_TaskPool.AddTask(webRequestTask);
            return webRequestTask.SerialId;
        }

        /// <summary>
        /// Removes a web request task by serial id.
        /// </summary>
        /// <param name="serialId">The serial id of the web request task to remove.</param>
        /// <returns>Whether the web request task was removed successfully.</returns>
        public bool RemoveWebRequest(int serialId)
        {
            return m_TaskPool.RemoveTask(serialId);
        }

        /// <summary>
        /// Removes web request tasks by tag.
        /// </summary>
        /// <param name="tag">The tag of the web request tasks to remove.</param>
        /// <returns>The count of removed web request tasks.</returns>
        public int RemoveWebRequests(string tag)
        {
            return m_TaskPool.RemoveTasks(tag);
        }

        /// <summary>
        /// Removes all web request tasks.
        /// </summary>
        /// <returns>The count of removed web request tasks.</returns>
        public int RemoveAllWebRequests()
        {
            return m_TaskPool.RemoveAllTasks();
        }

        private void OnWebRequestAgentStart(WebRequestAgent sender)
        {
            if (m_WebRequestStartEventHandler != null)
            {
                WebRequestStartEventArgs webRequestStartEventArgs = WebRequestStartEventArgs.Create(sender.Task.SerialId, sender.Task.WebRequestUri, sender.Task.UserData);
                m_WebRequestStartEventHandler(this, webRequestStartEventArgs);
                ReferencePool.Release(webRequestStartEventArgs);
            }
        }

        private void OnWebRequestAgentSuccess(WebRequestAgent sender, byte[] webResponseBytes)
        {
            if (m_WebRequestSuccessEventHandler != null)
            {
                WebRequestSuccessEventArgs webRequestSuccessEventArgs = WebRequestSuccessEventArgs.Create(sender.Task.SerialId, sender.Task.WebRequestUri, webResponseBytes, sender.Task.UserData);
                m_WebRequestSuccessEventHandler(this, webRequestSuccessEventArgs);
                ReferencePool.Release(webRequestSuccessEventArgs);
            }
        }

        private void OnWebRequestAgentFailure(WebRequestAgent sender, string errorMessage)
        {
            if (m_WebRequestFailureEventHandler != null)
            {
                WebRequestFailureEventArgs webRequestFailureEventArgs = WebRequestFailureEventArgs.Create(sender.Task.SerialId, sender.Task.WebRequestUri, errorMessage, sender.Task.UserData);
                m_WebRequestFailureEventHandler(this, webRequestFailureEventArgs);
                ReferencePool.Release(webRequestFailureEventArgs);
            }
        }
    }
}
