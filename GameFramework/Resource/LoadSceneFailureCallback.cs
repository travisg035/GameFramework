//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load scene failure callback method.
    /// </summary>
    /// <param name="sceneAssetName">The asset name of the scene to load.</param>
    /// <param name="status">The load resource status.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadSceneFailureCallback(string sceneAssetName, LoadResourceStatus status, string errorMessage, object userData);
}
