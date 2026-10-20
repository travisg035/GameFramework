//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Runtime.InteropServices;

namespace GameFramework.Resource
{
    public partial struct ResourcePackVersionList
    {
        /// <summary>
        /// Resource.
        /// </summary>
        [StructLayout(LayoutKind.Auto)]
        public struct Resource
        {
            private readonly string m_Name;
            private readonly string m_Variant;
            private readonly string m_Extension;
            private readonly byte m_LoadType;
            private readonly long m_Offset;
            private readonly int m_Length;
            private readonly int m_HashCode;
            private readonly int m_CompressedLength;
            private readonly int m_CompressedHashCode;

            /// <summary>
            /// Initializes a new instance of the Resource struct.
            /// </summary>
            /// <param name="name">Resource name.</param>
            /// <param name="variant">Resource variant name.</param>
            /// <param name="extension">Resource extension name.</param>
            /// <param name="loadType">Resource load type.</param>
            /// <param name="offset">Resource offset.</param>
            /// <param name="length">Resource length.</param>
            /// <param name="hashCode">Resource hash code.</param>
            /// <param name="compressedLength">Resource compressed length.</param>
            /// <param name="compressedHashCode">Resource compressed hash code.</param>
            public Resource(string name, string variant, string extension, byte loadType, long offset, int length, int hashCode, int compressedLength, int compressedHashCode)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new GameFrameworkException("Name is invalid.");
                }

                m_Name = name;
                m_Variant = variant;
                m_Extension = extension;
                m_LoadType = loadType;
                m_Offset = offset;
                m_Length = length;
                m_HashCode = hashCode;
                m_CompressedLength = compressedLength;
                m_CompressedHashCode = compressedHashCode;
            }

            /// <summary>
            /// Gets the resource name.
            /// </summary>
            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            /// <summary>
            /// Gets the resource variant name.
            /// </summary>
            public string Variant
            {
                get
                {
                    return m_Variant;
                }
            }

            /// <summary>
            /// Gets the resource extension name.
            /// </summary>
            public string Extension
            {
                get
                {
                    return m_Extension;
                }
            }

            /// <summary>
            /// Gets the resource load type.
            /// </summary>
            public byte LoadType
            {
                get
                {
                    return m_LoadType;
                }
            }

            /// <summary>
            /// Gets the resource offset.
            /// </summary>
            public long Offset
            {
                get
                {
                    return m_Offset;
                }
            }

            /// <summary>
            /// Gets the resource length.
            /// </summary>
            public int Length
            {
                get
                {
                    return m_Length;
                }
            }

            /// <summary>
            /// Gets the resource hash code.
            /// </summary>
            public int HashCode
            {
                get
                {
                    return m_HashCode;
                }
            }

            /// <summary>
            /// Gets the resource compressed length.
            /// </summary>
            public int CompressedLength
            {
                get
                {
                    return m_CompressedLength;
                }
            }

            /// <summary>
            /// Gets the resource compressed hash code.
            /// </summary>
            public int CompressedHashCode
            {
                get
                {
                    return m_CompressedHashCode;
                }
            }
        }
    }
}
