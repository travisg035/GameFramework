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
    /// Localization manager.
    /// </summary>
    internal sealed partial class LocalizationManager : GameFrameworkModule, ILocalizationManager
    {
        private readonly Dictionary<string, string> m_Dictionary;
        private readonly DataProvider<ILocalizationManager> m_DataProvider;
        private ILocalizationHelper m_LocalizationHelper;
        private Language m_Language;

        /// <summary>
        /// Initializes a new instance of the LocalizationManager class.
        /// </summary>
        public LocalizationManager()
        {
            m_Dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
            m_DataProvider = new DataProvider<ILocalizationManager>(this);
            m_LocalizationHelper = null;
            m_Language = Language.Unspecified;
        }

        /// <summary>
        /// Gets or sets the localization language.
        /// </summary>
        public Language Language
        {
            get
            {
                return m_Language;
            }
            set
            {
                if (value == Language.Unspecified)
                {
                    throw new GameFrameworkException("Language is invalid.");
                }

                m_Language = value;
            }
        }

        /// <summary>
        /// Gets the system language.
        /// </summary>
        public Language SystemLanguage
        {
            get
            {
                if (m_LocalizationHelper == null)
                {
                    throw new GameFrameworkException("You must set localization helper first.");
                }

                return m_LocalizationHelper.SystemLanguage;
            }
        }

        /// <summary>
        /// Gets the number of dictionaries.
        /// </summary>
        public int DictionaryCount
        {
            get
            {
                return m_Dictionary.Count;
            }
        }

        /// <summary>
        /// Gets the size of the cached binary stream buffer.
        /// </summary>
        public int CachedBytesSize
        {
            get
            {
                return DataProvider<ILocalizationManager>.CachedBytesSize;
            }
        }

        /// <summary>
        /// Read dictionary success event.
        /// </summary>
        public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
        {
            add
            {
                m_DataProvider.ReadDataSuccess += value;
            }
            remove
            {
                m_DataProvider.ReadDataSuccess -= value;
            }
        }

        /// <summary>
        /// Read dictionary failure event.
        /// </summary>
        public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
        {
            add
            {
                m_DataProvider.ReadDataFailure += value;
            }
            remove
            {
                m_DataProvider.ReadDataFailure -= value;
            }
        }

        /// <summary>
        /// Read dictionary update event.
        /// </summary>
        public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate
        {
            add
            {
                m_DataProvider.ReadDataUpdate += value;
            }
            remove
            {
                m_DataProvider.ReadDataUpdate -= value;
            }
        }

        /// <summary>
        /// Read dictionary dependency asset event.
        /// </summary>
        public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependencyAsset
        {
            add
            {
                m_DataProvider.ReadDataDependencyAsset += value;
            }
            remove
            {
                m_DataProvider.ReadDataDependencyAsset -= value;
            }
        }

        /// <summary>
        /// Updates the localization manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// Shuts down and cleans up the localization manager.
        /// </summary>
        internal override void Shutdown()
        {
        }

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">Resource manager.</param>
        public void SetResourceManager(IResourceManager resourceManager)
        {
            m_DataProvider.SetResourceManager(resourceManager);
        }

        /// <summary>
        /// Sets the localization data provider helper.
        /// </summary>
        /// <param name="dataProviderHelper">Localization data provider helper.</param>
        public void SetDataProviderHelper(IDataProviderHelper<ILocalizationManager> dataProviderHelper)
        {
            m_DataProvider.SetDataProviderHelper(dataProviderHelper);
        }

        /// <summary>
        /// Sets the localization helper.
        /// </summary>
        /// <param name="localizationHelper">Localization helper.</param>
        public void SetLocalizationHelper(ILocalizationHelper localizationHelper)
        {
            if (localizationHelper == null)
            {
                throw new GameFrameworkException("Localization helper is invalid.");
            }

            m_LocalizationHelper = localizationHelper;
        }

        /// <summary>
        /// Ensures the binary stream cache has allocated sufficient memory and keeps it cached.
        /// </summary>
        /// <param name="ensureSize">The size of memory to ensure for the binary stream cache.</param>
        public void EnsureCachedBytesSize(int ensureSize)
        {
            DataProvider<ILocalizationManager>.EnsureCachedBytesSize(ensureSize);
        }

        /// <summary>
        /// Releases the cached binary stream buffer.
        /// </summary>
        public void FreeCachedBytes()
        {
            DataProvider<ILocalizationManager>.FreeCachedBytes();
        }

        /// <summary>
        /// Reads dictionary data.
        /// </summary>
        /// <param name="dictionaryAssetName">Dictionary asset name.</param>
        public void ReadData(string dictionaryAssetName)
        {
            m_DataProvider.ReadData(dictionaryAssetName);
        }

        /// <summary>
        /// Reads dictionary data.
        /// </summary>
        /// <param name="dictionaryAssetName">Dictionary asset name.</param>
        /// <param name="priority">Priority for loading the dictionary asset.</param>
        public void ReadData(string dictionaryAssetName, int priority)
        {
            m_DataProvider.ReadData(dictionaryAssetName, priority);
        }

        /// <summary>
        /// Reads dictionary data.
        /// </summary>
        /// <param name="dictionaryAssetName">Dictionary asset name.</param>
        /// <param name="userData">User-defined data.</param>
        public void ReadData(string dictionaryAssetName, object userData)
        {
            m_DataProvider.ReadData(dictionaryAssetName, userData);
        }

        /// <summary>
        /// Reads dictionary data.
        /// </summary>
        /// <param name="dictionaryAssetName">Dictionary asset name.</param>
        /// <param name="priority">Priority for loading the dictionary asset.</param>
        /// <param name="userData">User-defined data.</param>
        public void ReadData(string dictionaryAssetName, int priority, object userData)
        {
            m_DataProvider.ReadData(dictionaryAssetName, priority, userData);
        }

        /// <summary>
        /// Parses dictionary data.
        /// </summary>
        /// <param name="dictionaryString">The dictionary string to parse.</param>
        /// <returns>Whether the dictionary was parsed successfully.</returns>
        public bool ParseData(string dictionaryString)
        {
            return m_DataProvider.ParseData(dictionaryString);
        }

        /// <summary>
        /// Parses dictionary data.
        /// </summary>
        /// <param name="dictionaryString">The dictionary string to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the dictionary was parsed successfully.</returns>
        public bool ParseData(string dictionaryString, object userData)
        {
            return m_DataProvider.ParseData(dictionaryString, userData);
        }

        /// <summary>
        /// Parses dictionary data.
        /// </summary>
        /// <param name="dictionaryBytes">The dictionary byte array to parse.</param>
        /// <returns>Whether the dictionary was parsed successfully.</returns>
        public bool ParseData(byte[] dictionaryBytes)
        {
            return m_DataProvider.ParseData(dictionaryBytes);
        }

        /// <summary>
        /// Parses dictionary data.
        /// </summary>
        /// <param name="dictionaryBytes">The dictionary byte array to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the dictionary was parsed successfully.</returns>
        public bool ParseData(byte[] dictionaryBytes, object userData)
        {
            return m_DataProvider.ParseData(dictionaryBytes, userData);
        }

        /// <summary>
        /// Parses dictionary data.
        /// </summary>
        /// <param name="dictionaryBytes">The dictionary byte array to parse.</param>
        /// <param name="startIndex">The start index in the dictionary byte array.</param>
        /// <param name="length">The length of dictionary bytes to parse.</param>
        /// <returns>Whether the dictionary was parsed successfully.</returns>
        public bool ParseData(byte[] dictionaryBytes, int startIndex, int length)
        {
            return m_DataProvider.ParseData(dictionaryBytes, startIndex, length);
        }

        /// <summary>
        /// Parses dictionary data.
        /// </summary>
        /// <param name="dictionaryBytes">The dictionary byte array to parse.</param>
        /// <param name="startIndex">The start index in the dictionary byte array.</param>
        /// <param name="length">The length of dictionary bytes to parse.</param>
        /// <param name="userData">User-defined data.</param>
        /// <returns>Whether the dictionary was parsed successfully.</returns>
        public bool ParseData(byte[] dictionaryBytes, int startIndex, int length, object userData)
        {
            return m_DataProvider.ParseData(dictionaryBytes, startIndex, length, userData);
        }

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>The localized string.</returns>
        public string GetString(string key)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            return value;
        }

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T">The type of parameter.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg">Parameter value.</param>
        /// <returns>The localized string.</returns>
        public string GetString<T>(string key, T arg)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3}", key, value, arg, exception);
            }
        }

        /// <summary>
        /// Gets the localized string corresponding to the dictionary key.
        /// </summary>
        /// <typeparam name="T1">The type of parameter 1.</typeparam>
        /// <typeparam name="T2">The type of parameter 2.</typeparam>
        /// <param name="key">The dictionary key.</param>
        /// <param name="arg1">Parameter 1 value.</param>
        /// <param name="arg2">Parameter 2 value.</param>
        /// <returns>The localized string.</returns>
        public string GetString<T1, T2>(string key, T1 arg1, T2 arg2)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4}", key, value, arg1, arg2, exception);
            }
        }

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
        public string GetString<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5}", key, value, arg1, arg2, arg3, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6}", key, value, arg1, arg2, arg3, arg4, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7}", key, value, arg1, arg2, arg3, arg4, arg5, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8}", key, value, arg1, arg2, arg3, arg4, arg5, arg6, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", key, value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7, T8>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", key, value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", key, value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", key, value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", key, value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", key, value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", key, value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            }
            catch (Exception exception)
            {
                string args = Utility.Text.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
                return Utility.Text.Format("<Error>{0},{1},{2},{3}", key, value, args, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            }
            catch (Exception exception)
            {
                string args = Utility.Text.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                return Utility.Text.Format("<Error>{0},{1},{2},{3}", key, value, args, exception);
            }
        }

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
        public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            }
            catch (Exception exception)
            {
                string args = Utility.Text.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
                return Utility.Text.Format("<Error>{0},{1},{2},{3}", key, value, args, exception);
            }
        }

        /// <summary>
        /// Determines whether the raw dictionary string exists.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>Whether the raw dictionary string exists.</returns>
        public bool HasRawString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new GameFrameworkException("Key is invalid.");
            }

            return m_Dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets the raw dictionary string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>The raw dictionary string.</returns>
        public string GetRawString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new GameFrameworkException("Key is invalid.");
            }

            string value = null;
            if (m_Dictionary.TryGetValue(key, out value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Adds a raw dictionary string.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <param name="value">The dictionary value.</param>
        /// <returns>Whether the raw dictionary string was added successfully.</returns>
        public bool AddRawString(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new GameFrameworkException("Key is invalid.");
            }

            if (m_Dictionary.ContainsKey(key))
            {
                return false;
            }

            m_Dictionary.Add(key, value ?? string.Empty);
            return true;
        }

        /// <summary>
        /// Removes the raw dictionary string corresponding to the dictionary key.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <returns>Whether the raw dictionary string was removed successfully.</returns>
        public bool RemoveRawString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new GameFrameworkException("Key is invalid.");
            }

            return m_Dictionary.Remove(key);
        }

        /// <summary>
        /// Removes all raw dictionary strings.
        /// </summary>
        public void RemoveAllRawStrings()
        {
            m_Dictionary.Clear();
        }
    }
}
