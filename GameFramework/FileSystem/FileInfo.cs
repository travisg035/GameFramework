//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Runtime.InteropServices;

namespace GameFramework.FileSystem
{
    /// <summary>
    /// File info.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct FileInfo
    {
        private readonly string m_Name;
        private readonly long m_Offset;
        private readonly int m_Length;

        /// <summary>
        /// Initializes a new instance of the FileInfo struct.
        /// </summary>
        /// <param name="name">The file name.</param>
        /// <param name="offset">The file offset.</param>
        /// <param name="length">The file length.</param>
        public FileInfo(string name, long offset, int length)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new GameFrameworkException("Name is invalid.");
            }

            if (offset < 0L)
            {
                throw new GameFrameworkException("Offset is invalid.");
            }

            if (length < 0)
            {
                throw new GameFrameworkException("Length is invalid.");
            }

            m_Name = name;
            m_Offset = offset;
            m_Length = length;
        }

        /// <summary>
        /// Gets whether the file info is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(m_Name) && m_Offset >= 0L && m_Length >= 0;
            }
        }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// Gets the file offset.
        /// </summary>
        public long Offset
        {
            get
            {
                return m_Offset;
            }
        }

        /// <summary>
        /// Gets the file length.
        /// </summary>
        public int Length
        {
            get
            {
                return m_Length;
            }
        }
    }
}
