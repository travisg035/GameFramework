//------------------------------------------------------------
// Game Framework
// Copyright © KhắcDuy. All rights reserved.
// GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
//------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace GameFramework
{
    /// <summary>
    /// Data provider extension methods providing asynchronous operations.
    /// </summary>
    public static class DataProviderExtension
    {
        private const int DefaultPriority = 0;

        /// <summary>
        /// Reads data asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of the data provider owner.</typeparam>
        /// <param name="dataProvider">The data provider.</param>
        /// <param name="dataAssetName">Data asset name.</param>
        /// <param name="priority">Priority for loading the data asset.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>A task representing the asynchronous read operation.</returns>
        public static Task<bool> ReadDataAsync<T>(this IDataProvider<T> dataProvider, string dataAssetName, int priority = DefaultPriority, object userData = null)
        {
            if (dataProvider == null)
            {
                throw new GameFrameworkException("Data provider is invalid.");
            }

            if (string.IsNullOrEmpty(dataAssetName))
            {
                throw new GameFrameworkException("Data asset name is invalid.");
            }

            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            EventHandler<ReadDataSuccessEventArgs> successHandler = null;
            EventHandler<ReadDataFailureEventArgs> failureHandler = null;

            void Unsubscribe()
            {
                dataProvider.ReadDataSuccess -= successHandler;
                dataProvider.ReadDataFailure -= failureHandler;
            }

            successHandler = (sender, e) =>
            {
                if (e.DataAssetName == dataAssetName && ReferenceEquals(e.UserData, userData))
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetResult(true);
                }
            };

            failureHandler = (sender, e) =>
            {
                if (e.DataAssetName == dataAssetName && ReferenceEquals(e.UserData, userData))
                {
                    Unsubscribe();
                    taskCompletionSource.TrySetException(new GameFrameworkException(e.ErrorMessage));
                }
            };

            dataProvider.ReadDataSuccess += successHandler;
            dataProvider.ReadDataFailure += failureHandler;

            try
            {
                dataProvider.ReadData(dataAssetName, priority, userData);
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
