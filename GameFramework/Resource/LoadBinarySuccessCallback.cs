//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load binary success callback method.
    /// </summary>
    /// <param name="binaryAssetName">The name of the binary asset to load.</param>
    /// <param name="binaryBytes">The loaded binary bytes.</param>
    /// <param name="duration">The loading duration in seconds.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadBinarySuccessCallback(string binaryAssetName, byte[] binaryBytes, float duration, object userData);
}
