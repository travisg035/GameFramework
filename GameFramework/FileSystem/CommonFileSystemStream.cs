//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.IO;

namespace GameFramework.FileSystem
{
    /// <summary>
    /// Common file system stream.
    /// </summary>
    public sealed class CommonFileSystemStream : FileSystemStream, IDisposable
    {
        private readonly FileStream m_FileStream;

        /// <summary>
        /// Initializes a new instance of the CommonFileSystemStream class.
        /// </summary>
        /// <param name="fullPath">The full path of the file system to load.</param>
        /// <param name="access">The access mode of the file system to load.</param>
        /// <param name="createNew">Whether to create a new file system stream.</param>
        public CommonFileSystemStream(string fullPath, FileSystemAccess access, bool createNew)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                throw new GameFrameworkException("Full path is invalid.");
            }

            switch (access)
            {
                case FileSystemAccess.Read:
                    m_FileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    break;

                case FileSystemAccess.Write:
                    m_FileStream = new FileStream(fullPath, createNew ? FileMode.Create : FileMode.Open, FileAccess.Write, FileShare.Read);
                    break;

                case FileSystemAccess.ReadWrite:
                    m_FileStream = new FileStream(fullPath, createNew ? FileMode.Create : FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                    break;

                default:
                    throw new GameFrameworkException("Access is invalid.");
            }
        }

        /// <summary>
        /// Gets or sets the file system stream position.
        /// </summary>
        protected internal override long Position
        {
            get
            {
                return m_FileStream.Position;
            }
            set
            {
                m_FileStream.Position = value;
            }
        }

        /// <summary>
        /// Gets the file system stream length.
        /// </summary>
        protected internal override long Length
        {
            get
            {
                return m_FileStream.Length;
            }
        }

        /// <summary>
        /// Sets the file system stream length.
        /// </summary>
        /// <param name="length">The file system stream length to set.</param>
        protected internal override void SetLength(long length)
        {
            m_FileStream.SetLength(length);
        }

        /// <summary>
        /// Seeks the file system stream position.
        /// </summary>
        /// <param name="offset">The offset of the file system stream position to seek.</param>
        /// <param name="origin">The seek origin of the file system stream position.</param>
        protected internal override void Seek(long offset, SeekOrigin origin)
        {
            m_FileStream.Seek(offset, origin);
        }

        /// <summary>
        /// Reads a byte from the file system stream.
        /// </summary>
        /// <returns>The read byte, or -1 if the end of the file system stream has been reached.</returns>
        protected internal override int ReadByte()
        {
            return m_FileStream.ReadByte();
        }

        /// <summary>
        /// Reads bytes from the file system stream.
        /// </summary>
        /// <param name="buffer">The byte array to store the read file content.</param>
        /// <param name="startIndex">The starting position in the byte array to store the read file content.</param>
        /// <param name="length">The length of the byte array to store the read file content.</param>
        /// <returns>The actual number of bytes read.</returns>
        protected internal override int Read(byte[] buffer, int startIndex, int length)
        {
            return m_FileStream.Read(buffer, startIndex, length);
        }

        /// <summary>
        /// Writes a byte to the file system stream.
        /// </summary>
        /// <param name="value">The byte to write.</param>
        protected internal override void WriteByte(byte value)
        {
            m_FileStream.WriteByte(value);
        }

        /// <summary>
        /// Writes bytes to the file system stream.
        /// </summary>
        /// <param name="buffer">The byte array containing the file content to write.</param>
        /// <param name="startIndex">The starting position in the byte array of the file content to write.</param>
        /// <param name="length">The length of the byte array of the file content to write.</param>
        protected internal override void Write(byte[] buffer, int startIndex, int length)
        {
            m_FileStream.Write(buffer, startIndex, length);
        }

        /// <summary>
        /// Flushes the file system stream to the storage medium immediately.
        /// </summary>
        protected internal override void Flush()
        {
            m_FileStream.Flush();
        }

        /// <summary>
        /// Closes the file system stream.
        /// </summary>
        protected internal override void Close()
        {
            m_FileStream.Close();
        }

        /// <summary>
        /// Releases all resources used by the CommonFileSystemStream.
        /// </summary>
        public void Dispose()
        {
            m_FileStream.Dispose();
        }
    }
}
