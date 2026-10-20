//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load asset failure callback.
    /// </summary>
    /// <param name="assetName">The asset name to load.</param>
    /// <param name="status">Load resource status.</param>
    /// <param name="errorMessage">Error message.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadAssetFailureCallback(string assetName, LoadResourceStatus status, string errorMessage, object userData);
}
