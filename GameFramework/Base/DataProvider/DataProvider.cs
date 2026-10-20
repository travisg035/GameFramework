//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using System;

namespace GameFramework
{
    /// <summary>
    /// Data provider.
    /// </summary>
    /// <typeparam name="T">The type of the data provider owner.</typeparam>
    internal sealed class DataProvider<T> : IDataProvider<T>
    {
        private const int BlockSize = 1024 * 4;
        private static byte[] s_CachedBytes = null;

        private readonly T m_Owner;
        private readonly LoadAssetCallbacks m_LoadAssetCallbacks;
        private readonly LoadBinaryCallbacks m_LoadBinaryCallbacks;
        private IResourceManager m_ResourceManager;
        private IDataProviderHelper<T> m_DataProviderHelper;
        private EventHandler<ReadDataSuccessEventArgs> m_ReadDataSuccessEventHandler;
        private EventHandler<ReadDataFailureEventArgs> m_ReadDataFailureEventHandler;
        private EventHandler<ReadDataUpdateEventArgs> m_ReadDataUpdateEventHandler;
        private EventHandler<ReadDataDependencyAssetEventArgs> m_ReadDataDependencyAssetEventHandler;

        /// <summary>
        /// Initializes a new instance of the DataProvider class.
        /// </summary>
        /// <param name="owner">The owner of the data provider.</param>
        public DataProvider(T owner)
        {
            m_Owner = owner;
            m_LoadAssetCallbacks = new LoadAssetCallbacks(LoadAssetSuccessCallback, LoadAssetOrBinaryFailureCallback, LoadAssetUpdateCallback, LoadAssetDependencyAssetCallback);
            m_LoadBinaryCallbacks = new LoadBinaryCallbacks(LoadBinarySuccessCallback, LoadAssetOrBinaryFailureCallback);
            m_ResourceManager = null;
            m_DataProviderHelper = null;
            m_ReadDataSuccessEventHandler = null;
            m_ReadDataFailureEventHandler = null;
            m_ReadDataUpdateEventHandler = null;
            m_ReadDataDependencyAssetEventHandler = null;
        }

        /// <summary>
        /// Gets the size of the cached binary stream buffer.
        /// </summary>
        public static int CachedBytesSize
        {
            get
            {
                return s_CachedBytes != null ? s_CachedBytes.Length : 0;
            }
        }

        /// <summary>
        /// Read data success event.
        /// </summary>
        public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
        {
            add
            {
                m_ReadDataSuccessEventHandler += value;
            }
            remove
            {
                m_ReadDataSuccessEventHandler -= value;
            }
        }

        /// <summary>
        /// Read data failure event.
        /// </summary>
        public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
        {
            add
            {
                m_ReadDataFailureEventHandler += value;
            }
            remove
            {
                m_ReadDataFailureEventHandler -= value;
            }
        }

        /// <summary>
        /// Read data update event.
        /// </summary>
        public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate
        {
            add
            {
                m_ReadDataUpdateEventHandler += value;
            }
            remove
            {
                m_ReadDataUpdateEventHandler -= value;
            }
        }

        /// <summary>
        /// Read data dependency asset event.
        /// </summary>
        public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependencyAsset
        {
            add
            {
                m_ReadDataDependencyAssetEventHandler += value;
            }
            remove
            {
                m_ReadDataDependencyAssetEventHandler -= value;
            }
        }

        /// <summary>
        /// Ensures the binary stream cache has allocated sufficient memory and keeps it cached.
        /// </summary>
        /// <param name="ensureSize">The size of memory to ensure for the binary stream cache.</param>
        public static void EnsureCachedBytesSize(int ensureSize)
        {
            if (ensureSize < 0)
            {
                throw new GameFrameworkException("Ensure size is invalid.");
            }

            if (s_CachedBytes == null || s_CachedBytes.Length < ensureSize)
            {
                FreeCachedBytes();
                int size = (ensureSize - 1 + BlockSize) / BlockSize * BlockSize;
                s_CachedBytes = new byte[size];
            }
        }

        /// <summary>
        /// Releases the cached binary stream buffer.
        /// </summary>
        public static void FreeCachedBytes()
        {
            s_CachedBytes = null;
        }

        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        public void ReadData(string dataAssetName)
        {
            ReadData(dataAssetName, Constant.DefaultPriority, null);
        }

        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="priority">Priority for loading the data asset.</param>
        public void ReadData(string dataAssetName, int priority)
        {
            ReadData(dataAssetName, priority, null);
        }

        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="userData">User-defined data.</param>
        public void ReadData(string dataAssetName, object userData)
        {
            ReadData(dataAssetName, Constant.DefaultPriority, userData);
        }

        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="priority">Priority for loading the data asset.</param>
        /// <param name="userData">User-defined data.</param>
        public void ReadData(string dataAssetName, int priority, object userData)
        {
            if (m_ResourceManager == null)
            {
                throw new GameFrameworkException("You must set resource manager first.");
            }

            if (m_DataProviderHelper == null)
            {
                throw new GameFrameworkException("You must set data provider helper first.");
            }

            HasAssetResult result = m_ResourceManager.HasAsset(dataAssetName);
            switch (result)
            {
                case HasAssetResult.AssetOnDisk:
                case HasAssetResult.AssetOnFileSystem:
                    m_ResourceManager.LoadAsset(dataAssetName, priority, m_LoadAssetCallbacks, userData);
                    break;

                case HasAssetResult.BinaryOnDisk:
                    m_ResourceManager.LoadBinary(dataAssetName, m_LoadBinaryCallbacks, userData);
                    break;

                case HasAssetResult.BinaryOnFileSystem:
                    int dataLength = m_ResourceManager.GetBinaryLength(dataAssetName);
                    EnsureCachedBytesSize(dataLength);
                    if (dataLength != m_ResourceManager.LoadBinaryFromFileSystem(dataAssetName, s_CachedBytes))
                    {
                        throw new GameFrameworkException(Utility.Text.Format("Load binary '{0}' from file system with internal error.", dataAssetName));
                    }

                    try
                    {
                        if (!m_DataProviderHelper.ReadData(m_Owner, dataAssetName, s_CachedBytes, 0, dataLength, userData))
                        {
                            throw new GameFrameworkException(Utility.Text.Format("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
                        }

                        if (m_ReadDataSuccessEventHandler != null)
                        {
                            ReadDataSuccessEventArgs loadDataSuccessEventArgs = ReadDataSuccessEventArgs.Create(dataAssetName, 0f, userData);
                            m_ReadDataSuccessEventHandler(this, loadDataSuccessEventArgs);
                            ReferencePool.Release(loadDataSuccessEventArgs);
                        }
                    }
                    catch (Exception exception)
                    {
                        if (m_ReadDataFailureEventHandler != null)
                        {
                            ReadDataFailureEventArgs loadDataFailureEventArgs = ReadDataFailureEventArgs.Create(dataAssetName, exception.ToString(), userData);
                            m_ReadDataFailureEventHandler(this, loadDataFailureEventArgs);
                            ReferencePool.Release(loadDataFailureEventArgs);
                            return;
                        }

                        throw;
                    }

                    break;

                default:
                    throw new GameFrameworkException(Utility.Text.Format("Data asset '{0}' is '{1}'.", dataAssetName, result));
            }
        }

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataString">The data string to parse.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        public bool ParseData(string dataString)
        {
            return ParseData(dataString, null);
        }

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataString">The data string to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        public bool ParseData(string dataString, object userData)
        {
            if (m_DataProviderHelper == null)
            {
                throw new GameFrameworkException("You must set data helper first.");
            }

            if (dataString == null)
            {
                throw new GameFrameworkException("Data string is invalid.");
            }

            try
            {
                return m_DataProviderHelper.ParseData(m_Owner, dataString, userData);
            }
            catch (Exception exception)
            {
                if (exception is GameFrameworkException)
                {
                    throw;
                }

                throw new GameFrameworkException(Utility.Text.Format("Can not parse data string with exception '{0}'.", exception), exception);
            }
        }

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataBytes">The data bytes to parse.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        public bool ParseData(byte[] dataBytes)
        {
            if (dataBytes == null)
            {
                throw new GameFrameworkException("Data bytes is invalid.");
            }

            return ParseData(dataBytes, 0, dataBytes.Length, null);
        }

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataBytes">The data bytes to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        public bool ParseData(byte[] dataBytes, object userData)
        {
            if (dataBytes == null)
            {
                throw new GameFrameworkException("Data bytes is invalid.");
            }

            return ParseData(dataBytes, 0, dataBytes.Length, userData);
        }

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataBytes">The data bytes to parse.</param>
        /// <param name="startIndex">The start index in the data bytes.</param>
        /// <param name="length">The length of data bytes to parse.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        public bool ParseData(byte[] dataBytes, int startIndex, int length)
        {
            return ParseData(dataBytes, startIndex, length, null);
        }

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataBytes">The data bytes to parse.</param>
        /// <param name="startIndex">The start index in the data bytes.</param>
        /// <param name="length">The length of data bytes to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        public bool ParseData(byte[] dataBytes, int startIndex, int length, object userData)
        {
            if (m_DataProviderHelper == null)
            {
                throw new GameFrameworkException("You must set data helper first.");
            }

            if (dataBytes == null)
            {
                throw new GameFrameworkException("Data bytes is invalid.");
            }

            if (startIndex < 0 || length < 0 || startIndex + length > dataBytes.Length)
            {
                throw new GameFrameworkException("Start index or length is invalid.");
            }

            try
            {
                return m_DataProviderHelper.ParseData(m_Owner, dataBytes, startIndex, length, userData);
            }
            catch (Exception exception)
            {
                if (exception is GameFrameworkException)
                {
                    throw;
                }

                throw new GameFrameworkException(Utility.Text.Format("Can not parse data bytes with exception '{0}'.", exception), exception);
            }
        }

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">Resource manager.</param>
        internal void SetResourceManager(IResourceManager resourceManager)
        {
            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            m_ResourceManager = resourceManager;
        }

        /// <summary>
        /// Sets the data provider helper.
        /// </summary>
        /// <param name="dataProviderHelper">Data provider helper.</param>
        internal void SetDataProviderHelper(IDataProviderHelper<T> dataProviderHelper)
        {
            if (dataProviderHelper == null)
            {
                throw new GameFrameworkException("Data provider helper is invalid.");
            }

            m_DataProviderHelper = dataProviderHelper;
        }

        private void LoadAssetSuccessCallback(string dataAssetName, object dataAsset, float duration, object userData)
        {
            try
            {
                if (!m_DataProviderHelper.ReadData(m_Owner, dataAssetName, dataAsset, userData))
                {
                    throw new GameFrameworkException(Utility.Text.Format("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
                }

                if (m_ReadDataSuccessEventHandler != null)
                {
                    ReadDataSuccessEventArgs loadDataSuccessEventArgs = ReadDataSuccessEventArgs.Create(dataAssetName, duration, userData);
                    m_ReadDataSuccessEventHandler(this, loadDataSuccessEventArgs);
                    ReferencePool.Release(loadDataSuccessEventArgs);
                }
            }
            catch (Exception exception)
            {
                if (m_ReadDataFailureEventHandler != null)
                {
                    ReadDataFailureEventArgs loadDataFailureEventArgs = ReadDataFailureEventArgs.Create(dataAssetName, exception.ToString(), userData);
                    m_ReadDataFailureEventHandler(this, loadDataFailureEventArgs);
                    ReferencePool.Release(loadDataFailureEventArgs);
                    return;
                }

                throw;
            }
            finally
            {
                m_DataProviderHelper.ReleaseDataAsset(m_Owner, dataAsset);
            }
        }

        private void LoadAssetOrBinaryFailureCallback(string dataAssetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            string appendErrorMessage = Utility.Text.Format("Load data failure, data asset name '{0}', status '{1}', error message '{2}'.", dataAssetName, status, errorMessage);
            if (m_ReadDataFailureEventHandler != null)
            {
                ReadDataFailureEventArgs loadDataFailureEventArgs = ReadDataFailureEventArgs.Create(dataAssetName, appendErrorMessage, userData);
                m_ReadDataFailureEventHandler(this, loadDataFailureEventArgs);
                ReferencePool.Release(loadDataFailureEventArgs);
                return;
            }

            throw new GameFrameworkException(appendErrorMessage);
        }

        private void LoadAssetUpdateCallback(string dataAssetName, float progress, object userData)
        {
            if (m_ReadDataUpdateEventHandler != null)
            {
                ReadDataUpdateEventArgs loadDataUpdateEventArgs = ReadDataUpdateEventArgs.Create(dataAssetName, progress, userData);
                m_ReadDataUpdateEventHandler(this, loadDataUpdateEventArgs);
                ReferencePool.Release(loadDataUpdateEventArgs);
            }
        }

        private void LoadAssetDependencyAssetCallback(string dataAssetName, string dependencyAssetName, int loadedCount, int totalCount, object userData)
        {
            if (m_ReadDataDependencyAssetEventHandler != null)
            {
                ReadDataDependencyAssetEventArgs loadDataDependencyAssetEventArgs = ReadDataDependencyAssetEventArgs.Create(dataAssetName, dependencyAssetName, loadedCount, totalCount, userData);
                m_ReadDataDependencyAssetEventHandler(this, loadDataDependencyAssetEventArgs);
                ReferencePool.Release(loadDataDependencyAssetEventArgs);
            }
        }

        private void LoadBinarySuccessCallback(string dataAssetName, byte[] dataBytes, float duration, object userData)
        {
            try
            {
                if (!m_DataProviderHelper.ReadData(m_Owner, dataAssetName, dataBytes, 0, dataBytes.Length, userData))
                {
                    throw new GameFrameworkException(Utility.Text.Format("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
                }

                if (m_ReadDataSuccessEventHandler != null)
                {
                    ReadDataSuccessEventArgs loadDataSuccessEventArgs = ReadDataSuccessEventArgs.Create(dataAssetName, duration, userData);
                    m_ReadDataSuccessEventHandler(this, loadDataSuccessEventArgs);
                    ReferencePool.Release(loadDataSuccessEventArgs);
                }
            }
            catch (Exception exception)
            {
                if (m_ReadDataFailureEventHandler != null)
                {
                    ReadDataFailureEventArgs loadDataFailureEventArgs = ReadDataFailureEventArgs.Create(dataAssetName, exception.ToString(), userData);
                    m_ReadDataFailureEventHandler(this, loadDataFailureEventArgs);
                    ReferencePool.Release(loadDataFailureEventArgs);
                    return;
                }

                throw;
            }
        }
    }
}
