//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Fsm;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameFramework.Procedure
{
    /// <summary>
    /// Base class for procedures.
    /// </summary>
    public abstract class ProcedureBase : FsmState<IProcedureManager>
    {
        /// <summary>
        /// Called when the procedure state is initialized.
        /// </summary>
        /// <param name="procedureOwner">The owner of the procedure.</param>
        protected internal override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        /// <summary>
        /// Called when entering the procedure state.
        /// </summary>
        /// <param name="procedureOwner">The owner of the procedure.</param>
        protected internal override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
        }

        /// <summary>
        /// Called when updating the procedure state.
        /// </summary>
        /// <param name="procedureOwner">The owner of the procedure.</param>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        protected internal override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// Called when leaving the procedure state.
        /// </summary>
        /// <param name="procedureOwner">The owner of the procedure.</param>
        /// <param name="isShutdown">Whether it is triggered by shutting down the finite state machine.</param>
        protected internal override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

        /// <summary>
        /// Called when destroying the procedure state.
        /// </summary>
        /// <param name="procedureOwner">The owner of the procedure.</param>
        protected internal override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}
