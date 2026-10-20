//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Game framework log class.
    /// </summary>
    public static partial class GameFrameworkLog
    {
        private static ILogHelper s_LogHelper = null;

        /// <summary>
        /// Sets the game framework log helper.
        /// </summary>
        /// <param name="logHelper">The game framework log helper to set.</param>
        public static void SetLogHelper(ILogHelper logHelper)
        {
            s_LogHelper = logHelper;
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Debug(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, message);
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Debug(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, message);
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T">The type of the log argument.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg">The log argument.</param>
        public static void Debug<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        public static void Debug<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        public static void Debug<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        public static void Debug<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        public static void Debug<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// Logs a debug level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <typeparam name="T16">The type of log argument 16.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        /// <param name="arg16">Log argument 16.</param>
        public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Debug, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Info(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, message);
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Info(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, message);
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T">The type of the log argument.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg">The log argument.</param>
        public static void Info<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        public static void Info<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        public static void Info<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        public static void Info<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        public static void Info<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        public static void Info<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// Logs an info level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <typeparam name="T16">The type of log argument 16.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        /// <param name="arg16">Log argument 16.</param>
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Warning(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, message);
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Warning(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, message);
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T">The type of the log argument.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg">The log argument.</param>
        public static void Warning<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        public static void Warning<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        public static void Warning<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        public static void Warning<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        public static void Warning<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// Logs a warning level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <typeparam name="T16">The type of log argument 16.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        /// <param name="arg16">Log argument 16.</param>
        public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Error(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, message);
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Error(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, message);
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T">The type of the log argument.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg">The log argument.</param>
        public static void Error<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        public static void Error<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        public static void Error<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        public static void Error<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        public static void Error<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        public static void Error<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// Logs an error level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <typeparam name="T16">The type of log argument 16.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        /// <param name="arg16">Log argument 16.</param>
        public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Fatal(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, message);
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Fatal(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, message);
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T">The type of the log argument.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg">The log argument.</param>
        public static void Fatal<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        public static void Fatal<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        public static void Fatal<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        public static void Fatal<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        public static void Fatal<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
        }

        /// <summary>
        /// Logs a fatal level message.
        /// </summary>
        /// <typeparam name="T1">The type of log argument 1.</typeparam>
        /// <typeparam name="T2">The type of log argument 2.</typeparam>
        /// <typeparam name="T3">The type of log argument 3.</typeparam>
        /// <typeparam name="T4">The type of log argument 4.</typeparam>
        /// <typeparam name="T5">The type of log argument 5.</typeparam>
        /// <typeparam name="T6">The type of log argument 6.</typeparam>
        /// <typeparam name="T7">The type of log argument 7.</typeparam>
        /// <typeparam name="T8">The type of log argument 8.</typeparam>
        /// <typeparam name="T9">The type of log argument 9.</typeparam>
        /// <typeparam name="T10">The type of log argument 10.</typeparam>
        /// <typeparam name="T11">The type of log argument 11.</typeparam>
        /// <typeparam name="T12">The type of log argument 12.</typeparam>
        /// <typeparam name="T13">The type of log argument 13.</typeparam>
        /// <typeparam name="T14">The type of log argument 14.</typeparam>
        /// <typeparam name="T15">The type of log argument 15.</typeparam>
        /// <typeparam name="T16">The type of log argument 16.</typeparam>
        /// <param name="format">The log format.</param>
        /// <param name="arg1">Log argument 1.</param>
        /// <param name="arg2">Log argument 2.</param>
        /// <param name="arg3">Log argument 3.</param>
        /// <param name="arg4">Log argument 4.</param>
        /// <param name="arg5">Log argument 5.</param>
        /// <param name="arg6">Log argument 6.</param>
        /// <param name="arg7">Log argument 7.</param>
        /// <param name="arg8">Log argument 8.</param>
        /// <param name="arg9">Log argument 9.</param>
        /// <param name="arg10">Log argument 10.</param>
        /// <param name="arg11">Log argument 11.</param>
        /// <param name="arg12">Log argument 12.</param>
        /// <param name="arg13">Log argument 13.</param>
        /// <param name="arg14">Log argument 14.</param>
        /// <param name="arg15">Log argument 15.</param>
        /// <param name="arg16">Log argument 16.</param>
        public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameFrameworkLogLevel.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
        }
    }
}
