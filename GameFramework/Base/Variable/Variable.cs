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
    /// Variable base class.
    /// </summary>
    public abstract class Variable : IReference
    {
        /// <summary>
        /// Initializes a new instance of the Variable class.
        /// </summary>
        public Variable()
        {
        }

        /// <summary>
        /// Gets the variable type.
        /// </summary>
        public abstract Type Type
        {
            get;
        }

        /// <summary>
        /// Gets the variable value.
        /// </summary>
        /// <returns>The variable value.</returns>
        public abstract object GetValue();

        /// <summary>
        /// Sets the variable value.
        /// </summary>
        /// <param name="value">The variable value.</param>
        public abstract void SetValue(object value);

        /// <summary>
        /// Clears the variable value.
        /// </summary>
        public abstract void Clear();
    }
}
