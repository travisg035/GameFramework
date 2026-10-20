//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Task base class.
    /// </summary>
    internal abstract class TaskBase : IReference
    {
        /// <summary>
        /// Default task priority.
        /// </summary>
        public const int DefaultPriority = 0;

        private int m_SerialId;
        private string m_Tag;
        private int m_Priority;
        private object m_UserData;

        private bool m_Done;

        /// <summary>
        /// Initializes a new instance of the task base class.
        /// </summary>
        public TaskBase()
        {
            m_SerialId = 0;
            m_Tag = null;
            m_Priority = DefaultPriority;
            m_Done = false;
            m_UserData = null;
        }

        /// <summary>
        /// Gets the serial ID of the task.
        /// </summary>
        public int SerialId
        {
            get
            {
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
                return m_UserData;
            }
        }

        /// <summary>
        /// Gets or sets whether the task is done.
        /// </summary>
        public bool Done
        {
            get
            {
                return m_Done;
            }
            set
            {
                m_Done = value;
            }
        }

        /// <summary>
        /// Gets the task description.
        /// </summary>
        public virtual string Description
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Initializes the task base class.
        /// </summary>
        /// <param name="serialId">The serial ID of the task.</param>
        /// <param name="tag">The tag of the task.</param>
        /// <param name="priority">The priority of the task.</param>
        /// <param name="userData">The user custom data of the task.</param>
        internal void Initialize(int serialId, string tag, int priority, object userData)
        {
            m_SerialId = serialId;
            m_Tag = tag;
            m_Priority = priority;
            m_UserData = userData;
            m_Done = false;
        }

        /// <summary>
        /// Cleans up the task base class.
        /// </summary>
        public virtual void Clear()
        {
            m_SerialId = 0;
            m_Tag = null;
            m_Priority = DefaultPriority;
            m_UserData = null;
            m_Done = false;
        }
    }
}
