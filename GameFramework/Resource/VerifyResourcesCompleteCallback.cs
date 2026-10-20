//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Callback method invoked when verifying resources completes in updatable mode.
    /// </summary>
    /// <param name="result">The resource verification result; true if all succeeded, otherwise false.</param>
    public delegate void VerifyResourcesCompleteCallback(bool result);
}
