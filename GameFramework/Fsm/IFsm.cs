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
    /// Finite state machine interface.
    /// </summary>
    /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
    public interface IFsm<T> where T : class
    {
        /// <summary>
        /// Gets the finite state machine name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the finite state machine full name.
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// Gets the finite state machine owner.
        /// </summary>
        T Owner
        {
            get;
        }

        /// <summary>
        /// Gets the number of states in the finite state machine.
        /// </summary>
        int FsmStateCount
        {
            get;
        }

        /// <summary>
        /// Gets whether the finite state machine is running.
        /// </summary>
        bool IsRunning
        {
            get;
        }

        /// <summary>
        /// Gets whether the finite state machine is destroyed.
        /// </summary>
        bool IsDestroyed
        {
            get;
        }

        /// <summary>
        /// Gets the current finite state machine state.
        /// </summary>
        FsmState<T> CurrentState
        {
            get;
        }

        /// <summary>
        /// Gets the duration of the current finite state machine state in seconds.
        /// </summary>
        float CurrentStateTime
        {
            get;
        }

        /// <summary>
        /// Starts the finite state machine.
        /// </summary>
        /// <typeparam name="TState">The type of the finite state machine state to start.</typeparam>
        void Start<TState>() where TState : FsmState<T>;

        /// <summary>
        /// Starts the finite state machine.
        /// </summary>
        /// <param name="stateType">The type of the finite state machine state to start.</param>
        void Start(Type stateType);

        /// <summary>
        /// Determines whether a state exists in the finite state machine.
        /// </summary>
        /// <typeparam name="TState">The type of the finite state machine state to check.</typeparam>
        /// <returns>Whether the finite state machine state exists.</returns>
        bool HasState<TState>() where TState : FsmState<T>;

        /// <summary>
        /// Determines whether a state exists in the finite state machine.
        /// </summary>
        /// <param name="stateType">The type of the finite state machine state to check.</param>
        /// <returns>Whether the finite state machine state exists.</returns>
        bool HasState(Type stateType);

        /// <summary>
        /// Gets a state from the finite state machine.
        /// </summary>
        /// <typeparam name="TState">The type of the finite state machine state to get.</typeparam>
        /// <returns>The finite state machine state to get.</returns>
        TState GetState<TState>() where TState : FsmState<T>;

        /// <summary>
        /// Gets a state from the finite state machine.
        /// </summary>
        /// <param name="stateType">The type of the finite state machine state to get.</param>
        /// <returns>The finite state machine state to get.</returns>
        FsmState<T> GetState(Type stateType);

        /// <summary>
        /// Gets all states of the finite state machine.
        /// </summary>
        /// <returns>All states of the finite state machine.</returns>
        FsmState<T>[] GetAllStates();

        /// <summary>
        /// Gets all states of the finite state machine.
        /// </summary>
        /// <param name="results">The list to store all states of the finite state machine.</param>
        void GetAllStates(List<FsmState<T>> results);

        /// <summary>
        /// Determines whether the finite state machine data exists.
        /// </summary>
        /// <param name="name">The name of the finite state machine data.</param>
        /// <returns>Whether the finite state machine data exists.</returns>
        bool HasData(string name);

        /// <summary>
        /// Gets data from the finite state machine.
        /// </summary>
        /// <typeparam name="TData">The type of the finite state machine data to get.</typeparam>
        /// <param name="name">The name of the finite state machine data.</param>
        /// <returns>The finite state machine data to get.</returns>
        TData GetData<TData>(string name) where TData : Variable;

        /// <summary>
        /// Gets data from the finite state machine.
        /// </summary>
        /// <param name="name">The name of the finite state machine data.</param>
        /// <returns>The finite state machine data to get.</returns>
        Variable GetData(string name);

        /// <summary>
        /// Sets data in the finite state machine.
        /// </summary>
        /// <typeparam name="TData">The type of the finite state machine data to set.</typeparam>
        /// <param name="name">The name of the finite state machine data.</param>
        /// <param name="data">The finite state machine data to set.</param>
        void SetData<TData>(string name, TData data) where TData : Variable;

        /// <summary>
        /// Sets data in the finite state machine.
        /// </summary>
        /// <param name="name">The name of the finite state machine data.</param>
        /// <param name="data">The finite state machine data to set.</param>
        void SetData(string name, Variable data);

        /// <summary>
        /// Removes data from the finite state machine.
        /// </summary>
        /// <param name="name">The name of the finite state machine data.</param>
        /// <returns>Whether the finite state machine data was removed successfully.</returns>
        bool RemoveData(string name);
    }
}
