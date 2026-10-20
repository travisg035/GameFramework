//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.DataNode
{
    /// <summary>
    /// Data node manager interface.
    /// </summary>
    public interface IDataNodeManager
    {
        /// <summary>
        /// Gets the root data node.
        /// </summary>
        IDataNode Root
        {
            get;
        }

        /// <summary>
        /// Gets the data of the data node by type.
        /// </summary>
        /// <typeparam name="T">The data type to get.</typeparam>
        /// <param name="path">The search path relative to the node.</param>
        /// <returns>The data of the specified type.</returns>
        T GetData<T>(string path) where T : Variable;

        /// <summary>
        /// Gets the data of the data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <returns>The data of the data node.</returns>
        Variable GetData(string path);

        /// <summary>
        /// Gets the data of the data node by type.
        /// </summary>
        /// <typeparam name="T">The data type to get.</typeparam>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        /// <returns>The data of the specified type.</returns>
        T GetData<T>(string path, IDataNode node) where T : Variable;

        /// <summary>
        /// Gets the data of the data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        /// <returns>The data of the data node.</returns>
        Variable GetData(string path, IDataNode node);

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <typeparam name="T">The data type to set.</typeparam>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="data">The data to set.</param>
        void SetData<T>(string path, T data) where T : Variable;

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="data">The data to set.</param>
        void SetData(string path, Variable data);

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <typeparam name="T">The data type to set.</typeparam>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="data">The data to set.</param>
        /// <param name="node">The starting node to search from.</param>
        void SetData<T>(string path, T data, IDataNode node) where T : Variable;

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="data">The data to set.</param>
        /// <param name="node">The starting node to search from.</param>
        void SetData(string path, Variable data, IDataNode node);

        /// <summary>
        /// Gets a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <returns>The data node at the specified path, or null if not found.</returns>
        IDataNode GetNode(string path);

        /// <summary>
        /// Gets a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        /// <returns>The data node at the specified path, or null if not found.</returns>
        IDataNode GetNode(string path, IDataNode node);

        /// <summary>
        /// Gets or adds a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <returns>The data node at the specified path; creates the corresponding data node if not found.</returns>
        IDataNode GetOrAddNode(string path);

        /// <summary>
        /// Gets or adds a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        /// <returns>The data node at the specified path; creates the corresponding data node if not found.</returns>
        IDataNode GetOrAddNode(string path, IDataNode node);

        /// <summary>
        /// Removes a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        void RemoveNode(string path);

        /// <summary>
        /// Removes a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        void RemoveNode(string path, IDataNode node);

        /// <summary>
        /// Clears all data nodes.
        /// </summary>
        void Clear();
    }
}
