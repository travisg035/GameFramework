//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.DataNode
{
    /// <summary>
    /// Data node interface.
    /// </summary>
    public interface IDataNode
    {
        /// <summary>
        /// Gets the name of the data node.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the full name of the data node.
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// Gets the parent data node.
        /// </summary>
        IDataNode Parent
        {
            get;
        }

        /// <summary>
        /// Gets the count of child data nodes.
        /// </summary>
        int ChildCount
        {
            get;
        }

        /// <summary>
        /// Gets the data of the data node by type.
        /// </summary>
        /// <typeparam name="T">The data type to get.</typeparam>
        /// <returns>The data of the specified type.</returns>
        T GetData<T>() where T : Variable;

        /// <summary>
        /// Gets the data of the data node.
        /// </summary>
        /// <returns>The data of the data node.</returns>
        Variable GetData();

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <typeparam name="T">The data type to set.</typeparam>
        /// <param name="data">The data to set.</param>
        void SetData<T>(T data) where T : Variable;

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <param name="data">The data to set.</param>
        void SetData(Variable data);

        /// <summary>
        /// Checks whether the child data node exists by index.
        /// </summary>
        /// <param name="index">The child data node index.</param>
        /// <returns>Whether the child data node exists.</returns>
        bool HasChild(int index);

        /// <summary>
        /// Checks whether the child data node exists by name.
        /// </summary>
        /// <param name="name">The child data node name.</param>
        /// <returns>Whether the child data node exists.</returns>
        bool HasChild(string name);

        /// <summary>
        /// Gets the child data node by index.
        /// </summary>
        /// <param name="index">The child data node index.</param>
        /// <returns>The child data node at the specified index, or null if the index is out of range.</returns>
        IDataNode GetChild(int index);

        /// <summary>
        /// Gets the child data node by name.
        /// </summary>
        /// <param name="name">The child data node name.</param>
        /// <returns>The child data node with the specified name, or null if not found.</returns>
        IDataNode GetChild(string name);

        /// <summary>
        /// Gets or adds a child data node by name.
        /// </summary>
        /// <param name="name">The child data node name.</param>
        /// <returns>The child data node with the specified name; returns the existing child data node if it already exists, otherwise adds a new child data node.</returns>
        IDataNode GetOrAddChild(string name);

        /// <summary>
        /// Gets all child data nodes.
        /// </summary>
        /// <returns>All child data nodes.</returns>
        IDataNode[] GetAllChild();

        /// <summary>
        /// Gets all child data nodes.
        /// </summary>
        /// <param name="results">The list to store all child data nodes.</param>
        void GetAllChild(List<IDataNode> results);

        /// <summary>
        /// Removes the child data node by index.
        /// </summary>
        /// <param name="index">The child data node index.</param>
        void RemoveChild(int index);

        /// <summary>
        /// Removes the child data node by name.
        /// </summary>
        /// <param name="name">The child data node name.</param>
        void RemoveChild(string name);

        /// <summary>
        /// Clears the data and all child data nodes of the current data node.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the data node string.
        /// </summary>
        /// <returns>The data node string.</returns>
        string ToString();

        /// <summary>
        /// Gets the data string.
        /// </summary>
        /// <returns>The data string.</returns>
        string ToDataString();
    }
}
