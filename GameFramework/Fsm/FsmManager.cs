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
    /// Finite state machine manager.
    /// </summary>
    internal sealed class FsmManager : GameFrameworkModule, IFsmManager
    {
        private readonly Dictionary<TypeNamePair, FsmBase> m_Fsms;
        private readonly List<FsmBase> m_TempFsms;

        /// <summary>
        /// Initializes a new instance of the FsmManager class.
        /// </summary>
        public FsmManager()
        {
            m_Fsms = new Dictionary<TypeNamePair, FsmBase>();
            m_TempFsms = new List<FsmBase>();
        }

        /// <summary>
        /// Gets the game framework module priority.
        /// </summary>
        /// <remarks>Modules with higher priority will be updated first, and shut down later.</remarks>
        internal override int Priority
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the number of finite state machines.
        /// </summary>
        public int Count
        {
            get
            {
                return m_Fsms.Count;
            }
        }

        /// <summary>
        /// Updates the finite state machine manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_TempFsms.Clear();
            if (m_Fsms.Count <= 0)
            {
                return;
            }

            foreach (KeyValuePair<TypeNamePair, FsmBase> fsm in m_Fsms)
            {
                m_TempFsms.Add(fsm.Value);
            }

            foreach (FsmBase fsm in m_TempFsms)
            {
                if (fsm.IsDestroyed)
                {
                    continue;
                }

                fsm.Update(elapseSeconds, realElapseSeconds);
            }
        }

        /// <summary>
        /// Shuts down and cleans up the finite state machine manager.
        /// </summary>
        internal override void Shutdown()
        {
            foreach (KeyValuePair<TypeNamePair, FsmBase> fsm in m_Fsms)
            {
                fsm.Value.Shutdown();
            }

            m_Fsms.Clear();
            m_TempFsms.Clear();
        }

        /// <summary>
        /// Determines whether a finite state machine exists.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <returns>Whether the finite state machine exists.</returns>
        public bool HasFsm<T>() where T : class
        {
            return InternalHasFsm(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// Determines whether a finite state machine exists.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <returns>Whether the finite state machine exists.</returns>
        public bool HasFsm(Type ownerType)
        {
            if (ownerType == null)
            {
                throw new GameFrameworkException("Owner type is invalid.");
            }

            return InternalHasFsm(new TypeNamePair(ownerType));
        }

        /// <summary>
        /// Determines whether a finite state machine exists.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine.</param>
        /// <returns>Whether the finite state machine exists.</returns>
        public bool HasFsm<T>(string name) where T : class
        {
            return InternalHasFsm(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// Determines whether a finite state machine exists.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <param name="name">The name of the finite state machine.</param>
        /// <returns>Whether the finite state machine exists.</returns>
        public bool HasFsm(Type ownerType, string name)
        {
            if (ownerType == null)
            {
                throw new GameFrameworkException("Owner type is invalid.");
            }

            return InternalHasFsm(new TypeNamePair(ownerType, name));
        }

        /// <summary>
        /// Gets a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <returns>The finite state machine to get.</returns>
        public IFsm<T> GetFsm<T>() where T : class
        {
            return (IFsm<T>)InternalGetFsm(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// Gets a finite state machine.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <returns>The finite state machine to get.</returns>
        public FsmBase GetFsm(Type ownerType)
        {
            if (ownerType == null)
            {
                throw new GameFrameworkException("Owner type is invalid.");
            }

            return InternalGetFsm(new TypeNamePair(ownerType));
        }

        /// <summary>
        /// Gets a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine.</param>
        /// <returns>The finite state machine to get.</returns>
        public IFsm<T> GetFsm<T>(string name) where T : class
        {
            return (IFsm<T>)InternalGetFsm(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// Gets a finite state machine.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <param name="name">The name of the finite state machine.</param>
        /// <returns>The finite state machine to get.</returns>
        public FsmBase GetFsm(Type ownerType, string name)
        {
            if (ownerType == null)
            {
                throw new GameFrameworkException("Owner type is invalid.");
            }

            return InternalGetFsm(new TypeNamePair(ownerType, name));
        }

        /// <summary>
        /// Gets all finite state machines.
        /// </summary>
        /// <returns>All finite state machines.</returns>
        public FsmBase[] GetAllFsms()
        {
            int index = 0;
            FsmBase[] results = new FsmBase[m_Fsms.Count];
            foreach (KeyValuePair<TypeNamePair, FsmBase> fsm in m_Fsms)
            {
                results[index++] = fsm.Value;
            }

            return results;
        }

        /// <summary>
        /// Gets all finite state machines.
        /// </summary>
        /// <param name="results">The list to store all finite state machines.</param>
        public void GetAllFsms(List<FsmBase> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<TypeNamePair, FsmBase> fsm in m_Fsms)
            {
                results.Add(fsm.Value);
            }
        }

        /// <summary>
        /// Creates a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="owner">The owner of the finite state machine.</param>
        /// <param name="states">The state set of the finite state machine.</param>
        /// <returns>The created finite state machine.</returns>
        public IFsm<T> CreateFsm<T>(T owner, params FsmState<T>[] states) where T : class
        {
            return CreateFsm(string.Empty, owner, states);
        }

        /// <summary>
        /// Creates a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine.</param>
        /// <param name="owner">The owner of the finite state machine.</param>
        /// <param name="states">The state set of the finite state machine.</param>
        /// <returns>The created finite state machine.</returns>
        public IFsm<T> CreateFsm<T>(string name, T owner, params FsmState<T>[] states) where T : class
        {
            TypeNamePair typeNamePair = new TypeNamePair(typeof(T), name);
            if (HasFsm<T>(name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist FSM '{0}'.", typeNamePair));
            }

            Fsm<T> fsm = Fsm<T>.Create(name, owner, states);
            m_Fsms.Add(typeNamePair, fsm);
            return fsm;
        }

        /// <summary>
        /// Creates a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="owner">The owner of the finite state machine.</param>
        /// <param name="states">The state set of the finite state machine.</param>
        /// <returns>The created finite state machine.</returns>
        public IFsm<T> CreateFsm<T>(T owner, List<FsmState<T>> states) where T : class
        {
            return CreateFsm(string.Empty, owner, states);
        }

        /// <summary>
        /// Creates a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine.</param>
        /// <param name="owner">The owner of the finite state machine.</param>
        /// <param name="states">The state set of the finite state machine.</param>
        /// <returns>The created finite state machine.</returns>
        public IFsm<T> CreateFsm<T>(string name, T owner, List<FsmState<T>> states) where T : class
        {
            TypeNamePair typeNamePair = new TypeNamePair(typeof(T), name);
            if (HasFsm<T>(name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist FSM '{0}'.", typeNamePair));
            }

            Fsm<T> fsm = Fsm<T>.Create(name, owner, states);
            m_Fsms.Add(typeNamePair, fsm);
            return fsm;
        }

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        public bool DestroyFsm<T>() where T : class
        {
            return InternalDestroyFsm(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        public bool DestroyFsm(Type ownerType)
        {
            if (ownerType == null)
            {
                throw new GameFrameworkException("Owner type is invalid.");
            }

            return InternalDestroyFsm(new TypeNamePair(ownerType));
        }

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="name">The name of the finite state machine to destroy.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        public bool DestroyFsm<T>(string name) where T : class
        {
            return InternalDestroyFsm(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <param name="ownerType">The type of the finite state machine owner.</param>
        /// <param name="name">The name of the finite state machine to destroy.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        public bool DestroyFsm(Type ownerType, string name)
        {
            if (ownerType == null)
            {
                throw new GameFrameworkException("Owner type is invalid.");
            }

            return InternalDestroyFsm(new TypeNamePair(ownerType, name));
        }

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <typeparam name="T">The type of the finite state machine owner.</typeparam>
        /// <param name="fsm">The finite state machine to destroy.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        public bool DestroyFsm<T>(IFsm<T> fsm) where T : class
        {
            if (fsm == null)
            {
                throw new GameFrameworkException("FSM is invalid.");
            }

            return InternalDestroyFsm(new TypeNamePair(typeof(T), fsm.Name));
        }

        /// <summary>
        /// Destroys a finite state machine.
        /// </summary>
        /// <param name="fsm">The finite state machine to destroy.</param>
        /// <returns>Whether the finite state machine was destroyed successfully.</returns>
        public bool DestroyFsm(FsmBase fsm)
        {
            if (fsm == null)
            {
                throw new GameFrameworkException("FSM is invalid.");
            }

            return InternalDestroyFsm(new TypeNamePair(fsm.OwnerType, fsm.Name));
        }

        private bool InternalHasFsm(TypeNamePair typeNamePair)
        {
            return m_Fsms.ContainsKey(typeNamePair);
        }

        private FsmBase InternalGetFsm(TypeNamePair typeNamePair)
        {
            FsmBase fsm = null;
            if (m_Fsms.TryGetValue(typeNamePair, out fsm))
            {
                return fsm;
            }

            return null;
        }

        private bool InternalDestroyFsm(TypeNamePair typeNamePair)
        {
            FsmBase fsm = null;
            if (m_Fsms.TryGetValue(typeNamePair, out fsm))
            {
                fsm.Shutdown();
                return m_Fsms.Remove(typeNamePair);
            }

            return false;
        }
    }
}
