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
    /// Object pool manager interface.
    /// </summary>
    public interface IObjectPoolManager
    {
        /// <summary>
        /// Gets the count of object pools.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>Whether the object pool exists.</returns>
        bool HasObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>Whether the object pool exists.</returns>
        bool HasObjectPool(Type objectType);

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <returns>Whether the object pool exists.</returns>
        bool HasObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <returns>Whether the object pool exists.</returns>
        bool HasObjectPool(Type objectType, string name);

        /// <summary>
        /// Checks whether the object pool exists.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns>Whether the object pool exists.</returns>
        bool HasObjectPool(Predicate<ObjectPoolBase> condition);

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The object pool to get.</returns>
        IObjectPool<T> GetObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>The object pool to get.</returns>
        ObjectPoolBase GetObjectPool(Type objectType);

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <returns>The object pool to get.</returns>
        IObjectPool<T> GetObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <returns>The object pool to get.</returns>
        ObjectPoolBase GetObjectPool(Type objectType, string name);

        /// <summary>
        /// Gets an object pool.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns>The object pool to get.</returns>
        ObjectPoolBase GetObjectPool(Predicate<ObjectPoolBase> condition);

        /// <summary>
        /// Gets object pools.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns>The object pools to get.</returns>
        ObjectPoolBase[] GetObjectPools(Predicate<ObjectPoolBase> condition);

        /// <summary>
        /// Gets object pools.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="results">The list to store the object pools.</param>
        void GetObjectPools(Predicate<ObjectPoolBase> condition, List<ObjectPoolBase> results);

        /// <summary>
        /// Gets all object pools.
        /// </summary>
        /// <returns>All object pools.</returns>
        ObjectPoolBase[] GetAllObjectPools();

        /// <summary>
        /// Gets all object pools.
        /// </summary>
        /// <param name="results">The list to store all object pools.</param>
        void GetAllObjectPools(List<ObjectPoolBase> results);

        /// <summary>
        /// Gets all object pools.
        /// </summary>
        /// <param name="sort">Whether to sort by object pool priority.</param>
        /// <returns>All object pools.</returns>
        ObjectPoolBase[] GetAllObjectPools(bool sort);

        /// <summary>
        /// Gets all object pools.
        /// </summary>
        /// <param name="sort">Whether to sort by object pool priority.</param>
        /// <param name="results">The list to store all object pools.</param>
        void GetAllObjectPools(bool sort, List<ObjectPoolBase> results);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(float expireTime) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, float expireTime);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float expireTime) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float expireTime);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, float expireTime) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, float expireTime);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, int priority);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, float expireTime, int priority);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, float expireTime) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, float expireTime);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, int priority);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float expireTime, int priority);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, float expireTime, int priority);

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a single-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created single-spawn object pool.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, float expireTime, int priority);

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
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float autoReleaseInterval, int capacity, float expireTime, int priority) where T : ObjectBase;

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
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float autoReleaseInterval, int capacity, float expireTime, int priority);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(float expireTime) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, float expireTime);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float expireTime) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float expireTime);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, float expireTime) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, float expireTime);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, int priority);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, float expireTime, int priority);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, float expireTime) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, float expireTime);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, int priority);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float expireTime, int priority);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, float expireTime, int priority);

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// Creates a multi-spawn object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The object pool name.</param>
        /// <param name="capacity">The capacity of the object pool.</param>
        /// <param name="expireTime">The expire time in seconds for objects in the object pool.</param>
        /// <param name="priority">The priority of the object pool.</param>
        /// <returns>The created multi-spawn object pool.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, float expireTime, int priority);

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
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float autoReleaseInterval, int capacity, float expireTime, int priority) where T : ObjectBase;

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
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float autoReleaseInterval, int capacity, float expireTime, int priority);

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool(Type objectType);

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="name">The name of the object pool to destroy.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        /// <param name="name">The name of the object pool to destroy.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool(Type objectType, string name);

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="objectPool">The object pool to destroy.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool<T>(IObjectPool<T> objectPool) where T : ObjectBase;

        /// <summary>
        /// Destroys an object pool.
        /// </summary>
        /// <param name="objectPool">The object pool to destroy.</param>
        /// <returns>Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool(ObjectPoolBase objectPool);

        /// <summary>
        /// Releases releasable objects in the object pools.
        /// </summary>
        void Release();

        /// <summary>
        /// Releases all unused objects in the object pools.
        /// </summary>
        void ReleaseAllUnused();
    }
}
