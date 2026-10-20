//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Localization
{
    /// <summary>
    /// Localization helper interface.
    /// </summary>
    public interface ILocalizationHelper
    {
        /// <summary>
        /// Gets the system language.
        /// </summary>
        Language SystemLanguage
        {
            get;
        }
    }
}
