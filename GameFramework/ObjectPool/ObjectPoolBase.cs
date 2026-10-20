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
    /// Object pool base class.
    /// </summary>
    public abstract class ObjectPoolBase
    {
        private readonly string m_Name;

        /// <summary>
        /// Initializes a new instance of the ObjectPoolBase class.
        /// </summary>
        public ObjectPoolBase()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ObjectPoolBase class.
        /// </summary>
        /// <param name="name">The object pool name.</param>
        public ObjectPoolBase(string name)
        {
            m_Name = name ?? string.Empty;
        }

        /// <summary>
        /// Gets the object pool name.
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// Gets the full name of the object pool.
        /// </summary>
        public string FullName
        {
            get
            {
                return new TypeNamePair(ObjectType, m_Name).ToString();
            }
        }

        /// <summary>
        /// Gets the object type of the object pool.
        /// </summary>
        public abstract Type ObjectType
        {
            get;
        }

        /// <summary>
        /// Gets the count of objects in the object pool.
        /// </summary>
        public abstract int Count
        {
            get;
        }

        /// <summary>
        /// Gets the count of releasable objects in the object pool.
        /// </summary>
        public abstract int CanReleaseCount
        {
            get;
        }

        /// <summary>
        /// Gets whether the object is allowed to be spawned multiple times.
        /// </summary>
        public abstract bool AllowMultiSpawn
        {
            get;
        }

        /// <summary>
        /// Gets or sets the interval in seconds for automatically releasing releasable objects in the object pool.
        /// </summary>
        public abstract float AutoReleaseInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the capacity of the object pool.
        /// </summary>
        public abstract int Capacity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the expire time in seconds for objects in the object pool.
        /// </summary>
        public abstract float ExpireTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the priority of the object pool.
        /// </summary>
        public abstract int Priority
        {
            get;
            set;
        }

        /// <summary>
        /// Releases releasable objects in the object pool.
        /// </summary>
        public abstract void Release();

        /// <summary>
        /// Releases releasable objects in the object pool.
        /// </summary>
        /// <param name="toReleaseCount">The count of objects to attempt to release.</param>
        public abstract void Release(int toReleaseCount);

        /// <summary>
        /// Releases all unused objects in the object pool.
        /// </summary>
        public abstract void ReleaseAllUnused();

        /// <summary>
        /// Gets all object information.
        /// </summary>
        /// <returns>All object information.</returns>
        public abstract ObjectInfo[] GetAllObjectInfos();

        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        internal abstract void Shutdown();
    }
}
