//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Sound group interface.
    /// </summary>
    public interface ISoundGroup
    {
        /// <summary>
        /// Gets the sound group name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the number of sound agents.
        /// </summary>
        int SoundAgentCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets whether the sounds in the sound group avoid being replaced by sounds of the same priority.
        /// </summary>
        bool AvoidBeingReplacedBySamePriority
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the sound group is muted.
        /// </summary>
        bool Mute
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sound group volume.
        /// </summary>
        float Volume
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the sound group helper.
        /// </summary>
        ISoundGroupHelper Helper
        {
            get;
        }

        /// <summary>
        /// Stops all loaded sounds.
        /// </summary>
        void StopAllLoadedSounds();

        /// <summary>
        /// Stops all loaded sounds.
        /// </summary>
        /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
        void StopAllLoadedSounds(float fadeOutSeconds);
    }
}
