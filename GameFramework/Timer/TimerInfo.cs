//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System.Runtime.InteropServices;

namespace GameFramework.Timer
{
    /// <summary>
    /// Timer information.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct TimerInfo
    {
        private readonly int m_Id;
        private readonly float m_Duration;
        private readonly float m_RemainingTime;
        private readonly float m_ElapseTime;
        private readonly int m_RepeatCount;
        private readonly bool m_IsPaused;
        private readonly bool m_IsUnscaled;
        private readonly object m_UserData;

        /// <summary>
        /// Initializes a new instance of the TimerInfo struct.
        /// </summary>
        /// <param name="id">The timer ID.</param>
        /// <param name="duration">The timer duration in seconds.</param>
        /// <param name="remainingTime">The remaining time in seconds.</param>
        /// <param name="elapseTime">The elapsed time in seconds.</param>
        /// <param name="repeatCount">The repeat count.</param>
        /// <param name="isPaused">Whether the timer is paused.</param>
        /// <param name="isUnscaled">Whether the timer uses unscaled time.</param>
        /// <param name="userData">The custom user data.</param>
        public TimerInfo(int id, float duration, float remainingTime, float elapseTime, int repeatCount, bool isPaused, bool isUnscaled, object userData)
        {
            m_Id = id;
            m_Duration = duration;
            m_RemainingTime = remainingTime;
            m_ElapseTime = elapseTime;
            m_RepeatCount = repeatCount;
            m_IsPaused = isPaused;
            m_IsUnscaled = isUnscaled;
            m_UserData = userData;
        }

        /// <summary>
        /// Gets whether the timer info is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return m_Id > 0;
            }
        }

        /// <summary>
        /// Gets the timer ID.
        /// </summary>
        public int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// Gets the timer duration in seconds.
        /// </summary>
        public float Duration
        {
            get
            {
                return m_Duration;
            }
        }

        /// <summary>
        /// Gets the remaining time in seconds.
        /// </summary>
        public float RemainingTime
        {
            get
            {
                return m_RemainingTime;
            }
        }

        /// <summary>
        /// Gets the elapsed time in seconds.
        /// </summary>
        public float ElapseTime
        {
            get
            {
                return m_ElapseTime;
            }
        }

        /// <summary>
        /// Gets the remaining repeat count.
        /// </summary>
        /// <remarks>A value less than or equal to 0 indicates infinite repetition.</remarks>
        public int RepeatCount
        {
            get
            {
                return m_RepeatCount;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the timer is paused.
        /// </summary>
        public bool IsPaused
        {
            get
            {
                return m_IsPaused;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the timer uses unscaled time.
        /// </summary>
        public bool IsUnscaled
        {
            get
            {
                return m_IsUnscaled;
            }
        }

        /// <summary>
        /// Gets the custom user data.
        /// </summary>
        public object UserData
        {
            get
            {
                return m_UserData;
            }
        }
    }
}
