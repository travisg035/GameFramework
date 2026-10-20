//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load binary failure callback method.
    /// </summary>
    /// <param name="binaryAssetName">The name of the binary asset to load.</param>
    /// <param name="status">The load binary status.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadBinaryFailureCallback(string binaryAssetName, LoadResourceStatus status, string errorMessage, object userData);
}
