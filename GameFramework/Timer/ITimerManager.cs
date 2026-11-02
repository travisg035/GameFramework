//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.Timer
{
    /// <summary>
    /// Timer manager interface.
    /// </summary>
    public interface ITimerManager
    {
        /// <summary>
        /// Gets the number of active timers.
        /// </summary>
        int TimerCount
        {
            get;
        }

        /// <summary>
        /// Checks whether a timer with the specified ID exists.
        /// </summary>
        /// <param name="timerId">The timer ID to check.</param>
        /// <returns>Whether the timer exists.</returns>
        bool HasTimer(int timerId);

        /// <summary>
        /// Gets timer information by timer ID.
        /// </summary>
        /// <param name="timerId">The timer ID to get.</param>
        /// <returns>Timer information.</returns>
        TimerInfo GetTimerInfo(int timerId);

        /// <summary>
        /// Gets all timer information.
        /// </summary>
        /// <param name="results">List to store the timer information.</param>
        void GetAllTimerInfos(List<TimerInfo> results);

        /// <summary>
        /// Gets all timer information.
        /// </summary>
        /// <returns>Array of all timer information.</returns>
        TimerInfo[] GetAllTimerInfos();

        /// <summary>
        /// Adds a one-shot scaled timer.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="callback">Callback action when timer finishes.</param>
        /// <returns>The timer ID.</returns>
        int AddTimer(float duration, GameFrameworkAction<object> callback);

        /// <summary>
        /// Adds a one-shot scaled timer with user data.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="callback">Callback action when timer finishes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        int AddTimer(float duration, GameFrameworkAction<object> callback, object userData);

        /// <summary>
        /// Adds a repeating scaled timer.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <returns>The timer ID.</returns>
        int AddTimer(float duration, int repeatCount, GameFrameworkAction<object> callback);

        /// <summary>
        /// Adds a repeating scaled timer with user data.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        int AddTimer(float duration, int repeatCount, GameFrameworkAction<object> callback, object userData);

        /// <summary>
        /// Adds a timer with full options.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="isUnscaled">Whether to use unscaled/real time.</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        int AddTimer(float duration, int repeatCount, bool isUnscaled, GameFrameworkAction<object> callback, object userData);

        /// <summary>
        /// Adds a one-shot unscaled/real-time timer.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="callback">Callback action when timer finishes.</param>
        /// <returns>The timer ID.</returns>
        int AddUnscaledTimer(float duration, GameFrameworkAction<object> callback);

        /// <summary>
        /// Adds a one-shot unscaled/real-time timer with user data.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="callback">Callback action when timer finishes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        int AddUnscaledTimer(float duration, GameFrameworkAction<object> callback, object userData);

        /// <summary>
        /// Adds a repeating unscaled/real-time timer.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <returns>The timer ID.</returns>
        int AddUnscaledTimer(float duration, int repeatCount, GameFrameworkAction<object> callback);

        /// <summary>
        /// Adds a repeating unscaled/real-time timer with user data.
        /// </summary>
        /// <param name="duration">Duration in seconds per interval.</param>
        /// <param name="repeatCount">Repeat count (0 or negative means infinite).</param>
        /// <param name="callback">Callback action when timer finishes an interval.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>The timer ID.</returns>
        int AddUnscaledTimer(float duration, int repeatCount, GameFrameworkAction<object> callback, object userData);

        /// <summary>
        /// Pauses a timer.
        /// </summary>
        /// <param name="timerId">The timer ID to pause.</param>
        /// <returns>Whether the timer was paused successfully.</returns>
        bool PauseTimer(int timerId);

        /// <summary>
        /// Resumes a paused timer.
        /// </summary>
        /// <param name="timerId">The timer ID to resume.</param>
        /// <returns>Whether the timer was resumed successfully.</returns>
        bool ResumeTimer(int timerId);

        /// <summary>
        /// Resets a timer's elapsed time back to zero.
        /// </summary>
        /// <param name="timerId">The timer ID to reset.</param>
        /// <returns>Whether the timer was reset successfully.</returns>
        bool ResetTimer(int timerId);

        /// <summary>
        /// Resets a timer's elapsed time back to zero and optionally resets the repeat count.
        /// </summary>
        /// <param name="timerId">The timer ID to reset.</param>
        /// <param name="resetRepeatCount">Whether to reset the repeat count to its original value.</param>
        /// <returns>Whether the timer was reset successfully.</returns>
        bool ResetTimer(int timerId, bool resetRepeatCount);

        /// <summary>
        /// Removes a timer by ID.
        /// </summary>
        /// <param name="timerId">The timer ID to remove.</param>
        /// <returns>Whether the timer was removed successfully.</returns>
        bool RemoveTimer(int timerId);

        /// <summary>
        /// Removes all timers.
        /// </summary>
        void RemoveAllTimers();
    }
}
