//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.Fsm
{
    /// <summary>
    /// Finite state machine state base class.
    /// </summary>
    /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
    public abstract class FsmState<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the FsmState class.
        /// </summary>
        public FsmState()
        {
        }

        /// <summary>
        /// Called when the finite state machine state is initialized.
        /// </summary>
        /// <param name="fsm">The finite state machine reference.</param>
        protected internal virtual void OnInit(IFsm<T> fsm)
        {
        }

        /// <summary>
        /// Called when entering the finite state machine state.
        /// </summary>
        /// <param name="fsm">The finite state machine reference.</param>
        protected internal virtual void OnEnter(IFsm<T> fsm)
        {
        }

        /// <summary>
        /// Called when polling the finite state machine state.
        /// </summary>
        /// <param name="fsm">The finite state machine reference.</param>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        protected internal virtual void OnUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// Called when leaving the finite state machine state.
        /// </summary>
        /// <param name="fsm">The finite state machine reference.</param>
        /// <param name="isShutdown">Whether it is triggered when shutting down the finite state machine.</param>
        protected internal virtual void OnLeave(IFsm<T> fsm, bool isShutdown)
        {
        }

        /// <summary>
        /// Called when destroying the finite state machine state.
        /// </summary>
        /// <param name="fsm">The finite state machine reference.</param>
        protected internal virtual void OnDestroy(IFsm<T> fsm)
        {
        }

        /// <summary>
        /// Changes the current finite state machine state.
        /// </summary>
        /// <typeparam name="TState">The type of the finite state machine state to change to.</typeparam>
        /// <param name="fsm">The finite state machine reference.</param>
        protected void ChangeState<TState>(IFsm<T> fsm) where TState : FsmState<T>
        {
            Fsm<T> fsmImplement = (Fsm<T>)fsm;
            if (fsmImplement == null)
            {
                throw new GameFrameworkException("FSM is invalid.");
            }

            fsmImplement.ChangeState<TState>();
        }

        /// <summary>
        /// Changes the current finite state machine state.
        /// </summary>
        /// <param name="fsm">The finite state machine reference.</param>
        /// <param name="stateType">The type of the finite state machine state to change to.</param>
        protected void ChangeState(IFsm<T> fsm, Type stateType)
        {
            Fsm<T> fsmImplement = (Fsm<T>)fsm;
            if (fsmImplement == null)
            {
                throw new GameFrameworkException("FSM is invalid.");
            }

            if (stateType == null)
            {
                throw new GameFrameworkException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new GameFrameworkException(Utility.Text.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            fsmImplement.ChangeState(stateType);
        }
    }
}
