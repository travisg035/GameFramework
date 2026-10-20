//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;

namespace GameFramework
{
    /// <summary>
    /// Data provider creator.
    /// </summary>
    public static class DataProviderCreator
    {
        /// <summary>
        /// Gets the size of the cached binary stream buffer.
        /// </summary>
        /// <typeparam name="T">The type of the data provider owner.</typeparam>
        /// <returns>The size of the cached binary stream buffer.</returns>
        public static int GetCachedBytesSize<T>()
        {
            return DataProvider<T>.CachedBytesSize;
        }

        /// <summary>
        /// Ensures the binary stream cache has allocated sufficient memory and keeps it cached.
        /// </summary>
        /// <typeparam name="T">The type of the data provider owner.</typeparam>
        /// <param name="ensureSize">The size of memory to ensure for the binary stream cache.</param>
        public static void EnsureCachedBytesSize<T>(int ensureSize)
        {
            DataProvider<T>.EnsureCachedBytesSize(ensureSize);
        }

        /// <summary>
        /// Releases the cached binary stream buffer.
        /// </summary>
        /// <typeparam name="T">The type of the data provider owner.</typeparam>
        public static void FreeCachedBytes<T>()
        {
            DataProvider<T>.FreeCachedBytes();
        }

        /// <summary>
        /// Creates a data provider.
        /// </summary>
        /// <typeparam name="T">The type of the data provider owner.</typeparam>
        /// <param name="owner">The owner of the data provider.</param>
        /// <param name="resourceManager">Resource manager.</param>
        /// <param name="dataProviderHelper">Data provider helper.</param>
        /// <returns>The created data provider.</returns>
        public static IDataProvider<T> Create<T>(T owner, IResourceManager resourceManager, IDataProviderHelper<T> dataProviderHelper)
        {
            if (owner == null)
            {
                throw new GameFrameworkException("Owner is invalid.");
            }

            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            if (dataProviderHelper == null)
            {
                throw new GameFrameworkException("Data provider helper is invalid.");
            }

            DataProvider<T> dataProvider = new DataProvider<T>(owner);
            dataProvider.SetResourceManager(resourceManager);
            dataProvider.SetDataProviderHelper(dataProviderHelper);
            return dataProvider;
        }
    }
}
