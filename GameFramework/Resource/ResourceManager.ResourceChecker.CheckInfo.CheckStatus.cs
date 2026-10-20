//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    internal sealed partial class ResourceManager : GameFrameworkModule, IResourceManager
    {
        private sealed partial class ResourceChecker
        {
            private sealed partial class CheckInfo
            {
                /// <summary>
                /// Resource check status.
                /// </summary>
                public enum CheckStatus : byte
                {
                    /// <summary>
                    /// Resource status is unknown.
                    /// </summary>
                    Unknown = 0,

                    /// <summary>
                    /// Resource exists and is stored in the read-only area.
                    /// </summary>
                    StorageInReadOnly,

                    /// <summary>
                    /// Resource exists and is stored in the read-write area.
                    /// </summary>
                    StorageInReadWrite,

                    /// <summary>
                    /// Resource is not applicable to the current variant.
                    /// </summary>
                    Unavailable,

                    /// <summary>
                    /// Resource needs to be updated.
                    /// </summary>
                    Update,

                    /// <summary>
                    /// Resource is disused.
                    /// </summary>
                    Disuse
                }
            }
        }
    }
}
