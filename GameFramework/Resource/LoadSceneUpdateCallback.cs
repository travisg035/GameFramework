//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load scene update callback method.
    /// </summary>
    /// <param name="sceneAssetName">The asset name of the scene to load.</param>
    /// <param name="progress">The scene loading progress.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadSceneUpdateCallback(string sceneAssetName, float progress, object userData);
}
