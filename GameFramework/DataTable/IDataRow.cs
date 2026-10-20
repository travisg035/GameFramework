//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.DataTable
{
    /// <summary>
    /// Data row interface.
    /// </summary>
    public interface IDataRow
    {
        /// <summary>
        /// Gets the data row id.
        /// </summary>
        int Id
        {
            get;
        }

        /// <summary>
        /// Parses a data row.
        /// </summary>
        /// <param name="dataRowString">The data row string to parse.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the data row was parsed successfully.</returns>
        bool ParseDataRow(string dataRowString, object userData);

        /// <summary>
        /// Parses a data row.
        /// </summary>
        /// <param name="dataRowBytes">The data row binary bytes to parse.</param>
        /// <param name="startIndex">The start index of the data row binary bytes.</param>
        /// <param name="length">The length of the data row binary bytes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the data row was parsed successfully.</returns>
        bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData);
    }
}
