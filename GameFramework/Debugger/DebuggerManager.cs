//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Debugger
{
    /// <summary>
    /// Debugger manager.
    /// </summary>
    internal sealed partial class DebuggerManager : GameFrameworkModule, IDebuggerManager
    {
        private readonly DebuggerWindowGroup m_DebuggerWindowRoot;
        private bool m_ActiveWindow;

        /// <summary>
        /// Initializes a new instance of the DebuggerManager class.
        /// </summary>
        public DebuggerManager()
        {
            m_DebuggerWindowRoot = new DebuggerWindowGroup();
            m_ActiveWindow = false;
        }

        /// <summary>
        /// Gets the priority of the game framework module.
        /// </summary>
        /// <remarks>Modules with higher priority will be polled first and shut down later.</remarks>
        internal override int Priority
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets or sets whether the debugger window is active.
        /// </summary>
        public bool ActiveWindow
        {
            get
            {
                return m_ActiveWindow;
            }
            set
            {
                m_ActiveWindow = value;
            }
        }

        /// <summary>
        /// Gets the debugger window root node.
        /// </summary>
        public IDebuggerWindowGroup DebuggerWindowRoot
        {
            get
            {
                return m_DebuggerWindowRoot;
            }
        }

        /// <summary>
        /// Debugger manager update.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (!m_ActiveWindow)
            {
                return;
            }

            m_DebuggerWindowRoot.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// Shutdown and cleanup the debugger manager.
        /// </summary>
        internal override void Shutdown()
        {
            m_ActiveWindow = false;
            m_DebuggerWindowRoot.Shutdown();
        }

        /// <summary>
        /// Registers a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <param name="debuggerWindow">The debugger window to register.</param>
        /// <param name="args">Debugger window initialization arguments.</param>
        public void RegisterDebuggerWindow(string path, IDebuggerWindow debuggerWindow, params object[] args)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new GameFrameworkException("Path is invalid.");
            }

            if (debuggerWindow == null)
            {
                throw new GameFrameworkException("Debugger window is invalid.");
            }

            m_DebuggerWindowRoot.RegisterDebuggerWindow(path, debuggerWindow);
            debuggerWindow.Initialize(args);
        }

        /// <summary>
        /// Unregisters a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <returns>Whether the debugger window was unregistered successfully.</returns>
        public bool UnregisterDebuggerWindow(string path)
        {
            return m_DebuggerWindowRoot.UnregisterDebuggerWindow(path);
        }

        /// <summary>
        /// Gets a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <returns>The debugger window to get.</returns>
        public IDebuggerWindow GetDebuggerWindow(string path)
        {
            return m_DebuggerWindowRoot.GetDebuggerWindow(path);
        }

        /// <summary>
        /// Selects a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <returns>Whether the debugger window was selected successfully.</returns>
        public bool SelectDebuggerWindow(string path)
        {
            return m_DebuggerWindowRoot.SelectDebuggerWindow(path);
        }
    }
}
