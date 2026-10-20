//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load asset update callback method.
    /// </summary>
    /// <param name="assetName">The name of the asset to load.</param>
    /// <param name="progress">The asset loading progress.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadAssetUpdateCallback(string assetName, float progress, object userData);
}
