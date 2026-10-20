//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Unload scene success callback method.
    /// </summary>
    /// <param name="sceneAssetName">The asset name of the scene to unload.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void UnloadSceneSuccessCallback(string sceneAssetName, object userData);
}
