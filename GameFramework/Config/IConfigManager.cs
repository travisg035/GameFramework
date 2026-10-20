//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;

namespace GameFramework.Config
{
    /// <summary>
    /// Config manager interface.
    /// </summary>
    public interface IConfigManager : IDataProvider<IConfigManager>
    {
        /// <summary>
        /// Gets the count of configs.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Gets the cached bytes size.
        /// </summary>
        int CachedBytesSize
        {
            get;
        }

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        void SetResourceManager(IResourceManager resourceManager);

        /// <summary>
        /// Sets the config data provider helper.
        /// </summary>
        /// <param name="dataProviderHelper">The config data provider helper.</param>
        void SetDataProviderHelper(IDataProviderHelper<IConfigManager> dataProviderHelper);

        /// <summary>
        /// Sets the config helper.
        /// </summary>
        /// <param name="configHelper">The config helper.</param>
        void SetConfigHelper(IConfigHelper configHelper);

        /// <summary>
        /// Ensures the cached bytes size is large enough.
        /// </summary>
        /// <param name="ensureSize">The size to ensure.</param>
        void EnsureCachedBytesSize(int ensureSize);

        /// <summary>
        /// Frees the cached bytes.
        /// </summary>
        void FreeCachedBytes();

        /// <summary>
        /// Checks whether the specified config exists.
        /// </summary>
        /// <param name="configName">The name of the config to check.</param>
        /// <returns>Whether the specified config exists.</returns>
        bool HasConfig(string configName);

        /// <summary>
        /// Gets the boolean value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <returns>The boolean value read.</returns>
        bool GetBool(string configName);

        /// <summary>
        /// Gets the boolean value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <param name="defaultValue">The default value returned when the specified config does not exist.</param>
        /// <returns>The boolean value read.</returns>
        bool GetBool(string configName, bool defaultValue);

        /// <summary>
        /// Gets the integer value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <returns>The integer value read.</returns>
        int GetInt(string configName);

        /// <summary>
        /// Gets the integer value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <param name="defaultValue">The default value returned when the specified config does not exist.</param>
        /// <returns>The integer value read.</returns>
        int GetInt(string configName, int defaultValue);

        /// <summary>
        /// Gets the float value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <returns>The float value read.</returns>
        float GetFloat(string configName);

        /// <summary>
        /// Gets the float value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <param name="defaultValue">The default value returned when the specified config does not exist.</param>
        /// <returns>The float value read.</returns>
        float GetFloat(string configName, float defaultValue);

        /// <summary>
        /// Gets the string value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <returns>The string value read.</returns>
        string GetString(string configName);

        /// <summary>
        /// Gets the string value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <param name="defaultValue">The default value returned when the specified config does not exist.</param>
        /// <returns>The string value read.</returns>
        string GetString(string configName, string defaultValue);

        /// <summary>
        /// Adds the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to add.</param>
        /// <param name="configValue">The config value.</param>
        /// <returns>Whether the config was added successfully.</returns>
        bool AddConfig(string configName, string configValue);

        /// <summary>
        /// Adds the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to add.</param>
        /// <param name="boolValue">The boolean value of the config.</param>
        /// <param name="intValue">The integer value of the config.</param>
        /// <param name="floatValue">The float value of the config.</param>
        /// <param name="stringValue">The string value of the config.</param>
        /// <returns>Whether the config was added successfully.</returns>
        bool AddConfig(string configName, bool boolValue, int intValue, float floatValue, string stringValue);

        /// <summary>
        /// Removes the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to remove.</param>
        /// <returns>Whether the config was removed successfully.</returns>
        bool RemoveConfig(string configName);

        /// <summary>
        /// Removes all configs.
        /// </summary>
        void RemoveAllConfigs();
    }
}
