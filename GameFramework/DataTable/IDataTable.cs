//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.DataTable
{
    /// <summary>
    /// Data table interface.
    /// </summary>
    /// <typeparam name="T">The type of the data table row.</typeparam>
    public interface IDataTable<T> : IEnumerable<T> where T : IDataRow
    {
        /// <summary>
        /// Gets the data table name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the data table full name.
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// Gets the data table row type.
        /// </summary>
        Type Type
        {
            get;
        }

        /// <summary>
        /// Gets the count of data table rows.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Gets the data table row.
        /// </summary>
        /// <param name="id">The data table row id.</param>
        /// <returns>The data table row.</returns>
        T this[int id]
        {
            get;
        }

        /// <summary>
        /// Gets the data table row with the minimum id.
        /// </summary>
        T MinIdDataRow
        {
            get;
        }

        /// <summary>
        /// Gets the data table row with the maximum id.
        /// </summary>
        T MaxIdDataRow
        {
            get;
        }

        /// <summary>
        /// Checks whether a data table row exists.
        /// </summary>
        /// <param name="id">The data table row id.</param>
        /// <returns>Whether the data table row exists.</returns>
        bool HasDataRow(int id);

        /// <summary>
        /// Checks whether a data table row exists.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns>Whether the data table row exists.</returns>
        bool HasDataRow(Predicate<T> condition);

        /// <summary>
        /// Gets the data table row.
        /// </summary>
        /// <param name="id">The data table row id.</param>
        /// <returns>The data table row.</returns>
        T GetDataRow(int id);

        /// <summary>
        /// Gets the matching data table row.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns>The matching data table row.</returns>
        /// <remarks>If multiple matching data table rows exist, only the first matching data table row is returned.</remarks>
        T GetDataRow(Predicate<T> condition);

        /// <summary>
        /// Gets the matching data table rows.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns>The matching data table rows.</returns>
        T[] GetDataRows(Predicate<T> condition);

        /// <summary>
        /// Gets the matching data table rows.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="results">The list to store the matching data table rows.</param>
        void GetDataRows(Predicate<T> condition, List<T> results);

        /// <summary>
        /// Gets sorted data table rows.
        /// </summary>
        /// <param name="comparison">The comparison condition for sorting.</param>
        /// <returns>The sorted data table rows.</returns>
        T[] GetDataRows(Comparison<T> comparison);

        /// <summary>
        /// Gets sorted data table rows.
        /// </summary>
        /// <param name="comparison">The comparison condition for sorting.</param>
        /// <param name="results">The list to store the sorted data table rows.</param>
        void GetDataRows(Comparison<T> comparison, List<T> results);

        /// <summary>
        /// Gets sorted matching data table rows.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="comparison">The comparison condition for sorting.</param>
        /// <returns>The sorted matching data table rows.</returns>
        T[] GetDataRows(Predicate<T> condition, Comparison<T> comparison);

        /// <summary>
        /// Gets sorted matching data table rows.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="comparison">The comparison condition for sorting.</param>
        /// <param name="results">The list to store the sorted matching data table rows.</param>
        void GetDataRows(Predicate<T> condition, Comparison<T> comparison, List<T> results);

        /// <summary>
        /// Gets all data table rows.
        /// </summary>
        /// <returns>All data table rows.</returns>
        T[] GetAllDataRows();

        /// <summary>
        /// Gets all data table rows.
        /// </summary>
        /// <param name="results">The list to store all data table rows.</param>
        void GetAllDataRows(List<T> results);

        /// <summary>
        /// Adds a data table row.
        /// </summary>
        /// <param name="dataRowString">The data row string to parse.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the data table row was added successfully.</returns>
        bool AddDataRow(string dataRowString, object userData);

        /// <summary>
        /// Adds a data table row.
        /// </summary>
        /// <param name="dataRowBytes">The data row binary bytes to parse.</param>
        /// <param name="startIndex">The start index of the data row binary bytes.</param>
        /// <param name="length">The length of the data row binary bytes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the data table row was added successfully.</returns>
        bool AddDataRow(byte[] dataRowBytes, int startIndex, int length, object userData);

        /// <summary>
        /// Removes the specified data table row.
        /// </summary>
        /// <param name="id">The id of the data table row to remove.</param>
        /// <returns>Whether the data table row was removed successfully.</returns>
        bool RemoveDataRow(int id);

        /// <summary>
        /// Removes all data table rows.
        /// </summary>
        void RemoveAllDataRows();
    }
}
