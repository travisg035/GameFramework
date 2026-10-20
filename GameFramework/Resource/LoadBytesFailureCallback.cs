//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Load bytes failure callback method.
    /// </summary>
    /// <param name="fileUri">The file URI.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="userData">User-defined data.</param>
    public delegate void LoadBytesFailureCallback(string fileUri, string errorMessage, object userData);
}
