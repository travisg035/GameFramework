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
    /// Finite state machine abstract base class.
    /// </summary>
    public abstract class FsmBase
    {
        private string m_Name;
        private string m_FullName;

        /// <summary>
        /// Initializes a new instance of the FsmBase class.
        /// </summary>
        public FsmBase()
        {
            m_Name = string.Empty;
            m_FullName = null;
        }

        /// <summary>
        /// Gets the finite state machine name.
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            protected set
            {
                m_Name = value ?? string.Empty;
                m_FullName = null;
            }
        }

        /// <summary>
        /// Gets the finite state machine full name.
        /// </summary>
        public string FullName
        {
            get
            {
                if (m_FullName == null)
                {
                    m_FullName = new TypeNamePair(OwnerType, m_Name).ToString();
                }

                return m_FullName;
            }
        }

        /// <summary>
        /// Gets the finite state machine owner type.
        /// </summary>
        public abstract Type OwnerType
        {
            get;
        }

        /// <summary>
        /// Gets the number of states in the finite state machine.
        /// </summary>
        public abstract int FsmStateCount
        {
            get;
        }

        /// <summary>
        /// Gets whether the finite state machine is running.
        /// </summary>
        public abstract bool IsRunning
        {
            get;
        }

        /// <summary>
        /// Gets whether the finite state machine is destroyed.
        /// </summary>
        public abstract bool IsDestroyed
        {
            get;
        }

        /// <summary>
        /// Gets the name of the current finite state machine state.
        /// </summary>
        public abstract string CurrentStateName
        {
            get;
        }

        /// <summary>
        /// Gets the duration of the current finite state machine state in seconds.
        /// </summary>
        public abstract float CurrentStateTime
        {
            get;
        }

        /// <summary>
        /// Gets the name of the previous finite state machine state.
        /// </summary>
        public abstract string PreviousStateName
        {
            get;
        }

        /// <summary>
        /// Gets the duration of the previous finite state machine state in seconds.
        /// </summary>
        public abstract float PreviousStateTime
        {
            get;
        }

        /// <summary>
        /// Gets or sets whether the finite state machine is paused.
        /// </summary>
        public abstract bool IsPaused
        {
            get;
            set;
        }

        /// <summary>
        /// Pauses the finite state machine.
        /// </summary>
        public abstract void Pause();

        /// <summary>
        /// Resumes the finite state machine.
        /// </summary>
        public abstract void Resume();

        /// <summary>
        /// Determines whether the finite state machine is currently in the specified state.
        /// </summary>
        /// <param name="stateType">The type of the finite state machine state to check.</param>
        /// <returns>Whether the finite state machine is currently in the specified state.</returns>
        public abstract bool IsInState(Type stateType);

        /// <summary>
        /// Resets the duration of the current finite state machine state.
        /// </summary>
        public abstract void ResetCurrentStateTime();

        /// <summary>
        /// Updates the finite state machine.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// Shuts down and cleans up the finite state machine.
        /// </summary>
        internal abstract void Shutdown();
    }
}
