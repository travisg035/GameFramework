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
    /// Resource group collection interface.
    /// </summary>
    public interface IResourceGroupCollection
    {
        /// <summary>
        /// Gets whether the resource group collection is ready.
        /// </summary>
        bool Ready
        {
            get;
        }

        /// <summary>
        /// Gets the total number of resources in the resource group collection.
        /// </summary>
        int TotalCount
        {
            get;
        }

        /// <summary>
        /// Gets the number of ready resources in the resource group collection.
        /// </summary>
        int ReadyCount
        {
            get;
        }

        /// <summary>
        /// Gets the total length of resources in the resource group collection.
        /// </summary>
        long TotalLength
        {
            get;
        }

        /// <summary>
        /// Gets the total compressed length of resources in the resource group collection.
        /// </summary>
        long TotalCompressedLength
        {
            get;
        }

        /// <summary>
        /// Gets the total length of ready resources in the resource group collection.
        /// </summary>
        long ReadyLength
        {
            get;
        }

        /// <summary>
        /// Gets the total compressed length of ready resources in the resource group collection.
        /// </summary>
        long ReadyCompressedLength
        {
            get;
        }

        /// <summary>
        /// Gets the progress of the resource group collection.
        /// </summary>
        float Progress
        {
            get;
        }

        /// <summary>
        /// Gets the resource groups in the resource group collection.
        /// </summary>
        /// <returns>The resource groups in the resource group collection.</returns>
        IResourceGroup[] GetResourceGroups();

        /// <summary>
        /// Gets the resource names in the resource group collection.
        /// </summary>
        /// <returns>The resource names in the resource group collection.</returns>
        string[] GetResourceNames();

        /// <summary>
        /// Gets the resource names in the resource group collection.
        /// </summary>
        /// <param name="results">The resource names in the resource group collection.</param>
        void GetResourceNames(List<string> results);
    }
}
