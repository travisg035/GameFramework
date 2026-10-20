//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework.DataNode
{
    /// <summary>
    /// Data node manager.
    /// </summary>
    internal sealed partial class DataNodeManager : GameFrameworkModule, IDataNodeManager
    {
        private static readonly string[] EmptyStringArray = new string[] { };
        private static readonly string[] PathSplitSeparator = new string[] { ".", "/", "\\" };

        private const string RootName = "<Root>";
        private DataNode m_Root;

        /// <summary>
        /// Initializes a new instance of the DataNodeManager class.
        /// </summary>
        public DataNodeManager()
        {
            m_Root = DataNode.Create(RootName, null);
        }

        /// <summary>
        /// Gets the root data node.
        /// </summary>
        public IDataNode Root
        {
            get
            {
                return m_Root;
            }
        }

        /// <summary>
        /// Polls the data node manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// Shuts down and cleans up the data node manager.
        /// </summary>
        internal override void Shutdown()
        {
            ReferencePool.Release(m_Root);
            m_Root = null;
        }

        /// <summary>
        /// Gets the data of the data node by type.
        /// </summary>
        /// <typeparam name="T">The data type to get.</typeparam>
        /// <param name="path">The search path relative to the node.</param>
        /// <returns>The data of the specified type.</returns>
        public T GetData<T>(string path) where T : Variable
        {
            return GetData<T>(path, null);
        }

        /// <summary>
        /// Gets the data of the data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <returns>The data of the data node.</returns>
        public Variable GetData(string path)
        {
            return GetData(path, null);
        }

        /// <summary>
        /// Gets the data of the data node by type.
        /// </summary>
        /// <typeparam name="T">The data type to get.</typeparam>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        /// <returns>The data of the specified type.</returns>
        public T GetData<T>(string path, IDataNode node) where T : Variable
        {
            IDataNode current = GetNode(path, node);
            if (current == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Data node is not exist, path '{0}', node '{1}'.", path, node != null ? node.FullName : string.Empty));
            }

            return current.GetData<T>();
        }

        /// <summary>
        /// Gets the data of the data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        /// <returns>The data of the data node.</returns>
        public Variable GetData(string path, IDataNode node)
        {
            IDataNode current = GetNode(path, node);
            if (current == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Data node is not exist, path '{0}', node '{1}'.", path, node != null ? node.FullName : string.Empty));
            }

            return current.GetData();
        }

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <typeparam name="T">The data type to set.</typeparam>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="data">The data to set.</param>
        public void SetData<T>(string path, T data) where T : Variable
        {
            SetData(path, data, null);
        }

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="data">The data to set.</param>
        public void SetData(string path, Variable data)
        {
            SetData(path, data, null);
        }

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <typeparam name="T">The data type to set.</typeparam>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="data">The data to set.</param>
        /// <param name="node">The starting node to search from.</param>
        public void SetData<T>(string path, T data, IDataNode node) where T : Variable
        {
            IDataNode current = GetOrAddNode(path, node);
            current.SetData(data);
        }

        /// <summary>
        /// Sets the data of the data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="data">The data to set.</param>
        /// <param name="node">The starting node to search from.</param>
        public void SetData(string path, Variable data, IDataNode node)
        {
            IDataNode current = GetOrAddNode(path, node);
            current.SetData(data);
        }

        /// <summary>
        /// Gets a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <returns>The data node at the specified path, or null if not found.</returns>
        public IDataNode GetNode(string path)
        {
            return GetNode(path, null);
        }

        /// <summary>
        /// Gets a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        /// <returns>The data node at the specified path, or null if not found.</returns>
        public IDataNode GetNode(string path, IDataNode node)
        {
            IDataNode current = node ?? m_Root;
            string[] splitedPath = GetSplitedPath(path);
            foreach (string i in splitedPath)
            {
                current = current.GetChild(i);
                if (current == null)
                {
                    return null;
                }
            }

            return current;
        }

        /// <summary>
        /// Gets or adds a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <returns>The data node at the specified path; creates the corresponding data node if not found.</returns>
        public IDataNode GetOrAddNode(string path)
        {
            return GetOrAddNode(path, null);
        }

        /// <summary>
        /// Gets or adds a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        /// <returns>The data node at the specified path; creates the corresponding data node if not found.</returns>
        public IDataNode GetOrAddNode(string path, IDataNode node)
        {
            IDataNode current = node ?? m_Root;
            string[] splitedPath = GetSplitedPath(path);
            foreach (string i in splitedPath)
            {
                current = current.GetOrAddChild(i);
            }

            return current;
        }

        /// <summary>
        /// Removes a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        public void RemoveNode(string path)
        {
            RemoveNode(path, null);
        }

        /// <summary>
        /// Removes a data node.
        /// </summary>
        /// <param name="path">The search path relative to the node.</param>
        /// <param name="node">The starting node to search from.</param>
        public void RemoveNode(string path, IDataNode node)
        {
            IDataNode current = node ?? m_Root;
            IDataNode parent = current.Parent;
            string[] splitedPath = GetSplitedPath(path);
            foreach (string i in splitedPath)
            {
                parent = current;
                current = current.GetChild(i);
                if (current == null)
                {
                    return;
                }
            }

            if (parent != null)
            {
                parent.RemoveChild(current.Name);
            }
        }

        /// <summary>
        /// Clears all data nodes.
        /// </summary>
        public void Clear()
        {
            m_Root.Clear();
        }

        /// <summary>
        /// Splits the data node path.
        /// </summary>
        /// <param name="path">The data node path to split.</param>
        /// <returns>The splited string array.</returns>
        private static string[] GetSplitedPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return EmptyStringArray;
            }

            return path.Split(PathSplitSeparator, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
