//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Entity
{
    /// <summary>
    /// Show entity failure event.
    /// </summary>
    public sealed class ShowEntityFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ShowEntityFailureEventArgs class.
        /// </summary>
        public ShowEntityFailureEventArgs()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroupName = null;
            ErrorMessage = null;
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
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage
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
        /// Creates a show entity failure event.
        /// </summary>
        /// <param name="entityId">Entity id.</param>
        /// <param name="entityAssetName">Entity asset name.</param>
        /// <param name="entityGroupName">Entity group name.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created show entity failure event.</returns>
        public static ShowEntityFailureEventArgs Create(int entityId, string entityAssetName, string entityGroupName, string errorMessage, object userData)
        {
            ShowEntityFailureEventArgs showEntityFailureEventArgs = ReferencePool.Acquire<ShowEntityFailureEventArgs>();
            showEntityFailureEventArgs.EntityId = entityId;
            showEntityFailureEventArgs.EntityAssetName = entityAssetName;
            showEntityFailureEventArgs.EntityGroupName = entityGroupName;
            showEntityFailureEventArgs.ErrorMessage = errorMessage;
            showEntityFailureEventArgs.UserData = userData;
            return showEntityFailureEventArgs;
        }

        /// <summary>
        /// Clears the show entity failure event.
        /// </summary>
        public override void Clear()
        {
            EntityId = 0;
            EntityAssetName = null;
            EntityGroupName = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
