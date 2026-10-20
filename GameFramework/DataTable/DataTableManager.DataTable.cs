//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework.DataTable
{
    internal sealed partial class DataTableManager : GameFrameworkModule, IDataTableManager
    {
        /// <summary>
        /// Data table.
        /// </summary>
        /// <typeparam name="T">The type of the data table row.</typeparam>
        private sealed class DataTable<T> : DataTableBase, IDataTable<T> where T : class, IDataRow, new()
        {
            private readonly Dictionary<int, T> m_DataSet;
            private T m_MinIdDataRow;
            private T m_MaxIdDataRow;

            /// <summary>
            /// Initializes a new instance of the DataTable class.
            /// </summary>
            /// <param name="name">Data table name.</param>
            public DataTable(string name)
                : base(name)
            {
                m_DataSet = new Dictionary<int, T>();
                m_MinIdDataRow = null;
                m_MaxIdDataRow = null;
            }

            /// <summary>
            /// Gets the data table row type.
            /// </summary>
            public override Type Type
            {
                get
                {
                    return typeof(T);
                }
            }

            /// <summary>
            /// Gets the count of data table rows.
            /// </summary>
            public override int Count
            {
                get
                {
                    return m_DataSet.Count;
                }
            }

            /// <summary>
            /// Gets the data table row.
            /// </summary>
            /// <param name="id">The data table row id.</param>
            /// <returns>The data table row.</returns>
            public T this[int id]
            {
                get
                {
                    return GetDataRow(id);
                }
            }

            /// <summary>
            /// Gets the data table row with the minimum id.
            /// </summary>
            public T MinIdDataRow
            {
                get
                {
                    return m_MinIdDataRow;
                }
            }

            /// <summary>
            /// Gets the data table row with the maximum id.
            /// </summary>
            public T MaxIdDataRow
            {
                get
                {
                    return m_MaxIdDataRow;
                }
            }

            /// <summary>
            /// Checks whether a data table row exists.
            /// </summary>
            /// <param name="id">The data table row id.</param>
            /// <returns>Whether the data table row exists.</returns>
            public override bool HasDataRow(int id)
            {
                return m_DataSet.ContainsKey(id);
            }

            /// <summary>
            /// Checks whether a data table row exists.
            /// </summary>
            /// <param name="condition">The condition to check.</param>
            /// <returns>Whether the data table row exists.</returns>
            public bool HasDataRow(Predicate<T> condition)
            {
                if (condition == null)
                {
                    throw new GameFrameworkException("Condition is invalid.");
                }

                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Gets the data table row.
            /// </summary>
            /// <param name="id">The data table row id.</param>
            /// <returns>The data table row.</returns>
            public T GetDataRow(int id)
            {
                T dataRow = null;
                if (m_DataSet.TryGetValue(id, out dataRow))
                {
                    return dataRow;
                }

                return null;
            }

            /// <summary>
            /// Gets the matching data table row.
            /// </summary>
            /// <param name="condition">The condition to check.</param>
            /// <returns>The matching data table row.</returns>
            /// <remarks>If multiple matching data table rows exist, only the first matching data table row is returned.</remarks>
            public T GetDataRow(Predicate<T> condition)
            {
                if (condition == null)
                {
                    throw new GameFrameworkException("Condition is invalid.");
                }

                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        return dataRow.Value;
                    }
                }

                return null;
            }

            /// <summary>
            /// Gets the matching data table rows.
            /// </summary>
            /// <param name="condition">The condition to check.</param>
            /// <returns>The matching data table rows.</returns>
            public T[] GetDataRows(Predicate<T> condition)
            {
                if (condition == null)
                {
                    throw new GameFrameworkException("Condition is invalid.");
                }

                List<T> results = new List<T>();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        results.Add(dataRow.Value);
                    }
                }

                return results.ToArray();
            }

            /// <summary>
            /// Gets the matching data table rows.
            /// </summary>
            /// <param name="condition">The condition to check.</param>
            /// <param name="results">The list to store the matching data table rows.</param>
            public void GetDataRows(Predicate<T> condition, List<T> results)
            {
                if (condition == null)
                {
                    throw new GameFrameworkException("Condition is invalid.");
                }

                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        results.Add(dataRow.Value);
                    }
                }
            }

            /// <summary>
            /// Gets sorted data table rows.
            /// </summary>
            /// <param name="comparison">The comparison condition for sorting.</param>
            /// <returns>The sorted data table rows.</returns>
            public T[] GetDataRows(Comparison<T> comparison)
            {
                if (comparison == null)
                {
                    throw new GameFrameworkException("Comparison is invalid.");
                }

                List<T> results = new List<T>();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    results.Add(dataRow.Value);
                }

                results.Sort(comparison);
                return results.ToArray();
            }

            /// <summary>
            /// Gets sorted data table rows.
            /// </summary>
            /// <param name="comparison">The comparison condition for sorting.</param>
            /// <param name="results">The list to store the sorted data table rows.</param>
            public void GetDataRows(Comparison<T> comparison, List<T> results)
            {
                if (comparison == null)
                {
                    throw new GameFrameworkException("Comparison is invalid.");
                }

                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    results.Add(dataRow.Value);
                }

                results.Sort(comparison);
            }

            /// <summary>
            /// Gets sorted matching data table rows.
            /// </summary>
            /// <param name="condition">The condition to check.</param>
            /// <param name="comparison">The comparison condition for sorting.</param>
            /// <returns>The sorted matching data table rows.</returns>
            public T[] GetDataRows(Predicate<T> condition, Comparison<T> comparison)
            {
                if (condition == null)
                {
                    throw new GameFrameworkException("Condition is invalid.");
                }

                if (comparison == null)
                {
                    throw new GameFrameworkException("Comparison is invalid.");
                }

                List<T> results = new List<T>();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        results.Add(dataRow.Value);
                    }
                }

                results.Sort(comparison);
                return results.ToArray();
            }

            /// <summary>
            /// Gets sorted matching data table rows.
            /// </summary>
            /// <param name="condition">The condition to check.</param>
            /// <param name="comparison">The comparison condition for sorting.</param>
            /// <param name="results">The list to store the sorted matching data table rows.</param>
            public void GetDataRows(Predicate<T> condition, Comparison<T> comparison, List<T> results)
            {
                if (condition == null)
                {
                    throw new GameFrameworkException("Condition is invalid.");
                }

                if (comparison == null)
                {
                    throw new GameFrameworkException("Comparison is invalid.");
                }

                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        results.Add(dataRow.Value);
                    }
                }

                results.Sort(comparison);
            }

            /// <summary>
            /// Gets all data table rows.
            /// </summary>
            /// <returns>All data table rows.</returns>
            public T[] GetAllDataRows()
            {
                int index = 0;
                T[] results = new T[m_DataSet.Count];
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    results[index++] = dataRow.Value;
                }

                return results;
            }

            /// <summary>
            /// Gets all data table rows.
            /// </summary>
            /// <param name="results">The list to store all data table rows.</param>
            public void GetAllDataRows(List<T> results)
            {
                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    results.Add(dataRow.Value);
                }
            }

            /// <summary>
            /// Adds a data table row.
            /// </summary>
            /// <param name="dataRowString">The data row string to parse.</param>
            /// <param name="userData">Custom user data.</param>
            /// <returns>Whether the data table row was added successfully.</returns>
            public override bool AddDataRow(string dataRowString, object userData)
            {
                try
                {
                    T dataRow = new T();
                    if (!dataRow.ParseDataRow(dataRowString, userData))
                    {
                        return false;
                    }

                    InternalAddDataRow(dataRow);
                    return true;
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Utility.Text.Format("Can not parse data row string for data table '{0}' with exception '{1}'.", new TypeNamePair(typeof(T), Name), exception), exception);
                }
            }

            /// <summary>
            /// Adds a data table row.
            /// </summary>
            /// <param name="dataRowBytes">The data row binary bytes to parse.</param>
            /// <param name="startIndex">The start index of the data row binary bytes.</param>
            /// <param name="length">The length of the data row binary bytes.</param>
            /// <param name="userData">Custom user data.</param>
            /// <returns>Whether the data table row was added successfully.</returns>
            public override bool AddDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
            {
                try
                {
                    T dataRow = new T();
                    if (!dataRow.ParseDataRow(dataRowBytes, startIndex, length, userData))
                    {
                        return false;
                    }

                    InternalAddDataRow(dataRow);
                    return true;
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Utility.Text.Format("Can not parse data row bytes for data table '{0}' with exception '{1}'.", new TypeNamePair(typeof(T), Name), exception), exception);
                }
            }

            /// <summary>
            /// Removes the specified data table row.
            /// </summary>
            /// <param name="id">The id of the data table row to remove.</param>
            /// <returns>Whether the data table row was removed successfully.</returns>
            public override bool RemoveDataRow(int id)
            {
                if (!HasDataRow(id))
                {
                    return false;
                }

                if (!m_DataSet.Remove(id))
                {
                    return false;
                }

                if (m_MinIdDataRow != null && m_MinIdDataRow.Id == id || m_MaxIdDataRow != null && m_MaxIdDataRow.Id == id)
                {
                    m_MinIdDataRow = null;
                    m_MaxIdDataRow = null;
                    foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                    {
                        if (m_MinIdDataRow == null || m_MinIdDataRow.Id > dataRow.Key)
                        {
                            m_MinIdDataRow = dataRow.Value;
                        }

                        if (m_MaxIdDataRow == null || m_MaxIdDataRow.Id < dataRow.Key)
                        {
                            m_MaxIdDataRow = dataRow.Value;
                        }
                    }
                }

                return true;
            }

            /// <summary>
            /// Removes all data table rows.
            /// </summary>
            public override void RemoveAllDataRows()
            {
                m_DataSet.Clear();
                m_MinIdDataRow = null;
                m_MaxIdDataRow = null;
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>An enumerator that iterates through the collection.</returns>
            public IEnumerator<T> GetEnumerator()
            {
                return m_DataSet.Values.GetEnumerator();
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>An enumerator that iterates through the collection.</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return m_DataSet.Values.GetEnumerator();
            }

            /// <summary>
            /// Shuts down and cleans up the data table.
            /// </summary>
            internal override void Shutdown()
            {
                m_DataSet.Clear();
            }

            private void InternalAddDataRow(T dataRow)
            {
                if (m_DataSet.ContainsKey(dataRow.Id))
                {
                    throw new GameFrameworkException(Utility.Text.Format("Already exist '{0}' in data table '{1}'.", dataRow.Id, new TypeNamePair(typeof(T), Name)));
                }

                m_DataSet.Add(dataRow.Id, dataRow);

                if (m_MinIdDataRow == null || m_MinIdDataRow.Id > dataRow.Id)
                {
                    m_MinIdDataRow = dataRow;
                }

                if (m_MaxIdDataRow == null || m_MaxIdDataRow.Id < dataRow.Id)
                {
                    m_MaxIdDataRow = dataRow;
                }
            }
        }
    }
}
