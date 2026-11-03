//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace GameFramework.Entity
{
    /// <summary>
    /// Entity extension methods providing asynchronous operations.
    /// </summary>
    public static class EntityExtension
    {
        private const int DefaultPriority = 0;

        /// <summary>
        /// Shows an entity asynchronously.
        /// </summary>
        /// <param name="entityManager">The entity manager.</param>
        /// <param name="entityId">The entity id.</param>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <param name="entityGroupName">The entity group name.</param>
        /// <param name="priority">The priority for loading the entity asset.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the show entity operation returning the entity.</returns>
        public static Task<IEntity> ShowEntityAsync(this IEntityManager entityManager, int entityId, string entityAssetName, string entityGroupName, int priority = DefaultPriority, object userData = null)
        {
            if (entityManager == null)
            {
                throw new GameFrameworkException("Entity manager is invalid.");
            }

            if (string.IsNullOrEmpty(entityAssetName))
            {
                throw new GameFrameworkException("Entity asset name is invalid.");
            }

            if (string.IsNullOrEmpty(entityGroupName))
            {
                throw new GameFrameworkException("Entity group name is invalid.");
            }

            TaskCompletionSource<IEntity> taskCompletionSource = new TaskCompletionSource<IEntity>(TaskCreationOptions.RunContinuationsAsynchronously);

            EventHandler<ShowEntitySuccessEventArgs> successHandler = null;
            EventHandler<ShowEntityFailureEventArgs> failureHandler = null;

            void Unsubscribe()
            {
                entityManager.ShowEntitySuccess -= successHandler;
                entityManager.ShowEntityFailure -= failureHandler;
            }

            successHandler = (sender, e) =>
            {
                if (e.Entity != null && e.Entity.Id == entityId)
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetResult(e.Entity);
                }
            };

            failureHandler = (sender, e) =>
            {
                if (e.EntityId == entityId)
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetException(new GameFrameworkException(e.ErrorMessage));
                }
            };

            entityManager.ShowEntitySuccess += successHandler;
            entityManager.ShowEntityFailure += failureHandler;

            try
            {
                entityManager.ShowEntity(entityId, entityAssetName, entityGroupName, priority, userData);
            }
            catch (Exception exception)
            {
                Unsubscribe();
                taskCompletionSource.TrySetException(exception);
            }

            return taskCompletionSource.Task;
        }
    }
}
