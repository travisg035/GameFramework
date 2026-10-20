//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.FileSystem;
using System;

namespace GameFramework.Resource
{
    /// <summary>
    /// Load resource agent helper interface.
    /// </summary>
    public interface ILoadResourceAgentHelper
    {
        /// <summary>
        /// Load resource agent helper update event.
        /// </summary>
        event EventHandler<LoadResourceAgentHelperUpdateEventArgs> LoadResourceAgentHelperUpdate;

        /// <summary>
        /// Load resource agent helper asynchronous read resource file complete event.
        /// </summary>
        event EventHandler<LoadResourceAgentHelperReadFileCompleteEventArgs> LoadResourceAgentHelperReadFileComplete;

        /// <summary>
        /// Load resource agent helper asynchronous read resource bytes complete event.
        /// </summary>
        event EventHandler<LoadResourceAgentHelperReadBytesCompleteEventArgs> LoadResourceAgentHelperReadBytesComplete;

        /// <summary>
        /// Load resource agent helper asynchronous parse resource bytes complete event.
        /// </summary>
        event EventHandler<LoadResourceAgentHelperParseBytesCompleteEventArgs> LoadResourceAgentHelperParseBytesComplete;

        /// <summary>
        /// Load resource agent helper asynchronous load resource complete event.
        /// </summary>
        event EventHandler<LoadResourceAgentHelperLoadCompleteEventArgs> LoadResourceAgentHelperLoadComplete;

        /// <summary>
        /// Load resource agent helper error event.
        /// </summary>
        event EventHandler<LoadResourceAgentHelperErrorEventArgs> LoadResourceAgentHelperError;

        /// <summary>
        /// Starts asynchronously reading resource file through the load resource agent helper.
        /// </summary>
        /// <param name="fullPath">The full path of the resource to load.</param>
        void ReadFile(string fullPath);

        /// <summary>
        /// Starts asynchronously reading resource file through the load resource agent helper.
        /// </summary>
        /// <param name="fileSystem">The file system where the resource is located.</param>
        /// <param name="name">The name of the resource to load.</param>
        void ReadFile(IFileSystem fileSystem, string name);

        /// <summary>
        /// Starts asynchronously reading resource bytes through the load resource agent helper.
        /// </summary>
        /// <param name="fullPath">The full path of the resource to load.</param>
        void ReadBytes(string fullPath);

        /// <summary>
        /// Starts asynchronously reading resource bytes through the load resource agent helper.
        /// </summary>
        /// <param name="fileSystem">The file system where the resource is located.</param>
        /// <param name="name">The name of the resource to load.</param>
        void ReadBytes(IFileSystem fileSystem, string name);

        /// <summary>
        /// Starts asynchronously parsing resource bytes into a loadable object through the load resource agent helper.
        /// </summary>
        /// <param name="bytes">The resource bytes to load.</param>
        void ParseBytes(byte[] bytes);

        /// <summary>
        /// Starts asynchronously loading asset through the load resource agent helper.
        /// </summary>
        /// <param name="resource">The resource object.</param>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="assetType">The type of the asset to load.</param>
        /// <param name="isScene">Whether the asset to load is a scene.</param>
        void LoadAsset(object resource, string assetName, Type assetType, bool isScene);

        /// <summary>
        /// Resets the load resource agent helper.
        /// </summary>
        void Reset();
    }
}
