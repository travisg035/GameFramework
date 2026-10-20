//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Entity
{
    /// <summary>
    /// Hide entity complete event.
    /// </summary>
    public sealed class HideEntityCompleteEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the HideEntityCompleteEventArgs class.
        /// </summary>
        public HideEntityCompleteEventArgs()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroup = null;
            UserData = null;
        }

        /// <summary>
        /// Gets the entity id.
        /// </summary>
        public int EntityId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the entity asset name.
        /// </summary>
        public string EntityAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the entity group to which the entity belongs.
        /// </summary>
        public IEntityGroup EntityGroup
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user-defined data.
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a hide entity complete event.
        /// </summary>
        /// <param name="entityId">Entity id.</param>
        /// <param name="entityAssetName">Entity asset name.</param>
        /// <param name="entityGroup">The entity group to which the entity belongs.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created hide entity complete event.</returns>
        public static HideEntityCompleteEventArgs Create(int entityId, string entityAssetName, IEntityGroup entityGroup, object userData)
        {
            HideEntityCompleteEventArgs hideEntityCompleteEventArgs = ReferencePool.Acquire<HideEntityCompleteEventArgs>();
            hideEntityCompleteEventArgs.EntityId = entityId;
            hideEntityCompleteEventArgs.EntityAssetName = entityAssetName;
            hideEntityCompleteEventArgs.EntityGroup = entityGroup;
            hideEntityCompleteEventArgs.UserData = userData;
            return hideEntityCompleteEventArgs;
        }

        /// <summary>
        /// Clears the hide entity complete event.
        /// </summary>
        public override void Clear()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroup = null;
            UserData = null;
        }
    }
}
