//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.DataNode
{
    internal sealed partial class DataNodeManager : GameFrameworkModule, IDataNodeManager
    {
        /// <summary>
        /// Data node.
        /// </summary>
        private sealed class DataNode : IDataNode, IReference
        {
            private static readonly DataNode[] EmptyDataNodeArray = new DataNode[] { };

            private string m_Name;
            private Variable m_Data;
            private DataNode m_Parent;
            private List<DataNode> m_Childs;

            public DataNode()
            {
                m_Name = null;
                m_Data = null;
                m_Parent = null;
                m_Childs = null;
            }

            /// <summary>
            /// Creates a data node.
            /// </summary>
            /// <param name="name">The data node name.</param>
            /// <param name="parent">The parent data node.</param>
            /// <returns>The created data node.</returns>
            public static DataNode Create(string name, DataNode parent)
            {
                if (!IsValidName(name))
                {
                    throw new GameFrameworkException("Name of data node is invalid.");
                }

                DataNode node = ReferencePool.Acquire<DataNode>();
                node.m_Name = name;
                node.m_Parent = parent;
                return node;
            }

            /// <summary>
            /// Gets the name of the data node.
            /// </summary>
            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            /// <summary>
            /// Gets the full name of the data node.
            /// </summary>
            public string FullName
            {
                get
                {
                    return m_Parent == null ? m_Name : Utility.Text.Format("{0}{1}{2}", m_Parent.FullName, PathSplitSeparator[0], m_Name);
                }
            }

            /// <summary>
            /// Gets the parent data node.
            /// </summary>
            public IDataNode Parent
            {
                get
                {
                    return m_Parent;
                }
            }

            /// <summary>
            /// Gets the count of child data nodes.
            /// </summary>
            public int ChildCount
            {
                get
                {
                    return m_Childs != null ? m_Childs.Count : 0;
                }
            }

            /// <summary>
            /// Gets the data of the data node by type.
            /// </summary>
            /// <typeparam name="T">The data type to get.</typeparam>
            /// <returns>The data of the specified type.</returns>
            public T GetData<T>() where T : Variable
            {
                return (T)m_Data;
            }

            /// <summary>
            /// Gets the data of the data node.
            /// </summary>
            /// <returns>The data of the data node.</returns>
            public Variable GetData()
            {
                return m_Data;
            }

            /// <summary>
            /// Sets the data of the data node.
            /// </summary>
            /// <typeparam name="T">The data type to set.</typeparam>
            /// <param name="data">The data to set.</param>
            public void SetData<T>(T data) where T : Variable
            {
                SetData((Variable)data);
            }

            /// <summary>
            /// Sets the data of the data node.
            /// </summary>
            /// <param name="data">The data to set.</param>
            public void SetData(Variable data)
            {
                if (m_Data != null)
                {
                    ReferencePool.Release(m_Data);
                }

                m_Data = data;
            }

            /// <summary>
            /// Checks whether the child data node exists by index.
            /// </summary>
            /// <param name="index">The child data node index.</param>
            /// <returns>Whether the child data node exists.</returns>
            public bool HasChild(int index)
            {
                return index >= 0 && index < ChildCount;
            }

            /// <summary>
            /// Checks whether the child data node exists by name.
            /// </summary>
            /// <param name="name">The child data node name.</param>
            /// <returns>Whether the child data node exists.</returns>
            public bool HasChild(string name)
            {
                if (!IsValidName(name))
                {
                    throw new GameFrameworkException("Name is invalid.");
                }

                if (m_Childs == null)
                {
                    return false;
                }

                foreach (DataNode child in m_Childs)
                {
                    if (child.Name == name)
                    {
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Gets the child data node by index.
            /// </summary>
            /// <param name="index">The child data node index.</param>
            /// <returns>The child data node at the specified index, or null if the index is out of range.</returns>
            public IDataNode GetChild(int index)
            {
                return index >= 0 && index < ChildCount ? m_Childs[index] : null;
            }

            /// <summary>
            /// Gets the child data node by name.
            /// </summary>
            /// <param name="name">The child data node name.</param>
            /// <returns>The child data node with the specified name, or null if not found.</returns>
            public IDataNode GetChild(string name)
            {
                if (!IsValidName(name))
                {
                    throw new GameFrameworkException("Name is invalid.");
                }

                if (m_Childs == null)
                {
                    return null;
                }

                foreach (DataNode child in m_Childs)
                {
                    if (child.Name == name)
                    {
                        return child;
                    }
                }

                return null;
            }

            /// <summary>
            /// Gets or adds a child data node by name.
            /// </summary>
            /// <param name="name">The child data node name.</param>
            /// <returns>The child data node with the specified name; returns the existing child data node if it already exists, otherwise adds a new child data node.</returns>
            public IDataNode GetOrAddChild(string name)
            {
                DataNode node = (DataNode)GetChild(name);
                if (node != null)
                {
                    return node;
                }

                node = Create(name, this);

                if (m_Childs == null)
                {
                    m_Childs = new List<DataNode>();
                }

                m_Childs.Add(node);

                return node;
            }

            /// <summary>
            /// Gets all child data nodes.
            /// </summary>
            /// <returns>All child data nodes.</returns>
            public IDataNode[] GetAllChild()
            {
                if (m_Childs == null)
                {
                    return EmptyDataNodeArray;
                }

                return m_Childs.ToArray();
            }

            /// <summary>
            /// Gets all child data nodes.
            /// </summary>
            /// <param name="results">The list to store all child data nodes.</param>
            public void GetAllChild(List<IDataNode> results)
            {
                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                if (m_Childs == null)
                {
                    return;
                }

                foreach (DataNode child in m_Childs)
                {
                    results.Add(child);
                }
            }

            /// <summary>
            /// Removes the child data node by index.
            /// </summary>
            /// <param name="index">The child data node index.</param>
            public void RemoveChild(int index)
            {
                DataNode node = (DataNode)GetChild(index);
                if (node == null)
                {
                    return;
                }

                m_Childs.Remove(node);
                ReferencePool.Release(node);
            }

            /// <summary>
            /// Removes the child data node by name.
            /// </summary>
            /// <param name="name">The child data node name.</param>
            public void RemoveChild(string name)
            {
                DataNode node = (DataNode)GetChild(name);
                if (node == null)
                {
                    return;
                }

                m_Childs.Remove(node);
                ReferencePool.Release(node);
            }

            public void Clear()
            {
                if (m_Data != null)
                {
                    ReferencePool.Release(m_Data);
                    m_Data = null;
                }

                if (m_Childs != null)
                {
                    foreach (DataNode child in m_Childs)
                    {
                        ReferencePool.Release(child);
                    }

                    m_Childs.Clear();
                }
            }

            /// <summary>
            /// Gets the data node string.
            /// </summary>
            /// <returns>The data node string.</returns>
            public override string ToString()
            {
                return Utility.Text.Format("{0}: {1}", FullName, ToDataString());
            }

            /// <summary>
            /// Gets the data string.
            /// </summary>
            /// <returns>The data string.</returns>
            public string ToDataString()
            {
                if (m_Data == null)
                {
                    return "<Null>";
                }

                return Utility.Text.Format("[{0}] {1}", m_Data.Type.Name, m_Data);
            }

            /// <summary>
            /// Checks whether the data node name is valid.
            /// </summary>
            /// <param name="name">The data node name to check.</param>
            /// <returns>Whether the data node name is valid.</returns>
            private static bool IsValidName(string name)
            {
                if (string.IsNullOrEmpty(name))
                {
                    return false;
                }

                foreach (string pathSplitSeparator in PathSplitSeparator)
                {
                    if (name.Contains(pathSplitSeparator))
                    {
                        return false;
                    }
                }

                return true;
            }

            void IReference.Clear()
            {
                m_Name = null;
                m_Parent = null;
                Clear();
            }
        }
    }
}
