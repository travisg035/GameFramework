//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Debugger
{
    /// <summary>
    /// Debugger window group interface.
    /// </summary>
    public interface IDebuggerWindowGroup : IDebuggerWindow
    {
        /// <summary>
        /// Gets the number of debugger windows.
        /// </summary>
        int DebuggerWindowCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the index of the selected debugger window.
        /// </summary>
        int SelectedIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the currently selected debugger window.
        /// </summary>
        IDebuggerWindow SelectedWindow
        {
            get;
        }

        /// <summary>
        /// Gets the debugger window names in the debugger window group.
        /// </summary>
        string[] GetDebuggerWindowNames();

        /// <summary>
        /// Gets a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <returns>The debugger window to get.</returns>
        IDebuggerWindow GetDebuggerWindow(string path);

        /// <summary>
        /// Registers a debugger window.
        /// </summary>
        /// <param name="path">Debugger window path.</param>
        /// <param name="debuggerWindow">The debugger window to register.</param>
        void RegisterDebuggerWindow(string path, IDebuggerWindow debuggerWindow);
    }
}
