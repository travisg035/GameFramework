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
    /// Object pool interface.
    /// </summary>
    /// <typeparam name="T">The object type.</typeparam>
    public interface IObjectPool<T> where T : ObjectBase
    {
        /// <summary>
        /// Gets the object pool name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the full name of the object pool.
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// Gets the object type of the object pool.
        /// </summary>
        Type ObjectType
        {
            get;
        }

        /// <summary>
        /// Gets the count of objects in the object pool.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Gets the count of releasable objects in the object pool.
        /// </summary>
        int CanReleaseCount
        {
            get;
        }

        /// <summary>
        /// Gets whether the object is allowed to be spawned multiple times.
        /// </summary>
        bool AllowMultiSpawn
        {
            get;
        }

        /// <summary>
        /// Gets or sets the interval in seconds for automatically releasing releasable objects in the object pool.
        /// </summary>
        float AutoReleaseInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the capacity of the object pool.
        /// </summary>
        int Capacity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the expire time in seconds for objects in the object pool.
        /// </summary>
        float ExpireTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the priority of the object pool.
        /// </summary>
        int Priority
        {
            get;
            set;
        }

        /// <summary>
        /// Registers an object.
        /// </summary>
        /// <param name="obj">The object to register.</param>
        /// <param name="spawned">Whether the object has been spawned.</param>
        void Register(T obj, bool spawned);

        /// <summary>
        /// Checks whether an object can be spawned.
        /// </summary>
        /// <returns>Whether the object to check exists.</returns>
        bool CanSpawn();

        /// <summary>
        /// Checks whether an object can be spawned.
        /// </summary>
        /// <param name="name">The object name.</param>
        /// <returns>Whether the object to check exists.</returns>
        bool CanSpawn(string name);

        /// <summary>
        /// Spawns an object.
        /// </summary>
        /// <returns>The spawned object.</returns>
        T Spawn();

        /// <summary>
        /// Spawns an object.
        /// </summary>
        /// <param name="name">The object name.</param>
        /// <returns>The spawned object.</returns>
        T Spawn(string name);

        /// <summary>
        /// Unspawns an object.
        /// </summary>
        /// <param name="obj">The object to unspawn.</param>
        void Unspawn(T obj);

        /// <summary>
        /// Unspawns an object.
        /// </summary>
        /// <param name="target">The target object to unspawn.</param>
        void Unspawn(object target);

        /// <summary>
        /// Sets whether the object is locked.
        /// </summary>
        /// <param name="obj">The object to set locked.</param>
        /// <param name="locked">Whether it is locked.</param>
        void SetLocked(T obj, bool locked);

        /// <summary>
        /// Sets whether the object is locked.
        /// </summary>
        /// <param name="target">The target object to set locked.</param>
        /// <param name="locked">Whether it is locked.</param>
        void SetLocked(object target, bool locked);

        /// <summary>
        /// Sets the priority of the object.
        /// </summary>
        /// <param name="obj">The object to set priority.</param>
        /// <param name="priority">Priority.</param>
        void SetPriority(T obj, int priority);

        /// <summary>
        /// Sets the priority of the object.
        /// </summary>
        /// <param name="target">The target object to set priority.</param>
        /// <param name="priority">Priority.</param>
        void SetPriority(object target, int priority);

        /// <summary>
        /// Releases an object.
        /// </summary>
        /// <param name="obj">The object to release.</param>
        /// <returns>Whether the object was released successfully.</returns>
        bool ReleaseObject(T obj);

        /// <summary>
        /// Releases an object.
        /// </summary>
        /// <param name="target">The target object to release.</param>
        /// <returns>Whether the object was released successfully.</returns>
        bool ReleaseObject(object target);

        /// <summary>
        /// Releases releasable objects in the object pool.
        /// </summary>
        void Release();

        /// <summary>
        /// Releases releasable objects in the object pool.
        /// </summary>
        /// <param name="toReleaseCount">The count of objects to attempt to release.</param>
        void Release(int toReleaseCount);

        /// <summary>
        /// Releases releasable objects in the object pool.
        /// </summary>
        /// <param name="releaseObjectFilterCallback">Release object filter callback.</param>
        void Release(ReleaseObjectFilterCallback<T> releaseObjectFilterCallback);

        /// <summary>
        /// Releases releasable objects in the object pool.
        /// </summary>
        /// <param name="toReleaseCount">The count of objects to attempt to release.</param>
        /// <param name="releaseObjectFilterCallback">Release object filter callback.</param>
        void Release(int toReleaseCount, ReleaseObjectFilterCallback<T> releaseObjectFilterCallback);

        /// <summary>
        /// Releases all unused objects in the object pool.
        /// </summary>
        void ReleaseAllUnused();
    }
}
