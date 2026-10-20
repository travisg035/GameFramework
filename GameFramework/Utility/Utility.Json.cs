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
        /// JSON utility functions.
        /// </summary>
        public static partial class Json
        {
            private static IJsonHelper s_JsonHelper = null;

            /// <summary>
            /// Sets the JSON helper.
            /// </summary>
            /// <param name="jsonHelper">The JSON helper to set.</param>
            public static void SetJsonHelper(IJsonHelper jsonHelper)
            {
                s_JsonHelper = jsonHelper;
            }

            /// <summary>
            /// Serializes an object to a JSON string.
            /// </summary>
            /// <param name="obj">The object to serialize.</param>
            /// <returns>The serialized JSON string.</returns>
            public static string ToJson(object obj)
            {
                if (s_JsonHelper == null)
                {
                    throw new GameFrameworkException("JSON helper is invalid.");
                }

                try
                {
                    return s_JsonHelper.ToJson(obj);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Text.Format("Can not convert to JSON with exception '{0}'.", exception), exception);
                }
            }

            /// <summary>
            /// Deserializes a JSON string to an object.
            /// </summary>
            /// <typeparam name="T">The type of the object.</typeparam>
            /// <param name="json">The JSON string to deserialize.</param>
            /// <returns>The deserialized object.</returns>
            public static T ToObject<T>(string json)
            {
                if (s_JsonHelper == null)
                {
                    throw new GameFrameworkException("JSON helper is invalid.");
                }

                try
                {
                    return s_JsonHelper.ToObject<T>(json);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Text.Format("Can not convert to object with exception '{0}'.", exception), exception);
                }
            }

            /// <summary>
            /// Deserializes a JSON string to an object.
            /// </summary>
            /// <param name="objectType">The type of the object.</param>
            /// <param name="json">The JSON string to deserialize.</param>
            /// <returns>The deserialized object.</returns>
            public static object ToObject(Type objectType, string json)
            {
                if (s_JsonHelper == null)
                {
                    throw new GameFrameworkException("JSON helper is invalid.");
                }

                if (objectType == null)
                {
                    throw new GameFrameworkException("Object type is invalid.");
                }

                try
                {
                    return s_JsonHelper.ToObject(objectType, json);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Text.Format("Can not convert to object with exception '{0}'.", exception), exception);
                }
            }
        }
    }
}
