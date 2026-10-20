//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace GameFramework
{
    /// <summary>
    /// Game framework exception class.
    /// </summary>
    [Serializable]
    public class GameFrameworkException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the GameFrameworkException class.
        /// </summary>
        public GameFrameworkException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the GameFrameworkException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GameFrameworkException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GameFrameworkException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
        public GameFrameworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GameFrameworkException class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected GameFrameworkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}