//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;

namespace GameFramework.Fsm
{
    /// <summary>
    /// FSM module self-check utility.
    /// </summary>
    internal static class FsmSelfCheck
    {
        private sealed class DummyOwner
        {
        }

        private abstract class DummyBaseState : FsmState<DummyOwner>
        {
        }

        private sealed class StateA : DummyBaseState
        {
            public int UpdateCount
            {
                get;
                private set;
            }

            protected internal override void OnUpdate(IFsm<DummyOwner> fsm, float elapseSeconds, float realElapseSeconds)
            {
                base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
                UpdateCount++;
            }

            public void TriggerRollback(IFsm<DummyOwner> fsm)
            {
                RollbackState(fsm);
            }
        }

        private sealed class StateB : DummyBaseState
        {
            public int UpdateCount
            {
                get;
                private set;
            }

            protected internal override void OnUpdate(IFsm<DummyOwner> fsm, float elapseSeconds, float realElapseSeconds)
            {
                base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
                UpdateCount++;
            }

            public void TriggerRollback(IFsm<DummyOwner> fsm)
            {
                RollbackState(fsm);
            }
        }

        public static void Run()
        {
            FsmManager fsmManager = new FsmManager();
            DummyOwner owner = new DummyOwner();
            StateA stateA = new StateA();
            StateB stateB = new StateB();

            IFsm<DummyOwner> fsm = fsmManager.CreateFsm("TestFsm", owner, stateA, stateB);

            // 1. Check FullName caching (Zero-GC)
            string name1 = fsm.FullName;
            string name2 = fsm.FullName;
            if (!object.ReferenceEquals(name1, name2))
            {
                throw new GameFrameworkException("FSM FullName caching assertion failed: references not identical.");
            }

            // 2. Check initial state & IsInState
            fsm.Start<StateA>();
            if (!fsm.IsInState<StateA>() || !fsm.IsInState(typeof(DummyBaseState)))
            {
                throw new GameFrameworkException("FSM IsInState assertion failed for initial state.");
            }

            if (fsm.IsInState<StateB>())
            {
                throw new GameFrameworkException("FSM IsInState assertion failed: returned true for unentered state.");
            }

            // 3. Check Update and CurrentStateTime
            fsmManager.Update(2f, 2f);
            if (stateA.UpdateCount != 1 || fsm.CurrentStateTime < 1.9f)
            {
                throw new GameFrameworkException("FSM Update assertion failed.");
            }

            // 4. Check ResetCurrentStateTime
            fsm.ResetCurrentStateTime();
            if (fsm.CurrentStateTime != 0f)
            {
                throw new GameFrameworkException("FSM ResetCurrentStateTime assertion failed.");
            }

            // 5. Check Pause / Resume
            fsm.Pause();
            if (!fsm.IsPaused)
            {
                throw new GameFrameworkException("FSM Pause assertion failed.");
            }

            fsmManager.Update(1f, 1f);
            if (stateA.UpdateCount != 1 || fsm.CurrentStateTime != 0f)
            {
                throw new GameFrameworkException("FSM paused Update assertion failed: state updated while paused.");
            }

            fsm.Resume();
            if (fsm.IsPaused)
            {
                throw new GameFrameworkException("FSM Resume assertion failed.");
            }

            fsmManager.Update(1f, 1f);
            if (stateA.UpdateCount != 2 || fsm.CurrentStateTime < 0.9f)
            {
                throw new GameFrameworkException("FSM resumed Update assertion failed.");
            }

            // 6. Check ChangeState and PreviousState tracking
            float stateATime = fsm.CurrentStateTime;
            Fsm<DummyOwner> impl = (Fsm<DummyOwner>)fsm;
            impl.ChangeState<StateB>();

            if (!fsm.IsInState<StateB>())
            {
                throw new GameFrameworkException("FSM ChangeState assertion failed: not in StateB.");
            }

            if (fsm.PreviousState != stateA || fsm.PreviousStateTime != stateATime)
            {
                throw new GameFrameworkException("FSM PreviousState tracking assertion failed.");
            }

            FsmBase fsmBase = (FsmBase)fsm;
            if (fsmBase.PreviousStateName != typeof(StateA).FullName)
            {
                throw new GameFrameworkException("FSM PreviousStateName assertion failed.");
            }

            // 7. Check RollbackState
            stateB.TriggerRollback(fsm);
            if (!fsm.IsInState<StateA>())
            {
                throw new GameFrameworkException("FSM RollbackState assertion failed: not back in StateA.");
            }

            if (fsm.PreviousState != stateB)
            {
                throw new GameFrameworkException("FSM RollbackState assertion failed: PreviousState is not StateB.");
            }

            // 8. Cleanup & Shutdown
            fsmManager.Shutdown();
            if (!fsm.IsDestroyed || fsm.IsRunning)
            {
                throw new GameFrameworkException("FSM Shutdown assertion failed.");
            }
        }
    }
}
