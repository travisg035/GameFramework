//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load asset dependency asset callback.
    /// </summary>
    /// <param name="assetName">The asset name to load.</param>
    /// <param name="dependencyAssetName">The dependency asset name being loaded.</param>
    /// <param name="loadedCount">The number of dependency assets currently loaded.</param>
    /// <param name="totalCount">The total number of dependency assets to load.</param>
    /// <param name="userData">The user-defined data.</param>
    public delegate void LoadAssetDependencyAssetCallback(string assetName, string dependencyAssetName, int loadedCount, int totalCount, object userData);
}
