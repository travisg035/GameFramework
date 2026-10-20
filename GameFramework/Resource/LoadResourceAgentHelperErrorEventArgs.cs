//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load resource agent helper error event.
    /// </summary>
    public sealed class LoadResourceAgentHelperErrorEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadResourceAgentHelperErrorEventArgs"/> class.
        /// </summary>
        public LoadResourceAgentHelperErrorEventArgs()
        {
            Status = LoadResourceStatus.Success;
            ErrorMessage = null;
        }

        /// <summary>
        /// Gets the load resource status.
        /// </summary>
        public LoadResourceStatus Status
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
        /// Creates a new load resource agent helper error event.
        /// </summary>
        /// <param name="status">The load resource status.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The created load resource agent helper error event.</returns>
        public static LoadResourceAgentHelperErrorEventArgs Create(LoadResourceStatus status, string errorMessage)
        {
            LoadResourceAgentHelperErrorEventArgs loadResourceAgentHelperErrorEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperErrorEventArgs>();
            loadResourceAgentHelperErrorEventArgs.Status = status;
            loadResourceAgentHelperErrorEventArgs.ErrorMessage = errorMessage;
            return loadResourceAgentHelperErrorEventArgs;
        }

        /// <summary>
        /// Clears the load resource agent helper error event.
        /// </summary>
        public override void Clear()
        {
            Status = LoadResourceStatus.Success;
            ErrorMessage = null;
        }
    }
}
