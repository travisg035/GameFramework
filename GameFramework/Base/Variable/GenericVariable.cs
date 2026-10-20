//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework
{
    /// <summary>
    /// Strongly-typed variable.
    /// </summary>
    /// <typeparam name="T">The variable type.</typeparam>
    public abstract class Variable<T> : Variable
    {
        private T m_Value;

        /// <summary>
        /// Initializes a new instance of the Variable class.
        /// </summary>
        public Variable()
        {
            m_Value = default(T);
        }

        /// <summary>
        /// Gets the variable type.
        /// </summary>
        public override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// Gets or sets the variable value.
        /// </summary>
        public T Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }

        /// <summary>
        /// Gets the variable value.
        /// </summary>
        /// <returns>The variable value.</returns>
        public override object GetValue()
        {
            return m_Value;
        }

        /// <summary>
        /// Sets the variable value.
        /// </summary>
        /// <param name="value">The variable value.</param>
        public override void SetValue(object value)
        {
            m_Value = (T)value;
        }

        /// <summary>
        /// Clears the variable value.
        /// </summary>
        public override void Clear()
        {
            m_Value = default(T);
        }

        /// <summary>
        /// Gets the string representation of the variable.
        /// </summary>
        /// <returns>The string representation of the variable.</returns>
        public override string ToString()
        {
            return (m_Value != null) ? m_Value.ToString() : "<Null>";
        }
    }
}
