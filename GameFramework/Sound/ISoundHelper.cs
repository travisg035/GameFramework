//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Sound helper interface.
    /// </summary>
    public interface ISoundHelper
    {
        /// <summary>
        /// Releases the sound asset.
        /// </summary>
        /// <param name="soundAsset">The sound asset to release.</param>
        void ReleaseSoundAsset(object soundAsset);
    }
}
