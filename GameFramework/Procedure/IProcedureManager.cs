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
    /// Procedure manager interface.
    /// </summary>
    public interface IProcedureManager
    {
        /// <summary>
        /// Gets the current procedure.
        /// </summary>
        ProcedureBase CurrentProcedure
        {
            get;
        }

        /// <summary>
        /// Gets the elapsed time of the current procedure in seconds.
        /// </summary>
        float CurrentProcedureTime
        {
            get;
        }

        /// <summary>
        /// Initializes the procedure manager.
        /// </summary>
        /// <param name="fsmManager">Finite state machine manager.</param>
        /// <param name="procedures">Procedures contained in the procedure manager.</param>
        void Initialize(IFsmManager fsmManager, params ProcedureBase[] procedures);

        /// <summary>
        /// Starts a procedure.
        /// </summary>
        /// <typeparam name="T">The type of the procedure to start.</typeparam>
        void StartProcedure<T>() where T : ProcedureBase;

        /// <summary>
        /// Starts a procedure.
        /// </summary>
        /// <param name="procedureType">The type of the procedure to start.</param>
        void StartProcedure(Type procedureType);

        /// <summary>
        /// Checks whether a procedure exists.
        /// </summary>
        /// <typeparam name="T">The type of the procedure to check.</typeparam>
        /// <returns>Whether the procedure exists.</returns>
        bool HasProcedure<T>() where T : ProcedureBase;

        /// <summary>
        /// Checks whether a procedure exists.
        /// </summary>
        /// <param name="procedureType">The type of the procedure to check.</param>
        /// <returns>Whether the procedure exists.</returns>
        bool HasProcedure(Type procedureType);

        /// <summary>
        /// Gets a procedure.
        /// </summary>
        /// <typeparam name="T">The type of the procedure to get.</typeparam>
        /// <returns>The procedure.</returns>
        ProcedureBase GetProcedure<T>() where T : ProcedureBase;

        /// <summary>
        /// Gets a procedure.
        /// </summary>
        /// <param name="procedureType">The type of the procedure to get.</param>
        /// <returns>The procedure.</returns>
        ProcedureBase GetProcedure(Type procedureType);
    }
}
