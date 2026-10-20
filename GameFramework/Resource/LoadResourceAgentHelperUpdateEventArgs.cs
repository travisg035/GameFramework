//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load resource agent helper update event.
    /// </summary>
    public sealed class LoadResourceAgentHelperUpdateEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadResourceAgentHelperUpdateEventArgs"/> class.
        /// </summary>
        public LoadResourceAgentHelperUpdateEventArgs()
        {
            Type = LoadResourceProgress.Unknown;
            Progress = 0f;
        }

        /// <summary>
        /// Gets the progress type.
        /// </summary>
        public LoadResourceProgress Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the progress.
        /// </summary>
        public float Progress
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new load resource agent helper update event.
        /// </summary>
        /// <param name="type">The progress type.</param>
        /// <param name="progress">The progress value.</param>
        /// <returns>The created load resource agent helper update event.</returns>
        public static LoadResourceAgentHelperUpdateEventArgs Create(LoadResourceProgress type, float progress)
        {
            LoadResourceAgentHelperUpdateEventArgs loadResourceAgentHelperUpdateEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperUpdateEventArgs>();
            loadResourceAgentHelperUpdateEventArgs.Type = type;
            loadResourceAgentHelperUpdateEventArgs.Progress = progress;
            return loadResourceAgentHelperUpdateEventArgs;
        }

        /// <summary>
        /// Clears the load resource agent helper update event.
        /// </summary>
        public override void Clear()
        {
            Type = LoadResourceProgress.Unknown;
            Progress = 0f;
        }
    }
}
