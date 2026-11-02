//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

namespace GameFramework.Timer
{
    internal sealed partial class TimerManager : GameFrameworkModule, ITimerManager
    {
        private sealed class TimerItem : IReference
        {
            private int m_Id;
            private float m_Duration;
            private float m_ElapseTime;
            private int m_RepeatCount;
            private int m_RemainingRepeatCount;
            private bool m_IsUnscaled;
            private bool m_IsPaused;
            private bool m_IsDeleted;
            private GameFrameworkAction<object> m_Callback;
            private object m_UserData;

            public TimerItem()
            {
                m_Id = 0;
                m_Duration = 0f;
                m_ElapseTime = 0f;
                m_RepeatCount = 0;
                m_RemainingRepeatCount = 0;
                m_IsUnscaled = false;
                m_IsPaused = false;
                m_IsDeleted = false;
                m_Callback = null;
                m_UserData = null;
            }

            public int Id
            {
                get
                {
                    return m_Id;
                }
            }

            public float Duration
            {
                get
                {
                    return m_Duration;
                }
            }

            public float ElapseTime
            {
                get
                {
                    return m_ElapseTime;
                }
            }

            public float RemainingTime
            {
                get
                {
                    float remaining = m_Duration - m_ElapseTime;
                    return remaining > 0f ? remaining : 0f;
                }
            }

            public int RepeatCount
            {
                get
                {
                    return m_RemainingRepeatCount;
                }
            }

            public bool IsUnscaled
            {
                get
                {
                    return m_IsUnscaled;
                }
            }

            public bool IsPaused
            {
                get
                {
                    return m_IsPaused;
                }
                set
                {
                    m_IsPaused = value;
                }
            }

            public bool IsDeleted
            {
                get
                {
                    return m_IsDeleted;
                }
                set
                {
                    m_IsDeleted = value;
                }
            }

            public object UserData
            {
                get
                {
                    return m_UserData;
                }
            }

            public bool IsReached
            {
                get
                {
                    return m_ElapseTime >= m_Duration;
                }
            }

            public static TimerItem Create(int id, float duration, int repeatCount, bool isUnscaled, GameFrameworkAction<object> callback, object userData)
            {
                TimerItem timerItem = ReferencePool.Acquire<TimerItem>();
                timerItem.m_Id = id;
                timerItem.m_Duration = duration;
                timerItem.m_ElapseTime = 0f;
                timerItem.m_RepeatCount = repeatCount;
                timerItem.m_RemainingRepeatCount = repeatCount;
                timerItem.m_IsUnscaled = isUnscaled;
                timerItem.m_IsPaused = false;
                timerItem.m_IsDeleted = false;
                timerItem.m_Callback = callback;
                timerItem.m_UserData = userData;
                return timerItem;
            }

            public void Clear()
            {
                m_Id = 0;
                m_Duration = 0f;
                m_ElapseTime = 0f;
                m_RepeatCount = 0;
                m_RemainingRepeatCount = 0;
                m_IsUnscaled = false;
                m_IsPaused = false;
                m_IsDeleted = false;
                m_Callback = null;
                m_UserData = null;
            }

            public void Advance(float deltaSeconds)
            {
                m_ElapseTime += deltaSeconds;
            }

            public void Reset()
            {
                Reset(false);
            }

            public void Reset(bool resetRepeatCount)
            {
                m_ElapseTime = 0f;
                if (resetRepeatCount)
                {
                    m_RemainingRepeatCount = m_RepeatCount;
                }
            }

            public void InvokeCallback()
            {
                if (m_Callback != null)
                {
                    m_Callback(m_UserData);
                }
            }

            public bool StepRepeat()
            {
                if (m_RepeatCount <= 0)
                {
                    m_ElapseTime -= m_Duration;
                    if (m_ElapseTime < 0f)
                    {
                        m_ElapseTime = 0f;
                    }

                    return true;
                }

                m_RemainingRepeatCount--;
                if (m_RemainingRepeatCount > 0)
                {
                    m_ElapseTime -= m_Duration;
                    if (m_ElapseTime < 0f)
                    {
                        m_ElapseTime = 0f;
                    }

                    return true;
                }

                return false;
            }
        }
    }
}
