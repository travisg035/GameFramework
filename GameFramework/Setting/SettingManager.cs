//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GameFramework.Setting
{
    /// <summary>
    /// Setting manager.
    /// </summary>
    internal sealed class SettingManager : GameFrameworkModule, ISettingManager
    {
        private ISettingHelper m_SettingHelper;

        /// <summary>
        /// Initializes a new instance of the SettingManager class.
        /// </summary>
        public SettingManager()
        {
            m_SettingHelper = null;
        }

        /// <summary>
        /// Gets the number of setting items.
        /// </summary>
        public int Count
        {
            get
            {
                if (m_SettingHelper == null)
                {
                    throw new GameFrameworkException("Setting helper is invalid.");
                }

                return m_SettingHelper.Count;
            }
        }

        /// <summary>
        /// Updates the setting manager.
        /// </summary>
        /// <param name="elapseSeconds">Logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">Real elapsed time in seconds.</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// Shuts down and cleans up the setting manager.
        /// </summary>
        internal override void Shutdown()
        {
            Save();
        }

        /// <summary>
        /// Sets the setting helper.
        /// </summary>
        /// <param name="settingHelper">Setting helper.</param>
        public void SetSettingHelper(ISettingHelper settingHelper)
        {
            if (settingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            m_SettingHelper = settingHelper;
        }

        /// <summary>
        /// Loads settings.
        /// </summary>
        /// <returns>Whether settings were loaded successfully.</returns>
        public bool Load()
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            return m_SettingHelper.Load();
        }

        /// <summary>
        /// Saves settings.
        /// </summary>
        /// <returns>Whether settings were saved successfully.</returns>
        public bool Save()
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            return m_SettingHelper.Save();
        }

        /// <summary>
        /// Gets all setting item names.
        /// </summary>
        /// <returns>All setting item names.</returns>
        public string[] GetAllSettingNames()
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            return m_SettingHelper.GetAllSettingNames();
        }

        /// <summary>
        /// Gets all setting item names.
        /// </summary>
        /// <param name="results">The list to store all setting item names.</param>
        public void GetAllSettingNames(List<string> results)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            m_SettingHelper.GetAllSettingNames(results);
        }

        /// <summary>
        /// Determines whether the specified setting item exists.
        /// </summary>
        /// <param name="settingName">The name of the setting item to check.</param>
        /// <returns>Whether the specified setting item exists.</returns>
        public bool HasSetting(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.HasSetting(settingName);
        }

        /// <summary>
        /// Removes the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to remove.</param>
        /// <returns>Whether the specified setting item was removed successfully.</returns>
        public bool RemoveSetting(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.RemoveSetting(settingName);
        }

        /// <summary>
        /// Removes all setting items.
        /// </summary>
        public void RemoveAllSettings()
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            m_SettingHelper.RemoveAllSettings();
        }

        /// <summary>
        /// Reads a boolean value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read boolean value.</returns>
        public bool GetBool(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetBool(settingName);
        }

        /// <summary>
        /// Reads a boolean value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultValue">The default value returned when the specified setting item does not exist.</param>
        /// <returns>The read boolean value.</returns>
        public bool GetBool(string settingName, bool defaultValue)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetBool(settingName, defaultValue);
        }

        /// <summary>
        /// Writes a boolean value to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="value">The boolean value to write.</param>
        public void SetBool(string settingName, bool value)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            m_SettingHelper.SetBool(settingName, value);
        }

        /// <summary>
        /// Reads an integer value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read integer value.</returns>
        public int GetInt(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetInt(settingName);
        }

        /// <summary>
        /// Reads an integer value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultValue">The default value returned when the specified setting item does not exist.</param>
        /// <returns>The read integer value.</returns>
        public int GetInt(string settingName, int defaultValue)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetInt(settingName, defaultValue);
        }

        /// <summary>
        /// Writes an integer value to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="value">The integer value to write.</param>
        public void SetInt(string settingName, int value)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            m_SettingHelper.SetInt(settingName, value);
        }

        /// <summary>
        /// Reads a floating-point value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read floating-point value.</returns>
        public float GetFloat(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetFloat(settingName);
        }

        /// <summary>
        /// Reads a floating-point value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultValue">The default value returned when the specified setting item does not exist.</param>
        /// <returns>The read floating-point value.</returns>
        public float GetFloat(string settingName, float defaultValue)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetFloat(settingName, defaultValue);
        }

        /// <summary>
        /// Writes a floating-point value to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="value">The floating-point value to write.</param>
        public void SetFloat(string settingName, float value)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            m_SettingHelper.SetFloat(settingName, value);
        }

        /// <summary>
        /// Reads a string value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read string value.</returns>
        public string GetString(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetString(settingName);
        }

        /// <summary>
        /// Reads a string value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultValue">The default value returned when the specified setting item does not exist.</param>
        /// <returns>The read string value.</returns>
        public string GetString(string settingName, string defaultValue)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetString(settingName, defaultValue);
        }

        /// <summary>
        /// Writes a string value to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="value">The string value to write.</param>
        public void SetString(string settingName, string value)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            m_SettingHelper.SetString(settingName, value);
        }

        /// <summary>
        /// Reads an object from the specified setting item.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read object.</returns>
        public T GetObject<T>(string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetObject<T>(settingName);
        }

        /// <summary>
        /// Reads an object from the specified setting item.
        /// </summary>
        /// <param name="objectType">The type of the object to read.</param>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read object.</returns>
        public object GetObject(Type objectType, string settingName)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (objectType == null)
            {
                throw new GameFrameworkException("Object type is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetObject(objectType, settingName);
        }

        /// <summary>
        /// Reads an object from the specified setting item.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultObj">The default object returned when the specified setting item does not exist.</param>
        /// <returns>The read object.</returns>
        public T GetObject<T>(string settingName, T defaultObj)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetObject(settingName, defaultObj);
        }

        /// <summary>
        /// Reads an object from the specified setting item.
        /// </summary>
        /// <param name="objectType">The type of the object to read.</param>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultObj">The default object returned when the specified setting item does not exist.</param>
        /// <returns>The read object.</returns>
        public object GetObject(Type objectType, string settingName, object defaultObj)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (objectType == null)
            {
                throw new GameFrameworkException("Object type is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            return m_SettingHelper.GetObject(objectType, settingName, defaultObj);
        }

        /// <summary>
        /// Writes an object to the specified setting item.
        /// </summary>
        /// <typeparam name="T">The type of the object to write.</typeparam>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="obj">The object to write.</param>
        public void SetObject<T>(string settingName, T obj)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            m_SettingHelper.SetObject(settingName, obj);
        }

        /// <summary>
        /// Writes an object to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="obj">The object to write.</param>
        public void SetObject(string settingName, object obj)
        {
            if (m_SettingHelper == null)
            {
                throw new GameFrameworkException("Setting helper is invalid.");
            }

            if (string.IsNullOrEmpty(settingName))
            {
                throw new GameFrameworkException("Setting name is invalid.");
            }

            m_SettingHelper.SetObject(settingName, obj);
        }
    }
}
