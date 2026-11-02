//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.Localization
{
    /// <summary>
    /// Localization manager interface.
    /// </summary>
    public interface ILocalizationManager : IDataProvider<ILocalizationManager>
    {
        /// <summary>
        /// Gets or sets the localization language.
        /// </summary>
        Language Language
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the system language.
        /// </summary>
        Language SystemLanguage
        {
            get;
        }

        /// <summary>
        /// Gets the number of dictionaries.
        /// </summary>
        int DictionaryCount
        {
            get;
        }

        /// <summary>
        /// Gets the size of the cached binary stream buffer.
        /// </summary>
        int CachedBytesSize
        {
            get;
        }

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">Resource manager.</param>
        void SetResourceManager(IResourceManager resourceManager);

        /// <summary>
        /// Sets the localization data provider helper.
        /// </summary>
        /// <param name="dataProviderHelper">Localization data provider helper.</param>
        void SetDataProviderHelper(IDataProviderHelper<ILocalizationManager> dataProviderHelper);

        /// <summary>
        /// Sets the localization helper.
        /// </summary>
        /// <param name="localizationHelper">Localization helper.</param>
        void SetLocalizationHelper(ILocalizationHelper localizationHelper);

        /// <summary>
        /// Ensures the binary stream cache has allocated sufficient memory and keeps it cached.
        /// </summary>
        /// <param name="ensureSize">The size of memory to ensure for the binary stream cache.</param>
        void EnsureCachedBytesSize(int ensureSize);

        /// <summary>
        /// Releases the cached binary stream buffer.
        /// </summary>
        void FreeCachedBytes();

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>The localized string.</returns>
        string GetString(string key);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T">The type of parameter.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg">Parameter value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T>(string key, T arg);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2>(string key, T1 arg1, T2 arg2);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <typeparam name="T8">The type of parameter 8.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <param name="arg8">Parameter 8 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <typeparam name="T8">The type of parameter 8.</typeparam>
        /// <typeparam name="T9">The type of parameter 9.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <param name="arg8">Parameter 8 value.</param>
        /// <param name="arg9">Parameter 9 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <typeparam name="T8">The type of parameter 8.</typeparam>
        /// <typeparam name="T9">The type of parameter 9.</typeparam>
        /// <typeparam name="T10">The type of parameter 10.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <param name="arg8">Parameter 8 value.</param>
        /// <param name="arg9">Parameter 9 value.</param>
        /// <param name="arg10">Parameter 10 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <typeparam name="T8">The type of parameter 8.</typeparam>
        /// <typeparam name="T9">The type of parameter 9.</typeparam>
        /// <typeparam name="T10">The type of parameter 10.</typeparam>
        /// <typeparam name="T11">The type of parameter 11.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <param name="arg8">Parameter 8 value.</param>
        /// <param name="arg9">Parameter 9 value.</param>
        /// <param name="arg10">Parameter 10 value.</param>
        /// <param name="arg11">Parameter 11 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <typeparam name="T8">The type of parameter 8.</typeparam>
        /// <typeparam name="T9">The type of parameter 9.</typeparam>
        /// <typeparam name="T10">The type of parameter 10.</typeparam>
        /// <typeparam name="T11">The type of parameter 11.</typeparam>
        /// <typeparam name="T12">The type of parameter 12.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <param name="arg8">Parameter 8 value.</param>
        /// <param name="arg9">Parameter 9 value.</param>
        /// <param name="arg10">Parameter 10 value.</param>
        /// <param name="arg11">Parameter 11 value.</param>
        /// <param name="arg12">Parameter 12 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <typeparam name="T8">The type of parameter 8.</typeparam>
        /// <typeparam name="T9">The type of parameter 9.</typeparam>
        /// <typeparam name="T10">The type of parameter 10.</typeparam>
        /// <typeparam name="T11">The type of parameter 11.</typeparam>
        /// <typeparam name="T12">The type of parameter 12.</typeparam>
        /// <typeparam name="T13">The type of parameter 13.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <param name="arg8">Parameter 8 value.</param>
        /// <param name="arg9">Parameter 9 value.</param>
        /// <param name="arg10">Parameter 10 value.</param>
        /// <param name="arg11">Parameter 11 value.</param>
        /// <param name="arg12">Parameter 12 value.</param>
        /// <param name="arg13">Parameter 13 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <typeparam name="T8">The type of parameter 8.</typeparam>
        /// <typeparam name="T9">The type of parameter 9.</typeparam>
        /// <typeparam name="T10">The type of parameter 10.</typeparam>
        /// <typeparam name="T11">The type of parameter 11.</typeparam>
        /// <typeparam name="T12">The type of parameter 12.</typeparam>
        /// <typeparam name="T13">The type of parameter 13.</typeparam>
        /// <typeparam name="T14">The type of parameter 14.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <param name="arg8">Parameter 8 value.</param>
        /// <param name="arg9">Parameter 9 value.</param>
        /// <param name="arg10">Parameter 10 value.</param>
        /// <param name="arg11">Parameter 11 value.</param>
        /// <param name="arg12">Parameter 12 value.</param>
        /// <param name="arg13">Parameter 13 value.</param>
        /// <param name="arg14">Parameter 14 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <typeparam name="T8">The type of parameter 8.</typeparam>
        /// <typeparam name="T9">The type of parameter 9.</typeparam>
        /// <typeparam name="T10">The type of parameter 10.</typeparam>
        /// <typeparam name="T11">The type of parameter 11.</typeparam>
        /// <typeparam name="T12">The type of parameter 12.</typeparam>
        /// <typeparam name="T13">The type of parameter 13.</typeparam>
        /// <typeparam name="T14">The type of parameter 14.</typeparam>
        /// <typeparam name="T15">The type of parameter 15.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <param name="arg8">Parameter 8 value.</param>
        /// <param name="arg9">Parameter 9 value.</param>
        /// <param name="arg10">Parameter 10 value.</param>
        /// <param name="arg11">Parameter 11 value.</param>
        /// <param name="arg12">Parameter 12 value.</param>
        /// <param name="arg13">Parameter 13 value.</param>
        /// <param name="arg14">Parameter 14 value.</param>
        /// <param name="arg15">Parameter 15 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <typeparam name="T3">The type of parameter 3.</typeparam>
        /// <typeparam name="T4">The type of parameter 4.</typeparam>
        /// <typeparam name="T5">The type of parameter 5.</typeparam>
        /// <typeparam name="T6">The type of parameter 6.</typeparam>
        /// <typeparam name="T7">The type of parameter 7.</typeparam>
        /// <typeparam name="T8">The type of parameter 8.</typeparam>
        /// <typeparam name="T9">The type of parameter 9.</typeparam>
        /// <typeparam name="T10">The type of parameter 10.</typeparam>
        /// <typeparam name="T11">The type of parameter 11.</typeparam>
        /// <typeparam name="T12">The type of parameter 12.</typeparam>
        /// <typeparam name="T13">The type of parameter 13.</typeparam>
        /// <typeparam name="T14">The type of parameter 14.</typeparam>
        /// <typeparam name="T15">The type of parameter 15.</typeparam>
        /// <typeparam name="T16">The type of parameter 16.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <param name="arg3">Parameter 3 value.</param>
        /// <param name="arg4">Parameter 4 value.</param>
        /// <param name="arg5">Parameter 5 value.</param>
        /// <param name="arg6">Parameter 6 value.</param>
        /// <param name="arg7">Parameter 7 value.</param>
        /// <param name="arg8">Parameter 8 value.</param>
        /// <param name="arg9">Parameter 9 value.</param>
        /// <param name="arg10">Parameter 10 value.</param>
        /// <param name="arg11">Parameter 11 value.</param>
        /// <param name="arg12">Parameter 12 value.</param>
        /// <param name="arg13">Parameter 13 value.</param>
        /// <param name="arg14">Parameter 14 value.</param>
        /// <param name="arg15">Parameter 15 value.</param>
        /// <param name="arg16">Parameter 16 value.</param>
        /// <returns>The localized string.</returns>
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16);

        /// <summary>
        /// Determines whether the raw dictionary string exists.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>Whether the raw dictionary string exists.</returns>
        bool HasRawString(string key);

        /// <summary>
        /// Gets the raw dictionary string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>The raw dictionary string.</returns>
        string GetRawString(string key);

        /// <summary>
        /// Attempts to get the raw dictionary string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <param name="value">The raw dictionary string if found; otherwise, null.</param>
        /// <returns>true if the raw dictionary string was found; otherwise, false.</returns>
        bool TryGetRawString(string key, out string value);

        /// <summary>
        /// Attempts to get the localized string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <param name="value">The localized string if found; otherwise, null.</param>
        /// <returns>true if the localized string was found; otherwise, false.</returns>
        bool TryGetString(string key, out string value);

        /// <summary>
        /// Gets all raw dictionary strings.
        /// </summary>
        /// <param name="results">The dictionary to store the results.</param>
        void GetAllRawStrings(IDictionary<string, string> results);

        /// <summary>
        /// Gets all raw dictionary strings.
        /// </summary>
        /// <returns>All raw dictionary strings.</returns>
        IDictionary<string, string> GetAllRawStrings();

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        /// <summary>
        /// Determines whether the raw dictionary string exists.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>Whether the raw dictionary string exists.</returns>
        bool HasRawString(ReadOnlySpan<char> key);

        /// <summary>
        /// Gets the raw dictionary string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>The raw dictionary string.</returns>
        string GetRawString(ReadOnlySpan<char> key);

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>The localized string.</returns>
        string GetString(ReadOnlySpan<char> key);

        /// <summary>
        /// Attempts to get the raw dictionary string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <param name="value">The raw dictionary string if found; otherwise, null.</param>
        /// <returns>true if the raw dictionary string was found; otherwise, false.</returns>
        bool TryGetRawString(ReadOnlySpan<char> key, out string value);

        /// <summary>
        /// Attempts to get the localized string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <param name="value">The localized string if found; otherwise, null.</param>
        /// <returns>true if the localized string was found; otherwise, false.</returns>
        bool TryGetString(ReadOnlySpan<char> key, out string value);
#endif

        /// <summary>
        /// Adds a raw dictionary string.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <param name="value">The dictionary value.</param>
        /// <returns>Whether the raw dictionary string was added successfully.</returns>
        bool AddRawString(string key, string value);

        /// <summary>
        /// Removes the raw dictionary string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>Whether the raw dictionary string was removed successfully.</returns>
        bool RemoveRawString(string key);

        /// <summary>
        /// Removes all raw dictionary strings.
        /// </summary>
        void RemoveAllRawStrings();
    }
}
