//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Update version list success callback.
    /// </summary>
    /// <param name="downloadPath">The download path where the updated version list is stored.</param>
    /// <param name="downloadUri">The URI for updating the version list.</param>
    public delegate void UpdateVersionListSuccessCallback(string downloadPath, string downloadUri);
}
