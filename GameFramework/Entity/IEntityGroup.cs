//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.Entity
{
    /// <summary>
    /// Entity group interface.
    /// </summary>
    public interface IEntityGroup
    {
        /// <summary>
        /// Gets the entity group name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the count of entities in the entity group.
        /// </summary>
        int EntityCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the interval in seconds for automatically releasing releasable objects in the entity group instance object pool.
        /// </summary>
        float InstanceAutoReleaseInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the capacity of the entity group instance object pool.
        /// </summary>
        int InstanceCapacity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the expire time in seconds for objects in the entity group instance object pool.
        /// </summary>
        float InstanceExpireTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the priority of the entity group instance object pool.
        /// </summary>
        int InstancePriority
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the entity group helper.
        /// </summary>
        IEntityGroupHelper Helper
        {
            get;
        }

        /// <summary>
        /// Checks whether the entity exists in the entity group.
        /// </summary>
        /// <param name="entityId">The serial ID of the entity.</param>
        /// <returns>Whether the entity exists in the entity group.</returns>
        bool HasEntity(int entityId);

        /// <summary>
        /// Checks whether the entity exists in the entity group.
        /// </summary>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <returns>Whether the entity exists in the entity group.</returns>
        bool HasEntity(string entityAssetName);

        /// <summary>
        /// Gets an entity from the entity group.
        /// </summary>
        /// <param name="entityId">The serial ID of the entity.</param>
        /// <returns>The entity to get.</returns>
        IEntity GetEntity(int entityId);

        /// <summary>
        /// Gets an entity from the entity group.
        /// </summary>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <returns>The entity to get.</returns>
        IEntity GetEntity(string entityAssetName);

        /// <summary>
        /// Gets entities from the entity group.
        /// </summary>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <returns>The entities to get.</returns>
        IEntity[] GetEntities(string entityAssetName);

        /// <summary>
        /// Gets entities from the entity group.
        /// </summary>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <param name="results">The entities to get.</param>
        void GetEntities(string entityAssetName, List<IEntity> results);

        /// <summary>
        /// Gets all entities from the entity group.
        /// </summary>
        /// <returns>All entities in the entity group.</returns>
        IEntity[] GetAllEntities();

        /// <summary>
        /// Gets all entities from the entity group.
        /// </summary>
        /// <param name="results">All entities in the entity group.</param>
        void GetAllEntities(List<IEntity> results);

        /// <summary>
        /// Sets whether the entity instance is locked.
        /// </summary>
        /// <param name="entityInstance">The entity instance.</param>
        /// <param name="locked">Whether the entity instance is locked.</param>
        void SetEntityInstanceLocked(object entityInstance, bool locked);

        /// <summary>
        /// Sets the priority of the entity instance.
        /// </summary>
        /// <param name="entityInstance">The entity instance.</param>
        /// <param name="priority">The entity instance priority.</param>
        void SetEntityInstancePriority(object entityInstance, int priority);
    }
}
