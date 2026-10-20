//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using System;

namespace GameFramework.DataTable
{
    /// <summary>
    /// Data table base class.
    /// </summary>
    public abstract class DataTableBase : IDataProvider<DataTableBase>
    {
        private readonly string m_Name;
        private readonly DataProvider<DataTableBase> m_DataProvider;

        /// <summary>
        /// Initializes a new instance of the DataTableBase class.
        /// </summary>
        public DataTableBase()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataTableBase class.
        /// </summary>
        /// <param name="name">Data table name.</param>
        public DataTableBase(string name)
        {
            m_Name = name ?? string.Empty;
            m_DataProvider = new DataProvider<DataTableBase>(this);
        }

        /// <summary>
        /// Gets the data table name.
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// Gets the data table full name.
        /// </summary>
        public string FullName
        {
            get
            {
                return new TypeNamePair(Type, m_Name).ToString();
            }
        }

        /// <summary>
        /// Gets the data table row type.
        /// </summary>
        public abstract Type Type
        {
            get;
        }

        /// <summary>
        /// Gets the count of data table rows.
        /// </summary>
        public abstract int Count
        {
            get;
        }

        /// <summary>
        /// Read data table success event.
        /// </summary>
        public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
        {
            add
            {
                m_DataProvider.ReadDataSuccess += value;
            }
            remove
            {
                m_DataProvider.ReadDataSuccess -= value;
            }
        }

        /// <summary>
        /// Read data table failure event.
        /// </summary>
        public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
        {
            add
            {
                m_DataProvider.ReadDataFailure += value;
            }
            remove
            {
                m_DataProvider.ReadDataFailure -= value;
            }
        }

        /// <summary>
        /// Read data table update event.
        /// </summary>
        public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate
        {
            add
            {
                m_DataProvider.ReadDataUpdate += value;
            }
            remove
            {
                m_DataProvider.ReadDataUpdate -= value;
            }
        }

        /// <summary>
        /// Read data table dependency asset event.
        /// </summary>
        public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependencyAsset
        {
            add
            {
                m_DataProvider.ReadDataDependencyAsset += value;
            }
            remove
            {
                m_DataProvider.ReadDataDependencyAsset -= value;
            }
        }

        /// <summary>
        /// Reads data table.
        /// </summary>
        /// <param name="dataTableAssetName">Data table asset name.</param>
        public void ReadData(string dataTableAssetName)
        {
            m_DataProvider.ReadData(dataTableAssetName);
        }

        /// <summary>
        /// Reads data table.
        /// </summary>
        /// <param name="dataTableAssetName">Data table asset name.</param>
        /// <param name="priority">Priority for loading the data table asset.</param>
        public void ReadData(string dataTableAssetName, int priority)
        {
            m_DataProvider.ReadData(dataTableAssetName, priority);
        }

        /// <summary>
        /// Reads data table.
        /// </summary>
        /// <param name="dataTableAssetName">Data table asset name.</param>
        /// <param name="userData">Custom user data.</param>
        public void ReadData(string dataTableAssetName, object userData)
        {
            m_DataProvider.ReadData(dataTableAssetName, userData);
        }

        /// <summary>
        /// Reads data table.
        /// </summary>
        /// <param name="dataTableAssetName">Data table asset name.</param>
        /// <param name="priority">Priority for loading the data table asset.</param>
        /// <param name="userData">Custom user data.</param>
        public void ReadData(string dataTableAssetName, int priority, object userData)
        {
            m_DataProvider.ReadData(dataTableAssetName, priority, userData);
        }

        /// <summary>
        /// Parses data table.
        /// </summary>
        /// <param name="dataTableString">The data table string to parse.</param>
        /// <returns>Whether the data table was parsed successfully.</returns>
        public bool ParseData(string dataTableString)
        {
            return m_DataProvider.ParseData(dataTableString);
        }

        /// <summary>
        /// Parses data table.
        /// </summary>
        /// <param name="dataTableString">The data table string to parse.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the data table was parsed successfully.</returns>
        public bool ParseData(string dataTableString, object userData)
        {
            return m_DataProvider.ParseData(dataTableString, userData);
        }

        /// <summary>
        /// Parses data table.
        /// </summary>
        /// <param name="dataTableBytes">The data table binary bytes to parse.</param>
        /// <returns>Whether the data table was parsed successfully.</returns>
        public bool ParseData(byte[] dataTableBytes)
        {
            return m_DataProvider.ParseData(dataTableBytes);
        }

        /// <summary>
        /// Parses data table.
        /// </summary>
        /// <param name="dataTableBytes">The data table binary bytes to parse.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the data table was parsed successfully.</returns>
        public bool ParseData(byte[] dataTableBytes, object userData)
        {
            return m_DataProvider.ParseData(dataTableBytes, userData);
        }

        /// <summary>
        /// Parses data table.
        /// </summary>
        /// <param name="dataTableBytes">The data table binary bytes to parse.</param>
        /// <param name="startIndex">The start index of the data table binary bytes.</param>
        /// <param name="length">The length of the data table binary bytes.</param>
        /// <returns>Whether the data table was parsed successfully.</returns>
        public bool ParseData(byte[] dataTableBytes, int startIndex, int length)
        {
            return m_DataProvider.ParseData(dataTableBytes, startIndex, length);
        }

        /// <summary>
        /// Parses data table.
        /// </summary>
        /// <param name="dataTableBytes">The data table binary bytes to parse.</param>
        /// <param name="startIndex">The start index of the data table binary bytes.</param>
        /// <param name="length">The length of the data table binary bytes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the data table was parsed successfully.</returns>
        public bool ParseData(byte[] dataTableBytes, int startIndex, int length, object userData)
        {
            return m_DataProvider.ParseData(dataTableBytes, startIndex, length, userData);
        }

        /// <summary>
        /// Checks whether a data table row exists.
        /// </summary>
        /// <param name="id">The data table row id.</param>
        /// <returns>Whether the data table row exists.</returns>
        public abstract bool HasDataRow(int id);

        /// <summary>
        /// Adds a data table row.
        /// </summary>
        /// <param name="dataRowString">The data row string to parse.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the data table row was added successfully.</returns>
        public abstract bool AddDataRow(string dataRowString, object userData);

        /// <summary>
        /// Adds a data table row.
        /// </summary>
        /// <param name="dataRowBytes">The data row binary bytes to parse.</param>
        /// <param name="startIndex">The start index of the data row binary bytes.</param>
        /// <param name="length">The length of the data row binary bytes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the data table row was added successfully.</returns>
        public abstract bool AddDataRow(byte[] dataRowBytes, int startIndex, int length, object userData);

        /// <summary>
        /// Removes the specified data table row.
        /// </summary>
        /// <param name="id">The id of the data table row to remove.</param>
        /// <returns>Whether the data table row was removed successfully.</returns>
        public abstract bool RemoveDataRow(int id);

        /// <summary>
        /// Removes all data table rows.
        /// </summary>
        public abstract void RemoveAllDataRows();

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        internal void SetResourceManager(IResourceManager resourceManager)
        {
            m_DataProvider.SetResourceManager(resourceManager);
        }

        /// <summary>
        /// Sets the data provider helper.
        /// </summary>
        /// <param name="dataProviderHelper">The data provider helper.</param>
        internal void SetDataProviderHelper(IDataProviderHelper<DataTableBase> dataProviderHelper)
        {
            m_DataProvider.SetDataProviderHelper(dataProviderHelper);
        }

        /// <summary>
        /// Shuts down and cleans up the data table.
        /// </summary>
        internal abstract void Shutdown();
    }
}
