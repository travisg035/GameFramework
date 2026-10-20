//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.UI
{
    /// <summary>
    /// UI form interface.
    /// </summary>
    public interface IUIForm
    {
        /// <summary>
        /// Gets the serial ID of the UI form.
        /// </summary>
        int SerialId
        {
            get;
        }

        /// <summary>
        /// Gets the UI form asset name.
        /// </summary>
        string UIFormAssetName
        {
            get;
        }

        /// <summary>
        /// Gets the UI form instance.
        /// </summary>
        object Handle
        {
            get;
        }

        /// <summary>
        /// Gets the UI group that the UI form belongs to.
        /// </summary>
        IUIGroup UIGroup
        {
            get;
        }

        /// <summary>
        /// Gets the depth of the UI form in the UI group.
        /// </summary>
        int DepthInUIGroup
        {
            get;
        }

        /// <summary>
        /// Gets whether to pause the covered UI form.
        /// </summary>
        bool PauseCoveredUIForm
        {
            get;
        }

        /// <summary>
        /// Initializes the UI form.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroup">The UI group that the UI form belongs to.</param>
        /// <param name="pauseCoveredUIForm">Whether to pause the covered UI form.</param>
        /// <param name="isNewInstance">Whether it is a new instance.</param>
        /// <param name="userData">The user-defined data.</param>
        void OnInit(int serialId, string uiFormAssetName, IUIGroup uiGroup, bool pauseCoveredUIForm, bool isNewInstance, object userData);

        /// <summary>
        /// Recycles the UI form.
        /// </summary>
        void OnRecycle();

        /// <summary>
        /// Opens the UI form.
        /// </summary>
        /// <param name="userData">The user-defined data.</param>
        void OnOpen(object userData);

        /// <summary>
        /// Closes the UI form.
        /// </summary>
        /// <param name="isShutdown">Whether it is triggered when shutting down the UI manager.</param>
        /// <param name="userData">The user-defined data.</param>
        void OnClose(bool isShutdown, object userData);

        /// <summary>
        /// Pauses the UI form.
        /// </summary>
        void OnPause();

        /// <summary>
        /// Resumes the UI form from pause.
        /// </summary>
        void OnResume();

        /// <summary>
        /// Covers the UI form.
        /// </summary>
        void OnCover();

        /// <summary>
        /// Reveals the UI form from cover.
        /// </summary>
        void OnReveal();

        /// <summary>
        /// Refocuses the UI form.
        /// </summary>
        /// <param name="userData">The user-defined data.</param>
        void OnRefocus(object userData);

        /// <summary>
        /// Polling the UI form.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed seconds.</param>
        void OnUpdate(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// UI form depth changed.
        /// </summary>
        /// <param name="uiGroupDepth">The depth of the UI group.</param>
        /// <param name="depthInUIGroup">The depth of the UI form in the UI group.</param>
        void OnDepthChanged(int uiGroupDepth, int depthInUIGroup);
    }
}
