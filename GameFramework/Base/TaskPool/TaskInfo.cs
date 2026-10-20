//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Runtime.InteropServices;

namespace GameFramework
{
    /// <summary>
    /// Task information.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct TaskInfo
    {
        private readonly bool m_IsValid;
        private readonly int m_SerialId;
        private readonly string m_Tag;
        private readonly int m_Priority;
        private readonly object m_UserData;
        private readonly TaskStatus m_Status;
        private readonly string m_Description;

        /// <summary>
        /// Initializes a new instance of the task information.
        /// </summary>
        /// <param name="serialId">The serial ID of the task.</param>
        /// <param name="tag">The tag of the task.</param>
        /// <param name="priority">The priority of the task.</param>
        /// <param name="userData">The user custom data of the task.</param>
        /// <param name="status">The task status.</param>
        /// <param name="description">The task description.</param>
        public TaskInfo(int serialId, string tag, int priority, object userData, TaskStatus status, string description)
        {
            m_IsValid = true;
            m_SerialId = serialId;
            m_Tag = tag;
            m_Priority = priority;
            m_UserData = userData;
            m_Status = status;
            m_Description = description;
        }

        /// <summary>
        /// Gets whether the task information is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return m_IsValid;
            }
        }

        /// <summary>
        /// Gets the serial ID of the task.
        /// </summary>
        public int SerialId
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_SerialId;
            }
        }

        /// <summary>
        /// Gets the tag of the task.
        /// </summary>
        public string Tag
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Tag;
            }
        }

        /// <summary>
        /// Gets the priority of the task.
        /// </summary>
        public int Priority
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Priority;
            }
        }

        /// <summary>
        /// Gets the user custom data of the task.
        /// </summary>
        public object UserData
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_UserData;
            }
        }

        /// <summary>
        /// Gets the task status.
        /// </summary>
        public TaskStatus Status
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Status;
            }
        }

        /// <summary>
        /// Gets the task description.
        /// </summary>
        public string Description
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Description;
            }
        }
    }
}
