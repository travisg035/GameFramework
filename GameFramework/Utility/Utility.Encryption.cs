//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework
{
    public static partial class Utility
    {
        /// <summary>
        /// Encryption and decryption utility functions.
        /// </summary>
        public static class Encryption
        {
            internal const int QuickEncryptLength = 220;

            /// <summary>
            /// Fast version of performing XOR operation on bytes using code.
            /// </summary>
            /// <param name="bytes">The original binary bytes.</param>
            /// <param name="code">The XOR key binary bytes.</param>
            /// <returns>The XORed binary bytes.</returns>
            public static byte[] GetQuickXorBytes(byte[] bytes, byte[] code)
            {
                return GetXorBytes(bytes, 0, QuickEncryptLength, code);
            }

            /// <summary>
            /// Fast version of performing XOR operation on bytes using code. This method reuses and overwrites the passed-in bytes as return value without allocating extra memory.
            /// </summary>
            /// <param name="bytes">The original and XORed binary bytes.</param>
            /// <param name="code">The XOR key binary bytes.</param>
            public static void GetQuickSelfXorBytes(byte[] bytes, byte[] code)
            {
                GetSelfXorBytes(bytes, 0, QuickEncryptLength, code);
            }

            /// <summary>
            /// Performs XOR operation on bytes using code.
            /// </summary>
            /// <param name="bytes">The original binary bytes.</param>
            /// <param name="code">The XOR key binary bytes.</param>
            /// <returns>The XORed binary bytes.</returns>
            public static byte[] GetXorBytes(byte[] bytes, byte[] code)
            {
                if (bytes == null)
                {
                    return null;
                }

                return GetXorBytes(bytes, 0, bytes.Length, code);
            }

            /// <summary>
            /// Performs XOR operation on bytes using code. This method reuses and overwrites the passed-in bytes as return value without allocating extra memory.
            /// </summary>
            /// <param name="bytes">The original and XORed binary bytes.</param>
            /// <param name="code">The XOR key binary bytes.</param>
            public static void GetSelfXorBytes(byte[] bytes, byte[] code)
            {
                if (bytes == null)
                {
                    return;
                }

                GetSelfXorBytes(bytes, 0, bytes.Length, code);
            }

            /// <summary>
            /// Performs XOR operation on bytes using code.
            /// </summary>
            /// <param name="bytes">The original binary bytes.</param>
            /// <param name="startIndex">The start index of the XOR calculation.</param>
            /// <param name="length">The length of the XOR calculation. If less than 0, calculates the entire binary bytes.</param>
            /// <param name="code">The XOR key binary bytes.</param>
            /// <returns>The XORed binary bytes.</returns>
            public static byte[] GetXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
            {
                if (bytes == null)
                {
                    return null;
                }

                int bytesLength = bytes.Length;
                byte[] results = new byte[bytesLength];
                Array.Copy(bytes, 0, results, 0, bytesLength);
                GetSelfXorBytes(results, startIndex, length, code);
                return results;
            }

            /// <summary>
            /// Performs XOR operation on bytes using code. This method reuses and overwrites the passed-in bytes as return value without allocating extra memory.
            /// </summary>
            /// <param name="bytes">The original and XORed binary bytes.</param>
            /// <param name="startIndex">The start index of the XOR calculation.</param>
            /// <param name="length">The length of the XOR calculation.</param>
            /// <param name="code">The XOR key binary bytes.</param>
            public static void GetSelfXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
            {
                if (bytes == null)
                {
                    return;
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

                if (startIndex < 0 || length < 0 || startIndex + length > bytes.Length)
                {
                    throw new GameFrameworkException("Start index or length is invalid.");
                }

                int codeIndex = startIndex % codeLength;
                for (int i = startIndex; i < length; i++)
                {
                    bytes[i] ^= code[codeIndex++];
                    codeIndex %= codeLength;
                }
            }
        }
    }
}
