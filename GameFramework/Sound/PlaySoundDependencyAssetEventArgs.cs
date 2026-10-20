//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Play sound dependency asset event.
    /// </summary>
    public sealed class PlaySoundDependencyAssetEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaySoundDependencyAssetEventArgs"/> class.
        /// </summary>
        public PlaySoundDependencyAssetEventArgs()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundGroupName = null;
            PlaySoundParams = null;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
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
        /// Gets the name of the dependency asset being loaded.
        /// </summary>
        public string DependencyAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of dependency assets currently loaded.
        /// </summary>
        public int LoadedCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the total number of dependency assets to load.
        /// </summary>
        public int TotalCount
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
        /// Creates a play sound dependency asset event.
        /// </summary>
        /// <param name="serialId">The serial ID of the sound.</param>
        /// <param name="soundAssetName">The sound asset name.</param>
        /// <param name="soundGroupName">The sound group name.</param>
        /// <param name="playSoundParams">The play sound parameters.</param>
        /// <param name="dependencyAssetName">The name of the dependency asset being loaded.</param>
        /// <param name="loadedCount">The number of dependency assets currently loaded.</param>
        /// <param name="totalCount">The total number of dependency assets to load.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The created play sound dependency asset event.</returns>
        public static PlaySoundDependencyAssetEventArgs Create(int serialId, string soundAssetName, string soundGroupName, PlaySoundParams playSoundParams, string dependencyAssetName, int loadedCount, int totalCount, object userData)
        {
            PlaySoundDependencyAssetEventArgs playSoundDependencyAssetEventArgs = ReferencePool.Acquire<PlaySoundDependencyAssetEventArgs>();
            playSoundDependencyAssetEventArgs.SerialId = serialId;
            playSoundDependencyAssetEventArgs.SoundAssetName = soundAssetName;
            playSoundDependencyAssetEventArgs.SoundGroupName = soundGroupName;
            playSoundDependencyAssetEventArgs.PlaySoundParams = playSoundParams;
            playSoundDependencyAssetEventArgs.DependencyAssetName = dependencyAssetName;
            playSoundDependencyAssetEventArgs.LoadedCount = loadedCount;
            playSoundDependencyAssetEventArgs.TotalCount = totalCount;
            playSoundDependencyAssetEventArgs.UserData = userData;
            return playSoundDependencyAssetEventArgs;
        }

        /// <summary>
        /// Clears the play sound dependency asset event.
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
            SoundAssetName = null;
            SoundGroupName = null;
            PlaySoundParams = null;
            DependencyAssetName = null;
            LoadedCount = 0;
            TotalCount = 0;
            UserData = null;
        }
    }
}
