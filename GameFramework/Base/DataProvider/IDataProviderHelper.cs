//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework
{
    /// <summary>
    /// Data provider helper interface.
    /// </summary>
    /// <typeparam name="T">The type of the data provider owner.</typeparam>
    public interface IDataProviderHelper<T>
    {
        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataProviderOwner">The owner of the data provider.</param>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="dataAsset">Data asset.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was read successfully.</returns>
        bool ReadData(T dataProviderOwner, string dataAssetName, object dataAsset, object userData);

        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataProviderOwner">The owner of the data provider.</param>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="dataBytes">Data bytes.</param>
        /// <param name="startIndex">The start index in the data bytes.</param>
        /// <param name="length">The length of data bytes to read.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was read successfully.</returns>
        bool ReadData(T dataProviderOwner, string dataAssetName, byte[] dataBytes, int startIndex, int length, object userData);

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataProviderOwner">The owner of the data provider.</param>
        /// <param name="dataString">The data string to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        bool ParseData(T dataProviderOwner, string dataString, object userData);

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataProviderOwner">The owner of the data provider.</param>
        /// <param name="dataBytes">The data bytes to parse.</param>
        /// <param name="startIndex">The start index in the data bytes.</param>
        /// <param name="length">The length of data bytes to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        bool ParseData(T dataProviderOwner, byte[] dataBytes, int startIndex, int length, object userData);

        /// <summary>
        /// Releases the data asset.
        /// </summary>
        /// <param name="dataProviderOwner">The owner of the data provider.</param>
        /// <param name="dataAsset">The data asset to release.</param>
        void ReleaseDataAsset(T dataProviderOwner, object dataAsset);
    }
}
