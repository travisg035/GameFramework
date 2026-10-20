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
    /// Data provider interface.
    /// </summary>
    /// <typeparam name="T">The type of the data provider owner.</typeparam>
    public interface IDataProvider<T>
    {
        /// <summary>
        /// Read data success event.
        /// </summary>
        event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess;

        /// <summary>
        /// Read data failure event.
        /// </summary>
        event EventHandler<ReadDataFailureEventArgs> ReadDataFailure;

        /// <summary>
        /// Read data update event.
        /// </summary>
        event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate;

        /// <summary>
        /// Read data dependency asset event.
        /// </summary>
        event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependencyAsset;

        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        void ReadData(string dataAssetName);

        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="priority">Priority for loading the data asset.</param>
        void ReadData(string dataAssetName, int priority);

        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="userData">User-defined data.</param>
        void ReadData(string dataAssetName, object userData);

        /// <summary>
        /// Reads data.
        /// </summary>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="priority">Priority for loading the data asset.</param>
        /// <param name="userData">User-defined data.</param>
        void ReadData(string dataAssetName, int priority, object userData);

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataString">The data string to parse.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        bool ParseData(string dataString);

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataString">The data string to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        bool ParseData(string dataString, object userData);

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataBytes">The data bytes to parse.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        bool ParseData(byte[] dataBytes);

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataBytes">The data bytes to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        bool ParseData(byte[] dataBytes, object userData);

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataBytes">The data bytes to parse.</param>
        /// <param name="startIndex">The start index in the data bytes.</param>
        /// <param name="length">The length of data bytes to parse.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        bool ParseData(byte[] dataBytes, int startIndex, int length);

        /// <summary>
        /// Parses data.
        /// </summary>
        /// <param name="dataBytes">The data bytes to parse.</param>
        /// <param name="startIndex">The start index in the data bytes.</param>
        /// <param name="length">The length of data bytes to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the data was parsed successfully.</returns>
        bool ParseData(byte[] dataBytes, int startIndex, int length, object userData);
    }
}
