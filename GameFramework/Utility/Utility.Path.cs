//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.IO;

namespace GameFramework
{
    public static partial class Utility
    {
        /// <summary>
        /// Path utility functions.
        /// </summary>
        public static class Path
        {
            /// <summary>
            /// Gets the regular path.
            /// </summary>
            /// <param name="path">The path to normalize.</param>
            /// <returns>The regular path.</returns>
            public static string GetRegularPath(string path)
            {
                if (path == null)
                {
                    return null;
                }

                if (path.IndexOf('\\') < 0)
                {
                    return path;
                }

                return path.Replace('\\', '/');
            }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
            /// <summary>
            /// Normalizes the path into destination span by replacing backslashes with slashes.
            /// </summary>
            /// <param name="path">The path to normalize.</param>
            /// <param name="destination">The destination span.</param>
            /// <returns>The number of characters written into destination.</returns>
            public static int GetRegularPath(ReadOnlySpan<char> path, Span<char> destination)
            {
                if (destination.Length < path.Length)
                {
                    throw new GameFrameworkException("Destination is invalid.");
                }

                for (int i = 0; i < path.Length; i++)
                {
                    destination[i] = path[i] == '\\' ? '/' : path[i];
                }

                return path.Length;
            }
#endif

            /// <summary>
            /// Gets the remote path (prefixed with file:// or http://).
            /// </summary>
            /// <param name="path">The original path.</param>
            /// <returns>The remote path.</returns>
            public static string GetRemotePath(string path)
            {
                string regularPath = GetRegularPath(path);
                if (regularPath == null)
                {
                    return null;
                }

                return regularPath.Contains("://") ? regularPath : ("file:///" + regularPath).Replace("file:////", "file:///");
            }

            /// <summary>
            /// Removes empty directory.
            /// </summary>
            /// <param name="directoryName">The directory name to process.</param>
            /// <returns>Whether the empty directory was removed successfully.</returns>
            public static bool RemoveEmptyDirectory(string directoryName)
            {
                if (string.IsNullOrEmpty(directoryName))
                {
                    throw new GameFrameworkException("Directory name is invalid.");
                }

                try
                {
                    if (!Directory.Exists(directoryName))
                    {
                        return false;
                    }

                    // Do not use SearchOption.AllDirectories to delete as many directories as possible in environments where exceptions may occur
                    string[] subDirectoryNames = Directory.GetDirectories(directoryName, "*");
                    int subDirectoryCount = subDirectoryNames.Length;
                    foreach (string subDirectoryName in subDirectoryNames)
                    {
                        if (RemoveEmptyDirectory(subDirectoryName))
                        {
                            subDirectoryCount--;
                        }
                    }

                    if (subDirectoryCount > 0)
                    {
                        return false;
                    }

                    if (Directory.GetFiles(directoryName, "*").Length > 0)
                    {
                        return false;
                    }

                    Directory.Delete(directoryName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
