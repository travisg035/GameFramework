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
    /// File system stream.
    /// </summary>
    public abstract class FileSystemStream
    {
        /// <summary>
        /// Cached byte array length.
        /// </summary>
        protected const int CachedBytesLength = 0x1000;

        /// <summary>
        /// Cached byte array.
        /// </summary>
        protected static readonly byte[] s_CachedBytes = new byte[CachedBytesLength];

        /// <summary>
        /// Gets or sets the file system stream position.
        /// </summary>
        protected internal abstract long Position
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the file system stream length.
        /// </summary>
        protected internal abstract long Length
        {
            get;
        }

        /// <summary>
        /// Sets the file system stream length.
        /// </summary>
        /// <param name="length">The file system stream length to set.</param>
        protected internal abstract void SetLength(long length);

        /// <summary>
        /// Seeks the file system stream position.
        /// </summary>
        /// <param name="offset">The offset of the file system stream position to seek.</param>
        /// <param name="origin">The seek origin of the file system stream position.</param>
        protected internal abstract void Seek(long offset, SeekOrigin origin);

        /// <summary>
        /// Reads a byte from the file system stream.
        /// </summary>
        /// <returns>The read byte, or -1 if the end of the file system stream has been reached.</returns>
        protected internal abstract int ReadByte();

        /// <summary>
        /// Reads bytes from the file system stream.
        /// </summary>
        /// <param name="buffer">The byte array to store the read file content.</param>
        /// <param name="startIndex">The starting position in the byte array to store the read file content.</param>
        /// <param name="length">The length of the byte array to store the read file content.</param>
        /// <returns>The actual number of bytes read.</returns>
        protected internal abstract int Read(byte[] buffer, int startIndex, int length);

        /// <summary>
        /// Reads bytes from the file system stream.
        /// </summary>
        /// <param name="stream">The stream to store the read file content.</param>
        /// <param name="length">The length of the bytes to read into the stream.</param>
        /// <returns>The actual number of bytes read.</returns>
        protected internal int Read(Stream stream, int length)
        {
            int bytesRead = 0;
            int bytesLeft = length;
            while ((bytesRead = Read(s_CachedBytes, 0, bytesLeft < CachedBytesLength ? bytesLeft : CachedBytesLength)) > 0)
            {
                bytesLeft -= bytesRead;
                stream.Write(s_CachedBytes, 0, bytesRead);
            }

            Array.Clear(s_CachedBytes, 0, CachedBytesLength);
            return length - bytesLeft;
        }

        /// <summary>
        /// Writes a byte to the file system stream.
        /// </summary>
        /// <param name="value">The byte to write.</param>
        protected internal abstract void WriteByte(byte value);

        /// <summary>
        /// Writes bytes to the file system stream.
        /// </summary>
        /// <param name="buffer">The byte array containing the file content to write.</param>
        /// <param name="startIndex">The starting position in the byte array of the file content to write.</param>
        /// <param name="length">The length of the byte array of the file content to write.</param>
        protected internal abstract void Write(byte[] buffer, int startIndex, int length);

        /// <summary>
        /// Writes bytes to the file system stream.
        /// </summary>
        /// <param name="stream">The stream containing the file content to write.</param>
        /// <param name="length">The length of the bytes to write from the stream.</param>
        protected internal void Write(Stream stream, int length)
        {
            int bytesRead = 0;
            int bytesLeft = length;
            while ((bytesRead = stream.Read(s_CachedBytes, 0, bytesLeft < CachedBytesLength ? bytesLeft : CachedBytesLength)) > 0)
            {
                bytesLeft -= bytesRead;
                Write(s_CachedBytes, 0, bytesRead);
            }

            Array.Clear(s_CachedBytes, 0, CachedBytesLength);
        }

        /// <summary>
        /// Flushes the file system stream to the storage medium immediately.
        /// </summary>
        protected internal abstract void Flush();

        /// <summary>
        /// Closes the file system stream.
        /// </summary>
        protected internal abstract void Close();
    }
}
