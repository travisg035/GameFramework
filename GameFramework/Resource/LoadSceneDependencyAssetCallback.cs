//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load scene dependency asset callback method.
    /// </summary>
    /// <param name="sceneAssetName">The asset name of the scene to load.</param>
    /// <param name="dependencyAssetName">The asset name of the loaded dependency.</param>
    /// <param name="loadedCount">The current number of loaded dependencies.</param>
    /// <param name="totalCount">The total number of dependencies to load.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadSceneDependencyAssetCallback(string sceneAssetName, string dependencyAssetName, int loadedCount, int totalCount, object userData);
}
