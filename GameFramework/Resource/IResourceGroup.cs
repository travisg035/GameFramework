//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.Resource
{
    /// <summary>
    /// Resource group interface.
    /// </summary>
    public interface IResourceGroup
    {
        /// <summary>
        /// Gets the resource group name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets whether the resource group is ready.
        /// </summary>
        bool Ready
        {
            get;
        }

        /// <summary>
        /// Gets the total number of resources in the resource group.
        /// </summary>
        int TotalCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of ready resources in the resource group.
        /// </summary>
        int ReadyCount
        {
            get;
        }

        /// <summary>
        /// Gets the total length of resources in the resource group.
        /// </summary>
        long TotalLength
        {
            get;
        }

        /// <summary>
        /// Gets the total compressed length of resources in the resource group.
        /// </summary>
        long TotalCompressedLength
        {
            get;
        }

        /// <summary>
        /// Gets the total length of ready resources in the resource group.
        /// </summary>
        long ReadyLength
        {
            get;
        }

        /// <summary>
        /// Gets the total compressed length of ready resources in the resource group.
        /// </summary>
        long ReadyCompressedLength
        {
            get;
        }

        /// <summary>
        /// Gets the progress of the resource group.
        /// </summary>
        float Progress
        {
            get;
        }

        /// <summary>
        /// Gets the resource names in the resource group.
        /// </summary>
        /// <returns>The resource names in the resource group.</returns>
        string[] GetResourceNames();

        /// <summary>
        /// Gets the resource names in the resource group.
        /// </summary>
        /// <param name="results">The resource names in the resource group.</param>
        void GetResourceNames(List<string> results);
    }
}
