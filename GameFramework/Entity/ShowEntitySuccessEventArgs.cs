//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Entity
{
    /// <summary>
    /// Show entity success event.
    /// </summary>
    public sealed class ShowEntitySuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ShowEntitySuccessEventArgs class.
        /// </summary>
        public ShowEntitySuccessEventArgs()
        {
            Entity = null;
            Duration = 0f;
            UserData = null;
        }

        /// <summary>
        /// Gets the successfully shown entity.
        /// </summary>
        public IEntity Entity
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the loading duration in seconds.
        /// </summary>
        public float Duration
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
        /// Creates a show entity success event.
        /// </summary>
        /// <param name="entity">The successfully shown entity.</param>
        /// <param name="duration">Loading duration in seconds.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>The created show entity success event.</returns>
        public static ShowEntitySuccessEventArgs Create(IEntity entity, float duration, object userData)
        {
            ShowEntitySuccessEventArgs showEntitySuccessEventArgs = ReferencePool.Acquire<ShowEntitySuccessEventArgs>();
            showEntitySuccessEventArgs.Entity = entity;
            showEntitySuccessEventArgs.Duration = duration;
            showEntitySuccessEventArgs.UserData = userData;
            return showEntitySuccessEventArgs;
        }

        /// <summary>
        /// Clears the show entity success event.
        /// </summary>
        public override void Clear()
        {
            Entity = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
