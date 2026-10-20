//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.Sound
{
    internal sealed partial class SoundManager : GameFrameworkModule, ISoundManager
    {
        /// <summary>
        /// Sound agent.
        /// </summary>
        private sealed class SoundAgent : ISoundAgent
        {
            private readonly SoundGroup m_SoundGroup;
            private readonly ISoundHelper m_SoundHelper;
            private readonly ISoundAgentHelper m_SoundAgentHelper;
            private int m_SerialId;
            private object m_SoundAsset;
            private DateTime m_SetSoundAssetTime;
            private bool m_MuteInSoundGroup;
            private float m_VolumeInSoundGroup;

            /// <summary>
            /// Initializes a new instance of the SoundAgent class.
            /// </summary>
            /// <param name="soundGroup">The sound group to which the sound agent belongs.</param>
            /// <param name="soundHelper">The sound helper interface.</param>
            /// <param name="soundAgentHelper">The sound agent helper interface.</param>
            public SoundAgent(SoundGroup soundGroup, ISoundHelper soundHelper, ISoundAgentHelper soundAgentHelper)
            {
                if (soundGroup == null)
                {
                    throw new GameFrameworkException("Sound group is invalid.");
                }

                if (soundHelper == null)
                {
                    throw new GameFrameworkException("Sound helper is invalid.");
                }

                if (soundAgentHelper == null)
                {
                    throw new GameFrameworkException("Sound agent helper is invalid.");
                }

                m_SoundGroup = soundGroup;
                m_SoundHelper = soundHelper;
                m_SoundAgentHelper = soundAgentHelper;
                m_SoundAgentHelper.ResetSoundAgent += OnResetSoundAgent;
                m_SerialId = 0;
                m_SoundAsset = null;
                Reset();
            }

            /// <summary>
            /// Gets the sound group to which the sound agent belongs.
            /// </summary>
            public ISoundGroup SoundGroup
            {
                get
                {
                    return m_SoundGroup;
                }
            }

            /// <summary>
            /// Gets or sets the serial ID of the sound.
            /// </summary>
            public int SerialId
            {
                get
                {
                    return m_SerialId;
                }
                set
                {
                    m_SerialId = value;
                }
            }

            /// <summary>
            /// Gets whether the sound is currently playing.
            /// </summary>
            public bool IsPlaying
            {
                get
                {
                    return m_SoundAgentHelper.IsPlaying;
                }
            }

            /// <summary>
            /// Gets the sound length.
            /// </summary>
            public float Length
            {
                get
                {
                    return m_SoundAgentHelper.Length;
                }
            }

            /// <summary>
            /// Gets or sets the playback time.
            /// </summary>
            public float Time
            {
                get
                {
                    return m_SoundAgentHelper.Time;
                }
                set
                {
                    m_SoundAgentHelper.Time = value;
                }
            }

            /// <summary>
            /// Gets whether the sound is muted.
            /// </summary>
            public bool Mute
            {
                get
                {
                    return m_SoundAgentHelper.Mute;
                }
            }

            /// <summary>
            /// Gets or sets whether to mute in the sound group.
            /// </summary>
            public bool MuteInSoundGroup
            {
                get
                {
                    return m_MuteInSoundGroup;
                }
                set
                {
                    m_MuteInSoundGroup = value;
                    RefreshMute();
                }
            }

            /// <summary>
            /// Gets or sets whether to loop playback.
            /// </summary>
            public bool Loop
            {
                get
                {
                    return m_SoundAgentHelper.Loop;
                }
                set
                {
                    m_SoundAgentHelper.Loop = value;
                }
            }

            /// <summary>
            /// Gets or sets the sound priority.
            /// </summary>
            public int Priority
            {
                get
                {
                    return m_SoundAgentHelper.Priority;
                }
                set
                {
                    m_SoundAgentHelper.Priority = value;
                }
            }

            /// <summary>
            /// Gets the volume.
            /// </summary>
            public float Volume
            {
                get
                {
                    return m_SoundAgentHelper.Volume;
                }
            }

            /// <summary>
            /// Gets or sets the volume in the sound group.
            /// </summary>
            public float VolumeInSoundGroup
            {
                get
                {
                    return m_VolumeInSoundGroup;
                }
                set
                {
                    m_VolumeInSoundGroup = value;
                    RefreshVolume();
                }
            }

            /// <summary>
            /// Gets or sets the sound pitch.
            /// </summary>
            public float Pitch
            {
                get
                {
                    return m_SoundAgentHelper.Pitch;
                }
                set
                {
                    m_SoundAgentHelper.Pitch = value;
                }
            }

            /// <summary>
            /// Gets or sets the sound pan stereo.
            /// </summary>
            public float PanStereo
            {
                get
                {
                    return m_SoundAgentHelper.PanStereo;
                }
                set
                {
                    m_SoundAgentHelper.PanStereo = value;
                }
            }

            /// <summary>
            /// Gets or sets the sound spatial blend.
            /// </summary>
            public float SpatialBlend
            {
                get
                {
                    return m_SoundAgentHelper.SpatialBlend;
                }
                set
                {
                    m_SoundAgentHelper.SpatialBlend = value;
                }
            }

            /// <summary>
            /// Gets or sets the sound max distance.
            /// </summary>
            public float MaxDistance
            {
                get
                {
                    return m_SoundAgentHelper.MaxDistance;
                }
                set
                {
                    m_SoundAgentHelper.MaxDistance = value;
                }
            }

            /// <summary>
            /// Gets or sets the sound Doppler level.
            /// </summary>
            public float DopplerLevel
            {
                get
                {
                    return m_SoundAgentHelper.DopplerLevel;
                }
                set
                {
                    m_SoundAgentHelper.DopplerLevel = value;
                }
            }

            /// <summary>
            /// Gets the sound agent helper.
            /// </summary>
            public ISoundAgentHelper Helper
            {
                get
                {
                    return m_SoundAgentHelper;
                }
            }

            /// <summary>
            /// Gets the time when the sound asset was set.
            /// </summary>
            internal DateTime SetSoundAssetTime
            {
                get
                {
                    return m_SetSoundAssetTime;
                }
            }

            /// <summary>
            /// Plays the sound.
            /// </summary>
            public void Play()
            {
                m_SoundAgentHelper.Play(Constant.DefaultFadeInSeconds);
            }

            /// <summary>
            /// Plays the sound.
            /// </summary>
            /// <param name="fadeInSeconds">The sound fade in time in seconds.</param>
            public void Play(float fadeInSeconds)
            {
                m_SoundAgentHelper.Play(fadeInSeconds);
            }

            /// <summary>
            /// Stops playing the sound.
            /// </summary>
            public void Stop()
            {
                m_SoundAgentHelper.Stop(Constant.DefaultFadeOutSeconds);
            }

            /// <summary>
            /// Stops playing the sound.
            /// </summary>
            /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
            public void Stop(float fadeOutSeconds)
            {
                m_SoundAgentHelper.Stop(fadeOutSeconds);
            }

            /// <summary>
            /// Pauses playing the sound.
            /// </summary>
            public void Pause()
            {
                m_SoundAgentHelper.Pause(Constant.DefaultFadeOutSeconds);
            }

            /// <summary>
            /// Pauses playing the sound.
            /// </summary>
            /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
            public void Pause(float fadeOutSeconds)
            {
                m_SoundAgentHelper.Pause(fadeOutSeconds);
            }

            /// <summary>
            /// Resumes playing the sound.
            /// </summary>
            public void Resume()
            {
                m_SoundAgentHelper.Resume(Constant.DefaultFadeInSeconds);
            }

            /// <summary>
            /// Resumes playing the sound.
            /// </summary>
            /// <param name="fadeInSeconds">The sound fade in time in seconds.</param>
            public void Resume(float fadeInSeconds)
            {
                m_SoundAgentHelper.Resume(fadeInSeconds);
            }

            /// <summary>
            /// Resets the sound agent.
            /// </summary>
            public void Reset()
            {
                if (m_SoundAsset != null)
                {
                    m_SoundHelper.ReleaseSoundAsset(m_SoundAsset);
                    m_SoundAsset = null;
                }

                m_SetSoundAssetTime = DateTime.MinValue;
                Time = Constant.DefaultTime;
                MuteInSoundGroup = Constant.DefaultMute;
                Loop = Constant.DefaultLoop;
                Priority = Constant.DefaultPriority;
                VolumeInSoundGroup = Constant.DefaultVolume;
                Pitch = Constant.DefaultPitch;
                PanStereo = Constant.DefaultPanStereo;
                SpatialBlend = Constant.DefaultSpatialBlend;
                MaxDistance = Constant.DefaultMaxDistance;
                DopplerLevel = Constant.DefaultDopplerLevel;
                m_SoundAgentHelper.Reset();
            }

            internal bool SetSoundAsset(object soundAsset)
            {
                Reset();
                m_SoundAsset = soundAsset;
                m_SetSoundAssetTime = DateTime.UtcNow;
                return m_SoundAgentHelper.SetSoundAsset(soundAsset);
            }

            internal void RefreshMute()
            {
                m_SoundAgentHelper.Mute = m_SoundGroup.Mute || m_MuteInSoundGroup;
            }

            internal void RefreshVolume()
            {
                m_SoundAgentHelper.Volume = m_SoundGroup.Volume * m_VolumeInSoundGroup;
            }

            private void OnResetSoundAgent(object sender, ResetSoundAgentEventArgs e)
            {
                Reset();
            }
        }
    }
}
