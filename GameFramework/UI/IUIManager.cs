//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.ObjectPool;
using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.UI
{
    /// <summary>
    /// UI manager interface.
    /// </summary>
    public interface IUIManager
    {
        /// <summary>
        /// Gets the count of UI groups.
        /// </summary>
        int UIGroupCount
        {
            get;
        }

        /// <summary>
        /// Gets or sets the interval in seconds for automatically releasing releasable objects in the UI form instance object pool.
        /// </summary>
        float InstanceAutoReleaseInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the capacity of the UI form instance object pool.
        /// </summary>
        int InstanceCapacity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the expire time in seconds for objects in the UI form instance object pool.
        /// </summary>
        float InstanceExpireTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the priority of the UI form instance object pool.
        /// </summary>
        int InstancePriority
        {
            get;
            set;
        }

        /// <summary>
        /// Open UI form success event.
        /// </summary>
        event EventHandler<OpenUIFormSuccessEventArgs> OpenUIFormSuccess;

        /// <summary>
        /// Open UI form failure event.
        /// </summary>
        event EventHandler<OpenUIFormFailureEventArgs> OpenUIFormFailure;

        /// <summary>
        /// Open UI form update event.
        /// </summary>
        event EventHandler<OpenUIFormUpdateEventArgs> OpenUIFormUpdate;

        /// <summary>
        /// Open UI form dependency asset event.
        /// </summary>
        event EventHandler<OpenUIFormDependencyAssetEventArgs> OpenUIFormDependencyAsset;

        /// <summary>
        /// Close UI form complete event.
        /// </summary>
        event EventHandler<CloseUIFormCompleteEventArgs> CloseUIFormComplete;

        /// <summary>
        /// Sets the object pool manager.
        /// </summary>
        /// <param name="objectPoolManager">The object pool manager.</param>
        void SetObjectPoolManager(IObjectPoolManager objectPoolManager);

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        void SetResourceManager(IResourceManager resourceManager);

        /// <summary>
        /// Sets the UI form helper.
        /// </summary>
        /// <param name="uiFormHelper">The UI form helper.</param>
        void SetUIFormHelper(IUIFormHelper uiFormHelper);

        /// <summary>
        /// Checks whether the UI group exists.
        /// </summary>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <returns>Whether the UI group exists.</returns>
        bool HasUIGroup(string uiGroupName);

        /// <summary>
        /// Gets a UI group.
        /// </summary>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <returns>The UI group to get.</returns>
        IUIGroup GetUIGroup(string uiGroupName);

        /// <summary>
        /// Gets all UI groups.
        /// </summary>
        /// <returns>All UI groups.</returns>
        IUIGroup[] GetAllUIGroups();

        /// <summary>
        /// Gets all UI groups.
        /// </summary>
        /// <param name="results">All UI groups.</param>
        void GetAllUIGroups(List<IUIGroup> results);

        /// <summary>
        /// Adds a UI group.
        /// </summary>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="uiGroupHelper">The UI group helper.</param>
        /// <returns>Whether the UI group was added successfully.</returns>
        bool AddUIGroup(string uiGroupName, IUIGroupHelper uiGroupHelper);

        /// <summary>
        /// Adds a UI group.
        /// </summary>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="uiGroupDepth">The UI group depth.</param>
        /// <param name="uiGroupHelper">The UI group helper.</param>
        /// <returns>Whether the UI group was added successfully.</returns>
        bool AddUIGroup(string uiGroupName, int uiGroupDepth, IUIGroupHelper uiGroupHelper);

        /// <summary>
        /// Checks whether the UI form exists.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <returns>Whether the UI form exists.</returns>
        bool HasUIForm(int serialId);

        /// <summary>
        /// Checks whether the UI form exists.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <returns>Whether the UI form exists.</returns>
        bool HasUIForm(string uiFormAssetName);

        /// <summary>
        /// Gets a UI form.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <returns>The UI form to get.</returns>
        IUIForm GetUIForm(int serialId);

        /// <summary>
        /// Gets a UI form.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <returns>The UI form to get.</returns>
        IUIForm GetUIForm(string uiFormAssetName);

        /// <summary>
        /// Gets UI forms.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <returns>The UI forms to get.</returns>
        IUIForm[] GetUIForms(string uiFormAssetName);

        /// <summary>
        /// Gets UI forms.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="results">The UI forms to get.</param>
        void GetUIForms(string uiFormAssetName, List<IUIForm> results);

        /// <summary>
        /// Gets all loaded UI forms.
        /// </summary>
        /// <returns>All loaded UI forms.</returns>
        IUIForm[] GetAllLoadedUIForms();

        /// <summary>
        /// Gets all loaded UI forms.
        /// </summary>
        /// <param name="results">All loaded UI forms.</param>
        void GetAllLoadedUIForms(List<IUIForm> results);

        /// <summary>
        /// Gets the serial IDs of all loading UI forms.
        /// </summary>
        /// <returns>The serial IDs of all loading UI forms.</returns>
        int[] GetAllLoadingUIFormSerialIds();

        /// <summary>
        /// Gets the serial IDs of all loading UI forms.
        /// </summary>
        /// <param name="results">The serial IDs of all loading UI forms.</param>
        void GetAllLoadingUIFormSerialIds(List<int> results);

        /// <summary>
        /// Checks whether the UI form is loading.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form.</param>
        /// <returns>Whether the UI form is loading.</returns>
        bool IsLoadingUIForm(int serialId);

        /// <summary>
        /// Checks whether the UI form is loading.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <returns>Whether the UI form is loading.</returns>
        bool IsLoadingUIForm(string uiFormAssetName);

        /// <summary>
        /// Checks whether the UI form is valid.
        /// </summary>
        /// <param name="uiForm">The UI form.</param>
        /// <returns>Whether the UI form is valid.</returns>
        bool IsValidUIForm(IUIForm uiForm);

        /// <summary>
        /// Opens a UI form.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <returns>The serial ID of the UI form.</returns>
        int OpenUIForm(string uiFormAssetName, string uiGroupName);

        /// <summary>
        /// Opens a UI form.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="priority">The priority of loading the UI form asset.</param>
        /// <returns>The serial ID of the UI form.</returns>
        int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority);

        /// <summary>
        /// Opens a UI form.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="pauseCoveredUIForm">Whether to pause the covered UI form.</param>
        /// <returns>The serial ID of the UI form.</returns>
        int OpenUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm);

        /// <summary>
        /// Opens a UI form.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The serial ID of the UI form.</returns>
        int OpenUIForm(string uiFormAssetName, string uiGroupName, object userData);

        /// <summary>
        /// Opens a UI form.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="priority">The priority of loading the UI form asset.</param>
        /// <param name="pauseCoveredUIForm">Whether to pause the covered UI form.</param>
        /// <returns>The serial ID of the UI form.</returns>
        int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm);

        /// <summary>
        /// Opens a UI form.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="priority">The priority of loading the UI form asset.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The serial ID of the UI form.</returns>
        int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority, object userData);

        /// <summary>
        /// Opens a UI form.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="pauseCoveredUIForm">Whether to pause the covered UI form.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The serial ID of the UI form.</returns>
        int OpenUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, object userData);

        /// <summary>
        /// Opens a UI form.
        /// </summary>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="priority">The priority of loading the UI form asset.</param>
        /// <param name="pauseCoveredUIForm">Whether to pause the covered UI form.</param>
        /// <param name="userData">The user-defined data.</param>
        /// <returns>The serial ID of the UI form.</returns>
        int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm, object userData);

        /// <summary>
        /// Closes a UI form.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form to close.</param>
        void CloseUIForm(int serialId);

        /// <summary>
        /// Closes a UI form.
        /// </summary>
        /// <param name="serialId">The serial ID of the UI form to close.</param>
        /// <param name="userData">The user-defined data.</param>
        void CloseUIForm(int serialId, object userData);

        /// <summary>
        /// Closes a UI form.
        /// </summary>
        /// <param name="uiForm">The UI form to close.</param>
        void CloseUIForm(IUIForm uiForm);

        /// <summary>
        /// Closes a UI form.
        /// </summary>
        /// <param name="uiForm">The UI form to close.</param>
        /// <param name="userData">The user-defined data.</param>
        void CloseUIForm(IUIForm uiForm, object userData);

        /// <summary>
        /// Closes all loaded UI forms.
        /// </summary>
        void CloseAllLoadedUIForms();

        /// <summary>
        /// Closes all loaded UI forms.
        /// </summary>
        /// <param name="userData">The user-defined data.</param>
        void CloseAllLoadedUIForms(object userData);

        /// <summary>
        /// Closes all loading UI forms.
        /// </summary>
        void CloseAllLoadingUIForms();

        /// <summary>
        /// Refocuses a UI form.
        /// </summary>
        /// <param name="uiForm">The UI form to refocus.</param>
        void RefocusUIForm(IUIForm uiForm);

        /// <summary>
        /// Refocuses a UI form.
        /// </summary>
        /// <param name="uiForm">The UI form to refocus.</param>
        /// <param name="userData">The user-defined data.</param>
        void RefocusUIForm(IUIForm uiForm, object userData);

        /// <summary>
        /// Sets whether the UI form instance is locked.
        /// </summary>
        /// <param name="uiFormInstance">The UI form instance to set whether it is locked.</param>
        /// <param name="locked">Whether the UI form instance is locked.</param>
        void SetUIFormInstanceLocked(object uiFormInstance, bool locked);

        /// <summary>
        /// Sets the priority of the UI form instance.
        /// </summary>
        /// <param name="uiFormInstance">The UI form instance to set priority.</param>
        /// <param name="priority">The priority of the UI form instance.</param>
        void SetUIFormInstancePriority(object uiFormInstance, int priority);
    }
}
