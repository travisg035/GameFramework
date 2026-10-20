//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.ObjectPool
{
    internal sealed partial class ObjectPoolManager : GameFrameworkModule, IObjectPoolManager
    {
        /// <summary>
        /// Internal object.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        private sealed class Object<T> : IReference where T : ObjectBase
        {
            private T m_Object;
            private int m_SpawnCount;

            /// <summary>
            /// Initializes a new instance of the Object class.
            /// </summary>
            public Object()
            {
                m_Object = null;
                m_SpawnCount = 0;
            }

            /// <summary>
            /// Gets the object name.
            /// </summary>
            public string Name
            {
                get
                {
                    return m_Object.Name;
                }
            }

            /// <summary>
            /// Gets or sets whether the object is locked.
            /// </summary>
            public bool Locked
            {
                get
                {
                    return m_Object.Locked;
                }
                internal set
                {
                    m_Object.Locked = value;
                }
            }

            /// <summary>
            /// Gets or sets the priority of the object.
            /// </summary>
            public int Priority
            {
                get
                {
                    return m_Object.Priority;
                }
                internal set
                {
                    m_Object.Priority = value;
                }
            }

            /// <summary>
            /// Gets the custom can release flag.
            /// </summary>
            public bool CustomCanReleaseFlag
            {
                get
                {
                    return m_Object.CustomCanReleaseFlag;
                }
            }

            /// <summary>
            /// Gets the last use time of the object.
            /// </summary>
            public DateTime LastUseTime
            {
                get
                {
                    return m_Object.LastUseTime;
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

            /// <summary>
            /// Creates an internal object.
            /// </summary>
            /// <param name="obj">The object.</param>
            /// <param name="spawned">Whether the object has been spawned.</param>
            /// <returns>The created internal object.</returns>
            public static Object<T> Create(T obj, bool spawned)
            {
                if (obj == null)
                {
                    throw new GameFrameworkException("Object is invalid.");
                }

                Object<T> internalObject = ReferencePool.Acquire<Object<T>>();
                internalObject.m_Object = obj;
                internalObject.m_SpawnCount = spawned ? 1 : 0;
                if (spawned)
                {
                    obj.OnSpawn();
                }

                return internalObject;
            }

            /// <summary>
            /// Clears the internal object.
            /// </summary>
            public void Clear()
            {
                m_Object = null;
                m_SpawnCount = 0;
            }

            /// <summary>
            /// Peeks the object.
            /// </summary>
            /// <returns>The object.</returns>
            public T Peek()
            {
                return m_Object;
            }

            /// <summary>
            /// Spawns the object.
            /// </summary>
            /// <returns>The object.</returns>
            public T Spawn()
            {
                m_SpawnCount++;
                m_Object.LastUseTime = DateTime.UtcNow;
                m_Object.OnSpawn();
                return m_Object;
            }

            /// <summary>
            /// Unspawns the object.
            /// </summary>
            public void Unspawn()
            {
                m_Object.OnUnspawn();
                m_Object.LastUseTime = DateTime.UtcNow;
                m_SpawnCount--;
                if (m_SpawnCount < 0)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Object '{0}' spawn count is less than 0.", Name));
                }
            }

            /// <summary>
            /// Releases the object.
            /// </summary>
            /// <param name="isShutdown">Whether it is triggered when the object pool is shutting down.</param>
            public void Release(bool isShutdown)
            {
                m_Object.Release(isShutdown);
                ReferencePool.Release(m_Object);
            }
        }
    }
}
