//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.FileSystem
{
    /// <summary>
    /// File system manager.
    /// </summary>
    internal sealed class FileSystemManager : GameFrameworkModule, IFileSystemManager
    {
        private readonly Dictionary<string, FileSystem> m_FileSystems;

        private IFileSystemHelper m_FileSystemHelper;

        /// <summary>
        /// Initializes a new instance of the FileSystemManager class.
        /// </summary>
        public FileSystemManager()
        {
            m_FileSystems = new Dictionary<string, FileSystem>(StringComparer.Ordinal);
            m_FileSystemHelper = null;
        }

        /// <summary>
        /// Gets the priority of the game framework module.
        /// </summary>
        /// <remarks>Modules with higher priority will be polled first and shut down later.</remarks>
        internal override int Priority
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Gets the count of file systems.
        /// </summary>
        public int Count
        {
            get
            {
                return m_FileSystems.Count;
            }
        }

        /// <summary>
        /// Polls the file system manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// Shuts down and cleans up the file system manager.
        /// </summary>
        internal override void Shutdown()
        {
            while (m_FileSystems.Count > 0)
            {
                foreach (KeyValuePair<string, FileSystem> fileSystem in m_FileSystems)
                {
                    DestroyFileSystem(fileSystem.Value, false);
                    break;
                }
            }
        }

        /// <summary>
        /// Sets the file system helper.
        /// </summary>
        /// <param name="fileSystemHelper">The file system helper.</param>
        public void SetFileSystemHelper(IFileSystemHelper fileSystemHelper)
        {
            if (fileSystemHelper == null)
            {
                throw new GameFrameworkException("File system helper is invalid.");
            }

            m_FileSystemHelper = fileSystemHelper;
        }

        /// <summary>
        /// Checks whether a file system exists.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to check.</param>
        /// <returns>Whether the file system exists.</returns>
        public bool HasFileSystem(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                throw new GameFrameworkException("Full path is invalid.");
            }

            return m_FileSystems.ContainsKey(Utility.Path.GetRegularPath(fullPath));
        }

        /// <summary>
        /// Gets a file system.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to get.</param>
        /// <returns>The file system to get.</returns>
        public IFileSystem GetFileSystem(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                throw new GameFrameworkException("Full path is invalid.");
            }

            FileSystem fileSystem = null;
            if (m_FileSystems.TryGetValue(Utility.Path.GetRegularPath(fullPath), out fileSystem))
            {
                return fileSystem;
            }

            return null;
        }

        /// <summary>
        /// Creates a file system.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to create.</param>
        /// <param name="access">The access mode of the file system to create.</param>
        /// <param name="maxFileCount">The maximum file count of the file system to create.</param>
        /// <param name="maxBlockCount">The maximum block count of the file system to create.</param>
        /// <returns>The created file system.</returns>
        public IFileSystem CreateFileSystem(string fullPath, FileSystemAccess access, int maxFileCount, int maxBlockCount)
        {
            if (m_FileSystemHelper == null)
            {
                throw new GameFrameworkException("File system helper is invalid.");
            }

            if (string.IsNullOrEmpty(fullPath))
            {
                throw new GameFrameworkException("Full path is invalid.");
            }

            if (access == FileSystemAccess.Unspecified)
            {
                throw new GameFrameworkException("Access is invalid.");
            }

            if (access == FileSystemAccess.Read)
            {
                throw new GameFrameworkException("Access read is invalid.");
            }

            fullPath = Utility.Path.GetRegularPath(fullPath);
            if (m_FileSystems.ContainsKey(fullPath))
            {
                throw new GameFrameworkException(Utility.Text.Format("File system '{0}' is already exist.", fullPath));
            }

            FileSystemStream fileSystemStream = m_FileSystemHelper.CreateFileSystemStream(fullPath, access, true);
            if (fileSystemStream == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Create file system stream for '{0}' failure.", fullPath));
            }

            FileSystem fileSystem = FileSystem.Create(fullPath, access, fileSystemStream, maxFileCount, maxBlockCount);
            if (fileSystem == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Create file system '{0}' failure.", fullPath));
            }

            m_FileSystems.Add(fullPath, fileSystem);
            return fileSystem;
        }

        /// <summary>
        /// Loads a file system.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to load.</param>
        /// <param name="access">The access mode of the file system to load.</param>
        /// <returns>The loaded file system.</returns>
        public IFileSystem LoadFileSystem(string fullPath, FileSystemAccess access)
        {
            if (m_FileSystemHelper == null)
            {
                throw new GameFrameworkException("File system helper is invalid.");
            }

            if (string.IsNullOrEmpty(fullPath))
            {
                throw new GameFrameworkException("Full path is invalid.");
            }

            if (access == FileSystemAccess.Unspecified)
            {
                throw new GameFrameworkException("Access is invalid.");
            }

            fullPath = Utility.Path.GetRegularPath(fullPath);
            if (m_FileSystems.ContainsKey(fullPath))
            {
                throw new GameFrameworkException(Utility.Text.Format("File system '{0}' is already exist.", fullPath));
            }

            FileSystemStream fileSystemStream = m_FileSystemHelper.CreateFileSystemStream(fullPath, access, false);
            if (fileSystemStream == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Create file system stream for '{0}' failure.", fullPath));
            }

            FileSystem fileSystem = FileSystem.Load(fullPath, access, fileSystemStream);
            if (fileSystem == null)
            {
                fileSystemStream.Close();
                throw new GameFrameworkException(Utility.Text.Format("Load file system '{0}' failure.", fullPath));
            }

            m_FileSystems.Add(fullPath, fileSystem);
            return fileSystem;
        }

        /// <summary>
        /// Destroys a file system.
        /// </summary>
        /// <param name="fileSystem">The file system to destroy.</param>
        /// <param name="deletePhysicalFile">Whether to delete the physical file corresponding to the file system.</param>
        public void DestroyFileSystem(IFileSystem fileSystem, bool deletePhysicalFile)
        {
            if (fileSystem == null)
            {
                throw new GameFrameworkException("File system is invalid.");
            }

            string fullPath = fileSystem.FullPath;
            ((FileSystem)fileSystem).Shutdown();
            m_FileSystems.Remove(fullPath);

            if (deletePhysicalFile && File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        /// <summary>
        /// Gets all file systems.
        /// </summary>
        /// <returns>All file systems.</returns>
        public IFileSystem[] GetAllFileSystems()
        {
            int index = 0;
            IFileSystem[] results = new IFileSystem[m_FileSystems.Count];
            foreach (KeyValuePair<string, FileSystem> fileSystem in m_FileSystems)
            {
                results[index++] = fileSystem.Value;
            }

            return results;
        }

        /// <summary>
        /// Gets all file systems.
        /// </summary>
        /// <param name="results">All file systems.</param>
        public void GetAllFileSystems(List<IFileSystem> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<string, FileSystem> fileSystem in m_FileSystems)
            {
                results.Add(fileSystem.Value);
            }
        }
    }
}
