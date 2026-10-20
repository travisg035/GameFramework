//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Text;

namespace GameFramework
{
    public static partial class Utility
    {
        /// <summary>
        /// Type converter utility functions.
        /// </summary>
        public static class Converter
        {
            private const float InchesToCentimeters = 2.54f; // 1 inch = 2.54 cm
            private const float CentimetersToInches = 1f / InchesToCentimeters; // 1 cm = 0.3937 inches

            /// <summary>
            /// Gets the byte order ("endianness") in which data is stored in this computer architecture.
            /// </summary>
            public static bool IsLittleEndian
            {
                get
                {
                    return BitConverter.IsLittleEndian;
                }
            }

            /// <summary>
            /// Gets or sets the screen dots per inch (DPI).
            /// </summary>
            public static float ScreenDpi
            {
                get;
                set;
            }

            /// <summary>
            /// Converts pixels to centimeters.
            /// </summary>
            /// <param name="pixels">Pixels.</param>
            /// <returns>Centimeters.</returns>
            public static float GetCentimetersFromPixels(float pixels)
            {
                if (ScreenDpi <= 0)
                {
                    throw new GameFrameworkException("You must set screen DPI first.");
                }

                return InchesToCentimeters * pixels / ScreenDpi;
            }

            /// <summary>
            /// Converts centimeters to pixels.
            /// </summary>
            /// <param name="centimeters">Centimeters.</param>
            /// <returns>Pixels.</returns>
            public static float GetPixelsFromCentimeters(float centimeters)
            {
                if (ScreenDpi <= 0)
                {
                    throw new GameFrameworkException("You must set screen DPI first.");
                }

                return CentimetersToInches * centimeters * ScreenDpi;
            }

            /// <summary>
            /// Converts pixels to inches.
            /// </summary>
            /// <param name="pixels">Pixels.</param>
            /// <returns>Inches.</returns>
            public static float GetInchesFromPixels(float pixels)
            {
                if (ScreenDpi <= 0)
                {
                    throw new GameFrameworkException("You must set screen DPI first.");
                }

                return pixels / ScreenDpi;
            }

            /// <summary>
            /// Converts inches to pixels.
            /// </summary>
            /// <param name="inches">Inches.</param>
            /// <returns>Pixels.</returns>
            public static float GetPixelsFromInches(float inches)
            {
                if (ScreenDpi <= 0)
                {
                    throw new GameFrameworkException("You must set screen DPI first.");
                }

                return inches * ScreenDpi;
            }

            /// <summary>
            /// Gets the specified boolean value in binary bytes.
            /// </summary>
            /// <param name="value">The boolean value to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(bool value)
            {
                byte[] buffer = new byte[1];
                GetBytes(value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified boolean value in binary bytes.
            /// </summary>
            /// <param name="value">The boolean value to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static void GetBytes(bool value, byte[] buffer)
            {
                GetBytes(value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified boolean value in binary bytes.
            /// </summary>
            /// <param name="value">The boolean value to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static void GetBytes(bool value, byte[] buffer, int startIndex)
            {
                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0 || startIndex + 1 > buffer.Length)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                buffer[startIndex] = value ? (byte)1 : (byte)0;
            }

            /// <summary>
            /// Returns a boolean value converted from the first byte in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>true if the first byte in value is non-zero; otherwise, false.</returns>
            public static bool GetBoolean(byte[] value)
            {
                return BitConverter.ToBoolean(value, 0);
            }

            /// <summary>
            /// Returns a boolean value converted from the byte at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>true if the byte at the specified position in value is non-zero; otherwise, false.</returns>
            public static bool GetBoolean(byte[] value, int startIndex)
            {
                return BitConverter.ToBoolean(value, startIndex);
            }

            /// <summary>
            /// Gets the specified Unicode character value in binary bytes.
            /// </summary>
            /// <param name="value">The character to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(char value)
            {
                byte[] buffer = new byte[2];
                GetBytes((short)value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified Unicode character value in binary bytes.
            /// </summary>
            /// <param name="value">The character to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static void GetBytes(char value, byte[] buffer)
            {
                GetBytes((short)value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified Unicode character value in binary bytes.
            /// </summary>
            /// <param name="value">The character to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static void GetBytes(char value, byte[] buffer, int startIndex)
            {
                GetBytes((short)value, buffer, startIndex);
            }

            /// <summary>
            /// Returns a Unicode character converted from the first two bytes in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>A character formed by two bytes.</returns>
            public static char GetChar(byte[] value)
            {
                return BitConverter.ToChar(value, 0);
            }

            /// <summary>
            /// Returns a Unicode character converted from two bytes at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>A character formed by two bytes.</returns>
            public static char GetChar(byte[] value, int startIndex)
            {
                return BitConverter.ToChar(value, startIndex);
            }

            /// <summary>
            /// Gets the specified 16-bit signed integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(short value)
            {
                byte[] buffer = new byte[2];
                GetBytes(value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified 16-bit signed integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static void GetBytes(short value, byte[] buffer)
            {
                GetBytes(value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified 16-bit signed integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static unsafe void GetBytes(short value, byte[] buffer, int startIndex)
            {
                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0 || startIndex + 2 > buffer.Length)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                fixed (byte* valueRef = buffer)
                {
                    *(short*)(valueRef + startIndex) = value;
                }
            }

            /// <summary>
            /// Returns a 16-bit signed integer converted from the first two bytes in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>A 16-bit signed integer formed by two bytes.</returns>
            public static short GetInt16(byte[] value)
            {
                return BitConverter.ToInt16(value, 0);
            }

            /// <summary>
            /// Returns a 16-bit signed integer converted from two bytes at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>A 16-bit signed integer formed by two bytes.</returns>
            public static short GetInt16(byte[] value, int startIndex)
            {
                return BitConverter.ToInt16(value, startIndex);
            }

            /// <summary>
            /// Gets the specified 16-bit unsigned integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(ushort value)
            {
                byte[] buffer = new byte[2];
                GetBytes((short)value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified 16-bit unsigned integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static void GetBytes(ushort value, byte[] buffer)
            {
                GetBytes((short)value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified 16-bit unsigned integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static void GetBytes(ushort value, byte[] buffer, int startIndex)
            {
                GetBytes((short)value, buffer, startIndex);
            }

            /// <summary>
            /// Returns a 16-bit unsigned integer converted from the first two bytes in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>A 16-bit unsigned integer formed by two bytes.</returns>
            public static ushort GetUInt16(byte[] value)
            {
                return BitConverter.ToUInt16(value, 0);
            }

            /// <summary>
            /// Returns a 16-bit unsigned integer converted from two bytes at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>A 16-bit unsigned integer formed by two bytes.</returns>
            public static ushort GetUInt16(byte[] value, int startIndex)
            {
                return BitConverter.ToUInt16(value, startIndex);
            }

            /// <summary>
            /// Gets the specified 32-bit signed integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(int value)
            {
                byte[] buffer = new byte[4];
                GetBytes(value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified 32-bit signed integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static void GetBytes(int value, byte[] buffer)
            {
                GetBytes(value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified 32-bit signed integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static unsafe void GetBytes(int value, byte[] buffer, int startIndex)
            {
                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0 || startIndex + 4 > buffer.Length)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                fixed (byte* valueRef = buffer)
                {
                    *(int*)(valueRef + startIndex) = value;
                }
            }

            /// <summary>
            /// Returns a 32-bit signed integer converted from the first four bytes in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>A 32-bit signed integer formed by four bytes.</returns>
            public static int GetInt32(byte[] value)
            {
                return BitConverter.ToInt32(value, 0);
            }

            /// <summary>
            /// Returns a 32-bit signed integer converted from four bytes at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>A 32-bit signed integer formed by four bytes.</returns>
            public static int GetInt32(byte[] value, int startIndex)
            {
                return BitConverter.ToInt32(value, startIndex);
            }

            /// <summary>
            /// Gets the specified 32-bit unsigned integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(uint value)
            {
                byte[] buffer = new byte[4];
                GetBytes((int)value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified 32-bit unsigned integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static void GetBytes(uint value, byte[] buffer)
            {
                GetBytes((int)value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified 32-bit unsigned integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static void GetBytes(uint value, byte[] buffer, int startIndex)
            {
                GetBytes((int)value, buffer, startIndex);
            }

            /// <summary>
            /// Returns a 32-bit unsigned integer converted from the first four bytes in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>A 32-bit unsigned integer formed by four bytes.</returns>
            public static uint GetUInt32(byte[] value)
            {
                return BitConverter.ToUInt32(value, 0);
            }

            /// <summary>
            /// Returns a 32-bit unsigned integer converted from four bytes at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>A 32-bit unsigned integer formed by four bytes.</returns>
            public static uint GetUInt32(byte[] value, int startIndex)
            {
                return BitConverter.ToUInt32(value, startIndex);
            }

            /// <summary>
            /// Gets the specified 64-bit signed integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(long value)
            {
                byte[] buffer = new byte[8];
                GetBytes(value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified 64-bit signed integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static void GetBytes(long value, byte[] buffer)
            {
                GetBytes(value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified 64-bit signed integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static unsafe void GetBytes(long value, byte[] buffer, int startIndex)
            {
                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0 || startIndex + 8 > buffer.Length)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                fixed (byte* valueRef = buffer)
                {
                    *(long*)(valueRef + startIndex) = value;
                }
            }

            /// <summary>
            /// Returns a 64-bit signed integer converted from the first eight bytes in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>A 64-bit signed integer formed by eight bytes.</returns>
            public static long GetInt64(byte[] value)
            {
                return BitConverter.ToInt64(value, 0);
            }

            /// <summary>
            /// Returns a 64-bit signed integer converted from eight bytes at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>A 64-bit signed integer formed by eight bytes.</returns>
            public static long GetInt64(byte[] value, int startIndex)
            {
                return BitConverter.ToInt64(value, startIndex);
            }

            /// <summary>
            /// Gets the specified 64-bit unsigned integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(ulong value)
            {
                byte[] buffer = new byte[8];
                GetBytes((long)value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified 64-bit unsigned integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static void GetBytes(ulong value, byte[] buffer)
            {
                GetBytes((long)value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified 64-bit unsigned integer value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static void GetBytes(ulong value, byte[] buffer, int startIndex)
            {
                GetBytes((long)value, buffer, startIndex);
            }

            /// <summary>
            /// Returns a 64-bit unsigned integer converted from the first eight bytes in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>A 64-bit unsigned integer formed by eight bytes.</returns>
            public static ulong GetUInt64(byte[] value)
            {
                return BitConverter.ToUInt64(value, 0);
            }

            /// <summary>
            /// Returns a 64-bit unsigned integer converted from eight bytes at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>A 64-bit unsigned integer formed by eight bytes.</returns>
            public static ulong GetUInt64(byte[] value, int startIndex)
            {
                return BitConverter.ToUInt64(value, startIndex);
            }

            /// <summary>
            /// Gets the specified single-precision floating-point value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static unsafe byte[] GetBytes(float value)
            {
                byte[] buffer = new byte[4];
                GetBytes(*(int*)&value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified single-precision floating-point value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static unsafe void GetBytes(float value, byte[] buffer)
            {
                GetBytes(*(int*)&value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified single-precision floating-point value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static unsafe void GetBytes(float value, byte[] buffer, int startIndex)
            {
                GetBytes(*(int*)&value, buffer, startIndex);
            }

            /// <summary>
            /// Returns a single-precision floating-point number converted from the first four bytes in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>A single-precision floating-point number formed by four bytes.</returns>
            public static float GetSingle(byte[] value)
            {
                return BitConverter.ToSingle(value, 0);
            }

            /// <summary>
            /// Returns a single-precision floating-point number converted from four bytes at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>A single-precision floating-point number formed by four bytes.</returns>
            public static float GetSingle(byte[] value, int startIndex)
            {
                return BitConverter.ToSingle(value, startIndex);
            }

            /// <summary>
            /// Gets the specified double-precision floating-point value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static unsafe byte[] GetBytes(double value)
            {
                byte[] buffer = new byte[8];
                GetBytes(*(long*)&value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// Gets the specified double-precision floating-point value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            public static unsafe void GetBytes(double value, byte[] buffer)
            {
                GetBytes(*(long*)&value, buffer, 0);
            }

            /// <summary>
            /// Gets the specified double-precision floating-point value in binary bytes.
            /// </summary>
            /// <param name="value">The number to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            public static unsafe void GetBytes(double value, byte[] buffer, int startIndex)
            {
                GetBytes(*(long*)&value, buffer, startIndex);
            }

            /// <summary>
            /// Returns a double-precision floating-point number converted from the first eight bytes in the specified byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>A double-precision floating-point number formed by eight bytes.</returns>
            public static double GetDouble(byte[] value)
            {
                return BitConverter.ToDouble(value, 0);
            }

            /// <summary>
            /// Returns a double-precision floating-point number converted from eight bytes at the specified position in the byte array.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <returns>A double-precision floating-point number formed by eight bytes.</returns>
            public static double GetDouble(byte[] value, int startIndex)
            {
                return BitConverter.ToDouble(value, startIndex);
            }

            /// <summary>
            /// Gets the UTF-8 encoded string in binary bytes.
            /// </summary>
            /// <param name="value">The string to convert.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(string value)
            {
                return GetBytes(value, Encoding.UTF8);
            }

            /// <summary>
            /// Gets the UTF-8 encoded string in binary bytes.
            /// </summary>
            /// <param name="value">The string to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <returns>The actual number of bytes written into buffer.</returns>
            public static int GetBytes(string value, byte[] buffer)
            {
                return GetBytes(value, Encoding.UTF8, buffer, 0);
            }

            /// <summary>
            /// Gets the UTF-8 encoded string in binary bytes.
            /// </summary>
            /// <param name="value">The string to convert.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            /// <returns>The actual number of bytes written into buffer.</returns>
            public static int GetBytes(string value, byte[] buffer, int startIndex)
            {
                return GetBytes(value, Encoding.UTF8, buffer, startIndex);
            }

            /// <summary>
            /// Gets the string in binary bytes using the specified encoding.
            /// </summary>
            /// <param name="value">The string to convert.</param>
            /// <param name="encoding">The encoding to use.</param>
            /// <returns>The binary bytes containing the result.</returns>
            public static byte[] GetBytes(string value, Encoding encoding)
            {
                if (value == null)
                {
                    throw new GameFrameworkException("Value is invalid.");
                }

                if (encoding == null)
                {
                    throw new GameFrameworkException("Encoding is invalid.");
                }

                return encoding.GetBytes(value);
            }

            /// <summary>
            /// Gets the string in binary bytes using the specified encoding.
            /// </summary>
            /// <param name="value">The string to convert.</param>
            /// <param name="encoding">The encoding to use.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <returns>The actual number of bytes written into buffer.</returns>
            public static int GetBytes(string value, Encoding encoding, byte[] buffer)
            {
                return GetBytes(value, encoding, buffer, 0);
            }

            /// <summary>
            /// Gets the string in binary bytes using the specified encoding.
            /// </summary>
            /// <param name="value">The string to convert.</param>
            /// <param name="encoding">The encoding to use.</param>
            /// <param name="buffer">The binary bytes containing the result.</param>
            /// <param name="startIndex">The start index within buffer.</param>
            /// <returns>The actual number of bytes written into buffer.</returns>
            public static int GetBytes(string value, Encoding encoding, byte[] buffer, int startIndex)
            {
                if (value == null)
                {
                    throw new GameFrameworkException("Value is invalid.");
                }

                if (encoding == null)
                {
                    throw new GameFrameworkException("Encoding is invalid.");
                }

                return encoding.GetBytes(value, 0, value.Length, buffer, startIndex);
            }

            /// <summary>
            /// Returns a string converted from the byte array using UTF-8 encoding.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <returns>The converted string.</returns>
            public static string GetString(byte[] value)
            {
                return GetString(value, Encoding.UTF8);
            }

            /// <summary>
            /// Returns a string converted from the byte array using the specified encoding.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="encoding">The encoding to use.</param>
            /// <returns>The converted string.</returns>
            public static string GetString(byte[] value, Encoding encoding)
            {
                if (value == null)
                {
                    throw new GameFrameworkException("Value is invalid.");
                }

                if (encoding == null)
                {
                    throw new GameFrameworkException("Encoding is invalid.");
                }

                return encoding.GetString(value);
            }

            /// <summary>
            /// Returns a string converted from the byte array using UTF-8 encoding.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <param name="length">The length.</param>
            /// <returns>The converted string.</returns>
            public static string GetString(byte[] value, int startIndex, int length)
            {
                return GetString(value, startIndex, length, Encoding.UTF8);
            }

            /// <summary>
            /// Returns a string converted from the byte array using the specified encoding.
            /// </summary>
            /// <param name="value">The byte array.</param>
            /// <param name="startIndex">The start index within value.</param>
            /// <param name="length">The length.</param>
            /// <param name="encoding">The encoding to use.</param>
            /// <returns>The converted string.</returns>
            public static string GetString(byte[] value, int startIndex, int length, Encoding encoding)
            {
                if (value == null)
                {
                    throw new GameFrameworkException("Value is invalid.");
                }

                if (encoding == null)
                {
                    throw new GameFrameworkException("Encoding is invalid.");
                }

                return encoding.GetString(value, startIndex, length);
            }
        }
    }
}
