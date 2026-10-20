//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Debugger
{
    /// <summary>
    /// Debugger manager interface.
    /// </summary>
    public interface IDebuggerManager
    {
        /// <summary>
        /// Gets or sets whether the debugger window is active.
        /// </summary>
        bool ActiveWindow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the debugger window root node.
        /// </summary>
        IDebuggerWindowGroup DebuggerWindowRoot
        {
            get;
        }

        /// <summary>
        /// Registers a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <param name="debuggerWindow">The debugger window to register.</param>
        /// <param name="args">Debugger window initialization arguments.</param>
        void RegisterDebuggerWindow(string path, IDebuggerWindow debuggerWindow, params object[] args);

        /// <summary>
        /// Unregisters a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <returns>Whether the debugger window was unregistered successfully.</returns>
        bool UnregisterDebuggerWindow(string path);

        /// <summary>
        /// Gets a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <returns>The debugger window to get.</returns>
        IDebuggerWindow GetDebuggerWindow(string path);

        /// <summary>
        /// Selects a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <returns>Whether the debugger window was selected successfully.</returns>
        bool SelectDebuggerWindow(string path);
    }
}
