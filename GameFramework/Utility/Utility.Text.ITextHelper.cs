//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    public static partial class Utility
    {
        public static partial class Text
        {
            /// <summary>
            /// Text helper interface.
            /// </summary>
            public interface ITextHelper
            {
                /// <summary>
                /// Formats a string with one argument.
                /// </summary>
                /// <typeparam name="T">The type of argument.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg">Argument to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T>(string format, T arg);

                /// <summary>
                /// Formats a string with two arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2>(string format, T1 arg1, T2 arg2);

                /// <summary>
                /// Formats a string with three arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3);

                /// <summary>
                /// Formats a string with four arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

                /// <summary>
                /// Formats a string with five arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

                /// <summary>
                /// Formats a string with six arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

                /// <summary>
                /// Formats a string with seven arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

                /// <summary>
                /// Formats a string with eight arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <typeparam name="T8">The type of argument 8.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <param name="arg8">Argument 8 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

                /// <summary>
                /// Formats a string with nine arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <typeparam name="T8">The type of argument 8.</typeparam>
                /// <typeparam name="T9">The type of argument 9.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <param name="arg8">Argument 8 to format.</param>
                /// <param name="arg9">Argument 9 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

                /// <summary>
                /// Formats a string with ten arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <typeparam name="T8">The type of argument 8.</typeparam>
                /// <typeparam name="T9">The type of argument 9.</typeparam>
                /// <typeparam name="T10">The type of argument 10.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <param name="arg8">Argument 8 to format.</param>
                /// <param name="arg9">Argument 9 to format.</param>
                /// <param name="arg10">Argument 10 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);

                /// <summary>
                /// Formats a string with eleven arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <typeparam name="T8">The type of argument 8.</typeparam>
                /// <typeparam name="T9">The type of argument 9.</typeparam>
                /// <typeparam name="T10">The type of argument 10.</typeparam>
                /// <typeparam name="T11">The type of argument 11.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <param name="arg8">Argument 8 to format.</param>
                /// <param name="arg9">Argument 9 to format.</param>
                /// <param name="arg10">Argument 10 to format.</param>
                /// <param name="arg11">Argument 11 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);

                /// <summary>
                /// Formats a string with twelve arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <typeparam name="T8">The type of argument 8.</typeparam>
                /// <typeparam name="T9">The type of argument 9.</typeparam>
                /// <typeparam name="T10">The type of argument 10.</typeparam>
                /// <typeparam name="T11">The type of argument 11.</typeparam>
                /// <typeparam name="T12">The type of argument 12.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <param name="arg8">Argument 8 to format.</param>
                /// <param name="arg9">Argument 9 to format.</param>
                /// <param name="arg10">Argument 10 to format.</param>
                /// <param name="arg11">Argument 11 to format.</param>
                /// <param name="arg12">Argument 12 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12);

                /// <summary>
                /// Formats a string with thirteen arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <typeparam name="T8">The type of argument 8.</typeparam>
                /// <typeparam name="T9">The type of argument 9.</typeparam>
                /// <typeparam name="T10">The type of argument 10.</typeparam>
                /// <typeparam name="T11">The type of argument 11.</typeparam>
                /// <typeparam name="T12">The type of argument 12.</typeparam>
                /// <typeparam name="T13">The type of argument 13.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <param name="arg8">Argument 8 to format.</param>
                /// <param name="arg9">Argument 9 to format.</param>
                /// <param name="arg10">Argument 10 to format.</param>
                /// <param name="arg11">Argument 11 to format.</param>
                /// <param name="arg12">Argument 12 to format.</param>
                /// <param name="arg13">Argument 13 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13);

                /// <summary>
                /// Formats a string with fourteen arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <typeparam name="T8">The type of argument 8.</typeparam>
                /// <typeparam name="T9">The type of argument 9.</typeparam>
                /// <typeparam name="T10">The type of argument 10.</typeparam>
                /// <typeparam name="T11">The type of argument 11.</typeparam>
                /// <typeparam name="T12">The type of argument 12.</typeparam>
                /// <typeparam name="T13">The type of argument 13.</typeparam>
                /// <typeparam name="T14">The type of argument 14.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <param name="arg8">Argument 8 to format.</param>
                /// <param name="arg9">Argument 9 to format.</param>
                /// <param name="arg10">Argument 10 to format.</param>
                /// <param name="arg11">Argument 11 to format.</param>
                /// <param name="arg12">Argument 12 to format.</param>
                /// <param name="arg13">Argument 13 to format.</param>
                /// <param name="arg14">Argument 14 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14);

                /// <summary>
                /// Formats a string with fifteen arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <typeparam name="T8">The type of argument 8.</typeparam>
                /// <typeparam name="T9">The type of argument 9.</typeparam>
                /// <typeparam name="T10">The type of argument 10.</typeparam>
                /// <typeparam name="T11">The type of argument 11.</typeparam>
                /// <typeparam name="T12">The type of argument 12.</typeparam>
                /// <typeparam name="T13">The type of argument 13.</typeparam>
                /// <typeparam name="T14">The type of argument 14.</typeparam>
                /// <typeparam name="T15">The type of argument 15.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <param name="arg8">Argument 8 to format.</param>
                /// <param name="arg9">Argument 9 to format.</param>
                /// <param name="arg10">Argument 10 to format.</param>
                /// <param name="arg11">Argument 11 to format.</param>
                /// <param name="arg12">Argument 12 to format.</param>
                /// <param name="arg13">Argument 13 to format.</param>
                /// <param name="arg14">Argument 14 to format.</param>
                /// <param name="arg15">Argument 15 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15);

                /// <summary>
                /// Formats a string with sixteen arguments.
                /// </summary>
                /// <typeparam name="T1">The type of argument 1.</typeparam>
                /// <typeparam name="T2">The type of argument 2.</typeparam>
                /// <typeparam name="T3">The type of argument 3.</typeparam>
                /// <typeparam name="T4">The type of argument 4.</typeparam>
                /// <typeparam name="T5">The type of argument 5.</typeparam>
                /// <typeparam name="T6">The type of argument 6.</typeparam>
                /// <typeparam name="T7">The type of argument 7.</typeparam>
                /// <typeparam name="T8">The type of argument 8.</typeparam>
                /// <typeparam name="T9">The type of argument 9.</typeparam>
                /// <typeparam name="T10">The type of argument 10.</typeparam>
                /// <typeparam name="T11">The type of argument 11.</typeparam>
                /// <typeparam name="T12">The type of argument 12.</typeparam>
                /// <typeparam name="T13">The type of argument 13.</typeparam>
                /// <typeparam name="T14">The type of argument 14.</typeparam>
                /// <typeparam name="T15">The type of argument 15.</typeparam>
                /// <typeparam name="T16">The type of argument 16.</typeparam>
                /// <param name="format">Composite format string.</param>
                /// <param name="arg1">Argument 1 to format.</param>
                /// <param name="arg2">Argument 2 to format.</param>
                /// <param name="arg3">Argument 3 to format.</param>
                /// <param name="arg4">Argument 4 to format.</param>
                /// <param name="arg5">Argument 5 to format.</param>
                /// <param name="arg6">Argument 6 to format.</param>
                /// <param name="arg7">Argument 7 to format.</param>
                /// <param name="arg8">Argument 8 to format.</param>
                /// <param name="arg9">Argument 9 to format.</param>
                /// <param name="arg10">Argument 10 to format.</param>
                /// <param name="arg11">Argument 11 to format.</param>
                /// <param name="arg12">Argument 12 to format.</param>
                /// <param name="arg13">Argument 13 to format.</param>
                /// <param name="arg14">Argument 14 to format.</param>
                /// <param name="arg15">Argument 15 to format.</param>
                /// <param name="arg16">Argument 16 to format.</param>
                /// <returns>Formatted string.</returns>
                string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16);
            }
        }
    }
}
