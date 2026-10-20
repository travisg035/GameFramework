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
    /// Web request manager interface.
    /// </summary>
    public interface IWebRequestManager
    {
        /// <summary>
        /// Gets the total count of web request agents.
        /// </summary>
        int TotalAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets the count of free web request agents.
        /// </summary>
        int FreeAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets the count of working web request agents.
        /// </summary>
        int WorkingAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets the count of waiting web request tasks.
        /// </summary>
        int WaitingTaskCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the web request timeout in seconds.
        /// </summary>
        float Timeout
        {
            get;
            set;
        }

        /// <summary>
        /// Web request start event.
        /// </summary>
        event EventHandler<WebRequestStartEventArgs> WebRequestStart;

        /// <summary>
        /// Web request success event.
        /// </summary>
        event EventHandler<WebRequestSuccessEventArgs> WebRequestSuccess;

        /// <summary>
        /// Web request failure event.
        /// </summary>
        event EventHandler<WebRequestFailureEventArgs> WebRequestFailure;

        /// <summary>
        /// Adds a web request agent helper.
        /// </summary>
        /// <param name="webRequestAgentHelper">The web request agent helper to add.</param>
        void AddWebRequestAgentHelper(IWebRequestAgentHelper webRequestAgentHelper);

        /// <summary>
        /// Gets web request task info by serial id.
        /// </summary>
        /// <param name="serialId">The serial id of the web request task to get.</param>
        /// <returns>The web request task info.</returns>
        TaskInfo GetWebRequestInfo(int serialId);

        /// <summary>
        /// Gets web request task infos by tag.
        /// </summary>
        /// <param name="tag">The tag of the web request task to get.</param>
        /// <returns>The web request task infos.</returns>
        TaskInfo[] GetWebRequestInfos(string tag);

        /// <summary>
        /// Gets web request task infos by tag.
        /// </summary>
        /// <param name="tag">The tag of the web request task to get.</param>
        /// <param name="results">The web request task infos.</param>
        void GetAllWebRequestInfos(string tag, List<TaskInfo> results);

        /// <summary>
        /// Gets all web request task infos.
        /// </summary>
        /// <returns>All web request task infos.</returns>
        TaskInfo[] GetAllWebRequestInfos();

        /// <summary>
        /// Gets all web request task infos.
        /// </summary>
        /// <param name="results">All web request task infos.</param>
        void GetAllWebRequestInfos(List<TaskInfo> results);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, byte[] postData);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, string tag);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, int priority);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, object userData);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, byte[] postData, string tag);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, byte[] postData, int priority);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, byte[] postData, object userData);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, string tag, int priority);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, string tag, object userData);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, int priority, object userData);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, byte[] postData, string tag, int priority);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, byte[] postData, string tag, object userData);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, byte[] postData, int priority, object userData);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, string tag, int priority, object userData);

        /// <summary>
        /// Adds a web request task.
        /// </summary>
        /// <param name="webRequestUri">Web request URI.</param>
        /// <param name="postData">The post data to send.</param>
        /// <param name="tag">Web request task tag.</param>
        /// <param name="priority">Web request task priority.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The serial id of the added web request task.</returns>
        int AddWebRequest(string webRequestUri, byte[] postData, string tag, int priority, object userData);

        /// <summary>
        /// Removes a web request task by serial id.
        /// </summary>
        /// <param name="serialId">The serial id of the web request task to remove.</param>
        /// <returns>Whether the web request task was removed successfully.</returns>
        bool RemoveWebRequest(int serialId);

        /// <summary>
        /// Removes web request tasks by tag.
        /// </summary>
        /// <param name="tag">The tag of the web request tasks to remove.</param>
        /// <returns>The count of removed web request tasks.</returns>
        int RemoveWebRequests(string tag);

        /// <summary>
        /// Removes all web request tasks.
        /// </summary>
        /// <returns>The count of removed web request tasks.</returns>
        int RemoveAllWebRequests();
    }
}
