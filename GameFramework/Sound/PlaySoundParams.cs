//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Play sound parameters.
    /// </summary>
    public sealed class PlaySoundParams : IReference
    {
        private bool m_Referenced;
        private float m_Time;
        private bool m_MuteInSoundGroup;
        private bool m_Loop;
        private int m_Priority;
        private float m_VolumeInSoundGroup;
        private float m_FadeInSeconds;
        private float m_Pitch;
        private float m_PanStereo;
        private float m_SpatialBlend;
        private float m_MaxDistance;
        private float m_DopplerLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaySoundParams"/> class.
        /// </summary>
        public PlaySoundParams()
        {
            m_Referenced = false;
            m_Time = Constant.DefaultTime;
            m_MuteInSoundGroup = Constant.DefaultMute;
            m_Loop = Constant.DefaultLoop;
            m_Priority = Constant.DefaultPriority;
            m_VolumeInSoundGroup = Constant.DefaultVolume;
            m_FadeInSeconds = Constant.DefaultFadeInSeconds;
            m_Pitch = Constant.DefaultPitch;
            m_PanStereo = Constant.DefaultPanStereo;
            m_SpatialBlend = Constant.DefaultSpatialBlend;
            m_MaxDistance = Constant.DefaultMaxDistance;
            m_DopplerLevel = Constant.DefaultDopplerLevel;
        }

        /// <summary>
        /// Gets or sets the playback time.
        /// </summary>
        public float Time
        {
            get
            {
                return m_Time;
            }
            set
            {
                m_Time = value;
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
            }
        }

        /// <summary>
        /// Gets or sets whether to loop playback.
        /// </summary>
        public bool Loop
        {
            get
            {
                return m_Loop;
            }
            set
            {
                m_Loop = value;
            }
        }

        /// <summary>
        /// Gets or sets the sound priority.
        /// </summary>
        public int Priority
        {
            get
            {
                return m_Priority;
            }
            set
            {
                m_Priority = value;
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
            }
        }

        /// <summary>
        /// Gets or sets the sound fade in time in seconds.
        /// </summary>
        public float FadeInSeconds
        {
            get
            {
                return m_FadeInSeconds;
            }
            set
            {
                m_FadeInSeconds = value;
            }
        }

        /// <summary>
        /// Gets or sets the sound pitch.
        /// </summary>
        public float Pitch
        {
            get
            {
                return m_Pitch;
            }
            set
            {
                m_Pitch = value;
            }
        }

        /// <summary>
        /// Gets or sets the sound pan stereo.
        /// </summary>
        public float PanStereo
        {
            get
            {
                return m_PanStereo;
            }
            set
            {
                m_PanStereo = value;
            }
        }

        /// <summary>
        /// Gets or sets the sound spatial blend.
        /// </summary>
        public float SpatialBlend
        {
            get
            {
                return m_SpatialBlend;
            }
            set
            {
                m_SpatialBlend = value;
            }
        }

        /// <summary>
        /// Gets or sets the sound max distance.
        /// </summary>
        public float MaxDistance
        {
            get
            {
                return m_MaxDistance;
            }
            set
            {
                m_MaxDistance = value;
            }
        }

        /// <summary>
        /// Gets or sets the sound Doppler level.
        /// </summary>
        public float DopplerLevel
        {
            get
            {
                return m_DopplerLevel;
            }
            set
            {
                m_DopplerLevel = value;
            }
        }

        internal bool Referenced
        {
            get
            {
                return m_Referenced;
            }
        }

        /// <summary>
        /// Creates play sound parameters.
        /// </summary>
        /// <returns>The created play sound parameters.</returns>
        public static PlaySoundParams Create()
        {
            PlaySoundParams playSoundParams = ReferencePool.Acquire<PlaySoundParams>();
            playSoundParams.m_Referenced = true;
            return playSoundParams;
        }

        /// <summary>
        /// Clears play sound parameters.
        /// </summary>
        public void Clear()
        {
            m_Time = Constant.DefaultTime;
            m_MuteInSoundGroup = Constant.DefaultMute;
            m_Loop = Constant.DefaultLoop;
            m_Priority = Constant.DefaultPriority;
            m_VolumeInSoundGroup = Constant.DefaultVolume;
            m_FadeInSeconds = Constant.DefaultFadeInSeconds;
            m_Pitch = Constant.DefaultPitch;
            m_PanStereo = Constant.DefaultPanStereo;
            m_SpatialBlend = Constant.DefaultSpatialBlend;
            m_MaxDistance = Constant.DefaultMaxDistance;
            m_DopplerLevel = Constant.DefaultDopplerLevel;
        }
    }
}
