//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.UI
{
    /// <summary>
    /// UI form helper interface.
    /// </summary>
    public interface IUIFormHelper
    {
        /// <summary>
        /// Instantiates a UI form.
        /// </summary>
        /// <param name="uiFormAsset">The UI form asset to instantiate.</param>
        /// <returns>The instantiated UI form.</returns>
        object InstantiateUIForm(object uiFormAsset);

        /// <summary>
        /// Creates a UI form.
        /// </summary>
        /// <param name="uiFormInstance">The UI form instance.</param>
        /// <param name="uiGroup">The UI group that the UI form belongs to.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The UI form.</returns>
        IUIForm CreateUIForm(object uiFormInstance, IUIGroup uiGroup, object userData);

        /// <summary>
        /// Releases the UI form.
        /// </summary>
        /// <param name="uiFormAsset">The UI form asset to release.</param>
        /// <param name="uiFormInstance">The UI form instance to release.</param>
        void ReleaseUIForm(object uiFormAsset, object uiFormInstance);
    }
}
