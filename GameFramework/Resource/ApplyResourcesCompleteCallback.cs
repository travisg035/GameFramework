//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Callback method invoked when applying resources from a resource pack completes in updatable mode.
    /// </summary>
    /// <param name="resourcePackPath">The resource pack path being applied.</param>
    /// <param name="result">The resource pack application result; true if all succeeded, otherwise false.</param>
    public delegate void ApplyResourcesCompleteCallback(string resourcePackPath, bool result);
}
