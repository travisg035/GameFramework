//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.FileSystem
{
    /// <summary>
    /// File system helper interface.
    /// </summary>
    public interface IFileSystemHelper
    {
        /// <summary>
        /// Create a file system stream.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to load.</param>
        /// <param name="access">The access mode of the file system to load.</param>
        /// <param name="createNew">Whether to create a new file system stream.</param>
        /// <returns>The created file system stream.</returns>
        FileSystemStream CreateFileSystemStream(string fullPath, FileSystemAccess access, bool createNew);
    }
}
