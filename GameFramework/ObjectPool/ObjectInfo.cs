//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace GameFramework.ObjectPool
{
    /// <summary>
    /// Object information.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct ObjectInfo
    {
        private readonly string m_Name;
        private readonly bool m_Locked;
        private readonly bool m_CustomCanReleaseFlag;
        private readonly int m_Priority;
        private readonly DateTime m_LastUseTime;
        private readonly int m_SpawnCount;

        /// <summary>
        /// Initializes a new instance of the ObjectInfo struct.
        /// </summary>
        /// <param name="name">The object name.</param>
        /// <param name="locked">Whether the object is locked.</param>
        /// <param name="customCanReleaseFlag">Custom can release flag of the object.</param>
        /// <param name="priority">Priority of the object.</param>
        /// <param name="lastUseTime">The last use time of the object.</param>
        /// <param name="spawnCount">The spawn count of the object.</param>
        public ObjectInfo(string name, bool locked, bool customCanReleaseFlag, int priority, DateTime lastUseTime, int spawnCount)
        {
            m_Name = name;
            m_Locked = locked;
            m_CustomCanReleaseFlag = customCanReleaseFlag;
            m_Priority = priority;
            m_LastUseTime = lastUseTime;
            m_SpawnCount = spawnCount;
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
        /// Gets whether the object is locked.
        /// </summary>
        public bool Locked
        {
            get
            {
                return m_Locked;
            }
        }

        /// <summary>
        /// Gets the custom can release flag of the object.
        /// </summary>
        public bool CustomCanReleaseFlag
        {
            get
            {
                return m_CustomCanReleaseFlag;
            }
        }

        /// <summary>
        /// Gets the priority of the object.
        /// </summary>
        public int Priority
        {
            get
            {
                return m_Priority;
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
        }

        /// <summary>
        /// Gets whether the object is in use.
        /// </summary>
        public bool IsInUse
        {
            get
            {
                return m_SpawnCount > 0;
            }
        }

        /// <summary>
        /// Gets the spawn count of the object.
        /// </summary>
        public int SpawnCount
        {
            get
            {
                return m_SpawnCount;
            }
        }
    }
}
