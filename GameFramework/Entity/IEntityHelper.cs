//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Entity
{
    /// <summary>
    /// Entity helper interface.
    /// </summary>
    public interface IEntityHelper
    {
        /// <summary>
        /// Instantiates an entity.
        /// </summary>
        /// <param name="entityAsset">The entity asset to instantiate.</param>
        /// <returns>The instantiated entity.</returns>
        object InstantiateEntity(object entityAsset);

        /// <summary>
        /// Creates an entity.
        /// </summary>
        /// <param name="entityInstance">The entity instance.</param>
        /// <param name="entityGroup">The entity group to which the entity belongs.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The entity.</returns>
        IEntity CreateEntity(object entityInstance, IEntityGroup entityGroup, object userData);

        /// <summary>
        /// Releases the entity.
        /// </summary>
        /// <param name="entityAsset">The entity asset to release.</param>
        /// <param name="entityInstance">The entity instance to release.</param>
        void ReleaseEntity(object entityAsset, object entityInstance);
    }
}
