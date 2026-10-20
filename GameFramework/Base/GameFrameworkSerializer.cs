//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace GameFramework
{
    /// <summary>
    /// Base class for game framework serializer.
    /// </summary>
    /// <typeparam name="T">The type of data to serialize.</typeparam>
    public abstract class GameFrameworkSerializer<T>
    {
        private readonly Dictionary<byte, SerializeCallback> m_SerializeCallbacks;
        private readonly Dictionary<byte, DeserializeCallback> m_DeserializeCallbacks;
        private readonly Dictionary<byte, TryGetValueCallback> m_TryGetValueCallbacks;
        private byte m_LatestSerializeCallbackVersion;

        /// <summary>
        /// Initializes a new instance of the GameFrameworkSerializer class.
        /// </summary>
        public GameFrameworkSerializer()
        {
            m_SerializeCallbacks = new Dictionary<byte, SerializeCallback>();
            m_DeserializeCallbacks = new Dictionary<byte, DeserializeCallback>();
            m_TryGetValueCallbacks = new Dictionary<byte, TryGetValueCallback>();
            m_LatestSerializeCallbackVersion = 0;
        }

        /// <summary>
        /// Serialize callback.
        /// </summary>
        /// <param name="stream">Target stream.</param>
        /// <param name="data">Data to serialize.</param>
        /// <returns>Whether the data was serialized successfully.</returns>
        public delegate bool SerializeCallback(Stream stream, T data);

        /// <summary>
        /// Deserialize callback.
        /// </summary>
        /// <param name="stream">Source stream.</param>
        /// <returns>Deserialized data.</returns>
        public delegate T DeserializeCallback(Stream stream);

        /// <summary>
        /// Callback for attempting to get the value of a specified key from the specified stream.
        /// </summary>
        /// <param name="stream">Source stream.</param>
        /// <param name="key">Specified key.</param>
        /// <param name="value">Value of the specified key.</param>
        /// <returns>Whether the value was successfully retrieved.</returns>
        public delegate bool TryGetValueCallback(Stream stream, string key, out object value);

        /// <summary>
        /// Registers a serialize callback.
        /// </summary>
        /// <param name="version">Version of the serialize callback.</param>
        /// <param name="callback">Serialize callback.</param>
        public void RegisterSerializeCallback(byte version, SerializeCallback callback)
        {
            if (callback == null)
            {
                throw new GameFrameworkException("Serialize callback is invalid.");
            }

            m_SerializeCallbacks[version] = callback;
            if (version > m_LatestSerializeCallbackVersion)
            {
                m_LatestSerializeCallbackVersion = version;
            }
        }

        /// <summary>
        /// Registers a deserialize callback.
        /// </summary>
        /// <param name="version">Version of the deserialize callback.</param>
        /// <param name="callback">Deserialize callback.</param>
        public void RegisterDeserializeCallback(byte version, DeserializeCallback callback)
        {
            if (callback == null)
            {
                throw new GameFrameworkException("Deserialize callback is invalid.");
            }

            m_DeserializeCallbacks[version] = callback;
        }

        /// <summary>
        /// Registers a try get value callback.
        /// </summary>
        /// <param name="version">Version of the try get value callback.</param>
        /// <param name="callback">Try get value callback.</param>
        public void RegisterTryGetValueCallback(byte version, TryGetValueCallback callback)
        {
            if (callback == null)
            {
                throw new GameFrameworkException("Try get value callback is invalid.");
            }

            m_TryGetValueCallbacks[version] = callback;
        }

        /// <summary>
        /// Serializes data to the target stream.
        /// </summary>
        /// <param name="stream">Target stream.</param>
        /// <param name="data">Data to serialize.</param>
        /// <returns>Whether the data was serialized successfully.</returns>
        public bool Serialize(Stream stream, T data)
        {
            if (m_SerializeCallbacks.Count <= 0)
            {
                throw new GameFrameworkException("No serialize callback registered.");
            }

            return Serialize(stream, data, m_LatestSerializeCallbackVersion);
        }

        /// <summary>
        /// Serializes data to the target stream using the specified version.
        /// </summary>
        /// <param name="stream">Target stream.</param>
        /// <param name="data">Data to serialize.</param>
        /// <param name="version">Version of the serialize callback.</param>
        /// <returns>Whether the data was serialized successfully.</returns>
        public bool Serialize(Stream stream, T data, byte version)
        {
            byte[] header = GetHeader();
            stream.WriteByte(header[0]);
            stream.WriteByte(header[1]);
            stream.WriteByte(header[2]);
            stream.WriteByte(version);
            SerializeCallback callback = null;
            if (!m_SerializeCallbacks.TryGetValue(version, out callback))
            {
                throw new GameFrameworkException(Utility.Text.Format("Serialize callback '{0}' is not exist.", version));
            }

            return callback(stream, data);
        }

        /// <summary>
        /// Deserializes data from the specified stream.
        /// </summary>
        /// <param name="stream">Source stream.</param>
        /// <returns>Deserialized data.</returns>
        public T Deserialize(Stream stream)
        {
            byte[] header = GetHeader();
            byte header0 = (byte)stream.ReadByte();
            byte header1 = (byte)stream.ReadByte();
            byte header2 = (byte)stream.ReadByte();
            if (header0 != header[0] || header1 != header[1] || header2 != header[2])
            {
                throw new GameFrameworkException(Utility.Text.Format("Header is invalid, need '{0}{1}{2}', current '{3}{4}{5}'.", (char)header[0], (char)header[1], (char)header[2], (char)header0, (char)header1, (char)header2));
            }

            byte version = (byte)stream.ReadByte();
            DeserializeCallback callback = null;
            if (!m_DeserializeCallbacks.TryGetValue(version, out callback))
            {
                throw new GameFrameworkException(Utility.Text.Format("Deserialize callback '{0}' is not exist.", version));
            }

            return callback(stream);
        }

        /// <summary>
        /// Attempts to get the value of the specified key from the stream.
        /// </summary>
        /// <param name="stream">Source stream.</param>
        /// <param name="key">Specified key.</param>
        /// <param name="value">Output value of the specified key.</param>
        /// <returns>Whether the value was successfully retrieved.</returns>
        public bool TryGetValue(Stream stream, string key, out object value)
        {
            value = null;
            byte[] header = GetHeader();
            byte header0 = (byte)stream.ReadByte();
            byte header1 = (byte)stream.ReadByte();
            byte header2 = (byte)stream.ReadByte();
            if (header0 != header[0] || header1 != header[1] || header2 != header[2])
            {
                return false;
            }

            byte version = (byte)stream.ReadByte();
            TryGetValueCallback callback = null;
            if (!m_TryGetValueCallbacks.TryGetValue(version, out callback))
            {
                return false;
            }

            return callback(stream, key, out value);
        }

        /// <summary>
        /// Gets the header identifier.
        /// </summary>
        /// <returns>Header identifier bytes.</returns>
        protected abstract byte[] GetHeader();
    }
}
