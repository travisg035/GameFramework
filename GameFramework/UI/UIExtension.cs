//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace GameFramework.UI
{
    /// <summary>
    /// UI extension methods providing asynchronous operations.
    /// </summary>
    public static class UIExtension
    {
        private const int DefaultPriority = 0;

        /// <summary>
        /// Opens a UI form asynchronously.
        /// </summary>
        /// <param name="uiManager">The UI manager.</param>
        /// <param name="uiFormAssetName">The UI form asset name.</param>
        /// <param name="uiGroupName">The UI group name.</param>
        /// <param name="priority">The priority for loading the UI form asset.</param>
        /// <param name="pauseCoveredUIForm">Whether to pause the covered UI form.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the open UI form operation returning the opened UI form.</returns>
        public static Task<IUIForm> OpenUIFormAsync(this IUIManager uiManager, string uiFormAssetName, string uiGroupName, int priority = DefaultPriority, bool pauseCoveredUIForm = false, object userData = null)
        {
            if (uiManager == null)
            {
                throw new GameFrameworkException("UI manager is invalid.");
            }

            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                throw new GameFrameworkException("UI form asset name is invalid.");
            }

            if (string.IsNullOrEmpty(uiGroupName))
            {
                throw new GameFrameworkException("UI group name is invalid.");
            }

            TaskCompletionSource<IUIForm> taskCompletionSource = new TaskCompletionSource<IUIForm>(TaskCreationOptions.RunContinuationsAsynchronously);

            int serialId = 0;
            EventHandler<OpenUIFormSuccessEventArgs> successHandler = null;
            EventHandler<OpenUIFormFailureEventArgs> failureHandler = null;

            void Unsubscribe()
            {
                uiManager.OpenUIFormSuccess -= successHandler;
                uiManager.OpenUIFormFailure -= failureHandler;
            }

            successHandler = (sender, e) =>
            {
                if (e.UIForm != null && e.UIForm.SerialId == serialId)
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetResult(e.UIForm);
                }
            };

            failureHandler = (sender, e) =>
            {
                if (e.SerialId == serialId)
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetException(new GameFrameworkException(e.ErrorMessage));
                }
            };

            uiManager.OpenUIFormSuccess += successHandler;
            uiManager.OpenUIFormFailure += failureHandler;

            try
            {
                serialId = uiManager.OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, userData);
            }
            catch (Exception exception)
            {
                Unsubscribe();
                taskCompletionSource.TrySetException(exception);
            }

            return taskCompletionSource.Task;
        }
    }
}
