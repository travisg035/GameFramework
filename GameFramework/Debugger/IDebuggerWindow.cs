//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Debugger
{
    /// <summary>
    /// Debugger window interface.
    /// </summary>
    public interface IDebuggerWindow
    {
        /// <summary>
        /// Initializes the debugger window.
        /// </summary>
        /// <param name="args">Debugger window initialization arguments.</param>
        void Initialize(params object[] args);

        /// <summary>
        /// Shuts down the debugger window.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Called when entering the debugger window.
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Called when leaving the debugger window.
        /// </summary>
        void OnLeave();

        /// <summary>
        /// Debugger window update.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        void OnUpdate(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// Debugger window draw.
        /// </summary>
        void OnDraw();
    }
}
