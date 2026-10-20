//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Play sound error codes.
    /// </summary>
    public enum PlaySoundErrorCode : byte
    {
        /// <summary>
        /// Unknown error.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Sound group does not exist.
        /// </summary>
        SoundGroupNotExist,

        /// <summary>
        /// Sound group has no sound agent.
        /// </summary>
        SoundGroupHasNoAgent,

        /// <summary>
        /// Load asset failure.
        /// </summary>
        LoadAssetFailure,

        /// <summary>
        /// Ignored due to low priority.
        /// </summary>
        IgnoredDueToLowPriority,

        /// <summary>
        /// Set sound asset failure.
        /// </summary>
        SetSoundAssetFailure
    }
}
