//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Update version list failure callback.
    /// </summary>
    /// <param name="downloadUri">The URI for updating the version list.</param>
    /// <param name="errorMessage">Error message.</param>
    public delegate void UpdateVersionListFailureCallback(string downloadUri, string errorMessage);
}
