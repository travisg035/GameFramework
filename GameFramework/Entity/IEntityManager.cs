//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.ObjectPool;
using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.Entity
{
    /// <summary>
    /// Entity manager interface.
    /// </summary>
    public interface IEntityManager
    {
        /// <summary>
        /// Gets the entity count.
        /// </summary>
        int EntityCount
        {
            get;
        }

        /// <summary>
        /// Gets the entity group count.
        /// </summary>
        int EntityGroupCount
        {
            get;
        }

        /// <summary>
        /// Show entity success event.
        /// </summary>
        event EventHandler<ShowEntitySuccessEventArgs> ShowEntitySuccess;

        /// <summary>
        /// Show entity failure event.
        /// </summary>
        event EventHandler<ShowEntityFailureEventArgs> ShowEntityFailure;

        /// <summary>
        /// Show entity update event.
        /// </summary>
        event EventHandler<ShowEntityUpdateEventArgs> ShowEntityUpdate;

        /// <summary>
        /// Show entity dependency asset event.
        /// </summary>
        event EventHandler<ShowEntityDependencyAssetEventArgs> ShowEntityDependencyAsset;

        /// <summary>
        /// Hide entity complete event.
        /// </summary>
        event EventHandler<HideEntityCompleteEventArgs> HideEntityComplete;

        /// <summary>
        /// Sets the object pool manager.
        /// </summary>
        /// <param name="objectPoolManager">The object pool manager.</param>
        void SetObjectPoolManager(IObjectPoolManager objectPoolManager);

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        void SetResourceManager(IResourceManager resourceManager);

        /// <summary>
        /// Sets the entity helper.
        /// </summary>
        /// <param name="entityHelper">The entity helper.</param>
        void SetEntityHelper(IEntityHelper entityHelper);

        /// <summary>
        /// Checks whether the entity group exists.
        /// </summary>
        /// <param name="entityGroupName">The entity group name.</param>
        /// <returns>Whether the entity group exists.</returns>
        bool HasEntityGroup(string entityGroupName);

        /// <summary>
        /// Gets an entity group.
        /// </summary>
        /// <param name="entityGroupName">The entity group name.</param>
        /// <returns>The entity group to get.</returns>
        IEntityGroup GetEntityGroup(string entityGroupName);

        /// <summary>
        /// Gets all entity groups.
        /// </summary>
        /// <returns>All entity groups.</returns>
        IEntityGroup[] GetAllEntityGroups();

        /// <summary>
        /// Gets all entity groups.
        /// </summary>
        /// <param name="results">All entity groups.</param>
        void GetAllEntityGroups(List<IEntityGroup> results);

        /// <summary>
        /// Adds an entity group.
        /// </summary>
        /// <param name="entityGroupName">The entity group name.</param>
        /// <param name="instanceAutoReleaseInterval">The interval in seconds for automatically releasing releasable objects in the entity instance object pool.</param>
        /// <param name="instanceCapacity">The capacity of the entity instance object pool.</param>
        /// <param name="instanceExpireTime">The expire time in seconds for objects in the entity instance object pool.</param>
        /// <param name="instancePriority">The priority of the entity instance object pool.</param>
        /// <param name="entityGroupHelper">The entity group helper.</param>
        /// <returns>Whether the entity group was added successfully.</returns>
        bool AddEntityGroup(string entityGroupName, float instanceAutoReleaseInterval, int instanceCapacity, float instanceExpireTime, int instancePriority, IEntityGroupHelper entityGroupHelper);

        /// <summary>
        /// Checks whether the entity exists.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Whether the entity exists.</returns>
        bool HasEntity(int entityId);

        /// <summary>
        /// Checks whether the entity exists.
        /// </summary>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <returns>Whether the entity exists.</returns>
        bool HasEntity(string entityAssetName);

        /// <summary>
        /// Gets an entity.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The entity to get.</returns>
        IEntity GetEntity(int entityId);

        /// <summary>
        /// Gets an entity.
        /// </summary>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <returns>The entity to get.</returns>
        IEntity GetEntity(string entityAssetName);

        /// <summary>
        /// Gets entities.
        /// </summary>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <returns>The entities to get.</returns>
        IEntity[] GetEntities(string entityAssetName);

        /// <summary>
        /// Gets entities.
        /// </summary>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <param name="results">The entities to get.</param>
        void GetEntities(string entityAssetName, List<IEntity> results);

        /// <summary>
        /// Gets all loaded entities.
        /// </summary>
        /// <returns>All loaded entities.</returns>
        IEntity[] GetAllLoadedEntities();

        /// <summary>
        /// Gets all loaded entities.
        /// </summary>
        /// <param name="results">All loaded entities.</param>
        void GetAllLoadedEntities(List<IEntity> results);

        /// <summary>
        /// Gets all loading entity ids.
        /// </summary>
        /// <returns>All loading entity ids.</returns>
        int[] GetAllLoadingEntityIds();

        /// <summary>
        /// Gets all loading entity ids.
        /// </summary>
        /// <param name="results">All loading entity ids.</param>
        void GetAllLoadingEntityIds(List<int> results);

        /// <summary>
        /// Checks whether the entity is loading.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Whether the entity is loading.</returns>
        bool IsLoadingEntity(int entityId);

        /// <summary>
        /// Checks whether the entity is valid.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Whether the entity is valid.</returns>
        bool IsValidEntity(IEntity entity);

        /// <summary>
        /// Shows the entity.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <param name="entityGroupName">The entity group name.</param>
        void ShowEntity(int entityId, string entityAssetName, string entityGroupName);

        /// <summary>
        /// Shows the entity.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <param name="entityGroupName">The entity group name.</param>
        /// <param name="priority">The priority for loading the entity asset.</param>
        void ShowEntity(int entityId, string entityAssetName, string entityGroupName, int priority);

        /// <summary>
        /// Shows the entity.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <param name="entityGroupName">The entity group name.</param>
        /// <param name="userData">The user-defined data.</param>
        void ShowEntity(int entityId, string entityAssetName, string entityGroupName, object userData);

        /// <summary>
        /// Shows the entity.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <param name="entityGroupName">The entity group name.</param>
        /// <param name="priority">The priority for loading the entity asset.</param>
        /// <param name="userData">The user-defined data.</param>
        void ShowEntity(int entityId, string entityAssetName, string entityGroupName, int priority, object userData);

        /// <summary>
        /// Hides the entity.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        void HideEntity(int entityId);

        /// <summary>
        /// Hides the entity.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <param name="userData">The user-defined data.</param>
        void HideEntity(int entityId, object userData);

        /// <summary>
        /// Hides the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void HideEntity(IEntity entity);

        /// <summary>
        /// Hides the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userData">The user-defined data.</param>
        void HideEntity(IEntity entity, object userData);

        /// <summary>
        /// Hides all loaded entities.
        /// </summary>
        void HideAllLoadedEntities();

        /// <summary>
        /// Hides all loaded entities.
        /// </summary>
        /// <param name="userData">The user-defined data.</param>
        void HideAllLoadedEntities(object userData);

        /// <summary>
        /// Hides all loading entities.
        /// </summary>
        void HideAllLoadingEntities();

        /// <summary>
        /// Gets the parent entity.
        /// </summary>
        /// <param name="childEntityId">The entity id of the child entity to get the parent entity.</param>
        /// <returns>The parent entity of the child entity.</returns>
        IEntity GetParentEntity(int childEntityId);

        /// <summary>
        /// Gets the parent entity.
        /// </summary>
        /// <param name="childEntity">The child entity to get the parent entity.</param>
        /// <returns>The parent entity of the child entity.</returns>
        IEntity GetParentEntity(IEntity childEntity);

        /// <summary>
        /// Gets the count of child entities.
        /// </summary>
        /// <param name="parentEntityId">The entity id of the parent entity to get the child entity count.</param>
        /// <returns>The count of child entities.</returns>
        int GetChildEntityCount(int parentEntityId);

        /// <summary>
        /// Gets a child entity.
        /// </summary>
        /// <param name="parentEntityId">The entity id of the parent entity to get the child entity.</param>
        /// <returns>The child entity.</returns>
        IEntity GetChildEntity(int parentEntityId);

        /// <summary>
        /// Gets a child entity.
        /// </summary>
        /// <param name="parentEntity">The parent entity to get the child entity.</param>
        /// <returns>The child entity.</returns>
        IEntity GetChildEntity(IEntity parentEntity);

        /// <summary>
        /// Gets all child entities.
        /// </summary>
        /// <param name="parentEntityId">The entity id of the parent entity to get all child entities.</param>
        /// <returns>All child entities.</returns>
        IEntity[] GetChildEntities(int parentEntityId);

        /// <summary>
        /// Gets all child entities.
        /// </summary>
        /// <param name="parentEntityId">The entity id of the parent entity to get all child entities.</param>
        /// <param name="results">All child entities.</param>
        void GetChildEntities(int parentEntityId, List<IEntity> results);

        /// <summary>
        /// Gets all child entities.
        /// </summary>
        /// <param name="parentEntity">The parent entity to get all child entities.</param>
        /// <returns>All child entities.</returns>
        IEntity[] GetChildEntities(IEntity parentEntity);

        /// <summary>
        /// Gets all child entities.
        /// </summary>
        /// <param name="parentEntity">The parent entity to get all child entities.</param>
        /// <param name="results">All child entities.</param>
        void GetChildEntities(IEntity parentEntity, List<IEntity> results);

        /// <summary>
        /// Attaches a child entity.
        /// </summary>
        /// <param name="childEntityId">The entity id of the child entity to attach.</param>
        /// <param name="parentEntityId">The entity id of the parent entity to attach to.</param>
        void AttachEntity(int childEntityId, int parentEntityId);

        /// <summary>
        /// Attaches a child entity.
        /// </summary>
        /// <param name="childEntityId">The entity id of the child entity to attach.</param>
        /// <param name="parentEntityId">The entity id of the parent entity to attach to.</param>
        /// <param name="userData">The user-defined data.</param>
        void AttachEntity(int childEntityId, int parentEntityId, object userData);

        /// <summary>
        /// Attaches a child entity.
        /// </summary>
        /// <param name="childEntityId">The entity id of the child entity to attach.</param>
        /// <param name="parentEntity">The parent entity to attach to.</param>
        void AttachEntity(int childEntityId, IEntity parentEntity);

        /// <summary>
        /// Attaches a child entity.
        /// </summary>
        /// <param name="childEntityId">The entity id of the child entity to attach.</param>
        /// <param name="parentEntity">The parent entity to attach to.</param>
        /// <param name="userData">The user-defined data.</param>
        void AttachEntity(int childEntityId, IEntity parentEntity, object userData);

        /// <summary>
        /// Attaches a child entity.
        /// </summary>
        /// <param name="childEntity">The child entity to attach.</param>
        /// <param name="parentEntityId">The entity id of the parent entity to attach to.</param>
        void AttachEntity(IEntity childEntity, int parentEntityId);

        /// <summary>
        /// Attaches a child entity.
        /// </summary>
        /// <param name="childEntity">The child entity to attach.</param>
        /// <param name="parentEntityId">The entity id of the parent entity to attach to.</param>
        /// <param name="userData">The user-defined data.</param>
        void AttachEntity(IEntity childEntity, int parentEntityId, object userData);

        /// <summary>
        /// Attaches a child entity.
        /// </summary>
        /// <param name="childEntity">The child entity to attach.</param>
        /// <param name="parentEntity">The parent entity to attach to.</param>
        void AttachEntity(IEntity childEntity, IEntity parentEntity);

        /// <summary>
        /// Attaches a child entity.
        /// </summary>
        /// <param name="childEntity">The child entity to attach.</param>
        /// <param name="parentEntity">The parent entity to attach to.</param>
        /// <param name="userData">The user-defined data.</param>
        void AttachEntity(IEntity childEntity, IEntity parentEntity, object userData);

        /// <summary>
        /// Detaches a child entity.
        /// </summary>
        /// <param name="childEntityId">The entity id of the child entity to detach.</param>
        void DetachEntity(int childEntityId);

        /// <summary>
        /// Detaches a child entity.
        /// </summary>
        /// <param name="childEntityId">The entity id of the child entity to detach.</param>
        /// <param name="userData">The user-defined data.</param>
        void DetachEntity(int childEntityId, object userData);

        /// <summary>
        /// Detaches a child entity.
        /// </summary>
        /// <param name="childEntity">The child entity to detach.</param>
        void DetachEntity(IEntity childEntity);

        /// <summary>
        /// Detaches a child entity.
        /// </summary>
        /// <param name="childEntity">The child entity to detach.</param>
        /// <param name="userData">The user-defined data.</param>
        void DetachEntity(IEntity childEntity, object userData);

        /// <summary>
        /// Detaches all child entities.
        /// </summary>
        /// <param name="parentEntityId">The entity id of the parent entity to detach.</param>
        void DetachChildEntities(int parentEntityId);

        /// <summary>
        /// Detaches all child entities.
        /// </summary>
        /// <param name="parentEntityId">The entity id of the parent entity to detach.</param>
        /// <param name="userData">The user-defined data.</param>
        void DetachChildEntities(int parentEntityId, object userData);

        /// <summary>
        /// Detaches all child entities.
        /// </summary>
        /// <param name="parentEntity">The parent entity to detach.</param>
        void DetachChildEntities(IEntity parentEntity);

        /// <summary>
        /// Detaches all child entities.
        /// </summary>
        /// <param name="parentEntity">The parent entity to detach.</param>
        /// <param name="userData">The user-defined data.</param>
        void DetachChildEntities(IEntity parentEntity, object userData);
    }
}
