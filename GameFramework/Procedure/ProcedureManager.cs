//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Fsm;
using System;

namespace GameFramework.Procedure
{
    /// <summary>
    /// Procedure manager.
    /// </summary>
    internal sealed class ProcedureManager : GameFrameworkModule, IProcedureManager
    {
        private IFsmManager m_FsmManager;
        private IFsm<IProcedureManager> m_ProcedureFsm;

        /// <summary>
        /// Initializes a new instance of the ProcedureManager class.
        /// </summary>
        public ProcedureManager()
        {
            m_FsmManager = null;
            m_ProcedureFsm = null;
        }

        /// <summary>
        /// Gets the priority of the game framework module.
        /// </summary>
        /// <remarks>Modules with higher priority will be polled first and shut down later.</remarks>
        internal override int Priority
        {
            get
            {
                return -2;
            }
        }

        /// <summary>
        /// Gets the current procedure.
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                if (m_ProcedureFsm == null)
                {
                    throw new GameFrameworkException("You must initialize procedure first.");
                }

                return (ProcedureBase)m_ProcedureFsm.CurrentState;
            }
        }

        /// <summary>
        /// Gets the elapsed time of the current procedure in seconds.
        /// </summary>
        public float CurrentProcedureTime
        {
            get
            {
                if (m_ProcedureFsm == null)
                {
                    throw new GameFrameworkException("You must initialize procedure first.");
                }

                return m_ProcedureFsm.CurrentStateTime;
            }
        }

        /// <summary>
        /// Procedure manager update.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// Shutdown and cleanup the procedure manager.
        /// </summary>
        internal override void Shutdown()
        {
            if (m_FsmManager != null)
            {
                if (m_ProcedureFsm != null)
                {
                    m_FsmManager.DestroyFsm(m_ProcedureFsm);
                    m_ProcedureFsm = null;
                }

                m_FsmManager = null;
            }
        }

        /// <summary>
        /// Initializes the procedure manager.
        /// </summary>
        /// <param name="fsmManager">Finite state machine manager.</param>
        /// <param name="procedures">Procedures contained in the procedure manager.</param>
        public void Initialize(IFsmManager fsmManager, params ProcedureBase[] procedures)
        {
            if (fsmManager == null)
            {
                throw new GameFrameworkException("FSM manager is invalid.");
            }

            m_FsmManager = fsmManager;
            m_ProcedureFsm = m_FsmManager.CreateFsm(this, procedures);
        }

        /// <summary>
        /// Starts a procedure.
        /// </summary>
        /// <typeparam name="T">The type of the procedure to start.</typeparam>
        public void StartProcedure<T>() where T : ProcedureBase
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            m_ProcedureFsm.Start<T>();
        }

        /// <summary>
        /// Starts a procedure.
        /// </summary>
        /// <param name="procedureType">The type of the procedure to start.</param>
        public void StartProcedure(Type procedureType)
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            m_ProcedureFsm.Start(procedureType);
        }

        /// <summary>
        /// Checks whether a procedure exists.
        /// </summary>
        /// <typeparam name="T">The type of the procedure to check.</typeparam>
        /// <returns>Whether the procedure exists.</returns>
        public bool HasProcedure<T>() where T : ProcedureBase
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            return m_ProcedureFsm.HasState<T>();
        }

        /// <summary>
        /// Checks whether a procedure exists.
        /// </summary>
        /// <param name="procedureType">The type of the procedure to check.</param>
        /// <returns>Whether the procedure exists.</returns>
        public bool HasProcedure(Type procedureType)
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            return m_ProcedureFsm.HasState(procedureType);
        }

        /// <summary>
        /// Gets a procedure.
        /// </summary>
        /// <typeparam name="T">The type of the procedure to get.</typeparam>
        /// <returns>The procedure.</returns>
        public ProcedureBase GetProcedure<T>() where T : ProcedureBase
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            return m_ProcedureFsm.GetState<T>();
        }

        /// <summary>
        /// Gets a procedure.
        /// </summary>
        /// <param name="procedureType">The type of the procedure to get.</param>
        /// <returns>The procedure.</returns>
        public ProcedureBase GetProcedure(Type procedureType)
        {
            if (m_ProcedureFsm == null)
            {
                throw new GameFrameworkException("You must initialize procedure first.");
            }

            return (ProcedureBase)m_ProcedureFsm.GetState(procedureType);
        }
    }
}
