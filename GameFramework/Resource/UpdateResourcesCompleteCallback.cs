//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Callback method invoked when updating the specified resource group completes in updatable mode.
    /// </summary>
    /// <param name="resourceGroup">The updated resource group.</param>
    /// <param name="result">The resource update result; true if all succeeded, otherwise false.</param>
    public delegate void UpdateResourcesCompleteCallback(IResourceGroup resourceGroup, bool result);
}
