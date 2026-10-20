//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource apply failure event.
    /// </summary>
    public sealed class ResourceApplyFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceApplyFailureEventArgs"/> class.
        /// </summary>
        public ResourceApplyFailureEventArgs()
        {
            Name = null;
            ResourcePackPath = null;
            ErrorMessage = null;
        }

        /// <summary>
        /// Gets the resource name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the resource pack path.
        /// </summary>
        public string ResourcePackPath
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
        /// Creates a new resource apply failure event.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <param name="resourcePackPath">The resource pack path.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The created resource apply failure event.</returns>
        public static ResourceApplyFailureEventArgs Create(string name, string resourcePackPath, string errorMessage)
        {
            ResourceApplyFailureEventArgs resourceApplyFailureEventArgs = ReferencePool.Acquire<ResourceApplyFailureEventArgs>();
            resourceApplyFailureEventArgs.Name = name;
            resourceApplyFailureEventArgs.ResourcePackPath = resourcePackPath;
            resourceApplyFailureEventArgs.ErrorMessage = errorMessage;
            return resourceApplyFailureEventArgs;
        }

        /// <summary>
        /// Clears the resource apply failure event.
        /// </summary>
        public override void Clear()
        {
            Name = null;
            ResourcePackPath = null;
            ErrorMessage = null;
        }
    }
}
