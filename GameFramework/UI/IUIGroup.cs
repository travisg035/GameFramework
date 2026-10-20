//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;

namespace GameFramework.UI
{
    /// <summary>
    /// UI group interface.
    /// </summary>
    public interface IUIGroup
    {
        /// <summary>
        /// Gets the UI group name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets or sets the UI group depth.
        /// </summary>
        int Depth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the UI group is paused.
        /// </summary>
        bool Pause
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the count of UI forms in the UI group.
        /// </summary>
        int UIFormCount
        {
            get;
        }

        /// <summary>
        /// Gets the current UI form.
        /// </summary>
        IUIForm CurrentUIForm
        {
            get;
        }

        /// <summary>
        /// Gets the UI group helper.
        /// </summary>
        IUIGroupHelper Helper
        {
            get;
        }

        /// <summary>
        /// Checks whether the UI form exists in the UI group.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <returns>Whether the UI form exists in the UI group.</returns>
        bool HasUIForm(int serialId);

        /// <summary>
        /// Checks whether the UI form exists in the UI group.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <returns>Whether the UI form exists in the UI group.</returns>
        bool HasUIForm(string uiFormAssetName);

        /// <summary>
        /// Gets a UI form from the UI group.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <returns>The UI form to get.</returns>
        IUIForm GetUIForm(int serialId);

        /// <summary>
        /// Gets a UI form from the UI group.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <returns>The UI form to get.</returns>
        IUIForm GetUIForm(string uiFormAssetName);

        /// <summary>
        /// Gets UI forms from the UI group.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <returns>The UI forms to get.</returns>
        IUIForm[] GetUIForms(string uiFormAssetName);

        /// <summary>
        /// Gets UI forms from the UI group.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="results">The UI forms to get.</param>
        void GetUIForms(string uiFormAssetName, List<IUIForm> results);

        /// <summary>
        /// Gets all UI forms from the UI group.
        /// </summary>
        /// <returns>All UI forms in the UI group.</returns>
        IUIForm[] GetAllUIForms();

        /// <summary>
        /// Gets all UI forms from the UI group.
        /// </summary>
        /// <param name="results">All UI forms in the UI group.</param>
        void GetAllUIForms(List<IUIForm> results);
    }
}
