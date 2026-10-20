//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.IO;

namespace GameFramework
{
    public static partial class Utility
    {
        /// <summary>
        /// Verifier utility functions.
        /// </summary>
        public static partial class Verifier
        {
            private const int CachedBytesLength = 0x1000;
            private static readonly byte[] s_CachedBytes = new byte[CachedBytesLength];
            private static readonly Crc32 s_Algorithm = new Crc32();

            /// <summary>
            /// Calculates the CRC32 of the binary bytes.
            /// </summary>
            /// <param name="bytes">The specified binary bytes.</param>
            /// <returns>The calculated CRC32.</returns>
            public static int GetCrc32(byte[] bytes)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                return GetCrc32(bytes, 0, bytes.Length);
            }

            /// <summary>
            /// Calculates the CRC32 of the binary bytes.
            /// </summary>
            /// <param name="bytes">The specified binary bytes.</param>
            /// <param name="offset">The offset of the binary bytes.</param>
            /// <param name="length">The length of the binary bytes.</param>
            /// <returns>The calculated CRC32.</returns>
            public static int GetCrc32(byte[] bytes, int offset, int length)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                if (offset < 0 || length < 0 || offset + length > bytes.Length)
                {
                    throw new GameFrameworkException("Offset or length is invalid.");
                }

                s_Algorithm.HashCore(bytes, offset, length);
                int result = (int)s_Algorithm.HashFinal();
                s_Algorithm.Initialize();
                return result;
            }

            /// <summary>
            /// Calculates the CRC32 of the stream.
            /// </summary>
            /// <param name="stream">The specified stream.</param>
            /// <returns>The calculated CRC32.</returns>
            public static int GetCrc32(Stream stream)
            {
                if (stream == null)
                {
                    throw new GameFrameworkException("Stream is invalid.");
                }

                while (true)
                {
                    int bytesRead = stream.Read(s_CachedBytes, 0, CachedBytesLength);
                    if (bytesRead > 0)
                    {
                        s_Algorithm.HashCore(s_CachedBytes, 0, bytesRead);
                    }
                    else
                    {
                        break;
                    }
                }

                int result = (int)s_Algorithm.HashFinal();
                s_Algorithm.Initialize();
                Array.Clear(s_CachedBytes, 0, CachedBytesLength);
                return result;
            }

            /// <summary>
            /// Gets the CRC32 bytes.
            /// </summary>
            /// <param name="crc32">The CRC32 value.</param>
            /// <returns>The CRC32 bytes.</returns>
            public static byte[] GetCrc32Bytes(int crc32)
            {
                return new byte[] { (byte)((crc32 >> 24) & 0xff), (byte)((crc32 >> 16) & 0xff), (byte)((crc32 >> 8) & 0xff), (byte)(crc32 & 0xff) };
            }

            /// <summary>
            /// Gets the CRC32 bytes.
            /// </summary>
            /// <param name="crc32">The CRC32 value.</param>
            /// <param name="bytes">The array to store the result.</param>
            public static void GetCrc32Bytes(int crc32, byte[] bytes)
            {
                GetCrc32Bytes(crc32, bytes, 0);
            }

            /// <summary>
            /// Gets the CRC32 bytes.
            /// </summary>
            /// <param name="crc32">The CRC32 value.</param>
            /// <param name="bytes">The array to store the result.</param>
            /// <param name="offset">The start index of the CRC32 bytes in the result array.</param>
            public static void GetCrc32Bytes(int crc32, byte[] bytes, int offset)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Result is invalid.");
                }

                if (offset < 0 || offset + 4 > bytes.Length)
                {
                    throw new GameFrameworkException("Offset or length is invalid.");
                }

                bytes[offset] = (byte)((crc32 >> 24) & 0xff);
                bytes[offset + 1] = (byte)((crc32 >> 16) & 0xff);
                bytes[offset + 2] = (byte)((crc32 >> 8) & 0xff);
                bytes[offset + 3] = (byte)(crc32 & 0xff);
            }

            internal static int GetCrc32(Stream stream, byte[] code, int length)
            {
                if (stream == null)
                {
                    throw new GameFrameworkException("Stream is invalid.");
                }

                if (code == null)
                {
                    throw new GameFrameworkException("Code is invalid.");
                }

                int codeLength = code.Length;
                if (codeLength <= 0)
                {
                    throw new GameFrameworkException("Code length is invalid.");
                }

                int bytesLength = (int)stream.Length;
                if (length < 0 || length > bytesLength)
                {
                    length = bytesLength;
                }

                int codeIndex = 0;
                while (true)
                {
                    int bytesRead = stream.Read(s_CachedBytes, 0, CachedBytesLength);
                    if (bytesRead > 0)
                    {
                        if (length > 0)
                        {
                            for (int i = 0; i < bytesRead && i < length; i++)
                            {
                                s_CachedBytes[i] ^= code[codeIndex++];
                                codeIndex %= codeLength;
                            }

                            length -= bytesRead;
                        }

                        s_Algorithm.HashCore(s_CachedBytes, 0, bytesRead);
                    }
                    else
                    {
                        break;
                    }
                }

                int result = (int)s_Algorithm.HashFinal();
                s_Algorithm.Initialize();
                Array.Clear(s_CachedBytes, 0, CachedBytesLength);
                return result;
            }
        }
    }
}
