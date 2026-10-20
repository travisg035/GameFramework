//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Entity
{
    /// <summary>
    /// Show entity update event.
    /// </summary>
    public sealed class ShowEntityUpdateEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ShowEntityUpdateEventArgs class.
        /// </summary>
        public ShowEntityUpdateEventArgs()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroupName = null;
            Progress = 0f;
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
        /// Gets the entity group name.
        /// </summary>
        public string EntityGroupName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the show entity progress.
        /// </summary>
        public float Progress
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
        /// Creates a show entity update event.
        /// </summary>
        /// <param name="entityId">Entity id.</param>
        /// <param name="entityAssetName">Entity asset name.</param>
        /// <param name="entityGroupName">Entity group name.</param>
        /// <param name="progress">Show entity progress.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created show entity update event.</returns>
        public static ShowEntityUpdateEventArgs Create(int entityId, string entityAssetName, string entityGroupName, float progress, object userData)
        {
            ShowEntityUpdateEventArgs showEntityUpdateEventArgs = ReferencePool.Acquire<ShowEntityUpdateEventArgs>();
            showEntityUpdateEventArgs.EntityId = entityId;
            showEntityUpdateEventArgs.EntityAssetName = entityAssetName;
            showEntityUpdateEventArgs.EntityGroupName = entityGroupName;
            showEntityUpdateEventArgs.Progress = progress;
            showEntityUpdateEventArgs.UserData = userData;
            return showEntityUpdateEventArgs;
        }

        /// <summary>
        /// Clears the show entity update event.
        /// </summary>
        public override void Clear()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroupName = null;
            Progress = 0f;
            UserData = null;
        }
    }
}
