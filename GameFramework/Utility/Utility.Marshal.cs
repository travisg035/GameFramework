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
        /// Marshal utility functions.
        /// </summary>
        public static class Marshal
        {
            private const int BlockSize = 1024 * 4;
            private static IntPtr s_CachedHGlobalPtr = IntPtr.Zero;
            private static int s_CachedHGlobalSize = 0;

            /// <summary>
            /// Gets the cached unmanaged memory size allocated from the process.
            /// </summary>
            public static int CachedHGlobalSize
            {
                get
                {
                    return s_CachedHGlobalSize;
                }
            }

            /// <summary>
            /// Ensures enough unmanaged memory is allocated from the process and cached.
            /// </summary>
            /// <param name="ensureSize">The size of unmanaged memory to ensure.</param>
            public static void EnsureCachedHGlobalSize(int ensureSize)
            {
                if (ensureSize < 0)
                {
                    throw new GameFrameworkException("Ensure size is invalid.");
                }

                if (s_CachedHGlobalPtr == IntPtr.Zero || s_CachedHGlobalSize < ensureSize)
                {
                    FreeCachedHGlobal();
                    int size = (ensureSize - 1 + BlockSize) / BlockSize * BlockSize;
                    s_CachedHGlobalPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
                    s_CachedHGlobalSize = size;
                }
            }

            /// <summary>
            /// Frees the cached unmanaged memory allocated from the process.
            /// </summary>
            public static void FreeCachedHGlobal()
            {
                if (s_CachedHGlobalPtr != IntPtr.Zero)
                {
                    System.Runtime.InteropServices.Marshal.FreeHGlobal(s_CachedHGlobalPtr);
                    s_CachedHGlobalPtr = IntPtr.Zero;
                    s_CachedHGlobalSize = 0;
                }
            }

            /// <summary>
            /// Converts an object to binary bytes.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="structure">The object to convert.</param>
            /// <returns>The converted binary bytes.</returns>
            public static byte[] StructureToBytes<T>(T structure)
            {
                return StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)));
            }

            /// <summary>
            /// Converts an object to binary bytes.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="structure">The object to convert.</param>
            /// <param name="structureSize">The size of the object to convert.</param>
            /// <returns>The converted binary bytes.</returns>
            internal static byte[] StructureToBytes<T>(T structure, int structureSize)
            {
                if (structureSize < 0)
                {
                    throw new GameFrameworkException("Structure size is invalid.");
                }

                EnsureCachedHGlobalSize(structureSize);
                System.Runtime.InteropServices.Marshal.StructureToPtr(structure, s_CachedHGlobalPtr, true);
                byte[] result = new byte[structureSize];
                System.Runtime.InteropServices.Marshal.Copy(s_CachedHGlobalPtr, result, 0, structureSize);
                return result;
            }

            /// <summary>
            /// Converts an object to binary bytes.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="structure">The object to convert.</param>
            /// <param name="result">The converted binary bytes.</param>
            public static void StructureToBytes<T>(T structure, byte[] result)
            {
                StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), result, 0);
            }

            /// <summary>
            /// Converts an object to binary bytes.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="structure">The object to convert.</param>
            /// <param name="structureSize">The size of the object to convert.</param>
            /// <param name="result">The converted binary bytes.</param>
            internal static void StructureToBytes<T>(T structure, int structureSize, byte[] result)
            {
                StructureToBytes(structure, structureSize, result, 0);
            }

            /// <summary>
            /// Converts an object to binary bytes.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="structure">The object to convert.</param>
            /// <param name="result">The converted binary bytes.</param>
            /// <param name="startIndex">The start index of the converted binary bytes.</param>
            public static void StructureToBytes<T>(T structure, byte[] result, int startIndex)
            {
                StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), result, startIndex);
            }

            /// <summary>
            /// Converts an object to binary bytes.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="structure">The object to convert.</param>
            /// <param name="structureSize">The size of the object to convert.</param>
            /// <param name="result">The converted binary bytes.</param>
            /// <param name="startIndex">The start index of the converted binary bytes.</param>
            internal static void StructureToBytes<T>(T structure, int structureSize, byte[] result, int startIndex)
            {
                if (structureSize < 0)
                {
                    throw new GameFrameworkException("Structure size is invalid.");
                }

                if (result == null)
                {
                    throw new GameFrameworkException("Result is invalid.");
                }

                if (startIndex < 0)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                if (startIndex + structureSize > result.Length)
                {
                    throw new GameFrameworkException("Result length is not enough.");
                }

                EnsureCachedHGlobalSize(structureSize);
                System.Runtime.InteropServices.Marshal.StructureToPtr(structure, s_CachedHGlobalPtr, true);
                System.Runtime.InteropServices.Marshal.Copy(s_CachedHGlobalPtr, result, startIndex, structureSize);
            }

            /// <summary>
            /// Converts binary bytes to an object.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="buffer">The binary bytes to convert.</param>
            /// <returns>The converted object.</returns>
            public static T BytesToStructure<T>(byte[] buffer)
            {
                return BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), buffer, 0);
            }

            /// <summary>
            /// Converts binary bytes to an object.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="buffer">The binary bytes to convert.</param>
            /// <param name="startIndex">The start index of the binary bytes to convert.</param>
            /// <returns>The converted object.</returns>
            public static T BytesToStructure<T>(byte[] buffer, int startIndex)
            {
                return BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), buffer, startIndex);
            }

            /// <summary>
            /// Converts binary bytes to an object.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="structureSize">The size of the object to convert.</param>
            /// <param name="buffer">The binary bytes to convert.</param>
            /// <returns>The converted object.</returns>
            internal static T BytesToStructure<T>(int structureSize, byte[] buffer)
            {
                return BytesToStructure<T>(structureSize, buffer, 0);
            }

            /// <summary>
            /// Converts binary bytes to an object.
            /// </summary>
            /// <typeparam name="T">The type of object to convert.</typeparam>
            /// <param name="structureSize">The size of the object to convert.</param>
            /// <param name="buffer">The binary bytes to convert.</param>
            /// <param name="startIndex">The start index of the binary bytes to convert.</param>
            /// <returns>The converted object.</returns>
            internal static T BytesToStructure<T>(int structureSize, byte[] buffer, int startIndex)
            {
                if (structureSize < 0)
                {
                    throw new GameFrameworkException("Structure size is invalid.");
                }

                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                if (startIndex + structureSize > buffer.Length)
                {
                    throw new GameFrameworkException("Buffer length is not enough.");
                }

                EnsureCachedHGlobalSize(structureSize);
                System.Runtime.InteropServices.Marshal.Copy(buffer, startIndex, s_CachedHGlobalPtr, structureSize);
                return (T)System.Runtime.InteropServices.Marshal.PtrToStructure(s_CachedHGlobalPtr, typeof(T));
            }
        }
    }
}
