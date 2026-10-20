//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Resource
{
    /// <summary>
    /// Callback method invoked when checking resources completes in updatable mode.
    /// </summary>
    /// <param name="movedCount">The count of moved resources.</param>
    /// <param name="removedCount">The count of removed resources.</param>
    /// <param name="updateCount">The count of updatable resources.</param>
    /// <param name="updateTotalLength">The total length of updatable resources.</param>
    /// <param name="updateTotalCompressedLength">The total compressed length of updatable resources.</param>
    public delegate void CheckResourcesCompleteCallback(int movedCount, int removedCount, int updateCount, long updateTotalLength, long updateTotalCompressedLength);
}
