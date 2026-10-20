//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.Config
{
    /// <summary>
    /// Config manager.
    /// </summary>
    internal sealed partial class ConfigManager : GameFrameworkModule, IConfigManager
    {
        private readonly Dictionary<string, ConfigData> m_ConfigDatas;
        private readonly DataProvider<IConfigManager> m_DataProvider;
        private IConfigHelper m_ConfigHelper;

        /// <summary>
        /// Initializes a new instance of the ConfigManager class.
        /// </summary>
        public ConfigManager()
        {
            m_ConfigDatas = new Dictionary<string, ConfigData>(StringComparer.Ordinal);
            m_DataProvider = new DataProvider<IConfigManager>(this);
            m_ConfigHelper = null;
        }

        /// <summary>
        /// Gets the count of configs.
        /// </summary>
        public int Count
        {
            get
            {
                return m_ConfigDatas.Count;
            }
        }

        /// <summary>
        /// Gets the cached bytes size.
        /// </summary>
        public int CachedBytesSize
        {
            get
            {
                return DataProvider<IConfigManager>.CachedBytesSize;
            }
        }

        /// <summary>
        /// Read config success event.
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
        /// Read config failure event.
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
        /// Read config update event.
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
        /// Read config dependency asset event.
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
        /// Polls the config manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// Shuts down and cleans up the config manager.
        /// </summary>
        internal override void Shutdown()
        {
        }

        /// <summary>
        /// Sets the resource manager.
        /// </summary>
        /// <param name="resourceManager">The resource manager.</param>
        public void SetResourceManager(IResourceManager resourceManager)
        {
            m_DataProvider.SetResourceManager(resourceManager);
        }

        /// <summary>
        /// Sets the config data provider helper.
        /// </summary>
        /// <param name="dataProviderHelper">The config data provider helper.</param>
        public void SetDataProviderHelper(IDataProviderHelper<IConfigManager> dataProviderHelper)
        {
            m_DataProvider.SetDataProviderHelper(dataProviderHelper);
        }

        /// <summary>
        /// Sets the config helper.
        /// </summary>
        /// <param name="configHelper">The config helper.</param>
        public void SetConfigHelper(IConfigHelper configHelper)
        {
            if (configHelper == null)
            {
                throw new GameFrameworkException("Config helper is invalid.");
            }

            m_ConfigHelper = configHelper;
        }

        /// <summary>
        /// Ensures the cached bytes size is large enough.
        /// </summary>
        /// <param name="ensureSize">The size to ensure.</param>
        public void EnsureCachedBytesSize(int ensureSize)
        {
            DataProvider<IConfigManager>.EnsureCachedBytesSize(ensureSize);
        }

        /// <summary>
        /// Frees the cached bytes.
        /// </summary>
        public void FreeCachedBytes()
        {
            DataProvider<IConfigManager>.FreeCachedBytes();
        }

        /// <summary>
        /// Reads config.
        /// </summary>
        /// <param name="configAssetName">The config asset name.</param>
        public void ReadData(string configAssetName)
        {
            m_DataProvider.ReadData(configAssetName);
        }

        /// <summary>
        /// Reads config.
        /// </summary>
        /// <param name="configAssetName">The config asset name.</param>
        /// <param name="priority">The priority of loading the config asset.</param>
        public void ReadData(string configAssetName, int priority)
        {
            m_DataProvider.ReadData(configAssetName, priority);
        }

        /// <summary>
        /// Reads config.
        /// </summary>
        /// <param name="configAssetName">The config asset name.</param>
        /// <param name="userData">Custom user data.</param>
        public void ReadData(string configAssetName, object userData)
        {
            m_DataProvider.ReadData(configAssetName, userData);
        }

        /// <summary>
        /// Reads config.
        /// </summary>
        /// <param name="configAssetName">The config asset name.</param>
        /// <param name="priority">The priority of loading the config asset.</param>
        /// <param name="userData">Custom user data.</param>
        public void ReadData(string configAssetName, int priority, object userData)
        {
            m_DataProvider.ReadData(configAssetName, priority, userData);
        }

        /// <summary>
        /// Parses config.
        /// </summary>
        /// <param name="configString">The config string to parse.</param>
        /// <returns>Whether the config was parsed successfully.</returns>
        public bool ParseData(string configString)
        {
            return m_DataProvider.ParseData(configString);
        }

        /// <summary>
        /// Parses config.
        /// </summary>
        /// <param name="configString">The config string to parse.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the config was parsed successfully.</returns>
        public bool ParseData(string configString, object userData)
        {
            return m_DataProvider.ParseData(configString, userData);
        }

        /// <summary>
        /// Parses config.
        /// </summary>
        /// <param name="configBytes">The config binary bytes to parse.</param>
        /// <returns>Whether the config was parsed successfully.</returns>
        public bool ParseData(byte[] configBytes)
        {
            return m_DataProvider.ParseData(configBytes);
        }

        /// <summary>
        /// Parses config.
        /// </summary>
        /// <param name="configBytes">The config binary bytes to parse.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the config was parsed successfully.</returns>
        public bool ParseData(byte[] configBytes, object userData)
        {
            return m_DataProvider.ParseData(configBytes, userData);
        }

        /// <summary>
        /// Parses config.
        /// </summary>
        /// <param name="configBytes">The config binary bytes to parse.</param>
        /// <param name="startIndex">The start index of the config binary bytes.</param>
        /// <param name="length">The length of the config binary bytes.</param>
        /// <returns>Whether the config was parsed successfully.</returns>
        public bool ParseData(byte[] configBytes, int startIndex, int length)
        {
            return m_DataProvider.ParseData(configBytes, startIndex, length);
        }

        /// <summary>
        /// Parses config.
        /// </summary>
        /// <param name="configBytes">The config binary bytes to parse.</param>
        /// <param name="startIndex">The start index of the config binary bytes.</param>
        /// <param name="length">The length of the config binary bytes.</param>
        /// <param name="userData">Custom user data.</param>
        /// <returns>Whether the config was parsed successfully.</returns>
        public bool ParseData(byte[] configBytes, int startIndex, int length, object userData)
        {
            return m_DataProvider.ParseData(configBytes, startIndex, length, userData);
        }

        /// <summary>
        /// Checks whether the specified config exists.
        /// </summary>
        /// <param name="configName">The name of the config to check.</param>
        /// <returns>Whether the specified config exists.</returns>
        public bool HasConfig(string configName)
        {
            return GetConfigData(configName).HasValue;
        }

        /// <summary>
        /// Gets the boolean value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <returns>The boolean value read.</returns>
        public bool GetBool(string configName)
        {
            ConfigData? configData = GetConfigData(configName);
            if (!configData.HasValue)
            {
                throw new GameFrameworkException(Utility.Text.Format("Config name '{0}' is not exist.", configName));
            }

            return configData.Value.BoolValue;
        }

        /// <summary>
        /// Gets the boolean value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <param name="defaultValue">The default value returned when the specified config does not exist.</param>
        /// <returns>The boolean value read.</returns>
        public bool GetBool(string configName, bool defaultValue)
        {
            ConfigData? configData = GetConfigData(configName);
            return configData.HasValue ? configData.Value.BoolValue : defaultValue;
        }

        /// <summary>
        /// Gets the integer value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <returns>The integer value read.</returns>
        public int GetInt(string configName)
        {
            ConfigData? configData = GetConfigData(configName);
            if (!configData.HasValue)
            {
                throw new GameFrameworkException(Utility.Text.Format("Config name '{0}' is not exist.", configName));
            }

            return configData.Value.IntValue;
        }

        /// <summary>
        /// Gets the integer value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <param name="defaultValue">The default value returned when the specified config does not exist.</param>
        /// <returns>The integer value read.</returns>
        public int GetInt(string configName, int defaultValue)
        {
            ConfigData? configData = GetConfigData(configName);
            return configData.HasValue ? configData.Value.IntValue : defaultValue;
        }

        /// <summary>
        /// Gets the float value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <returns>The float value read.</returns>
        public float GetFloat(string configName)
        {
            ConfigData? configData = GetConfigData(configName);
            if (!configData.HasValue)
            {
                throw new GameFrameworkException(Utility.Text.Format("Config name '{0}' is not exist.", configName));
            }

            return configData.Value.FloatValue;
        }

        /// <summary>
        /// Gets the float value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <param name="defaultValue">The default value returned when the specified config does not exist.</param>
        /// <returns>The float value read.</returns>
        public float GetFloat(string configName, float defaultValue)
        {
            ConfigData? configData = GetConfigData(configName);
            return configData.HasValue ? configData.Value.FloatValue : defaultValue;
        }

        /// <summary>
        /// Gets the string value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <returns>The string value read.</returns>
        public string GetString(string configName)
        {
            ConfigData? configData = GetConfigData(configName);
            if (!configData.HasValue)
            {
                throw new GameFrameworkException(Utility.Text.Format("Config name '{0}' is not exist.", configName));
            }

            return configData.Value.StringValue;
        }

        /// <summary>
        /// Gets the string value from the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to get.</param>
        /// <param name="defaultValue">The default value returned when the specified config does not exist.</param>
        /// <returns>The string value read.</returns>
        public string GetString(string configName, string defaultValue)
        {
            ConfigData? configData = GetConfigData(configName);
            return configData.HasValue ? configData.Value.StringValue : defaultValue;
        }

        /// <summary>
        /// Adds the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to add.</param>
        /// <param name="configValue">The config value.</param>
        /// <returns>Whether the config was added successfully.</returns>
        public bool AddConfig(string configName, string configValue)
        {
            bool boolValue = false;
            bool.TryParse(configValue, out boolValue);

            int intValue = 0;
            int.TryParse(configValue, out intValue);

            float floatValue = 0f;
            float.TryParse(configValue, out floatValue);

            return AddConfig(configName, boolValue, intValue, floatValue, configValue);
        }

        /// <summary>
        /// Adds the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to add.</param>
        /// <param name="boolValue">The boolean value of the config.</param>
        /// <param name="intValue">The integer value of the config.</param>
        /// <param name="floatValue">The float value of the config.</param>
        /// <param name="stringValue">The string value of the config.</param>
        /// <returns>Whether the config was added successfully.</returns>
        public bool AddConfig(string configName, bool boolValue, int intValue, float floatValue, string stringValue)
        {
            if (HasConfig(configName))
            {
                return false;
            }

            m_ConfigDatas.Add(configName, new ConfigData(boolValue, intValue, floatValue, stringValue));
            return true;
        }

        /// <summary>
        /// Removes the specified config.
        /// </summary>
        /// <param name="configName">The name of the config to remove.</param>
        public bool RemoveConfig(string configName)
        {
            if (!HasConfig(configName))
            {
                return false;
            }

            return m_ConfigDatas.Remove(configName);
        }

        /// <summary>
        /// Removes all configs.
        /// </summary>
        public void RemoveAllConfigs()
        {
            m_ConfigDatas.Clear();
        }

        private ConfigData? GetConfigData(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                throw new GameFrameworkException("Config name is invalid.");
            }

            ConfigData configData = default(ConfigData);
            if (m_ConfigDatas.TryGetValue(configName, out configData))
            {
                return configData;
            }

            return null;
        }
    }
}
