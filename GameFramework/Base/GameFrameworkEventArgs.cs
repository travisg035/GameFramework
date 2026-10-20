//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework
{
    /// <summary>
    /// Base class for game framework event data classes.
    /// </summary>
    public abstract class GameFrameworkEventArgs : EventArgs, IReference
    {
        /// <summary>
        /// Initializes a new instance of the game framework event data class.
        /// </summary>
        public GameFrameworkEventArgs()
        {
        }

        /// <summary>
        /// Clear references.
        /// </summary>
        public abstract void Clear();
    }
}