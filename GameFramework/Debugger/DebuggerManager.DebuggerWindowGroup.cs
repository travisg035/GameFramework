//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.Debugger
{
    internal sealed partial class DebuggerManager : GameFrameworkModule, IDebuggerManager
    {
        /// <summary>
        /// Debugger window group.
        /// </summary>
        private sealed class DebuggerWindowGroup : IDebuggerWindowGroup
        {
            private readonly List<KeyValuePair<string, IDebuggerWindow>> m_DebuggerWindows;
            private int m_SelectedIndex;
            private string[] m_DebuggerWindowNames;

            public DebuggerWindowGroup()
            {
                m_DebuggerWindows = new List<KeyValuePair<string, IDebuggerWindow>>();
                m_SelectedIndex = 0;
                m_DebuggerWindowNames = null;
            }

            /// <summary>
            /// Gets the number of debugger windows.
            /// </summary>
            public int DebuggerWindowCount
            {
                get
                {
                    return m_DebuggerWindows.Count;
                }
            }

            /// <summary>
            /// Gets or sets the index of the selected debugger window.
            /// </summary>
            public int SelectedIndex
            {
                get
                {
                    return m_SelectedIndex;
                }
                set
                {
                    m_SelectedIndex = value;
                }
            }

            /// <summary>
            /// Gets the currently selected debugger window.
            /// </summary>
            public IDebuggerWindow SelectedWindow
            {
                get
                {
                    if (m_SelectedIndex >= m_DebuggerWindows.Count)
                    {
                        return null;
                    }

                    return m_DebuggerWindows[m_SelectedIndex].Value;
                }
            }

            /// <summary>
            /// Initializes the debugger window group.
            /// </summary>
            /// <param name="args">Debugger window group initialization arguments.</param>
            public void Initialize(params object[] args)
            {
            }

            /// <summary>
            /// Shuts down the debugger window group.
            /// </summary>
            public void Shutdown()
            {
                foreach (KeyValuePair<string, IDebuggerWindow> debuggerWindow in m_DebuggerWindows)
                {
                    debuggerWindow.Value.Shutdown();
                }

                m_DebuggerWindows.Clear();
            }

            /// <summary>
            /// Called when entering the debugger window.
            /// </summary>
            public void OnEnter()
            {
                SelectedWindow.OnEnter();
            }

            /// <summary>
            /// Called when leaving the debugger window.
            /// </summary>
            public void OnLeave()
            {
                SelectedWindow.OnLeave();
            }

            /// <summary>
            /// Debugger window group update.
            /// </summary>
            /// <param name="elapseSeconds">Logical elapsed seconds.</param>
            /// <param name="realElapseSeconds">Real elapsed seconds.</param>
            public void OnUpdate(float elapseSeconds, float realElapseSeconds)
            {
                SelectedWindow.OnUpdate(elapseSeconds, realElapseSeconds);
            }

            /// <summary>
            /// Debugger window draw.
            /// </summary>
            public void OnDraw()
            {
            }

            private void RefreshDebuggerWindowNames()
            {
                int index = 0;
                m_DebuggerWindowNames = new string[m_DebuggerWindows.Count];
                foreach (KeyValuePair<string, IDebuggerWindow> debuggerWindow in m_DebuggerWindows)
                {
                    m_DebuggerWindowNames[index++] = debuggerWindow.Key;
                }
            }

            /// <summary>
            /// Gets the debugger window names in the debugger window group.
            /// </summary>
            public string[] GetDebuggerWindowNames()
            {
                return m_DebuggerWindowNames;
            }

            /// <summary>
            /// Gets a debugger window.
            /// </summary>
            /// <param name="path">Debugger window path.</param>
            /// <returns>The debugger window to get.</returns>
            public IDebuggerWindow GetDebuggerWindow(string path)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return null;
                }

                int pos = path.IndexOf('/');
                if (pos < 0 || pos >= path.Length - 1)
                {
                    return InternalGetDebuggerWindow(path);
                }

                string debuggerWindowGroupName = path.Substring(0, pos);
                string leftPath = path.Substring(pos + 1);
                DebuggerWindowGroup debuggerWindowGroup = (DebuggerWindowGroup)InternalGetDebuggerWindow(debuggerWindowGroupName);
                if (debuggerWindowGroup == null)
                {
                    return null;
                }

                return debuggerWindowGroup.GetDebuggerWindow(leftPath);
            }

            /// <summary>
            /// Selects a debugger window.
            /// </summary>
            /// <param name="path">Debugger window path.</param>
            /// <returns>Whether the debugger window was selected successfully.</returns>
            public bool SelectDebuggerWindow(string path)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return false;
                }

                int pos = path.IndexOf('/');
                if (pos < 0 || pos >= path.Length - 1)
                {
                    return InternalSelectDebuggerWindow(path);
                }

                string debuggerWindowGroupName = path.Substring(0, pos);
                string leftPath = path.Substring(pos + 1);
                DebuggerWindowGroup debuggerWindowGroup = (DebuggerWindowGroup)InternalGetDebuggerWindow(debuggerWindowGroupName);
                if (debuggerWindowGroup == null || !InternalSelectDebuggerWindow(debuggerWindowGroupName))
                {
                    return false;
                }

                return debuggerWindowGroup.SelectDebuggerWindow(leftPath);
            }

            /// <summary>
            /// Registers a debugger window.
            /// </summary>
            /// <param name="path">Debugger window path.</param>
            /// <param name="debuggerWindow">The debugger window to register.</param>
            public void RegisterDebuggerWindow(string path, IDebuggerWindow debuggerWindow)
            {
                if (string.IsNullOrEmpty(path))
                {
                    throw new GameFrameworkException("Path is invalid.");
                }

                int pos = path.IndexOf('/');
                if (pos < 0 || pos >= path.Length - 1)
                {
                    if (InternalGetDebuggerWindow(path) != null)
                    {
                        throw new GameFrameworkException("Debugger window has been registered.");
                    }

                    m_DebuggerWindows.Add(new KeyValuePair<string, IDebuggerWindow>(path, debuggerWindow));
                    RefreshDebuggerWindowNames();
                }
                else
                {
                    string debuggerWindowGroupName = path.Substring(0, pos);
                    string leftPath = path.Substring(pos + 1);
                    DebuggerWindowGroup debuggerWindowGroup = (DebuggerWindowGroup)InternalGetDebuggerWindow(debuggerWindowGroupName);
                    if (debuggerWindowGroup == null)
                    {
                        if (InternalGetDebuggerWindow(debuggerWindowGroupName) != null)
                        {
                            throw new GameFrameworkException("Debugger window has been registered, can not create debugger window group.");
                        }

                        debuggerWindowGroup = new DebuggerWindowGroup();
                        m_DebuggerWindows.Add(new KeyValuePair<string, IDebuggerWindow>(debuggerWindowGroupName, debuggerWindowGroup));
                        RefreshDebuggerWindowNames();
                    }

                    debuggerWindowGroup.RegisterDebuggerWindow(leftPath, debuggerWindow);
                }
            }

            /// <summary>
            /// Unregisters a debugger window.
            /// </summary>
            /// <param name="path">Debugger window path.</param>
            /// <returns>Whether the debugger window was unregistered successfully.</returns>
            public bool UnregisterDebuggerWindow(string path)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return false;
                }

                int pos = path.IndexOf('/');
                if (pos < 0 || pos >= path.Length - 1)
                {
                    IDebuggerWindow debuggerWindow = InternalGetDebuggerWindow(path);
                    bool result = m_DebuggerWindows.Remove(new KeyValuePair<string, IDebuggerWindow>(path, debuggerWindow));
                    debuggerWindow.Shutdown();
                    RefreshDebuggerWindowNames();
                    return result;
                }

                string debuggerWindowGroupName = path.Substring(0, pos);
                string leftPath = path.Substring(pos + 1);
                DebuggerWindowGroup debuggerWindowGroup = (DebuggerWindowGroup)InternalGetDebuggerWindow(debuggerWindowGroupName);
                if (debuggerWindowGroup == null)
                {
                    return false;
                }

                return debuggerWindowGroup.UnregisterDebuggerWindow(leftPath);
            }

            private IDebuggerWindow InternalGetDebuggerWindow(string name)
            {
                foreach (KeyValuePair<string, IDebuggerWindow> debuggerWindow in m_DebuggerWindows)
                {
                    if (debuggerWindow.Key == name)
                    {
                        return debuggerWindow.Value;
                    }
                }

                return null;
            }

            private bool InternalSelectDebuggerWindow(string name)
            {
                for (int i = 0; i < m_DebuggerWindows.Count; i++)
                {
                    if (m_DebuggerWindows[i].Key == name)
                    {
                        m_SelectedIndex = i;
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
