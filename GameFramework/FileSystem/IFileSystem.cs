//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace GameFramework.FileSystem
{
    /// <summary>
    /// File system interface.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Gets the full path of the file system.
        /// </summary>
        string FullPath
        {
            get;
        }

        /// <summary>
        /// Gets the access mode of the file system.
        /// </summary>
        FileSystemAccess Access
        {
            get;
        }

        /// <summary>
        /// Gets the file count.
        /// </summary>
        int FileCount
        {
            get;
        }

        /// <summary>
        /// Gets the maximum file count.
        /// </summary>
        int MaxFileCount
        {
            get;
        }

        /// <summary>
        /// Gets the file info.
        /// </summary>
        /// <param name="name">The name of the file whose info is to be obtained.</param>
        /// <returns>The file info.</returns>
        FileInfo GetFileInfo(string name);

        /// <summary>
        /// Gets all file infos.
        /// </summary>
        /// <returns>All file infos.</returns>
        FileInfo[] GetAllFileInfos();

        /// <summary>
        /// Gets all file infos.
        /// </summary>
        /// <param name="results">All file infos.</param>
        void GetAllFileInfos(List<FileInfo> results);

        /// <summary>
        /// Checks whether the specified file exists.
        /// </summary>
        /// <param name="name">The name of the file to check.</param>
        /// <returns>Whether the specified file exists.</returns>
        bool HasFile(string name);

        /// <summary>
        /// Reads the specified file.
        /// </summary>
        /// <param name="name">The name of the file to read.</param>
        /// <returns>The byte array containing the read file content.</returns>
        byte[] ReadFile(string name);

        /// <summary>
        /// Reads the specified file.
        /// </summary>
        /// <param name="name">The name of the file to read.</param>
        /// <param name="buffer">The byte array to store the read file content.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFile(string name, byte[] buffer);

        /// <summary>
        /// Reads the specified file.
        /// </summary>
        /// <param name="name">The name of the file to read.</param>
        /// <param name="buffer">The byte array to store the read file content.</param>
        /// <param name="startIndex">The starting position in the byte array to store the read file content.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFile(string name, byte[] buffer, int startIndex);

        /// <summary>
        /// Reads the specified file.
        /// </summary>
        /// <param name="name">The name of the file to read.</param>
        /// <param name="buffer">The byte array to store the read file content.</param>
        /// <param name="startIndex">The starting position in the byte array to store the read file content.</param>
        /// <param name="length">The length of the byte array to store the read file content.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFile(string name, byte[] buffer, int startIndex, int length);

        /// <summary>
        /// Reads the specified file.
        /// </summary>
        /// <param name="name">The name of the file to read.</param>
        /// <param name="stream">The stream to store the read file content.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFile(string name, Stream stream);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="length">The length of the segment to read.</param>
        /// <returns>The byte array containing the read file segment content.</returns>
        byte[] ReadFileSegment(string name, int length);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="offset">The offset of the segment to read.</param>
        /// <param name="length">The length of the segment to read.</param>
        /// <returns>The byte array containing the read file segment content.</returns>
        byte[] ReadFileSegment(string name, int offset, int length);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="buffer">The byte array to store the read file segment content.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFileSegment(string name, byte[] buffer);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="buffer">The byte array to store the read file segment content.</param>
        /// <param name="length">The length of the segment to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFileSegment(string name, byte[] buffer, int length);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="buffer">The byte array to store the read file segment content.</param>
        /// <param name="startIndex">The starting position in the byte array to store the read file segment content.</param>
        /// <param name="length">The length of the segment to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFileSegment(string name, byte[] buffer, int startIndex, int length);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="offset">The offset of the segment to read.</param>
        /// <param name="buffer">The byte array to store the read file segment content.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFileSegment(string name, int offset, byte[] buffer);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="offset">The offset of the segment to read.</param>
        /// <param name="buffer">The byte array to store the read file segment content.</param>
        /// <param name="length">The length of the segment to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFileSegment(string name, int offset, byte[] buffer, int length);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="offset">The offset of the segment to read.</param>
        /// <param name="buffer">The byte array to store the read file segment content.</param>
        /// <param name="startIndex">The starting position in the byte array to store the read file segment content.</param>
        /// <param name="length">The length of the segment to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFileSegment(string name, int offset, byte[] buffer, int startIndex, int length);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="stream">The stream to store the read file segment content.</param>
        /// <param name="length">The length of the segment to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFileSegment(string name, Stream stream, int length);

        /// <summary>
        /// Reads the specified segment of the specified file.
        /// </summary>
        /// <param name="name">The name of the file whose segment is to be read.</param>
        /// <param name="offset">The offset of the segment to read.</param>
        /// <param name="stream">The stream to store the read file segment content.</param>
        /// <param name="length">The length of the segment to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        int ReadFileSegment(string name, int offset, Stream stream, int length);

        /// <summary>
        /// Writes the specified file.
        /// </summary>
        /// <param name="name">The name of the file to write.</param>
        /// <param name="buffer">The byte array containing the file content to write.</param>
        /// <returns>Whether the file was written successfully.</returns>
        bool WriteFile(string name, byte[] buffer);

        /// <summary>
        /// Writes the specified file.
        /// </summary>
        /// <param name="name">The name of the file to write.</param>
        /// <param name="buffer">The byte array containing the file content to write.</param>
        /// <param name="startIndex">The starting position in the byte array of the file content to write.</param>
        /// <returns>Whether the file was written successfully.</returns>
        bool WriteFile(string name, byte[] buffer, int startIndex);

        /// <summary>
        /// Writes the specified file.
        /// </summary>
        /// <param name="name">The name of the file to write.</param>
        /// <param name="buffer">The byte array containing the file content to write.</param>
        /// <param name="startIndex">The starting position in the byte array of the file content to write.</param>
        /// <param name="length">The length of the byte array of the file content to write.</param>
        /// <returns>Whether the file was written successfully.</returns>
        bool WriteFile(string name, byte[] buffer, int startIndex, int length);

        /// <summary>
        /// Writes the specified file.
        /// </summary>
        /// <param name="name">The name of the file to write.</param>
        /// <param name="stream">The stream containing the file content to write.</param>
        /// <returns>Whether the file was written successfully.</returns>
        bool WriteFile(string name, Stream stream);

        /// <summary>
        /// Writes the specified file.
        /// </summary>
        /// <param name="name">The name of the file to write.</param>
        /// <param name="filePath">The file path containing the file content to write.</param>
        /// <returns>Whether the file was written successfully.</returns>
        bool WriteFile(string name, string filePath);

        /// <summary>
        /// Saves the specified file as a physical file.
        /// </summary>
        /// <param name="name">The name of the file to save as a physical file.</param>
        /// <param name="filePath">The file path to store the saved file content.</param>
        /// <returns>Whether the file was saved as a physical file successfully.</returns>
        bool SaveAsFile(string name, string filePath);

        /// <summary>
        /// Renames the specified file.
        /// </summary>
        /// <param name="oldName">The old name of the file to rename.</param>
        /// <param name="newName">The new name of the file.</param>
        /// <returns>Whether the file was renamed successfully.</returns>
        bool RenameFile(string oldName, string newName);

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="name">The name of the file to delete.</param>
        /// <returns>Whether the file was deleted successfully.</returns>
        bool DeleteFile(string name);
    }
}
