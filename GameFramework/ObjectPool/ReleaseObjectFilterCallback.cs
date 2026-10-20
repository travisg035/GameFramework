//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.ObjectPool
{
    /// <summary>
    /// Release object filter callback.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="candidateObjects">The collection of candidate objects to filter.</param>
    /// <param name="toReleaseCount">The count of objects to release.</param>
    /// <param name="expireTime">The reference expire time of objects.</param>
    /// <returns>The collection of filtered objects to release.</returns>
    public delegate List<T> ReleaseObjectFilterCallback<T>(List<T> candidateObjects, int toReleaseCount, DateTime expireTime) where T : ObjectBase;
}
