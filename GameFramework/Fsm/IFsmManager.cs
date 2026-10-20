//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.Fsm
{
    /// <summary>
    /// Finite state machine manager interface.
    /// </summary>
    public interface IFsmManager
    {
        /// <summary>
        /// Gets the number of finite state machines.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Determines whether a finite state machine exists.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <returns>Whether the finite state machine exists.</returns>
        bool HasFsm<T>() where T : class;

        /// <summary>
        /// Determines whether a finite state machine exists.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <returns>Whether the finite state machine exists.</returns>
        bool HasFsm(Type ownerType);

        /// <summary>
        /// Determines whether a finite state machine exists.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine.</param>
        /// <returns>Whether the finite state machine exists.</returns>
        bool HasFsm<T>(string name) where T : class;

        /// <summary>
        /// Determines whether a finite state machine exists.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <param name="name">The name of the finite state machine.</param>
        /// <returns>Whether the finite state machine exists.</returns>
        bool HasFsm(Type ownerType, string name);

        /// <summary>
        /// Gets a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <returns>The finite state machine to get.</returns>
        IFsm<T> GetFsm<T>() where T : class;

        /// <summary>
        /// Gets a finite state machine.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <returns>The finite state machine to get.</returns>
        FsmBase GetFsm(Type ownerType);

        /// <summary>
        /// Gets a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine.</param>
        /// <returns>The finite state machine to get.</returns>
        IFsm<T> GetFsm<T>(string name) where T : class;

        /// <summary>
        /// Gets a finite state machine.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <param name="name">The name of the finite state machine.</param>
        /// <returns>The finite state machine to get.</returns>
        FsmBase GetFsm(Type ownerType, string name);

        /// <summary>
        /// Gets all finite state machines.
        /// </summary>
        /// <returns>All finite state machines.</returns>
        FsmBase[] GetAllFsms();

        /// <summary>
        /// Gets all finite state machines.
        /// </summary>
        /// <param name="results">The list to store all finite state machines.</param>
        void GetAllFsms(List<FsmBase> results);

        /// <summary>
        /// Creates a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="owner">The owner of the finite state machine.</param>
        /// <param name="states">The state set of the finite state machine.</param>
        /// <returns>The created finite state machine.</returns>
        IFsm<T> CreateFsm<T>(T owner, params FsmState<T>[] states) where T : class;

        /// <summary>
        /// Creates a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine.</param>
        /// <param name="owner">The owner of the finite state machine.</param>
        /// <param name="states">The state set of the finite state machine.</param>
        /// <returns>The created finite state machine.</returns>
        IFsm<T> CreateFsm<T>(string name, T owner, params FsmState<T>[] states) where T : class;

        /// <summary>
        /// Creates a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="owner">The owner of the finite state machine.</param>
        /// <param name="states">The state set of the finite state machine.</param>
        /// <returns>The created finite state machine.</returns>
        IFsm<T> CreateFsm<T>(T owner, List<FsmState<T>> states) where T : class;

        /// <summary>
        /// Creates a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine.</param>
        /// <param name="owner">The owner of the finite state machine.</param>
        /// <param name="states">The state set of the finite state machine.</param>
        /// <returns>The created finite state machine.</returns>
        IFsm<T> CreateFsm<T>(string name, T owner, List<FsmState<T>> states) where T : class;

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        bool DestroyFsm<T>() where T : class;

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        bool DestroyFsm(Type ownerType);

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine to destroy.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        bool DestroyFsm<T>(string name) where T : class;

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <param name="name">The name of the finite state machine to destroy.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        bool DestroyFsm(Type ownerType, string name);

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="fsm">The finite state machine to destroy.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        bool DestroyFsm<T>(IFsm<T> fsm) where T : class;

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <param name="fsm">The finite state machine to destroy.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        bool DestroyFsm(FsmBase fsm);
    }
}
