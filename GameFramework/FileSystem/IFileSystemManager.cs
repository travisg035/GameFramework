//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.FileSystem
{
    /// <summary>
    /// File system manager interface.
    /// </summary>
    public interface IFileSystemManager
    {
        /// <summary>
        /// Gets the count of file systems.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Sets the file system helper.
        /// </summary>
        /// <param name="fileSystemHelper">The file system helper.</param>
        void SetFileSystemHelper(IFileSystemHelper fileSystemHelper);

        /// <summary>
        /// Checks whether a file system exists.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to check.</param>
        /// <returns>Whether the file system exists.</returns>
        bool HasFileSystem(string fullPath);

        /// <summary>
        /// Gets a file system.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to get.</param>
        /// <returns>The file system to get.</returns>
        IFileSystem GetFileSystem(string fullPath);

        /// <summary>
        /// Creates a file system.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to create.</param>
        /// <param name="access">The access mode of the file system to create.</param>
        /// <param name="maxFileCount">The maximum file count of the file system to create.</param>
        /// <param name="maxBlockCount">The maximum block count of the file system to create.</param>
        /// <returns>The created file system.</returns>
        IFileSystem CreateFileSystem(string fullPath, FileSystemAccess access, int maxFileCount, int maxBlockCount);

        /// <summary>
        /// Loads a file system.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to load.</param>
        /// <param name="access">The access mode of the file system to load.</param>
        /// <returns>The loaded file system.</returns>
        IFileSystem LoadFileSystem(string fullPath, FileSystemAccess access);

        /// <summary>
        /// Destroys a file system.
        /// </summary>
        /// <param name="fileSystem">The file system to destroy.</param>
        /// <param name="deletePhysicalFile">Whether to delete the physical file corresponding to the file system.</param>
        void DestroyFileSystem(IFileSystem fileSystem, bool deletePhysicalFile);

        /// <summary>
        /// Gets all file systems.
        /// </summary>
        /// <returns>All file systems.</returns>
        IFileSystem[] GetAllFileSystems();

        /// <summary>
        /// Gets all file systems.
        /// </summary>
        /// <param name="results">All file systems.</param>
        void GetAllFileSystems(List<IFileSystem> results);
    }
}
