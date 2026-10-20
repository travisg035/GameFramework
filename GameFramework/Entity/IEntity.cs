//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Entity
{
    /// <summary>
    /// Entity interface.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the entity id.
        /// </summary>
        int Id
        {
            get;
        }

        /// <summary>
        /// Gets the entity asset name.
        /// </summary>
        string EntityAssetName
        {
            get;
        }

        /// <summary>
        /// Gets the entity instance.
        /// </summary>
        object Handle
        {
            get;
        }

        /// <summary>
        /// Gets the entity group to which the entity belongs.
        /// </summary>
        IEntityGroup EntityGroup
        {
            get;
        }

        /// <summary>
        /// Initializes the entity.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <param name="entityAssetName">The entity asset name.</param>
        /// <param name="entityGroup">The entity group to which the entity belongs.</param>
        /// <param name="isNewInstance">Whether it is a new instance.</param>
        /// <param name="userData">The user-defined data.</param>
        void OnInit(int entityId, string entityAssetName, IEntityGroup entityGroup, bool isNewInstance, object userData);

        /// <summary>
        /// Recycles the entity.
        /// </summary>
        void OnRecycle();

        /// <summary>
        /// Shows the entity.
        /// </summary>
        /// <param name="userData">The user-defined data.</param>
        void OnShow(object userData);

        /// <summary>
        /// Hides the entity.
        /// </summary>
        /// <param name="isShutdown">Whether it is triggered when shutting down the entity manager.</param>
        /// <param name="userData">The user-defined data.</param>
        void OnHide(bool isShutdown, object userData);

        /// <summary>
        /// Attaches a child entity.
        /// </summary>
        /// <param name="childEntity">The attached child entity.</param>
        /// <param name="userData">The user-defined data.</param>
        void OnAttached(IEntity childEntity, object userData);

        /// <summary>
        /// Detaches a child entity.
        /// </summary>
        /// <param name="childEntity">The detached child entity.</param>
        /// <param name="userData">The user-defined data.</param>
        void OnDetached(IEntity childEntity, object userData);

        /// <summary>
        /// Attaches to a parent entity.
        /// </summary>
        /// <param name="parentEntity">The parent entity to attach to.</param>
        /// <param name="userData">The user-defined data.</param>
        void OnAttachTo(IEntity parentEntity, object userData);

        /// <summary>
        /// Detaches from a parent entity.
        /// </summary>
        /// <param name="parentEntity">The parent entity to detach from.</param>
        /// <param name="userData">The user-defined data.</param>
        void OnDetachFrom(IEntity parentEntity, object userData);

        /// <summary>
        /// Polling the entity.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        void OnUpdate(float elapseSeconds, float realElapseSeconds);
    }
}
