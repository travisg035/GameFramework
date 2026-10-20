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
        public static partial class Json
        {
            /// <summary>
            /// JSON helper interface.
            /// </summary>
            public interface IJsonHelper
            {
                /// <summary>
                /// Serializes an object to a JSON string.
                /// </summary>
                /// <param name="obj">The object to serialize.</param>
                /// <returns>The serialized JSON string.</returns>
                string ToJson(object obj);

                /// <summary>
                /// Deserializes a JSON string to an object.
                /// </summary>
                /// <typeparam name="T">The type of the object.</typeparam>
                /// <param name="json">The JSON string to deserialize.</param>
                /// <returns>The deserialized object.</returns>
                T ToObject<T>(string json);

                /// <summary>
                /// Deserializes a JSON string to an object.
                /// </summary>
                /// <param name="objectType">The type of the object.</param>
                /// <param name="json">The JSON string to deserialize.</param>
                /// <returns>The deserialized object.</returns>
                object ToObject(Type objectType, string json);
            }
        }
    }
}
