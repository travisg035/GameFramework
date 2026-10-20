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
    /// Event pool mode.
    /// </summary>
    [Flags]
    internal enum EventPoolMode : byte
    {
        /// <summary>
        /// Default event pool mode, which requires exactly one event handler.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Allows having no event handler.
        /// </summary>
        AllowNoHandler = 1,

        /// <summary>
        /// Allows having multiple event handlers.
        /// </summary>
        AllowMultiHandler = 2,

        /// <summary>
        /// Allows having duplicate event handlers.
        /// </summary>
        AllowDuplicateHandler = 4
    }
}
