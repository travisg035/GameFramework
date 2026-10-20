//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Play sound success event.
    /// </summary>
    public sealed class PlaySoundSuccessEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaySoundSuccessEventArgs"/> class.
        /// </summary>
        public PlaySoundSuccessEventArgs()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundAgent = null;
            Duration = 0f;
            UserData = null;
        }

        /// <summary>
        /// Gets the serial ID of the sound.
        /// </summary>
        public int SerialId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the sound asset name.
        /// </summary>
        public string SoundAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the sound agent used to play the sound.
        /// </summary>
        public ISoundAgent SoundAgent
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the loading duration.
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
        /// Creates a play sound success event.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound.</param>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundAgent">The sound agent used to play the sound.</param>
        /// <param name="duration">The loading duration.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The created play sound success event.</returns>
        public static PlaySoundSuccessEventArgs Create(int serialId, string soundAssetName, ISoundAgent soundAgent, float duration, object userData)
        {
            PlaySoundSuccessEventArgs playSoundSuccessEventArgs = ReferencePool.Acquire<PlaySoundSuccessEventArgs>();
            playSoundSuccessEventArgs.SerialId = serialId;
            playSoundSuccessEventArgs.SoundAssetName = soundAssetName;
            playSoundSuccessEventArgs.SoundAgent = soundAgent;
            playSoundSuccessEventArgs.Duration = duration;
            playSoundSuccessEventArgs.UserData = userData;
            return playSoundSuccessEventArgs;
        }

        /// <summary>
        /// Clears the play sound success event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundAgent = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
