//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;

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
        string GetString<T>(string key, T arg);
        string GetString<T1, T2>(string key, T1 arg1, T2 arg2);
        string GetString<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3);
        string GetString<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        string GetString<T1, T2, T3, T4, T5>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        string GetString<T1, T2, T3, T4, T5, T6>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
        string GetString<T1, T2, T3, T4, T5, T6, T7>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12);
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13);
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14);
        string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15);
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
