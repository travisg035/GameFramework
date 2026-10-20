//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.DataTable
{
    /// <summary>
    /// Data table manager interface.
    /// </summary>
    public interface IDataTableManager
    {
        /// <summary>
        /// Gets the count of data tables.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Gets the cached bytes size.
        /// </summary>
        int CachedBytesSize
        {
            get;
        }

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        void SetResourceManager(IResourceManager resourceManager);

        /// <summary>
        /// Sets the data table data provider helper.
        /// </summary>
        /// <param name="dataProviderHelper">The data table data provider helper.</param>
        void SetDataProviderHelper(IDataProviderHelper<DataTableBase> dataProviderHelper);

        /// <summary>
        /// Sets the data table helper.
        /// </summary>
        /// <param name="dataTableHelper">The data table helper.</param>
        void SetDataTableHelper(IDataTableHelper dataTableHelper);

        /// <summary>
        /// Ensures the cached bytes size is large enough.
        /// </summary>
        /// <param name="ensureSize">The size to ensure.</param>
        void EnsureCachedBytesSize(int ensureSize);

        /// <summary>
        /// Frees the cached bytes.
        /// </summary>
        void FreeCachedBytes();

        /// <summary>
        /// Checks whether the specified data table exists.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <returns>Whether the specified data table exists.</returns>
        bool HasDataTable<T>() where T : IDataRow;

        /// <summary>
        /// Checks whether the specified data table exists.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <returns>Whether the specified data table exists.</returns>
        bool HasDataTable(Type dataRowType);

        /// <summary>
        /// Checks whether the specified data table exists.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="name">The data table name.</param>
        /// <returns>Whether the specified data table exists.</returns>
        bool HasDataTable<T>(string name) where T : IDataRow;

        /// <summary>
        /// Checks whether the specified data table exists.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <param name="name">The data table name.</param>
        /// <returns>Whether the specified data table exists.</returns>
        bool HasDataTable(Type dataRowType, string name);

        /// <summary>
        /// Gets the specified data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <returns>The specified data table.</returns>
        IDataTable<T> GetDataTable<T>() where T : IDataRow;

        /// <summary>
        /// Gets the specified data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <returns>The specified data table.</returns>
        DataTableBase GetDataTable(Type dataRowType);

        /// <summary>
        /// Gets the specified data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="name">The data table name.</param>
        /// <returns>The specified data table.</returns>
        IDataTable<T> GetDataTable<T>(string name) where T : IDataRow;

        /// <summary>
        /// Gets the specified data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <param name="name">The data table name.</param>
        /// <returns>The specified data table.</returns>
        DataTableBase GetDataTable(Type dataRowType, string name);

        /// <summary>
        /// Gets all data tables.
        /// </summary>
        /// <returns>All data tables.</returns>
        DataTableBase[] GetAllDataTables();

        /// <summary>
        /// Gets all data tables.
        /// </summary>
        /// <param name="results">The list to store all data tables.</param>
        void GetAllDataTables(List<DataTableBase> results);

        /// <summary>
        /// Creates a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <returns>The created data table.</returns>
        IDataTable<T> CreateDataTable<T>() where T : class, IDataRow, new();

        /// <summary>
        /// Creates a data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <returns>The created data table.</returns>
        DataTableBase CreateDataTable(Type dataRowType);

        /// <summary>
        /// Creates a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="name">The data table name.</param>
        /// <returns>The created data table.</returns>
        IDataTable<T> CreateDataTable<T>(string name) where T : class, IDataRow, new();

        /// <summary>
        /// Creates a data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <param name="name">The data table name.</param>
        /// <returns>The created data table.</returns>
        DataTableBase CreateDataTable(Type dataRowType, string name);

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        bool DestroyDataTable<T>() where T : IDataRow;

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        bool DestroyDataTable(Type dataRowType);

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="name">The data table name.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        bool DestroyDataTable<T>(string name) where T : IDataRow;

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <param name="name">The data table name.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        bool DestroyDataTable(Type dataRowType, string name);

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="dataTable">The data table to destroy.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        bool DestroyDataTable<T>(IDataTable<T> dataTable) where T : IDataRow;

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <param name="dataTable">The data table to destroy.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        bool DestroyDataTable(DataTableBase dataTable);
    }
}
