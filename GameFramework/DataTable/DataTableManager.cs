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
    /// Data table manager.
    /// </summary>
    internal sealed partial class DataTableManager : GameFrameworkModule, IDataTableManager
    {
        private readonly Dictionary<TypeNamePair, DataTableBase> m_DataTables;
        private IResourceManager m_ResourceManager;
        private IDataProviderHelper<DataTableBase> m_DataProviderHelper;
        private IDataTableHelper m_DataTableHelper;

        /// <summary>
        /// Initializes a new instance of the DataTableManager class.
        /// </summary>
        public DataTableManager()
        {
            m_DataTables = new Dictionary<TypeNamePair, DataTableBase>();
            m_ResourceManager = null;
            m_DataProviderHelper = null;
            m_DataTableHelper = null;
        }

        /// <summary>
        /// Gets the count of data tables.
        /// </summary>
        public int Count
        {
            get
            {
                return m_DataTables.Count;
            }
        }

        /// <summary>
        /// Gets the cached bytes size.
        /// </summary>
        public int CachedBytesSize
        {
            get
            {
                return DataProvider<DataTableBase>.CachedBytesSize;
            }
        }

        /// <summary>
        /// Polls the data table manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// Shuts down and cleans up the data table manager.
        /// </summary>
        internal override void Shutdown()
        {
            foreach (KeyValuePair<TypeNamePair, DataTableBase> dataTable in m_DataTables)
            {
                dataTable.Value.Shutdown();
            }

            m_DataTables.Clear();
        }

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        public void SetResourceManager(IResourceManager resourceManager)
        {
            if (resourceManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            m_ResourceManager = resourceManager;
        }

        /// <summary>
        /// Sets the data table data provider helper.
        /// </summary>
        /// <param name="dataProviderHelper">The data table data provider helper.</param>
        public void SetDataProviderHelper(IDataProviderHelper<DataTableBase> dataProviderHelper)
        {
            if (dataProviderHelper == null)
            {
                throw new GameFrameworkException("Data provider helper is invalid.");
            }

            m_DataProviderHelper = dataProviderHelper;
        }

        /// <summary>
        /// Sets the data table helper.
        /// </summary>
        /// <param name="dataTableHelper">The data table helper.</param>
        public void SetDataTableHelper(IDataTableHelper dataTableHelper)
        {
            if (dataTableHelper == null)
            {
                throw new GameFrameworkException("Data table helper is invalid.");
            }

            m_DataTableHelper = dataTableHelper;
        }

        /// <summary>
        /// Ensures the cached bytes size is large enough.
        /// </summary>
        /// <param name="ensureSize">The size to ensure.</param>
        public void EnsureCachedBytesSize(int ensureSize)
        {
            DataProvider<DataTableBase>.EnsureCachedBytesSize(ensureSize);
        }

        /// <summary>
        /// Frees the cached bytes.
        /// </summary>
        public void FreeCachedBytes()
        {
            DataProvider<DataTableBase>.FreeCachedBytes();
        }

        /// <summary>
        /// Checks whether the specified data table exists.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <returns>Whether the specified data table exists.</returns>
        public bool HasDataTable<T>() where T : IDataRow
        {
            return InternalHasDataTable(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// Checks whether the specified data table exists.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <returns>Whether the specified data table exists.</returns>
        public bool HasDataTable(Type dataRowType)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalHasDataTable(new TypeNamePair(dataRowType));
        }

        /// <summary>
        /// Checks whether the specified data table exists.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="name">The data table name.</param>
        /// <returns>Whether the specified data table exists.</returns>
        public bool HasDataTable<T>(string name) where T : IDataRow
        {
            return InternalHasDataTable(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// Checks whether the specified data table exists.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <param name="name">The data table name.</param>
        /// <returns>Whether the specified data table exists.</returns>
        public bool HasDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalHasDataTable(new TypeNamePair(dataRowType, name));
        }

        /// <summary>
        /// Gets the specified data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <returns>The specified data table.</returns>
        public IDataTable<T> GetDataTable<T>() where T : IDataRow
        {
            return (IDataTable<T>)InternalGetDataTable(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// Gets the specified data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <returns>The specified data table.</returns>
        public DataTableBase GetDataTable(Type dataRowType)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalGetDataTable(new TypeNamePair(dataRowType));
        }

        /// <summary>
        /// Gets the specified data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="name">The data table name.</param>
        /// <returns>The specified data table.</returns>
        public IDataTable<T> GetDataTable<T>(string name) where T : IDataRow
        {
            return (IDataTable<T>)InternalGetDataTable(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// Gets the specified data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <param name="name">The data table name.</param>
        /// <returns>The specified data table.</returns>
        public DataTableBase GetDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalGetDataTable(new TypeNamePair(dataRowType, name));
        }

        /// <summary>
        /// Gets all data tables.
        /// </summary>
        /// <returns>All data tables.</returns>
        public DataTableBase[] GetAllDataTables()
        {
            int index = 0;
            DataTableBase[] results = new DataTableBase[m_DataTables.Count];
            foreach (KeyValuePair<TypeNamePair, DataTableBase> dataTable in m_DataTables)
            {
                results[index++] = dataTable.Value;
            }

            return results;
        }

        /// <summary>
        /// Gets all data tables.
        /// </summary>
        /// <param name="results">The list to store all data tables.</param>
        public void GetAllDataTables(List<DataTableBase> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<TypeNamePair, DataTableBase> dataTable in m_DataTables)
            {
                results.Add(dataTable.Value);
            }
        }

        /// <summary>
        /// Creates a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <returns>The created data table.</returns>
        public IDataTable<T> CreateDataTable<T>() where T : class, IDataRow, new()
        {
            return CreateDataTable<T>(string.Empty);
        }

        /// <summary>
        /// Creates a data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <returns>The created data table.</returns>
        public DataTableBase CreateDataTable(Type dataRowType)
        {
            return CreateDataTable(dataRowType, string.Empty);
        }

        /// <summary>
        /// Creates a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="name">The data table name.</param>
        /// <returns>The created data table.</returns>
        public IDataTable<T> CreateDataTable<T>(string name) where T : class, IDataRow, new()
        {
            if (m_ResourceManager == null)
            {
                throw new GameFrameworkException("You must set resource manager first.");
            }

            if (m_DataProviderHelper == null)
            {
                throw new GameFrameworkException("You must set data provider helper first.");
            }

            TypeNamePair typeNamePair = new TypeNamePair(typeof(T), name);
            if (HasDataTable<T>(name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist data table '{0}'.", typeNamePair));
            }

            DataTable<T> dataTable = new DataTable<T>(name);
            dataTable.SetResourceManager(m_ResourceManager);
            dataTable.SetDataProviderHelper(m_DataProviderHelper);
            m_DataTables.Add(typeNamePair, dataTable);
            return dataTable;
        }

        /// <summary>
        /// Creates a data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <param name="name">The data table name.</param>
        /// <returns>The created data table.</returns>
        public DataTableBase CreateDataTable(Type dataRowType, string name)
        {
            if (m_ResourceManager == null)
            {
                throw new GameFrameworkException("You must set resource manager first.");
            }

            if (m_DataProviderHelper == null)
            {
                throw new GameFrameworkException("You must set data provider helper first.");
            }

            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            TypeNamePair typeNamePair = new TypeNamePair(dataRowType, name);
            if (HasDataTable(dataRowType, name))
            {
                throw new GameFrameworkException(Utility.Text.Format("Already exist data table '{0}'.", typeNamePair));
            }

            Type dataTableType = typeof(DataTable<>).MakeGenericType(dataRowType);
            DataTableBase dataTable = (DataTableBase)Activator.CreateInstance(dataTableType, name);
            dataTable.SetResourceManager(m_ResourceManager);
            dataTable.SetDataProviderHelper(m_DataProviderHelper);
            m_DataTables.Add(typeNamePair, dataTable);
            return dataTable;
        }

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        public bool DestroyDataTable<T>() where T : IDataRow
        {
            return InternalDestroyDataTable(new TypeNamePair(typeof(T)));
        }

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        public bool DestroyDataTable(Type dataRowType)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalDestroyDataTable(new TypeNamePair(dataRowType));
        }

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="name">The data table name.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        public bool DestroyDataTable<T>(string name) where T : IDataRow
        {
            return InternalDestroyDataTable(new TypeNamePair(typeof(T), name));
        }

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <param name="dataRowType">The type of the data table row.</param>
        /// <param name="name">The data table name.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        public bool DestroyDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameFrameworkException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameFrameworkException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalDestroyDataTable(new TypeNamePair(dataRowType, name));
        }

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        /// <param name="dataTable">The data table to destroy.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        public bool DestroyDataTable<T>(IDataTable<T> dataTable) where T : IDataRow
        {
            if (dataTable == null)
            {
                throw new GameFrameworkException("Data table is invalid.");
            }

            return InternalDestroyDataTable(new TypeNamePair(typeof(T), dataTable.Name));
        }

        /// <summary>
        /// Destroys a data table.
        /// </summary>
        /// <param name="dataTable">The data table to destroy.</param>
        /// <returns>Whether the data table was destroyed successfully.</returns>
        public bool DestroyDataTable(DataTableBase dataTable)
        {
            if (dataTable == null)
            {
                throw new GameFrameworkException("Data table is invalid.");
            }

            return InternalDestroyDataTable(new TypeNamePair(dataTable.Type, dataTable.Name));
        }

        private bool InternalHasDataTable(TypeNamePair typeNamePair)
        {
            return m_DataTables.ContainsKey(typeNamePair);
        }

        private DataTableBase InternalGetDataTable(TypeNamePair typeNamePair)
        {
            DataTableBase dataTable = null;
            if (m_DataTables.TryGetValue(typeNamePair, out dataTable))
            {
                return dataTable;
            }

            return null;
        }

        private bool InternalDestroyDataTable(TypeNamePair typeNamePair)
        {
            DataTableBase dataTable = null;
            if (m_DataTables.TryGetValue(typeNamePair, out dataTable))
            {
                dataTable.Shutdown();
                return m_DataTables.Remove(typeNamePair);
            }

            return false;
        }
    }
}
