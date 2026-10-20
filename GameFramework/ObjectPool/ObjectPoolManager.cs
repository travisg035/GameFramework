//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.ObjectPool
{
    /// <summary>
    /// Object pool manager.
    /// </summary>
    internal sealed partial class ObjectPoolManager : GameFrameworkModule, IObjectPoolManager
    {
        private const int DefaultCapacity = int.MaxValue;
        private const float DefaultExpireTime = float.MaxValue;
        private const int DefaultPriority = 0;

        private readonly Dictionary<TypeNamePair, ObjectPoolBase> m_ObjectPools;
        private readonly List<ObjectPoolBase> m_CachedAllObjectPools;
        private readonly Comparison<ObjectPoolBase> m_ObjectPoolComparer;

        /// <summary>
        /// Initializes a new instance of the ObjectPoolManager class.
        /// </summary>
        public ObjectPoolManager()
        {
            m_ObjectPools = new Dictionary<TypeNamePair, ObjectPoolBase>();
            m_CachedAllObjectPools = new List<ObjectPoolBase>();
            m_ObjectPoolComparer = ObjectPoolComparer;
        }

        /// <summary>
        /// Gets the game framework module priority.
        /// </summary>
        /// <remarks>Modules with higher priority are polled first and shut down later.</remarks>
        internal override int Priority
        {
            get
            {
                return 6;
            }
        }

        /// <summary>
        /// Gets the count of object pools.
        /// </summary>
        public int Count
        {
            get
            {
                return m_ObjectPools.Count;
            }
        }

        /// <summary>
        /// Polls the object pool manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in m_ObjectPools)
            {
                objectPool.Value.Update(elapseSeconds, realElapseSeconds);
            }
        }

        /// <summary>
        /// Shuts down and cleans up the object pool manager.
        /// </summary>
        internal override void Shutdown()
        {
            foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in m_ObjectPools)
            {
                objectPool.Value.Shutdown();
            }

            m_ObjectPools.Clear();
            m_CachedAllObjectPools.Clear();
        }

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>Whether the object pool exists.</returns>
        public bool HasObjectPool<T>() where T : ObjectBase
        {
            return InternalHasObjectPool(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>Whether the object pool exists.</returns>
        public bool HasObjectPool(Type objectType)
        {
            if (objectType == null)
            {
                throw new GameFrameworkException("Object type is invalid.");
            }

            if (!typeof(ObjectBase).IsAssignableFrom(objectType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Object type '{0}' is invalid.", objectType.FullName));
            }

            return InternalHasObjectPool(new TypeNamePair(objectType));
        }

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <returns>Whether the object pool exists.</returns>
        public bool HasObjectPool<T>(string name) where T : ObjectBase
        {
            return InternalHasObjectPool(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <returns>Whether the object pool exists.</returns>
        public bool HasObjectPool(Type objectType, string name)
        {
            if (objectType == null)
            {
                throw new GameFrameworkException("Object type is invalid.");
            }

            if (!typeof(ObjectBase).IsAssignableFrom(objectType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Object type '{0}' is invalid.", objectType.FullName));
            }

            return InternalHasObjectPool(new TypeNamePair(objectType, name));
        }

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns>Whether the object pool exists.</returns>
        public bool HasObjectPool(Predicate<ObjectPoolBase> condition)
        {
            if (condition == null)
            {
                throw new GameFrameworkException("Condition is invalid.");
            }

            foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in m_ObjectPools)
            {
                if (condition(objectPool.Value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The object pool to get.</returns>
        public IObjectPool<T> GetObjectPool<T>() where T : ObjectBase
        {
            return (IObjectPool<T>)InternalGetObjectPool(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>The object pool to get.</returns>
        public ObjectPoolBase GetObjectPool(Type objectType)
        {
            if (objectType == null)
            {
                throw new GameFrameworkException("Object type is invalid.");
            }

            if (!typeof(ObjectBase).IsAssignableFrom(objectType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Object type '{0}' is invalid.", objectType.FullName));
            }

            return InternalGetObjectPool(new TypeNamePair(objectType));
        }

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <returns>The object pool to get.</returns>
        public IObjectPool<T> GetObjectPool<T>(string name) where T : ObjectBase
        {
            return (IObjectPool<T>)InternalGetObjectPool(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <returns>The object pool to get.</returns>
        public ObjectPoolBase GetObjectPool(Type objectType, string name)
        {
            if (objectType == null)
            {
                throw new GameFrameworkException("Object type is invalid.");
            }

            if (!typeof(ObjectBase).IsAssignableFrom(objectType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Object type '{0}' is invalid.", objectType.FullName));
            }

            return InternalGetObjectPool(new TypeNamePair(objectType, name));
        }

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns>The object pool to get.</returns>
        public ObjectPoolBase GetObjectPool(Predicate<ObjectPoolBase> condition)
        {
            if (condition == null)
            {
                throw new GameFrameworkException("Condition is invalid.");
            }

            foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in m_ObjectPools)
            {
                if (condition(objectPool.Value))
                {
                    return objectPool.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets object pools.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns>The object pools to get.</returns>
        public ObjectPoolBase[] GetObjectPools(Predicate<ObjectPoolBase> condition)
        {
            if (condition == null)
            {
                throw new GameFrameworkException("Condition is invalid.");
            }

            List<ObjectPoolBase> results = new List<ObjectPoolBase>();
            foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in m_ObjectPools)
            {
                if (condition(objectPool.Value))
                {
                    results.Add(objectPool.Value);
                }
            }

            return results.ToArray();
        }

        /// <summary>
        /// Gets object pools.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="results">The list to store the object pools.</param>
        public void GetObjectPools(Predicate<ObjectPoolBase> condition, List<ObjectPoolBase> results)
        {
            if (condition == null)
            {
                throw new GameFrameworkException("Condition is invalid.");
            }

            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in m_ObjectPools)
            {
                if (condition(objectPool.Value))
                {
                    results.Add(objectPool.Value);
                }
            }
        }

        /// <summary>
        /// Gets all object pools.
        /// </summary>
        /// <returns>All object pools.</returns>
        public ObjectPoolBase[] GetAllObjectPools()
        {
            return GetAllObjectPools(false);
        }

        /// <summary>
        /// Gets all object pools.
        /// </summary>
        /// <param name="results">The list to store all object pools.</param>
        public void GetAllObjectPools(List<ObjectPoolBase> results)
        {
            GetAllObjectPools(false, results);
        }

        /// <summary>
        /// Gets all object pools.
        /// </summary>
        /// <param name="sort">Whether to sort by object pool priority.</param>
        /// <returns>All object pools.</returns>
        public ObjectPoolBase[] GetAllObjectPools(bool sort)
        {
            if (sort)
            {
                List<ObjectPoolBase> results = new List<ObjectPoolBase>();
                foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in m_ObjectPools)
                {
                    results.Add(objectPool.Value);
                }

                results.Sort(m_ObjectPoolComparer);
                return results.ToArray();
            }
            else
            {
                int index = 0;
                ObjectPoolBase[] results = new ObjectPoolBase[m_ObjectPools.Count];
                foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in m_ObjectPools)
                {
                    results[index++] = objectPool.Value;
                }

                return results;
            }
        }

        /// <summary>
        /// Gets all object pools.
        /// </summary>
        /// <param name="sort">Whether to sort by object pool priority.</param>
        /// <param name="results">The list to store all object pools.</param>
        public void GetAllObjectPools(bool sort, List<ObjectPoolBase> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in m_ObjectPools)
            {
                results.Add(objectPool.Value);
            }

            if (sort)
            {
                results.Sort(m_ObjectPoolComparer);
            }
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>() where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, false, DefaultExpireTime, DefaultCapacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType)
        {
            return InternalCreateObjectPool(objectType, string.Empty, false, DefaultExpireTime, DefaultCapacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, false, DefaultExpireTime, DefaultCapacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name)
        {
            return InternalCreateObjectPool(objectType, name, false, DefaultExpireTime, DefaultCapacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, false, DefaultExpireTime, capacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity)
        {
            return InternalCreateObjectPool(objectType, string.Empty, false, DefaultExpireTime, capacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(float expireTime) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, false, expireTime, DefaultCapacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, float expireTime)
        {
            return InternalCreateObjectPool(objectType, string.Empty, false, expireTime, DefaultCapacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, false, DefaultExpireTime, capacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity)
        {
            return InternalCreateObjectPool(objectType, name, false, DefaultExpireTime, capacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float expireTime) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, false, expireTime, DefaultCapacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float expireTime)
        {
            return InternalCreateObjectPool(objectType, name, false, expireTime, DefaultCapacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, float expireTime) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, false, expireTime, capacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, float expireTime)
        {
            return InternalCreateObjectPool(objectType, string.Empty, false, expireTime, capacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, false, DefaultExpireTime, capacity, DefaultExpireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, int priority)
        {
            return InternalCreateObjectPool(objectType, string.Empty, false, DefaultExpireTime, capacity, DefaultExpireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, false, expireTime, DefaultCapacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, string.Empty, false, expireTime, DefaultCapacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, float expireTime) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, false, expireTime, capacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, float expireTime)
        {
            return InternalCreateObjectPool(objectType, name, false, expireTime, capacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, false, DefaultExpireTime, capacity, DefaultExpireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, int priority)
        {
            return InternalCreateObjectPool(objectType, name, false, DefaultExpireTime, capacity, DefaultExpireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, false, expireTime, DefaultCapacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, name, false, expireTime, DefaultCapacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, false, expireTime, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, string.Empty, false, expireTime, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, false, expireTime, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, name, false, expireTime, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="autoReleaseInterval">The interval in seconds for automatically releasing releasable objects in the object pool.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float autoReleaseInterval, int capacity, float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, false, autoReleaseInterval, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="autoReleaseInterval">The interval in seconds for automatically releasing releasable objects in the object pool.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float autoReleaseInterval, int capacity, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, name, false, autoReleaseInterval, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>() where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, true, DefaultExpireTime, DefaultCapacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType)
        {
            return InternalCreateObjectPool(objectType, string.Empty, true, DefaultExpireTime, DefaultCapacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, true, DefaultExpireTime, DefaultCapacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name)
        {
            return InternalCreateObjectPool(objectType, name, true, DefaultExpireTime, DefaultCapacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, true, DefaultExpireTime, capacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity)
        {
            return InternalCreateObjectPool(objectType, string.Empty, true, DefaultExpireTime, capacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(float expireTime) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, true, expireTime, DefaultCapacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, float expireTime)
        {
            return InternalCreateObjectPool(objectType, string.Empty, true, expireTime, DefaultCapacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, true, DefaultExpireTime, capacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity)
        {
            return InternalCreateObjectPool(objectType, name, true, DefaultExpireTime, capacity, DefaultExpireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float expireTime) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, true, expireTime, DefaultCapacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float expireTime)
        {
            return InternalCreateObjectPool(objectType, name, true, expireTime, DefaultCapacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, float expireTime) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, true, expireTime, capacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, float expireTime)
        {
            return InternalCreateObjectPool(objectType, string.Empty, true, expireTime, capacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, true, DefaultExpireTime, capacity, DefaultExpireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, int priority)
        {
            return InternalCreateObjectPool(objectType, string.Empty, true, DefaultExpireTime, capacity, DefaultExpireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, true, expireTime, DefaultCapacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, string.Empty, true, expireTime, DefaultCapacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, float expireTime) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, true, expireTime, capacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, float expireTime)
        {
            return InternalCreateObjectPool(objectType, name, true, expireTime, capacity, expireTime, DefaultPriority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, true, DefaultExpireTime, capacity, DefaultExpireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, int priority)
        {
            return InternalCreateObjectPool(objectType, name, true, DefaultExpireTime, capacity, DefaultExpireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, true, expireTime, DefaultCapacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, name, true, expireTime, DefaultCapacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(string.Empty, true, expireTime, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, string.Empty, true, expireTime, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, true, expireTime, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, name, true, expireTime, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="autoReleaseInterval">The interval in seconds for automatically releasing releasable objects in the object pool.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float autoReleaseInterval, int capacity, float expireTime, int priority) where T : ObjectBase
        {
            return InternalCreateObjectPool<T>(name, true, autoReleaseInterval, capacity, expireTime, priority);
        }

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="autoReleaseInterval">The interval in seconds for automatically releasing releasable objects in the object pool.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float autoReleaseInterval, int capacity, float expireTime, int priority)
        {
            return InternalCreateObjectPool(objectType, name, true, autoReleaseInterval, capacity, expireTime, priority);
        }

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        public bool DestroyObjectPool<T>() where T : ObjectBase
        {
            return InternalDestroyObjectPool(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        public bool DestroyObjectPool(Type objectType)
        {
            if (objectType == null)
            {
                throw new GameFrameworkException("Object type is invalid.");
            }

            if (!typeof(ObjectBase).IsAssignableFrom(objectType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Object type '{0}' is invalid.", objectType.FullName));
            }

            return InternalDestroyObjectPool(new TypeNamePair(objectType));
        }

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The name of the object pool to destroy.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        public bool DestroyObjectPool<T>(string name) where T : ObjectBase
        {
            return InternalDestroyObjectPool(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The name of the object pool to destroy.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        public bool DestroyObjectPool(Type objectType, string name)
        {
            if (objectType == null)
            {
                throw new GameFrameworkException("Object type is invalid.");
            }

            if (!typeof(ObjectBase).IsAssignableFrom(objectType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Object type '{0}' is invalid.", objectType.FullName));
            }

            return InternalDestroyObjectPool(new TypeNamePair(objectType, name));
        }

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="objectPool">The object pool to destroy.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        public bool DestroyObjectPool<T>(IObjectPool<T> objectPool) where T : ObjectBase
        {
            if (objectPool == null)
            {
                throw new GameFrameworkException("Object pool is invalid.");
            }

            return InternalDestroyObjectPool(new TypeNamePair(typeof(T), objectPool.Name));
        }

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <param name="objectPool">The object pool to destroy.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        public bool DestroyObjectPool(ObjectPoolBase objectPool)
        {
            if (objectPool == null)
            {
                throw new GameFrameworkException("Object pool is invalid.");
            }

            return InternalDestroyObjectPool(new TypeNamePair(objectPool.ObjectType, objectPool.Name));
        }

        /// <summary>
        /// Releases releasable objects in the object pools.
        /// </summary>
        public void Release()
        {
            GetAllObjectPools(true, m_CachedAllObjectPools);
            foreach (ObjectPoolBase objectPool in m_CachedAllObjectPools)
            {
                objectPool.Release();
            }
        }

        /// <summary>
        /// Releases all unused objects in the object pools.
        /// </summary>
        public void ReleaseAllUnused()
        {
            GetAllObjectPools(true, m_CachedAllObjectPools);
            foreach (ObjectPoolBase objectPool in m_CachedAllObjectPools)
            {
                objectPool.ReleaseAllUnused();
            }
        }

        private bool InternalHasObjectPool(TypeNamePair typeNamePair)
        {
            return m_ObjectPools.ContainsKey(typeNamePair);
        }

        private ObjectPoolBase InternalGetObjectPool(TypeNamePair typeNamePair)
        {
            ObjectPoolBase objectPool = null;
            if (m_ObjectPools.TryGetValue(typeNamePair, out objectPool))
            {
                return objectPool;
            }

            return null;
        }

        private IObjectPool<T> InternalCreateObjectPool<T>(string name, bool allowMultiSpawn, float autoReleaseInterval, int capacity, float expireTime, int priority) where T : ObjectBase
        {
            TypeNamePair typeNamePair = new TypeNamePair(typeof(T), name);
            if (HasObjectPool<T>(name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist object pool '{0}'.", typeNamePair));
            }

            ObjectPool<T> objectPool = new ObjectPool<T>(name, allowMultiSpawn, autoReleaseInterval, capacity, expireTime, priority);
            m_ObjectPools.Add(typeNamePair, objectPool);
            return objectPool;
        }

        private ObjectPoolBase InternalCreateObjectPool(Type objectType, string name, bool allowMultiSpawn, float autoReleaseInterval, int capacity, float expireTime, int priority)
        {
            if (objectType == null)
            {
                throw new GameFrameworkException("Object type is invalid.");
            }

            if (!typeof(ObjectBase).IsAssignableFrom(objectType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Object type '{0}' is invalid.", objectType.FullName));
            }

            TypeNamePair typeNamePair = new TypeNamePair(objectType, name);
            if (HasObjectPool(objectType, name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist object pool '{0}'.", typeNamePair));
            }

            Type objectPoolType = typeof(ObjectPool<>).MakeGenericType(objectType);
            ObjectPoolBase objectPool = (ObjectPoolBase)Activator.CreateInstance(objectPoolType, name, allowMultiSpawn, autoReleaseInterval, capacity, expireTime, priority);
            m_ObjectPools.Add(typeNamePair, objectPool);
            return objectPool;
        }

        private bool InternalDestroyObjectPool(TypeNamePair typeNamePair)
        {
            ObjectPoolBase objectPool = null;
            if (m_ObjectPools.TryGetValue(typeNamePair, out objectPool))
            {
                objectPool.Shutdown();
                return m_ObjectPools.Remove(typeNamePair);
            }

            return false;
        }

        private static int ObjectPoolComparer(ObjectPoolBase a, ObjectPoolBase b)
        {
            return a.Priority.CompareTo(b.Priority);
        }
    }
}
