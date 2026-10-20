//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Decrypt resource callback method.
    /// </summary>
    /// <param name="bytes">The binary bytes of the resource to decrypt.</param>
    /// <param name="startIndex">The start index of the binary bytes to decrypt.</param>
    /// <param name="count">The length of the binary bytes to decrypt.</param>
    /// <param name="name">The resource name.</param>
    /// <param name="variant">The variant name.</param>
    /// <param name="extension">The extension name.</param>
    /// <param name="storageInReadOnly">Whether the resource is stored in the read-only area.</param>
    /// <param name="fileSystem">The file system name.</param>
    /// <param name="loadType">The resource load type.</param>
    /// <param name="length">The resource length.</param>
    /// <param name="hashCode">The resource hash code.</param>
    public delegate void DecryptResourceCallback(byte[] bytes, int startIndex, int count, string name, string variant, string extension, bool storageInReadOnly, string fileSystem, byte loadType, int length, int hashCode);
}
