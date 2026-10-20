//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load bytes success callback method.
    /// </summary>
    /// <param name="fileUri">The file URI.</param>
    /// <param name="bytes">The loaded byte array.</param>
    /// <param name="duration">The loading duration in seconds.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadBytesSuccessCallback(string fileUri, byte[] bytes, float duration, object userData);
}
