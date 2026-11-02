//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Globalization;
using System.Text;

namespace GameFramework
{
    public static partial class Utility
    {
        public static partial class Text
        {
            /// <summary>
            /// Default text helper providing built-in zero-boxing composite string formatting.
            /// </summary>
            public sealed class DefaultTextHelper : ITextHelper
            {
                private const int MaxStringBuilderCapacity = 2048;

                [ThreadStatic]
                private static StringBuilder s_CachedStringBuilder = null;

                [ThreadStatic]
                private static bool s_IsAcquired = false;

                /// <summary>
                /// Initializes a new instance of the DefaultTextHelper class.
                /// </summary>
                public DefaultTextHelper()
                {
                }

                private interface IArgAppender
                {
                    void Append(StringBuilder sb, int index, string format);
                }

                private static StringBuilder AcquireStringBuilder(out bool isCached)
                {
                    if (!s_IsAcquired)
                    {
                        s_IsAcquired = true;
                        isCached = true;
                        if (s_CachedStringBuilder == null)
                        {
                            s_CachedStringBuilder = new StringBuilder(256);
                        }
                        else
                        {
                            s_CachedStringBuilder.Length = 0;
                            if (s_CachedStringBuilder.Capacity > MaxStringBuilderCapacity)
                            {
                                s_CachedStringBuilder.Capacity = MaxStringBuilderCapacity;
                            }
                        }

                        return s_CachedStringBuilder;
                    }

                    isCached = false;
                    return new StringBuilder(256);
                }

                private static void ReleaseStringBuilder(StringBuilder sb, bool isCached)
                {
                    if (isCached)
                    {
                        s_IsAcquired = false;
                    }
                }

                private static string FormatInternal<TArgs>(string format, in TArgs args) where TArgs : struct, IArgAppender
                {
                    if (format == null)
                    {
                        throw new GameFrameworkException("Format is invalid.");
                    }

                    StringBuilder sb = AcquireStringBuilder(out bool isCached);
                    try
                    {
                        int pos = 0;
                        int len = format.Length;
                        while (pos < len)
                        {
                            int literalStart = pos;
                            while (pos < len && format[pos] != '{' && format[pos] != '}')
                            {
                                pos++;
                            }

                            if (pos > literalStart)
                            {
                                sb.Append(format, literalStart, pos - literalStart);
                            }

                            if (pos >= len)
                            {
                                break;
                            }

                            char c = format[pos];
                            pos++;

                            if (c == '}')
                            {
                                if (pos < len && format[pos] == '}')
                                {
                                    pos++;
                                    sb.Append('}');
                                }
                                else
                                {
                                    throw new FormatException("Input string was not in a correct format.");
                                }
                            }
                            else
                            {
                                if (pos < len && format[pos] == '{')
                                {
                                    pos++;
                                    sb.Append('{');
                                }
                                else
                                {
                                    while (pos < len && format[pos] == ' ')
                                    {
                                        pos++;
                                    }

                                    if (pos >= len || format[pos] < '0' || format[pos] > '9')
                                    {
                                        throw new FormatException("Input string was not in a correct format.");
                                    }

                                    int index = 0;
                                    while (pos < len && format[pos] >= '0' && format[pos] <= '9')
                                    {
                                        index = index * 10 + (format[pos] - '0');
                                        pos++;
                                    }

                                    while (pos < len && format[pos] == ' ')
                                    {
                                        pos++;
                                    }

                                    int alignment = 0;
                                    if (pos < len && format[pos] == ',')
                                    {
                                        pos++;
                                        while (pos < len && format[pos] == ' ')
                                        {
                                            pos++;
                                        }

                                        if (pos >= len)
                                        {
                                            throw new FormatException("Input string was not in a correct format.");
                                        }

                                        bool negative = false;
                                        if (format[pos] == '-')
                                        {
                                            negative = true;
                                            pos++;
                                        }

                                        if (pos >= len || format[pos] < '0' || format[pos] > '9')
                                        {
                                            throw new FormatException("Input string was not in a correct format.");
                                        }

                                        while (pos < len && format[pos] >= '0' && format[pos] <= '9')
                                        {
                                            alignment = alignment * 10 + (format[pos] - '0');
                                            pos++;
                                        }

                                        if (negative)
                                        {
                                            alignment = -alignment;
                                        }

                                        while (pos < len && format[pos] == ' ')
                                        {
                                            pos++;
                                        }
                                    }

                                    string itemFormat = null;
                                    if (pos < len && format[pos] == ':')
                                    {
                                        pos++;
                                        int formatStart = pos;
                                        while (pos < len && format[pos] != '}')
                                        {
                                            pos++;
                                        }

                                        if (pos >= len)
                                        {
                                            throw new FormatException("Input string was not in a correct format.");
                                        }

                                        if (pos > formatStart)
                                        {
                                            itemFormat = format.Substring(formatStart, pos - formatStart);
                                        }
                                    }

                                    if (pos >= len || format[pos] != '}')
                                    {
                                        throw new FormatException("Input string was not in a correct format.");
                                    }

                                    pos++;

                                    int startLength = sb.Length;
                                    args.Append(sb, index, itemFormat);
                                    int written = sb.Length - startLength;

                                    if (alignment > 0 && written < alignment)
                                    {
                                        sb.Insert(startLength, new string(' ', alignment - written));
                                    }
                                    else if (alignment < 0 && written < -alignment)
                                    {
                                        sb.Append(' ', -alignment - written);
                                    }
                                }
                            }
                        }

                        return sb.ToString();
                    }
                    finally
                    {
                        ReleaseStringBuilder(sb, isCached);
                    }
                }

                private static class Formatter<T>
                {
                    public static readonly Action<StringBuilder, T, string> Format = CreateFormatAction();

                    private static Action<StringBuilder, T, string> CreateFormatAction()
                    {
                        Type type = typeof(T);

                        if (type == typeof(string))
                        {
                            Action<StringBuilder, string, string> action = (sb, val, fmt) =>
                            {
                                if (val != null)
                                {
                                    sb.Append(val);
                                }
                            };
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(int))
                        {
                            Action<StringBuilder, int, string> action = (sb, val, fmt) => AppendInt32(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(uint))
                        {
                            Action<StringBuilder, uint, string> action = (sb, val, fmt) => AppendUInt32(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(long))
                        {
                            Action<StringBuilder, long, string> action = (sb, val, fmt) => AppendInt64(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(ulong))
                        {
                            Action<StringBuilder, ulong, string> action = (sb, val, fmt) => AppendUInt64(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(short))
                        {
                            Action<StringBuilder, short, string> action = (sb, val, fmt) => AppendInt16(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(ushort))
                        {
                            Action<StringBuilder, ushort, string> action = (sb, val, fmt) => AppendUInt16(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(byte))
                        {
                            Action<StringBuilder, byte, string> action = (sb, val, fmt) => AppendByte(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(sbyte))
                        {
                            Action<StringBuilder, sbyte, string> action = (sb, val, fmt) => AppendSByte(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(float))
                        {
                            Action<StringBuilder, float, string> action = (sb, val, fmt) => AppendSingle(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(double))
                        {
                            Action<StringBuilder, double, string> action = (sb, val, fmt) => AppendDouble(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(decimal))
                        {
                            Action<StringBuilder, decimal, string> action = (sb, val, fmt) => AppendDecimal(sb, val, fmt);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(bool))
                        {
                            Action<StringBuilder, bool, string> action = (sb, val, fmt) => sb.Append(val);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (type == typeof(char))
                        {
                            Action<StringBuilder, char, string> action = (sb, val, fmt) => sb.Append(val);
                            return (Action<StringBuilder, T, string>)(object)action;
                        }

                        if (typeof(IFormattable).IsAssignableFrom(type))
                        {
                            return (sb, val, fmt) =>
                            {
                                if (val != null)
                                {
                                    IFormattable formattable = (IFormattable)val;
                                    string str = formattable.ToString(fmt, CultureInfo.InvariantCulture);
                                    if (str != null)
                                    {
                                        sb.Append(str);
                                    }
                                }
                            };
                        }

                        return (sb, val, fmt) =>
                        {
                            if (val != null)
                            {
                                string str = val.ToString();
                                if (str != null)
                                {
                                    sb.Append(str);
                                }
                            }
                        };
                    }
                }

                private static void AppendInt32(StringBuilder sb, int value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append(value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[32];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendUInt32(StringBuilder sb, uint value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append(value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[32];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendInt64(StringBuilder sb, long value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append(value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[32];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendUInt64(StringBuilder sb, ulong value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append(value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[32];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendInt16(StringBuilder sb, short value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append((int)value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[32];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendUInt16(StringBuilder sb, ushort value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append((uint)value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[32];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendByte(StringBuilder sb, byte value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append((int)value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[32];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendSByte(StringBuilder sb, sbyte value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append((int)value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[32];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendSingle(StringBuilder sb, float value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append(value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[64];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendDouble(StringBuilder sb, double value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append(value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[64];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private static void AppendDecimal(StringBuilder sb, decimal value, string format)
                {
                    if (string.IsNullOrEmpty(format))
                    {
                        sb.Append(value);
                        return;
                    }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                    Span<char> span = stackalloc char[64];
                    if (value.TryFormat(span, out int written, format, CultureInfo.InvariantCulture))
                    {
                        sb.Append(span.Slice(0, written));
                        return;
                    }
#endif

                    sb.Append(value.ToString(format, CultureInfo.InvariantCulture));
                }

                private readonly struct Args1<T1> : IArgAppender
                {
                    private readonly T1 m_Arg1;

                    public Args1(T1 arg1)
                    {
                        m_Arg1 = arg1;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args2<T1, T2> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;

                    public Args2(T1 arg1, T2 arg2)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args3<T1, T2, T3> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;

                    public Args3(T1 arg1, T2 arg2, T3 arg3)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args4<T1, T2, T3, T4> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;

                    public Args4(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args5<T1, T2, T3, T4, T5> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;

                    public Args5(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args6<T1, T2, T3, T4, T5, T6> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;

                    public Args6(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args7<T1, T2, T3, T4, T5, T6, T7> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;

                    public Args7(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args8<T1, T2, T3, T4, T5, T6, T7, T8> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;
                    private readonly T8 m_Arg8;

                    public Args8(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                        m_Arg8 = arg8;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            case 7:
                                Formatter<T8>.Format(sb, m_Arg8, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args9<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;
                    private readonly T8 m_Arg8;
                    private readonly T9 m_Arg9;

                    public Args9(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                        m_Arg8 = arg8;
                        m_Arg9 = arg9;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            case 7:
                                Formatter<T8>.Format(sb, m_Arg8, format);
                                break;
                            case 8:
                                Formatter<T9>.Format(sb, m_Arg9, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args10<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;
                    private readonly T8 m_Arg8;
                    private readonly T9 m_Arg9;
                    private readonly T10 m_Arg10;

                    public Args10(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                        m_Arg8 = arg8;
                        m_Arg9 = arg9;
                        m_Arg10 = arg10;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            case 7:
                                Formatter<T8>.Format(sb, m_Arg8, format);
                                break;
                            case 8:
                                Formatter<T9>.Format(sb, m_Arg9, format);
                                break;
                            case 9:
                                Formatter<T10>.Format(sb, m_Arg10, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args11<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;
                    private readonly T8 m_Arg8;
                    private readonly T9 m_Arg9;
                    private readonly T10 m_Arg10;
                    private readonly T11 m_Arg11;

                    public Args11(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                        m_Arg8 = arg8;
                        m_Arg9 = arg9;
                        m_Arg10 = arg10;
                        m_Arg11 = arg11;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            case 7:
                                Formatter<T8>.Format(sb, m_Arg8, format);
                                break;
                            case 8:
                                Formatter<T9>.Format(sb, m_Arg9, format);
                                break;
                            case 9:
                                Formatter<T10>.Format(sb, m_Arg10, format);
                                break;
                            case 10:
                                Formatter<T11>.Format(sb, m_Arg11, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args12<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;
                    private readonly T8 m_Arg8;
                    private readonly T9 m_Arg9;
                    private readonly T10 m_Arg10;
                    private readonly T11 m_Arg11;
                    private readonly T12 m_Arg12;

                    public Args12(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                        m_Arg8 = arg8;
                        m_Arg9 = arg9;
                        m_Arg10 = arg10;
                        m_Arg11 = arg11;
                        m_Arg12 = arg12;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            case 7:
                                Formatter<T8>.Format(sb, m_Arg8, format);
                                break;
                            case 8:
                                Formatter<T9>.Format(sb, m_Arg9, format);
                                break;
                            case 9:
                                Formatter<T10>.Format(sb, m_Arg10, format);
                                break;
                            case 10:
                                Formatter<T11>.Format(sb, m_Arg11, format);
                                break;
                            case 11:
                                Formatter<T12>.Format(sb, m_Arg12, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args13<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;
                    private readonly T8 m_Arg8;
                    private readonly T9 m_Arg9;
                    private readonly T10 m_Arg10;
                    private readonly T11 m_Arg11;
                    private readonly T12 m_Arg12;
                    private readonly T13 m_Arg13;

                    public Args13(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                        m_Arg8 = arg8;
                        m_Arg9 = arg9;
                        m_Arg10 = arg10;
                        m_Arg11 = arg11;
                        m_Arg12 = arg12;
                        m_Arg13 = arg13;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            case 7:
                                Formatter<T8>.Format(sb, m_Arg8, format);
                                break;
                            case 8:
                                Formatter<T9>.Format(sb, m_Arg9, format);
                                break;
                            case 9:
                                Formatter<T10>.Format(sb, m_Arg10, format);
                                break;
                            case 10:
                                Formatter<T11>.Format(sb, m_Arg11, format);
                                break;
                            case 11:
                                Formatter<T12>.Format(sb, m_Arg12, format);
                                break;
                            case 12:
                                Formatter<T13>.Format(sb, m_Arg13, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args14<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;
                    private readonly T8 m_Arg8;
                    private readonly T9 m_Arg9;
                    private readonly T10 m_Arg10;
                    private readonly T11 m_Arg11;
                    private readonly T12 m_Arg12;
                    private readonly T13 m_Arg13;
                    private readonly T14 m_Arg14;

                    public Args14(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                        m_Arg8 = arg8;
                        m_Arg9 = arg9;
                        m_Arg10 = arg10;
                        m_Arg11 = arg11;
                        m_Arg12 = arg12;
                        m_Arg13 = arg13;
                        m_Arg14 = arg14;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            case 7:
                                Formatter<T8>.Format(sb, m_Arg8, format);
                                break;
                            case 8:
                                Formatter<T9>.Format(sb, m_Arg9, format);
                                break;
                            case 9:
                                Formatter<T10>.Format(sb, m_Arg10, format);
                                break;
                            case 10:
                                Formatter<T11>.Format(sb, m_Arg11, format);
                                break;
                            case 11:
                                Formatter<T12>.Format(sb, m_Arg12, format);
                                break;
                            case 12:
                                Formatter<T13>.Format(sb, m_Arg13, format);
                                break;
                            case 13:
                                Formatter<T14>.Format(sb, m_Arg14, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args15<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;
                    private readonly T8 m_Arg8;
                    private readonly T9 m_Arg9;
                    private readonly T10 m_Arg10;
                    private readonly T11 m_Arg11;
                    private readonly T12 m_Arg12;
                    private readonly T13 m_Arg13;
                    private readonly T14 m_Arg14;
                    private readonly T15 m_Arg15;

                    public Args15(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                        m_Arg8 = arg8;
                        m_Arg9 = arg9;
                        m_Arg10 = arg10;
                        m_Arg11 = arg11;
                        m_Arg12 = arg12;
                        m_Arg13 = arg13;
                        m_Arg14 = arg14;
                        m_Arg15 = arg15;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            case 7:
                                Formatter<T8>.Format(sb, m_Arg8, format);
                                break;
                            case 8:
                                Formatter<T9>.Format(sb, m_Arg9, format);
                                break;
                            case 9:
                                Formatter<T10>.Format(sb, m_Arg10, format);
                                break;
                            case 10:
                                Formatter<T11>.Format(sb, m_Arg11, format);
                                break;
                            case 11:
                                Formatter<T12>.Format(sb, m_Arg12, format);
                                break;
                            case 12:
                                Formatter<T13>.Format(sb, m_Arg13, format);
                                break;
                            case 13:
                                Formatter<T14>.Format(sb, m_Arg14, format);
                                break;
                            case 14:
                                Formatter<T15>.Format(sb, m_Arg15, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                private readonly struct Args16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : IArgAppender
                {
                    private readonly T1 m_Arg1;
                    private readonly T2 m_Arg2;
                    private readonly T3 m_Arg3;
                    private readonly T4 m_Arg4;
                    private readonly T5 m_Arg5;
                    private readonly T6 m_Arg6;
                    private readonly T7 m_Arg7;
                    private readonly T8 m_Arg8;
                    private readonly T9 m_Arg9;
                    private readonly T10 m_Arg10;
                    private readonly T11 m_Arg11;
                    private readonly T12 m_Arg12;
                    private readonly T13 m_Arg13;
                    private readonly T14 m_Arg14;
                    private readonly T15 m_Arg15;
                    private readonly T16 m_Arg16;

                    public Args16(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
                    {
                        m_Arg1 = arg1;
                        m_Arg2 = arg2;
                        m_Arg3 = arg3;
                        m_Arg4 = arg4;
                        m_Arg5 = arg5;
                        m_Arg6 = arg6;
                        m_Arg7 = arg7;
                        m_Arg8 = arg8;
                        m_Arg9 = arg9;
                        m_Arg10 = arg10;
                        m_Arg11 = arg11;
                        m_Arg12 = arg12;
                        m_Arg13 = arg13;
                        m_Arg14 = arg14;
                        m_Arg15 = arg15;
                        m_Arg16 = arg16;
                    }

                    public void Append(StringBuilder sb, int index, string format)
                    {
                        switch (index)
                        {
                            case 0:
                                Formatter<T1>.Format(sb, m_Arg1, format);
                                break;
                            case 1:
                                Formatter<T2>.Format(sb, m_Arg2, format);
                                break;
                            case 2:
                                Formatter<T3>.Format(sb, m_Arg3, format);
                                break;
                            case 3:
                                Formatter<T4>.Format(sb, m_Arg4, format);
                                break;
                            case 4:
                                Formatter<T5>.Format(sb, m_Arg5, format);
                                break;
                            case 5:
                                Formatter<T6>.Format(sb, m_Arg6, format);
                                break;
                            case 6:
                                Formatter<T7>.Format(sb, m_Arg7, format);
                                break;
                            case 7:
                                Formatter<T8>.Format(sb, m_Arg8, format);
                                break;
                            case 8:
                                Formatter<T9>.Format(sb, m_Arg9, format);
                                break;
                            case 9:
                                Formatter<T10>.Format(sb, m_Arg10, format);
                                break;
                            case 10:
                                Formatter<T11>.Format(sb, m_Arg11, format);
                                break;
                            case 11:
                                Formatter<T12>.Format(sb, m_Arg12, format);
                                break;
                            case 12:
                                Formatter<T13>.Format(sb, m_Arg13, format);
                                break;
                            case 13:
                                Formatter<T14>.Format(sb, m_Arg14, format);
                                break;
                            case 14:
                                Formatter<T15>.Format(sb, m_Arg15, format);
                                break;
                            case 15:
                                Formatter<T16>.Format(sb, m_Arg16, format);
                                break;
                            default:
                                throw new FormatException(string.Format("Index (zero based) must be greater than or equal to zero and less than the size of the argument list. Index: '{0}'.", index));
                        }
                    }
                }

                /// <inheritdoc />
                public string Format<T>(string format, T arg)
                {
                    Args1<T> args = new Args1<T>(arg);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2>(string format, T1 arg1, T2 arg2)
                {
                    Args2<T1, T2> args = new Args2<T1, T2>(arg1, arg2);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
                {
                    Args3<T1, T2, T3> args = new Args3<T1, T2, T3>(arg1, arg2, arg3);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
                {
                    Args4<T1, T2, T3, T4> args = new Args4<T1, T2, T3, T4>(arg1, arg2, arg3, arg4);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
                {
                    Args5<T1, T2, T3, T4, T5> args = new Args5<T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
                {
                    Args6<T1, T2, T3, T4, T5, T6> args = new Args6<T1, T2, T3, T4, T5, T6>(arg1, arg2, arg3, arg4, arg5, arg6);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
                {
                    Args7<T1, T2, T3, T4, T5, T6, T7> args = new Args7<T1, T2, T3, T4, T5, T6, T7>(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7, T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
                {
                    Args8<T1, T2, T3, T4, T5, T6, T7, T8> args = new Args8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
                {
                    Args9<T1, T2, T3, T4, T5, T6, T7, T8, T9> args = new Args9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
                {
                    Args10<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> args = new Args10<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
                {
                    Args11<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> args = new Args11<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
                {
                    Args12<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> args = new Args12<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
                {
                    Args13<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> args = new Args13<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
                {
                    Args14<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> args = new Args14<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
                {
                    Args15<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> args = new Args15<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                    return FormatInternal(format, in args);
                }

                /// <inheritdoc />
                public string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
                {
                    Args16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> args = new Args16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                    return FormatInternal(format, in args);
                }
            }
        }
    }
}
