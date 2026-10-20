//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Play sound update event.
    /// </summary>
    public sealed class PlaySoundUpdateEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaySoundUpdateEventArgs"/> class.
        /// </summary>
        public PlaySoundUpdateEventArgs()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundGroupName = null;
            PlaySoundParams = null;
            Progress = 0f;
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
        /// Gets the sound group name.
        /// </summary>
        public string SoundGroupName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the play sound parameters.
        /// </summary>
        public PlaySoundParams PlaySoundParams
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the loading sound progress.
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
        /// Creates a play sound update event.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound.</param>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="playSoundParams">The play sound parameters.</param>
        /// <param name="progress">The loading sound progress.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The created play sound update event.</returns>
        public static PlaySoundUpdateEventArgs Create(int serialId, string soundAssetName, string soundGroupName, PlaySoundParams playSoundParams, float progress, object userData)
        {
            PlaySoundUpdateEventArgs playSoundUpdateEventArgs = ReferencePool.Acquire<PlaySoundUpdateEventArgs>();
            playSoundUpdateEventArgs.SerialId = serialId;
            playSoundUpdateEventArgs.SoundAssetName = soundAssetName;
            playSoundUpdateEventArgs.SoundGroupName = soundGroupName;
            playSoundUpdateEventArgs.PlaySoundParams = playSoundParams;
            playSoundUpdateEventArgs.Progress = progress;
            playSoundUpdateEventArgs.UserData = userData;
            return playSoundUpdateEventArgs;
        }

        /// <summary>
        /// Clears the play sound update event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundGroupName = null;
            PlaySoundParams = null;
            Progress = 0f;
            UserData = null;
        }
    }
}
