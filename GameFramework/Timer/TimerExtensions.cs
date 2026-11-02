//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace GameFramework.Timer
{
    /// <summary>
    /// Timer extension methods for async/await task support.
    /// </summary>
    public static class TimerExtensions
    {
        private sealed class AsyncWaitState : IReference
        {
            public int TimerId;
            public ITimerManager TimerManager;
            public TaskCompletionSource<bool> Tcs;
            public CancellationTokenRegistration Registration;

            public AsyncWaitState()
            {
                TimerId = 0;
                TimerManager = null;
                Tcs = null;
                Registration = default(CancellationTokenRegistration);
            }

            public void Clear()
            {
                TimerId = 0;
                TimerManager = null;
                Tcs = null;
                Registration = default(CancellationTokenRegistration);
            }
        }

        private static readonly GameFrameworkAction<object> s_OnTimerComplete = OnTimerCompleteCallback;

        private static void OnTimerCompleteCallback(object userData)
        {
            AsyncWaitState state = (AsyncWaitState)userData;
            state.Registration.Dispose();
            state.Tcs.TrySetResult(true);
            ReferencePool.Release(state);
        }

        /// <summary>
        /// Waits for a specified duration asynchronously using scaled time.
        /// </summary>
        /// <param name="timerManager">The timer manager.</param>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task awaiting the timer.</returns>
        public static Task WaitAsync(this ITimerManager timerManager, float duration, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WaitAsync(timerManager, duration, false, cancellationToken);
        }

        /// <summary>
        /// Waits for a specified duration asynchronously.
        /// </summary>
        /// <param name="timerManager">The timer manager.</param>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="isUnscaled">Whether to use unscaled/real time.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task awaiting the timer.</returns>
        public static Task WaitAsync(this ITimerManager timerManager, float duration, bool isUnscaled, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (timerManager == null)
            {
                throw new GameFrameworkException("Timer manager is invalid.");
            }

            if (cancellationToken.IsCancellationRequested)
            {
#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
                return Task.FromCanceled(cancellationToken);
#else
                TaskCompletionSource<bool> canceledTcs = new TaskCompletionSource<bool>();
                canceledTcs.SetCanceled();
                return canceledTcs.Task;
#endif
            }

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            AsyncWaitState state = ReferencePool.Acquire<AsyncWaitState>();
            state.TimerManager = timerManager;
            state.Tcs = tcs;

            int timerId = timerManager.AddTimer(duration, 1, isUnscaled, s_OnTimerComplete, state);
            state.TimerId = timerId;

            if (cancellationToken.CanBeCanceled)
            {
                state.Registration = cancellationToken.Register(() =>
                {
                    state.TimerManager.RemoveTimer(state.TimerId);
                    state.Tcs.TrySetCanceled();
                    ReferencePool.Release(state);
                });
            }

            return tcs.Task;
        }
    }
}
