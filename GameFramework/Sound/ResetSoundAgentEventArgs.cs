//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// Reset sound agent event.
    /// </summary>
    public sealed class ResetSoundAgentEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResetSoundAgentEventArgs"/> class.
        /// </summary>
        public ResetSoundAgentEventArgs()
        {
        }

        /// <summary>
        /// Creates a reset sound agent event.
        /// </summary>
        /// <returns>The created reset sound agent event.</returns>
        public static ResetSoundAgentEventArgs Create()
        {
            ResetSoundAgentEventArgs resetSoundAgentEventArgs = ReferencePool.Acquire<ResetSoundAgentEventArgs>();
            return resetSoundAgentEventArgs;
        }

        /// <summary>
        /// Clears the reset sound agent event.
        /// </summary>
        public override void Clear()
        {
        }
    }
}
