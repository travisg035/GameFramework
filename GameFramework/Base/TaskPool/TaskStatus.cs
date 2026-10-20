//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Task status.
    /// </summary>
    public enum TaskStatus : byte
    {
        /// <summary>
        /// Not started.
        /// </summary>
        Todo = 0,

        /// <summary>
        /// In progress.
        /// </summary>
        Doing,

        /// <summary>
        /// Completed.
        /// </summary>
        Done
    }
}
