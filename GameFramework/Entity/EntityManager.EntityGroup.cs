//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.ObjectPool;
using System.Collections.Generic;

namespace GameFramework.Entity
{
    internal sealed partial class EntityManager : GameFrameworkModule, IEntityManager
    {
        /// <summary>
        /// Entity group.
        /// </summary>
        private sealed class EntityGroup : IEntityGroup
        {
            private readonly string m_Name;
            private readonly IEntityGroupHelper m_EntityGroupHelper;
            private readonly IObjectPool<EntityInstanceObject> m_InstancePool;
            private readonly GameFrameworkLinkedList<IEntity> m_Entities;
            private LinkedListNode<IEntity> m_CachedNode;

            /// <summary>
            /// Initializes a new instance of the EntityGroup class.
            /// </summary>
            /// <param name="name">The entity group name.</param>
            /// <param name="instanceAutoReleaseInterval">The interval in seconds for automatically releasing releasable objects in the entity instance object pool.</param>
            /// <param name="instanceCapacity">The capacity of the entity instance object pool.</param>
            /// <param name="instanceExpireTime">The expire time in seconds for objects in the entity instance object pool.</param>
            /// <param name="instancePriority">The priority of the entity instance object pool.</param>
            /// <param name="entityGroupHelper">The entity group helper.</param>
            /// <param name="objectPoolManager">The object pool manager.</param>
            public EntityGroup(string name, float instanceAutoReleaseInterval, int instanceCapacity, float instanceExpireTime, int instancePriority, IEntityGroupHelper entityGroupHelper, IObjectPoolManager objectPoolManager)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new GameFrameworkException("Entity group name is invalid.");
                }

                if (entityGroupHelper == null)
                {
                    throw new GameFrameworkException("Entity group helper is invalid.");
                }

                m_Name = name;
                m_EntityGroupHelper = entityGroupHelper;
                m_InstancePool = objectPoolManager.CreateSingleSpawnObjectPool<EntityInstanceObject>(Utility.Text.Format("Entity Instance Pool ({0})", name), instanceCapacity, instanceExpireTime, instancePriority);
                m_InstancePool.AutoReleaseInterval = instanceAutoReleaseInterval;
                m_Entities = new GameFrameworkLinkedList<IEntity>();
                m_CachedNode = null;
            }

            /// <summary>
            /// Gets the entity group name.
            /// </summary>
            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            /// <summary>
            /// Gets the count of entities in the entity group.
            /// </summary>
            public int EntityCount
            {
                get
                {
                    return m_Entities.Count;
                }
            }

            /// <summary>
            /// Gets or sets the interval in seconds for automatically releasing releasable objects in the entity group instance object pool.
            /// </summary>
            public float InstanceAutoReleaseInterval
            {
                get
                {
                    return m_InstancePool.AutoReleaseInterval;
                }
                set
                {
                    m_InstancePool.AutoReleaseInterval = value;
                }
            }

            /// <summary>
            /// Gets or sets the capacity of the entity group instance object pool.
            /// </summary>
            public int InstanceCapacity
            {
                get
                {
                    return m_InstancePool.Capacity;
                }
                set
                {
                    m_InstancePool.Capacity = value;
                }
            }

            /// <summary>
            /// Gets or sets the expire time in seconds for objects in the entity group instance object pool.
            /// </summary>
            public float InstanceExpireTime
            {
                get
                {
                    return m_InstancePool.ExpireTime;
                }
                set
                {
                    m_InstancePool.ExpireTime = value;
                }
            }

            /// <summary>
            /// Gets or sets the priority of the entity group instance object pool.
            /// </summary>
            public int InstancePriority
            {
                get
                {
                    return m_InstancePool.Priority;
                }
                set
                {
                    m_InstancePool.Priority = value;
                }
            }

            /// <summary>
            /// Gets the entity group helper.
            /// </summary>
            public IEntityGroupHelper Helper
            {
                get
                {
                    return m_EntityGroupHelper;
                }
            }

            /// <summary>
            /// Polling the entity group.
            /// </summary>
            /// <param name="elapseSeconds">Logical elapsed seconds.</param>
            /// <param name="realElapseSeconds">Real elapsed seconds.</param>
            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                LinkedListNode<IEntity> current = m_Entities.First;
                while (current != null)
                {
                    m_CachedNode = current.Next;
                    current.Value.OnUpdate(elapseSeconds, realElapseSeconds);
                    current = m_CachedNode;
                    m_CachedNode = null;
                }
            }

            /// <summary>
            /// Checks whether the entity exists in the entity group.
            /// </summary>
            /// <param name="entityId">The serial ID of the entity.</param>
            /// <returns>Whether the entity exists in the entity group.</returns>
            public bool HasEntity(int entityId)
            {
                foreach (IEntity entity in m_Entities)
                {
                    if (entity.Id == entityId)
                    {
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Checks whether the entity exists in the entity group.
            /// </summary>
            /// <param name="entityAssetName">The entity asset name.</param>
            /// <returns>Whether the entity exists in the entity group.</returns>
            public bool HasEntity(string entityAssetName)
            {
                if (string.IsNullOrEmpty(entityAssetName))
                {
                    throw new GameFrameworkException("Entity asset name is invalid.");
                }

                foreach (IEntity entity in m_Entities)
                {
                    if (entity.EntityAssetName == entityAssetName)
                    {
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Gets an entity from the entity group.
            /// </summary>
            /// <param name="entityId">The serial ID of the entity.</param>
            /// <returns>The entity to get.</returns>
            public IEntity GetEntity(int entityId)
            {
                foreach (IEntity entity in m_Entities)
                {
                    if (entity.Id == entityId)
                    {
                        return entity;
                    }
                }

                return null;
            }

            /// <summary>
            /// Gets an entity from the entity group.
            /// </summary>
            /// <param name="entityAssetName">The entity asset name.</param>
            /// <returns>The entity to get.</returns>
            public IEntity GetEntity(string entityAssetName)
            {
                if (string.IsNullOrEmpty(entityAssetName))
                {
                    throw new GameFrameworkException("Entity asset name is invalid.");
                }

                foreach (IEntity entity in m_Entities)
                {
                    if (entity.EntityAssetName == entityAssetName)
                    {
                        return entity;
                    }
                }

                return null;
            }

            /// <summary>
            /// Gets entities from the entity group.
            /// </summary>
            /// <param name="entityAssetName">The entity asset name.</param>
            /// <returns>The entities to get.</returns>
            public IEntity[] GetEntities(string entityAssetName)
            {
                if (string.IsNullOrEmpty(entityAssetName))
                {
                    throw new GameFrameworkException("Entity asset name is invalid.");
                }

                List<IEntity> results = new List<IEntity>();
                foreach (IEntity entity in m_Entities)
                {
                    if (entity.EntityAssetName == entityAssetName)
                    {
                        results.Add(entity);
                    }
                }

                return results.ToArray();
            }

            /// <summary>
            /// Gets entities from the entity group.
            /// </summary>
            /// <param name="entityAssetName">The entity asset name.</param>
            /// <param name="results">The entities to get.</param>
            public void GetEntities(string entityAssetName, List<IEntity> results)
            {
                if (string.IsNullOrEmpty(entityAssetName))
                {
                    throw new GameFrameworkException("Entity asset name is invalid.");
                }

                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (IEntity entity in m_Entities)
                {
                    if (entity.EntityAssetName == entityAssetName)
                    {
                        results.Add(entity);
                    }
                }
            }

            /// <summary>
            /// Gets all entities from the entity group.
            /// </summary>
            /// <returns>All entities in the entity group.</returns>
            public IEntity[] GetAllEntities()
            {
                List<IEntity> results = new List<IEntity>();
                foreach (IEntity entity in m_Entities)
                {
                    results.Add(entity);
                }

                return results.ToArray();
            }

            /// <summary>
            /// Gets all entities from the entity group.
            /// </summary>
            /// <param name="results">All entities in the entity group.</param>
            public void GetAllEntities(List<IEntity> results)
            {
                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (IEntity entity in m_Entities)
                {
                    results.Add(entity);
                }
            }

            /// <summary>
            /// Adds an entity to the entity group.
            /// </summary>
            /// <param name="entity">The entity to add.</param>
            public void AddEntity(IEntity entity)
            {
                m_Entities.AddLast(entity);
            }

            /// <summary>
            /// Removes an entity from the entity group.
            /// </summary>
            /// <param name="entity">The entity to remove.</param>
            public void RemoveEntity(IEntity entity)
            {
                if (m_CachedNode != null && m_CachedNode.Value == entity)
                {
                    m_CachedNode = m_CachedNode.Next;
                }

                if (!m_Entities.Remove(entity))
                {
                    throw new GameFrameworkException(Utility.Text.Format("Entity group '{0}' not exists specified entity '[{1}]{2}'.", m_Name, entity.Id, entity.EntityAssetName));
                }
            }

            public void RegisterEntityInstanceObject(EntityInstanceObject obj, bool spawned)
            {
                m_InstancePool.Register(obj, spawned);
            }

            public EntityInstanceObject SpawnEntityInstanceObject(string name)
            {
                return m_InstancePool.Spawn(name);
            }

            public void UnspawnEntity(IEntity entity)
            {
                m_InstancePool.Unspawn(entity.Handle);
            }

            public void SetEntityInstanceLocked(object entityInstance, bool locked)
            {
                if (entityInstance == null)
                {
                    throw new GameFrameworkException("Entity instance is invalid.");
                }

                m_InstancePool.SetLocked(entityInstance, locked);
            }

            public void SetEntityInstancePriority(object entityInstance, int priority)
            {
                if (entityInstance == null)
                {
                    throw new GameFrameworkException("Entity instance is invalid.");
                }

                m_InstancePool.SetPriority(entityInstance, priority);
            }
        }
    }
}
