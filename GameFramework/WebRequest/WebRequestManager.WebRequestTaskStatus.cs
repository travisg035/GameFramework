//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.WebRequest
{
    internal sealed partial class WebRequestManager : GameFrameworkModule, IWebRequestManager
    {
        /// <summary>
        /// Web request task status.
        /// </summary>
        private enum WebRequestTaskStatus : byte
        {
            /// <summary>
            /// Pending.
            /// </summary>
            Todo = 0,

            /// <summary>
            /// In progress.
            /// </summary>
            Doing,

            /// <summary>
            /// Done.
            /// </summary>
            Done,

            /// <summary>
            /// Error occurred.
            /// </summary>
            Error
        }
    }
}
