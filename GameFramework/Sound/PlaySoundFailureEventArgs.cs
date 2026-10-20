//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Play sound failure event.
    /// </summary>
    public sealed class PlaySoundFailureEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaySoundFailureEventArgs"/> class.
        /// </summary>
        public PlaySoundFailureEventArgs()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundGroupName = null;
            PlaySoundParams = null;
            ErrorCode = PlaySoundErrorCode.Unknown;
            ErrorMessage = null;
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
        /// Gets the error code.
        /// </summary>
        public PlaySoundErrorCode ErrorCode
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
        /// Creates a play sound failure event.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound.</param>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="playSoundParams">The play sound parameters.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The created play sound failure event.</returns>
        public static PlaySoundFailureEventArgs Create(int serialId, string soundAssetName, string soundGroupName, PlaySoundParams playSoundParams, PlaySoundErrorCode errorCode, string errorMessage, object userData)
        {
            PlaySoundFailureEventArgs playSoundFailureEventArgs = ReferencePool.Acquire<PlaySoundFailureEventArgs>();
            playSoundFailureEventArgs.SerialId = serialId;
            playSoundFailureEventArgs.SoundAssetName = soundAssetName;
            playSoundFailureEventArgs.SoundGroupName = soundGroupName;
            playSoundFailureEventArgs.PlaySoundParams = playSoundParams;
            playSoundFailureEventArgs.ErrorCode = errorCode;
            playSoundFailureEventArgs.ErrorMessage = errorMessage;
            playSoundFailureEventArgs.UserData = userData;
            return playSoundFailureEventArgs;
        }

        /// <summary>
        /// Clears the play sound failure event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundGroupName = null;
            PlaySoundParams = null;
            ErrorCode = PlaySoundErrorCode.Unknown;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
