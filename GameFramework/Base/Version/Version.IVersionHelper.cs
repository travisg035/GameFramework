//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    public static partial class Version
    {
        /// <summary>
        /// Version helper interface.
        /// </summary>
        public interface IVersionHelper
        {
            /// <summary>
            /// Gets the game version.
            /// </summary>
            string GameVersion
            {
                get;
            }

            /// <summary>
            /// Gets the internal game version.
            /// </summary>
            int InternalGameVersion
            {
                get;
            }
        }
    }
}
