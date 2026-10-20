//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load asset success callback method.
    /// </summary>
    /// <param name="assetName">The name of the asset to load.</param>
    /// <param name="asset">The loaded asset.</param>
    /// <param name="duration">The loading duration in seconds.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadAssetSuccessCallback(string assetName, object asset, float duration, object userData);
}
