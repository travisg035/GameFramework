//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace GameFramework
{
    /// <summary>
    /// Type and name pair struct.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    internal struct TypeNamePair : IEquatable<TypeNamePair>
    {
        private readonly Type m_Type;
        private readonly string m_Name;

        /// <summary>
        /// Initializes a new instance of the TypeNamePair struct.
        /// </summary>
        /// <param name="type">Type.</param>
        public TypeNamePair(Type type)
            : this(type, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TypeNamePair struct.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="name">Name.</param>
        public TypeNamePair(Type type, string name)
        {
            if (type == null)
            {
                throw new GameFrameworkException("Type is invalid.");
            }

            m_Type = type;
            m_Name = name ?? string.Empty;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type
        {
            get
            {
                return m_Type;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// Gets the string representation of the type and name pair.
        /// </summary>
        /// <returns>The string representation of the type and name pair.</returns>
        public override string ToString()
        {
            if (m_Type == null)
            {
                throw new GameFrameworkException("Type is invalid.");
            }

            string typeName = m_Type.FullName;
            return string.IsNullOrEmpty(m_Name) ? typeName : Utility.Text.Format("{0}.{1}", typeName, m_Name);
        }

        /// <summary>
        /// Gets the hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return m_Type.GetHashCode() ^ m_Name.GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance equals a specified object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>true if obj equals this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is TypeNamePair && Equals((TypeNamePair)obj);
        }

        /// <summary>
        /// Determines whether this instance equals another TypeNamePair.
        /// </summary>
        /// <param name="value">The TypeNamePair to compare.</param>
        /// <returns>true if value equals this instance; otherwise, false.</returns>
        public bool Equals(TypeNamePair value)
        {
            return m_Type == value.m_Type && m_Name == value.m_Name;
        }

        /// <summary>
        /// Determines whether two TypeNamePair instances are equal.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <returns>true if the two instances are equal; otherwise, false.</returns>
        public static bool operator ==(TypeNamePair a, TypeNamePair b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether two TypeNamePair instances are not equal.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <returns>true if the two instances are not equal; otherwise, false.</returns>
        public static bool operator !=(TypeNamePair a, TypeNamePair b)
        {
            return !(a == b);
        }
    }
}
