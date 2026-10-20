//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.ObjectPool
{
    /// <summary>
    /// Object base class.
    /// </summary>
    public abstract class ObjectBase : IReference
    {
        private string m_Name;
        private object m_Target;
        private bool m_Locked;
        private int m_Priority;
        private DateTime m_LastUseTime;

        /// <summary>
        /// Initializes a new instance of the ObjectBase class.
        /// </summary>
        public ObjectBase()
        {
            m_Name = null;
            m_Target = null;
            m_Locked = false;
            m_Priority = 0;
            m_LastUseTime = default(DateTime);
        }

        /// <summary>
        /// Gets the object name.
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// Gets the target object.
        /// </summary>
        public object Target
        {
            get
            {
                return m_Target;
            }
        }

        /// <summary>
        /// Gets or sets whether the object is locked.
        /// </summary>
        public bool Locked
        {
            get
            {
                return m_Locked;
            }
            set
            {
                m_Locked = value;
            }
        }

        /// <summary>
        /// Gets or sets the priority of the object.
        /// </summary>
        public int Priority
        {
            get
            {
                return m_Priority;
            }
            set
            {
                m_Priority = value;
            }
        }

        /// <summary>
        /// Gets the custom can release flag.
        /// </summary>
        public virtual bool CustomCanReleaseFlag
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the last use time of the object.
        /// </summary>
        public DateTime LastUseTime
        {
            get
            {
                return m_LastUseTime;
            }
            internal set
            {
                m_LastUseTime = value;
            }
        }

        /// <summary>
        /// Initializes the object base class.
        /// </summary>
        /// <param name="target">The target object.</param>
        protected void Initialize(object target)
        {
            Initialize(null, target, false, 0);
        }

        /// <summary>
        /// Initializes the object base class.
        /// </summary>
        /// <param name="name">The object name.</param>
        /// <param name="target">The target object.</param>
        protected void Initialize(string name, object target)
        {
            Initialize(name, target, false, 0);
        }

        /// <summary>
        /// Initializes the object base class.
        /// </summary>
        /// <param name="name">The object name.</param>
        /// <param name="target">The target object.</param>
        /// <param name="locked">Whether the object is locked.</param>
        protected void Initialize(string name, object target, bool locked)
        {
            Initialize(name, target, locked, 0);
        }

        /// <summary>
        /// Initializes the object base class.
        /// </summary>
        /// <param name="name">The object name.</param>
        /// <param name="target">The target object.</param>
        /// <param name="priority">The priority of the object.</param>
        protected void Initialize(string name, object target, int priority)
        {
            Initialize(name, target, false, priority);
        }

        /// <summary>
        /// Initializes the object base class.
        /// </summary>
        /// <param name="name">The object name.</param>
        /// <param name="target">The target object.</param>
        /// <param name="locked">Whether the object is locked.</param>
        /// <param name="priority">The priority of the object.</param>
        protected void Initialize(string name, object target, bool locked, int priority)
        {
            if (target == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Target '{0}' is invalid.", name));
            }

            m_Name = name ?? string.Empty;
            m_Target = target;
            m_Locked = locked;
            m_Priority = priority;
            m_LastUseTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Clears the object base class.
        /// </summary>
        public virtual void Clear()
        {
            m_Name = null;
            m_Target = null;
            m_Locked = false;
            m_Priority = 0;
            m_LastUseTime = default(DateTime);
        }

        /// <summary>
        /// Event called when the object is spawned.
        /// </summary>
        protected internal virtual void OnSpawn()
        {
        }

        /// <summary>
        /// Event called when the object is unspawned.
        /// </summary>
        protected internal virtual void OnUnspawn()
        {
        }

        /// <summary>
        /// Releases the object.
        /// </summary>
        /// <param name="isShutdown">Whether it is triggered when the object pool is shutting down.</param>
        protected internal abstract void Release(bool isShutdown);
    }
}
