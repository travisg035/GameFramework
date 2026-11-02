//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.Timer
{
    /// <summary>
    /// Timer manager.
    /// </summary>
    internal sealed partial class TimerManager : GameFrameworkModule, ITimerManager
    {
        private readonly GameFrameworkLinkedList<TimerItem> m_Timers;
        private readonly Dictionary<int, TimerItem> m_TimerDict;
        private readonly Queue<TimerItem> m_PendingAddTimers;
        private int m_SerialId;
        private bool m_IsUpdating;

        /// <summary>
        /// Initializes a new instance of the TimerManager class.
        /// </summary>
        public TimerManager()
        {
            m_Timers = new GameFrameworkLinkedList<TimerItem>();
            m_TimerDict = new Dictionary<int, TimerItem>();
            m_PendingAddTimers = new Queue<TimerItem>();
            m_SerialId = 0;
            m_IsUpdating = false;
        }

        /// <summary>
        /// Gets the priority of the game framework module.
        /// </summary>
        internal override int Priority
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the number of active timers.
        /// </summary>
        public int TimerCount
        {
            get
            {
                return m_TimerDict.Count;
            }
        }

        /// <summary>
        /// Polling the timer manager module.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_IsUpdating = true;
            try
            {
                LinkedListNode<TimerItem> current = m_Timers.First;
                while (current != null)
                {
                    LinkedListNode<TimerItem> next = current.Next;
                    TimerItem item = current.Value;

                    if (!item.IsDeleted && !item.IsPaused)
                    {
                        float deltaSeconds = item.IsUnscaled ? realElapseSeconds : elapseSeconds;
                        item.Advance(deltaSeconds);

                        while (item.IsReached && !item.IsDeleted && !item.IsPaused)
                        {
                            item.InvokeCallback();

                            if (item.IsDeleted || item.IsPaused)
                            {
                                break;
                            }

                            if (!item.StepRepeat())
                            {
                                item.IsDeleted = true;
                                m_TimerDict.Remove(item.Id);
                                break;
                            }

                            if (item.Duration <= 0f)
                            {
                                break;
                            }
                        }
                    }

                    current = next;
                }
            }
            finally
            {
                m_IsUpdating = false;

                // Cleanup deleted or completed timers
                LinkedListNode<TimerItem> current = m_Timers.First;
                while (current != null)
                {
                    LinkedListNode<TimerItem> next = current.Next;
                    TimerItem item = current.Value;
                    if (item.IsDeleted)
                    {
                        m_TimerDict.Remove(item.Id);
                        m_Timers.Remove(current);
                        ReferencePool.Release(item);
                    }

                    current = next;
                }

                // Flush pending add timers
                while (m_PendingAddTimers.Count > 0)
                {
                    TimerItem pendingItem = m_PendingAddTimers.Dequeue();
                    if (!pendingItem.IsDeleted)
                    {
                        m_Timers.AddLast(pendingItem);
                    }
                    else
                    {
                        m_TimerDict.Remove(pendingItem.Id);
                        ReferencePool.Release(pendingItem);
                    }
                }
            }
        }

        /// <summary>
        /// Shutdown and cleanup the timer manager module.
        /// </summary>
        internal override void Shutdown()
        {
            RemoveAllTimers();
            m_Timers.ClearCachedNodes();
        }

        /// <summary>
        /// Checks whether a timer with the specified ID exists.
        /// </summary>
        /// <param name="timerId">The timer ID to check.</param>
        /// <returns>Whether the timer exists.</returns>
        public bool HasTimer(int timerId)
        {
            TimerItem item = null;
            return m_TimerDict.TryGetValue(timerId, out item) && !item.IsDeleted;
        }

        /// <summary>
        /// Gets timer information by timer ID.
        /// </summary>
        /// <param name="timerId">The timer ID to get.</param>
        /// <returns>Timer information.</returns>
        public TimerInfo GetTimerInfo(int timerId)
        {
            TimerItem item = null;
            if (m_TimerDict.TryGetValue(timerId, out item) && !item.IsDeleted)
            {
                return new TimerInfo(item.Id, item.Duration, item.RemainingTime, item.ElapseTime, item.RepeatCount, item.IsPaused, item.IsUnscaled, item.UserData);
            }

            return default(TimerInfo);
        }

        /// <summary>
        /// Gets all timer information.
        /// </summary>
        /// <param name="results">List to store the timer information.</param>
        public void GetAllTimerInfos(List<TimerInfo> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<int, TimerItem> pair in m_TimerDict)
            {
                TimerItem item = pair.Value;
                if (!item.IsDeleted)
                {
                    results.Add(new TimerInfo(item.Id, item.Duration, item.RemainingTime, item.ElapseTime, item.RepeatCount, item.IsPaused, item.IsUnscaled, item.UserData));
                }
            }
        }

        /// <summary>
        /// Gets all timer information.
        /// </summary>
        /// <returns>Array of all timer information.</returns>
        public TimerInfo[] GetAllTimerInfos()
        {
            List<TimerInfo> results = new List<TimerInfo>(m_TimerDict.Count);
            GetAllTimerInfos(results);
            return results.ToArray();
        }

        /// <summary>
        /// Adds a one-shot scaled timer.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="callback">Callback action when timer finishes.</param>
        /// <returns>The timer ID.</returns>
        public int AddTimer(float duration, GameFrameworkAction<object> callback)
        {
            return AddTimer(duration, 1, false, callback, null);
        }

        /// <summary>
        /// Adds a one-shot scaled timer with user data.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="callback">Callback action when timer finishes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        public int AddTimer(float duration, GameFrameworkAction<object> callback, object userData)
        {
            return AddTimer(duration, 1, false, callback, userData);
        }

        /// <summary>
        /// Adds a repeating scaled timer.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <returns>The timer ID.</returns>
        public int AddTimer(float duration, int repeatCount, GameFrameworkAction<object> callback)
        {
            return AddTimer(duration, repeatCount, false, callback, null);
        }

        /// <summary>
        /// Adds a repeating scaled timer with user data.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        public int AddTimer(float duration, int repeatCount, GameFrameworkAction<object> callback, object userData)
        {
            return AddTimer(duration, repeatCount, false, callback, userData);
        }

        /// <summary>
        /// Adds a timer with full options.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="isUnscaled">Whether to use unscaled/real time.</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        public int AddTimer(float duration, int repeatCount, bool isUnscaled, GameFrameworkAction<object> callback, object userData)
        {
            if (duration < 0f)
            {
                throw new GameFrameworkException("Timer duration is invalid.");
            }

            if (callback == null)
            {
                throw new GameFrameworkException("Timer callback is invalid.");
            }

            int id = GenerateSerialId();
            TimerItem item = TimerItem.Create(id, duration, repeatCount, isUnscaled, callback, userData);
            m_TimerDict.Add(id, item);

            if (m_IsUpdating)
            {
                m_PendingAddTimers.Enqueue(item);
            }
            else
            {
                m_Timers.AddLast(item);
            }

            return id;
        }

        /// <summary>
        /// Adds a one-shot unscaled/real-time timer.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="callback">Callback action when timer finishes.</param>
        /// <returns>The timer ID.</returns>
        public int AddUnscaledTimer(float duration, GameFrameworkAction<object> callback)
        {
            return AddTimer(duration, 1, true, callback, null);
        }

        /// <summary>
        /// Adds a one-shot unscaled/real-time timer with user data.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="callback">Callback action when timer finishes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        public int AddUnscaledTimer(float duration, GameFrameworkAction<object> callback, object userData)
        {
            return AddTimer(duration, 1, true, callback, userData);
        }

        /// <summary>
        /// Adds a repeating unscaled/real-time timer.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <returns>The timer ID.</returns>
        public int AddUnscaledTimer(float duration, int repeatCount, GameFrameworkAction<object> callback)
        {
            return AddTimer(duration, repeatCount, true, callback, null);
        }

        /// <summary>
        /// Adds a repeating unscaled/real-time timer with user data.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        public int AddUnscaledTimer(float duration, int repeatCount, GameFrameworkAction<object> callback, object userData)
        {
            return AddTimer(duration, repeatCount, true, callback, userData);
        }

        /// <summary>
        /// Pauses a timer.
        /// </summary>
        /// <param name="timerId">The timer ID to pause.</param>
        /// <returns>Whether the timer was paused successfully.</returns>
        public bool PauseTimer(int timerId)
        {
            TimerItem item = null;
            if (m_TimerDict.TryGetValue(timerId, out item) && !item.IsDeleted)
            {
                item.IsPaused = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Resumes a paused timer.
        /// </summary>
        /// <param name="timerId">The timer ID to resume.</param>
        /// <returns>Whether the timer was resumed successfully.</returns>
        public bool ResumeTimer(int timerId)
        {
            TimerItem item = null;
            if (m_TimerDict.TryGetValue(timerId, out item) && !item.IsDeleted)
            {
                item.IsPaused = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Resets a timer's elapsed time back to zero.
        /// </summary>
        /// <param name="timerId">The timer ID to reset.</param>
        /// <returns>Whether the timer was reset successfully.</returns>
        public bool ResetTimer(int timerId)
        {
            return ResetTimer(timerId, false);
        }

        /// <summary>
        /// Resets a timer's elapsed time back to zero and optionally resets the repeat count.
        /// </summary>
        /// <param name="timerId">The timer ID to reset.</param>
        /// <param name="resetRepeatCount">Whether to reset the repeat count to its original value.</param>
        /// <returns>Whether the timer was reset successfully.</returns>
        public bool ResetTimer(int timerId, bool resetRepeatCount)
        {
            TimerItem item = null;
            if (m_TimerDict.TryGetValue(timerId, out item) && !item.IsDeleted)
            {
                item.Reset(resetRepeatCount);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes a timer by ID.
        /// </summary>
        /// <param name="timerId">The timer ID to remove.</param>
        /// <returns>Whether the timer was removed successfully.</returns>
        public bool RemoveTimer(int timerId)
        {
            TimerItem item = null;
            if (!m_TimerDict.TryGetValue(timerId, out item) || item.IsDeleted)
            {
                return false;
            }

            item.IsDeleted = true;
            m_TimerDict.Remove(timerId);

            if (!m_IsUpdating)
            {
                m_Timers.Remove(item);
                ReferencePool.Release(item);
            }

            return true;
        }

        /// <summary>
        /// Removes all timers.
        /// </summary>
        public void RemoveAllTimers()
        {
            foreach (KeyValuePair<int, TimerItem> pair in m_TimerDict)
            {
                pair.Value.IsDeleted = true;
            }

            foreach (TimerItem pendingItem in m_PendingAddTimers)
            {
                pendingItem.IsDeleted = true;
            }

            m_TimerDict.Clear();

            if (!m_IsUpdating)
            {
                LinkedListNode<TimerItem> current = m_Timers.First;
                while (current != null)
                {
                    LinkedListNode<TimerItem> next = current.Next;
                    ReferencePool.Release(current.Value);
                    m_Timers.Remove(current);
                    current = next;
                }

                while (m_PendingAddTimers.Count > 0)
                {
                    ReferencePool.Release(m_PendingAddTimers.Dequeue());
                }
            }
        }

        private int GenerateSerialId()
        {
            if (m_SerialId >= int.MaxValue)
            {
                m_SerialId = 0;
            }

            while (true)
            {
                m_SerialId++;
                if (!m_TimerDict.ContainsKey(m_SerialId))
                {
                    return m_SerialId;
                }
            }
        }
    }
}
