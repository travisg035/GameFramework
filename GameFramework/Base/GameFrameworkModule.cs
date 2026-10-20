//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Abstract base class for game framework modules.
    /// </summary>
    internal abstract class GameFrameworkModule
    {
        /// <summary>
        /// Gets the priority of the game framework module.
        /// </summary>
        /// <remarks>Modules with higher priority will be polled first and shut down later.</remarks>
        internal virtual int Priority
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Polling the game framework module.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// Shutdown and cleanup the game framework module.
        /// </summary>
        internal abstract void Shutdown();
    }
}
