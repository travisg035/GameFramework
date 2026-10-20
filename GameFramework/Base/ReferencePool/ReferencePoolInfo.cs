//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace GameFramework
{
    /// <summary>
    /// Reference pool information.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct ReferencePoolInfo
    {
        private readonly Type m_Type;
        private readonly int m_UnusedReferenceCount;
        private readonly int m_UsingReferenceCount;
        private readonly int m_AcquireReferenceCount;
        private readonly int m_ReleaseReferenceCount;
        private readonly int m_AddReferenceCount;
        private readonly int m_RemoveReferenceCount;

        /// <summary>
        /// Initializes a new instance of the ReferencePoolInfo struct.
        /// </summary>
        /// <param name="type">Reference pool type.</param>
        /// <param name="unusedReferenceCount">Number of unused references.</param>
        /// <param name="usingReferenceCount">Number of using references.</param>
        /// <param name="acquireReferenceCount">Number of acquired references.</param>
        /// <param name="releaseReferenceCount">Number of released references.</param>
        /// <param name="addReferenceCount">Number of added references.</param>
        /// <param name="removeReferenceCount">Number of removed references.</param>
        public ReferencePoolInfo(Type type, int unusedReferenceCount, int usingReferenceCount, int acquireReferenceCount, int releaseReferenceCount, int addReferenceCount, int removeReferenceCount)
        {
            m_Type = type;
            m_UnusedReferenceCount = unusedReferenceCount;
            m_UsingReferenceCount = usingReferenceCount;
            m_AcquireReferenceCount = acquireReferenceCount;
            m_ReleaseReferenceCount = releaseReferenceCount;
            m_AddReferenceCount = addReferenceCount;
            m_RemoveReferenceCount = removeReferenceCount;
        }

        /// <summary>
        /// Gets the reference pool type.
        /// </summary>
        public Type Type
        {
            get
            {
                return m_Type;
            }
        }

        /// <summary>
        /// Gets the number of unused references.
        /// </summary>
        public int UnusedReferenceCount
        {
            get
            {
                return m_UnusedReferenceCount;
            }
        }

        /// <summary>
        /// Gets the number of using references.
        /// </summary>
        public int UsingReferenceCount
        {
            get
            {
                return m_UsingReferenceCount;
            }
        }

        /// <summary>
        /// Gets the number of acquired references.
        /// </summary>
        public int AcquireReferenceCount
        {
            get
            {
                return m_AcquireReferenceCount;
            }
        }

        /// <summary>
        /// Gets the number of released references.
        /// </summary>
        public int ReleaseReferenceCount
        {
            get
            {
                return m_ReleaseReferenceCount;
            }
        }

        /// <summary>
        /// Gets the number of added references.
        /// </summary>
        public int AddReferenceCount
        {
            get
            {
                return m_AddReferenceCount;
            }
        }

        /// <summary>
        /// Gets the number of removed references.
        /// </summary>
        public int RemoveReferenceCount
        {
            get
            {
                return m_RemoveReferenceCount;
            }
        }
    }
}
