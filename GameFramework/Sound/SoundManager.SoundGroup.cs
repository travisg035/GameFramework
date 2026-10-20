//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.Sound
{
    internal sealed partial class SoundManager : GameFrameworkModule, ISoundManager
    {
        /// <summary>
        /// Sound group.
        /// </summary>
        private sealed class SoundGroup : ISoundGroup
        {
            private readonly string m_Name;
            private readonly ISoundGroupHelper m_SoundGroupHelper;
            private readonly List<SoundAgent> m_SoundAgents;
            private bool m_AvoidBeingReplacedBySamePriority;
            private bool m_Mute;
            private float m_Volume;

            /// <summary>
            /// Initializes a new instance of the SoundGroup class.
            /// </summary>
            /// <param name="name">Sound group name.</param>
            /// <param name="soundGroupHelper">Sound group helper.</param>
            public SoundGroup(string name, ISoundGroupHelper soundGroupHelper)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new GameFrameworkException("Sound group name is invalid.");
                }

                if (soundGroupHelper == null)
                {
                    throw new GameFrameworkException("Sound group helper is invalid.");
                }

                m_Name = name;
                m_SoundGroupHelper = soundGroupHelper;
                m_SoundAgents = new List<SoundAgent>();
            }

            /// <summary>
            /// Gets the sound group name.
            /// </summary>
            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            /// <summary>
            /// Gets the number of sound agents.
            /// </summary>
            public int SoundAgentCount
            {
                get
                {
                    return m_SoundAgents.Count;
                }
            }

            /// <summary>
            /// Gets or sets whether the sounds in the sound group avoid being replaced by sounds of the same priority.
            /// </summary>
            public bool AvoidBeingReplacedBySamePriority
            {
                get
                {
                    return m_AvoidBeingReplacedBySamePriority;
                }
                set
                {
                    m_AvoidBeingReplacedBySamePriority = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the sound group is muted.
            /// </summary>
            public bool Mute
            {
                get
                {
                    return m_Mute;
                }
                set
                {
                    m_Mute = value;
                    foreach (SoundAgent soundAgent in m_SoundAgents)
                    {
                        soundAgent.RefreshMute();
                    }
                }
            }

            /// <summary>
            /// Gets or sets the sound group volume.
            /// </summary>
            public float Volume
            {
                get
                {
                    return m_Volume;
                }
                set
                {
                    m_Volume = value;
                    foreach (SoundAgent soundAgent in m_SoundAgents)
                    {
                        soundAgent.RefreshVolume();
                    }
                }
            }

            /// <summary>
            /// Gets the sound group helper.
            /// </summary>
            public ISoundGroupHelper Helper
            {
                get
                {
                    return m_SoundGroupHelper;
                }
            }

            /// <summary>
            /// Adds a sound agent helper.
            /// </summary>
            /// <param name="soundHelper">Sound helper interface.</param>
            /// <param name="soundAgentHelper">The sound agent helper to add.</param>
            public void AddSoundAgentHelper(ISoundHelper soundHelper, ISoundAgentHelper soundAgentHelper)
            {
                m_SoundAgents.Add(new SoundAgent(this, soundHelper, soundAgentHelper));
            }

            /// <summary>
            /// Plays a sound.
            /// </summary>
            /// <param name="serialId">The serial ID of the sound.</param>
            /// <param name="soundAsset">The sound asset.</param>
            /// <param name="playSoundParams">The play sound parameters.</param>
            /// <param name="errorCode">The error code.</param>
            /// <returns>The sound agent used to play the sound.</returns>
            public ISoundAgent PlaySound(int serialId, object soundAsset, PlaySoundParams playSoundParams, out PlaySoundErrorCode? errorCode)
            {
                errorCode = null;
                SoundAgent candidateAgent = null;
                foreach (SoundAgent soundAgent in m_SoundAgents)
                {
                    if (!soundAgent.IsPlaying)
                    {
                        candidateAgent = soundAgent;
                        break;
                    }

                    if (soundAgent.Priority < playSoundParams.Priority)
                    {
                        if (candidateAgent == null || soundAgent.Priority < candidateAgent.Priority)
                        {
                            candidateAgent = soundAgent;
                        }
                    }
                    else if (!m_AvoidBeingReplacedBySamePriority && soundAgent.Priority == playSoundParams.Priority)
                    {
                        if (candidateAgent == null || soundAgent.SetSoundAssetTime < candidateAgent.SetSoundAssetTime)
                        {
                            candidateAgent = soundAgent;
                        }
                    }
                }

                if (candidateAgent == null)
                {
                    errorCode = PlaySoundErrorCode.IgnoredDueToLowPriority;
                    return null;
                }

                if (!candidateAgent.SetSoundAsset(soundAsset))
                {
                    errorCode = PlaySoundErrorCode.SetSoundAssetFailure;
                    return null;
                }

                candidateAgent.SerialId = serialId;
                candidateAgent.Time = playSoundParams.Time;
                candidateAgent.MuteInSoundGroup = playSoundParams.MuteInSoundGroup;
                candidateAgent.Loop = playSoundParams.Loop;
                candidateAgent.Priority = playSoundParams.Priority;
                candidateAgent.VolumeInSoundGroup = playSoundParams.VolumeInSoundGroup;
                candidateAgent.Pitch = playSoundParams.Pitch;
                candidateAgent.PanStereo = playSoundParams.PanStereo;
                candidateAgent.SpatialBlend = playSoundParams.SpatialBlend;
                candidateAgent.MaxDistance = playSoundParams.MaxDistance;
                candidateAgent.DopplerLevel = playSoundParams.DopplerLevel;
                candidateAgent.Play(playSoundParams.FadeInSeconds);
                return candidateAgent;
            }

            /// <summary>
            /// Stops playing the sound.
            /// </summary>
            /// <param name="serialId">The serial ID of the sound to stop.</param>
            /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
            /// <returns>Whether the sound was stopped successfully.</returns>
            public bool StopSound(int serialId, float fadeOutSeconds)
            {
                foreach (SoundAgent soundAgent in m_SoundAgents)
                {
                    if (soundAgent.SerialId != serialId)
                    {
                        continue;
                    }

                    soundAgent.Stop(fadeOutSeconds);
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Pauses playing the sound.
            /// </summary>
            /// <param name="serialId">The serial ID of the sound to pause.</param>
            /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
            /// <returns>Whether the sound was paused successfully.</returns>
            public bool PauseSound(int serialId, float fadeOutSeconds)
            {
                foreach (SoundAgent soundAgent in m_SoundAgents)
                {
                    if (soundAgent.SerialId != serialId)
                    {
                        continue;
                    }

                    soundAgent.Pause(fadeOutSeconds);
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Resumes playing the sound.
            /// </summary>
            /// <param name="serialId">The serial ID of the sound to resume.</param>
            /// <param name="fadeInSeconds">The sound fade in time in seconds.</param>
            /// <returns>Whether the sound was resumed successfully.</returns>
            public bool ResumeSound(int serialId, float fadeInSeconds)
            {
                foreach (SoundAgent soundAgent in m_SoundAgents)
                {
                    if (soundAgent.SerialId != serialId)
                    {
                        continue;
                    }

                    soundAgent.Resume(fadeInSeconds);
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Stops all loaded sounds.
            /// </summary>
            public void StopAllLoadedSounds()
            {
                foreach (SoundAgent soundAgent in m_SoundAgents)
                {
                    if (soundAgent.IsPlaying)
                    {
                        soundAgent.Stop();
                    }
                }
            }

            /// <summary>
            /// Stops all loaded sounds.
            /// </summary>
            /// <param name="fadeOutSeconds">The sound fade out time in seconds.</param>
            public void StopAllLoadedSounds(float fadeOutSeconds)
            {
                foreach (SoundAgent soundAgent in m_SoundAgents)
                {
                    if (soundAgent.IsPlaying)
                    {
                        soundAgent.Stop(fadeOutSeconds);
                    }
                }
            }
        }
    }
}
