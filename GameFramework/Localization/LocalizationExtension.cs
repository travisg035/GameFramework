
//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace GameFramework.Localization
{
    /// <summary>
    /// Localization extension methods providing asynchronous operations.
    /// </summary>
    public static class LocalizationExtension
    {
        private const int DefaultPriority = 0;

        /// <summary>
        /// Reads dictionary data asynchronously.
        /// </summary>
        /// <param name="localizationManager">The localization manager.</param>
        /// <param name="dictionaryAssetName">Dictionary asset name.</param>
        /// <returns>A task representing the asynchronous read operation.</returns>
        public static Task<bool> ReadDataAsync(this ILocalizationManager localizationManager, string dictionaryAssetName)
        {
            return ReadDataAsync(localizationManager, dictionaryAssetName, DefaultPriority, null);
        }

        /// <summary>
        /// Reads dictionary data asynchronously.
        /// </summary>
        /// <param name="localizationManager">The localization manager.</param>
        /// <param name="dictionaryAssetName">Dictionary asset name.</param>
        /// <param name="priority">Priority for loading the dictionary asset.</param>
        /// <returns>A task representing the asynchronous read operation.</returns>
        public static Task<bool> ReadDataAsync(this ILocalizationManager localizationManager, string dictionaryAssetName, int priority)
        {
            return ReadDataAsync(localizationManager, dictionaryAssetName, priority, null);
        }

        /// <summary>
        /// Reads dictionary data asynchronously.
        /// </summary>
        /// <param name="localizationManager">The localization manager.</param>
        /// <param name="dictionaryAssetName">Dictionary asset name.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the asynchronous read operation.</returns>
        public static Task<bool> ReadDataAsync(this ILocalizationManager localizationManager, string dictionaryAssetName, object userData)
        {
            return ReadDataAsync(localizationManager, dictionaryAssetName, DefaultPriority, userData);
        }

        /// <summary>
        /// Reads dictionary data asynchronously.
        /// </summary>
        /// <param name="localizationManager">The localization manager.</param>
        /// <param name="dictionaryAssetName">Dictionary asset name.</param>
        /// <param name="priority">Priority for loading the dictionary asset.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the asynchronous read operation.</returns>
        public static Task<bool> ReadDataAsync(this ILocalizationManager localizationManager, string dictionaryAssetName, int priority, object userData)
        {
            if (localizationManager == null)
            {
                throw new GameFrameworkException("Localization manager is invalid.");
            }

            if (string.IsNullOrEmpty(dictionaryAssetName))
            {
                throw new GameFrameworkException("Dictionary asset name is invalid.");
            }

            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            EventHandler<ReadDataSuccessEventArgs> successHandler = null;
            EventHandler<ReadDataFailureEventArgs> failureHandler = null;

            void Unsubscribe()
            {
                localizationManager.ReadDataSuccess -= successHandler;
                localizationManager.ReadDataFailure -= failureHandler;
            }

            successHandler = (sender, e) =>
            {
                if (e.DataAssetName == dictionaryAssetName && ReferenceEquals(e.UserData, userData))
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetResult(true);
                }
            };

            failureHandler = (sender, e) =>
            {
                if (e.DataAssetName == dictionaryAssetName && ReferenceEquals(e.UserData, userData))
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetException(new GameFrameworkException(e.ErrorMessage));
                }
            };

            localizationManager.ReadDataSuccess += successHandler;
            localizationManager.ReadDataFailure += failureHandler;

            try
            {
                localizationManager.ReadData(dictionaryAssetName, priority, userData);
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
