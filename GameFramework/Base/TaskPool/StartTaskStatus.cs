//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Start task status.
    /// </summary>
    public enum StartTaskStatus : byte
    {
        /// <summary>
        /// The task can be completed immediately.
        /// </summary>
        Done = 0,

        /// <summary>
        /// The task can be continued.
        /// </summary>
        CanResume,

        /// <summary>
        /// The task cannot continue, needs to wait for another task to finish.
        /// </summary>
        HasToWait,

        /// <summary>
        /// The task cannot continue due to an unknown error.
        /// </summary>
        UnknownError
    }
}
