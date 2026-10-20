//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.Sound
{
    /// <summary>
    /// Sound agent helper interface.
    /// </summary>
    public interface ISoundAgentHelper
    {
        /// <summary>
        /// Gets whether the sound is currently playing.
        /// </summary>
        bool IsPlaying
        {
            get;
        }

        /// <summary>
        /// Gets the sound length.
        /// </summary>
        float Length
        {
            get;
        }

        /// <summary>
        /// Gets or sets the playback time.
        /// </summary>
        float Time
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the sound is muted.
        /// </summary>
        bool Mute
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to loop playback.
        /// </summary>
        bool Loop
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sound priority.
        /// </summary>
        int Priority
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        float Volume
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sound pitch.
        /// </summary>
        float Pitch
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sound pan stereo.
        /// </summary>
        float PanStereo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sound spatial blend.
        /// </summary>
        float SpatialBlend
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sound max distance.
        /// </summary>
        float MaxDistance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sound Doppler level.
        /// </summary>
        float DopplerLevel
        {
            get;
            set;
        }

        /// <summary>
        /// Reset sound agent event.
        /// </summary>
        event EventHandler<ResetSoundAgentEventArgs> ResetSoundAgent;

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="fadeInSeconds">The sound fade in time in seconds.</param>
        void Play(float fadeInSeconds);

        /// <summary>
        /// Stops playing the sound.
        /// </summary>
        /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
        void Stop(float fadeOutSeconds);

        /// <summary>
        /// Pauses playing the sound.
        /// </summary>
        /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
        void Pause(float fadeOutSeconds);

        /// <summary>
        /// Resumes playing the sound.
        /// </summary>
        /// <param name="fadeInSeconds">The sound fade in time in seconds.</param>
        void Resume(float fadeInSeconds);

        /// <summary>
        /// Resets the sound agent helper.
        /// </summary>
        void Reset();

        /// <summary>
        /// Sets the sound asset.
        /// </summary>
        /// <param name="soundAsset">The sound asset.</param>
        /// <returns>Whether the sound asset was set successfully.</returns>
        bool SetSoundAsset(object soundAsset);
    }
}
