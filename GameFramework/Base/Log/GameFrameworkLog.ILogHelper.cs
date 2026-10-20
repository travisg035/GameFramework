//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    public static partial class GameFrameworkLog
    {
        /// <summary>
        /// Game framework log helper interface.
        /// </summary>
        public interface ILogHelper
        {
            /// <summary>
            /// Logs a message.
            /// </summary>
            /// <param name="level">Game framework log level.</param>
            /// <param name="message">Log message.</param>
            void Log(GameFrameworkLogLevel level, object message);
        }
    }
}
