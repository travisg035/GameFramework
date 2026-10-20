//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Sound agent interface.
    /// </summary>
    public interface ISoundAgent
    {
        /// <summary>
        /// Gets the sound group to which the sound agent belongs.
        /// </summary>
        ISoundGroup SoundGroup
        {
            get;
        }

        /// <summary>
        /// Gets the serial ID of the sound.
        /// </summary>
        int SerialId
        {
            get;
        }

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
        /// Gets whether the sound is muted.
        /// </summary>
        bool Mute
        {
            get;
        }

        /// <summary>
        /// Gets or sets whether to mute in the sound group.
        /// </summary>
        bool MuteInSoundGroup
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
        /// Gets the volume.
        /// </summary>
        float Volume
        {
            get;
        }

        /// <summary>
        /// Gets or sets the volume in the sound group.
        /// </summary>
        float VolumeInSoundGroup
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
        /// Gets the sound agent helper.
        /// </summary>
        ISoundAgentHelper Helper
        {
            get;
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        void Play();

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="fadeInSeconds">The sound fade in time in seconds.</param>
        void Play(float fadeInSeconds);

        /// <summary>
        /// Stops playing the sound.
        /// </summary>
        void Stop();

        /// <summary>
        /// Stops playing the sound.
        /// </summary>
        /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
        void Stop(float fadeOutSeconds);

        /// <summary>
        /// Pauses playing the sound.
        /// </summary>
        void Pause();

        /// <summary>
        /// Pauses playing the sound.
        /// </summary>
        /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
        void Pause(float fadeOutSeconds);

        /// <summary>
        /// Resumes playing the sound.
        /// </summary>
        void Resume();

        /// <summary>
        /// Resumes playing the sound.
        /// </summary>
        /// <param name="fadeInSeconds">The sound fade in time in seconds.</param>
        void Resume(float fadeInSeconds);

        /// <summary>
        /// Resets the sound agent.
        /// </summary>
        void Reset();
    }
}
