//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.FileSystem
{
    /// <summary>
    /// File system access.
    /// </summary>
    [Flags]
    public enum FileSystemAccess : byte
    {
        /// <summary>
        /// Unspecified.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Read-only.
        /// </summary>
        Read = 1,

        /// <summary>
        /// Write-only.
        /// </summary>
        Write = 2,

        /// <summary>
        /// Read and write.
        /// </summary>
        ReadWrite = 3
    }
}
