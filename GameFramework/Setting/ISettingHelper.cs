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
    /// Setting helper interface.
    /// </summary>
    public interface ISettingHelper
    {
        /// <summary>
        /// Gets the number of setting items.
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Loads settings.
        /// </summary>
        /// <returns>Whether settings were loaded successfully.</returns>
        bool Load();

        /// <summary>
        /// Saves settings.
        /// </summary>
        /// <returns>Whether settings were saved successfully.</returns>
        bool Save();

        /// <summary>
        /// Gets all setting item names.
        /// </summary>
        /// <returns>All setting item names.</returns>
        string[] GetAllSettingNames();

        /// <summary>
        /// Gets all setting item names.
        /// </summary>
        /// <param name="results">The list to store all setting item names.</param>
        void GetAllSettingNames(List<string> results);

        /// <summary>
        /// Determines whether the specified setting item exists.
        /// </summary>
        /// <param name="settingName">The name of the setting item to check.</param>
        /// <returns>Whether the specified setting item exists.</returns>
        bool HasSetting(string settingName);

        /// <summary>
        /// Removes the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to remove.</param>
        /// <returns>Whether the specified setting item was removed successfully.</returns>
        bool RemoveSetting(string settingName);

        /// <summary>
        /// Removes all setting items.
        /// </summary>
        void RemoveAllSettings();

        /// <summary>
        /// Reads a boolean value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read boolean value.</returns>
        bool GetBool(string settingName);

        /// <summary>
        /// Reads a boolean value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultValue">The default value returned when the specified setting item does not exist.</param>
        /// <returns>The read boolean value.</returns>
        bool GetBool(string settingName, bool defaultValue);

        /// <summary>
        /// Writes a boolean value to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="value">The boolean value to write.</param>
        void SetBool(string settingName, bool value);

        /// <summary>
        /// Reads an integer value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read integer value.</returns>
        int GetInt(string settingName);

        /// <summary>
        /// Reads an integer value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultValue">The default value returned when the specified setting item does not exist.</param>
        /// <returns>The read integer value.</returns>
        int GetInt(string settingName, int defaultValue);

        /// <summary>
        /// Writes an integer value to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="value">The integer value to write.</param>
        void SetInt(string settingName, int value);

        /// <summary>
        /// Reads a floating-point value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read floating-point value.</returns>
        float GetFloat(string settingName);

        /// <summary>
        /// Reads a floating-point value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultValue">The default value returned when the specified setting item does not exist.</param>
        /// <returns>The read floating-point value.</returns>
        float GetFloat(string settingName, float defaultValue);

        /// <summary>
        /// Writes a floating-point value to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="value">The floating-point value to write.</param>
        void SetFloat(string settingName, float value);

        /// <summary>
        /// Reads a string value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read string value.</returns>
        string GetString(string settingName);

        /// <summary>
        /// Reads a string value from the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultValue">The default value returned when the specified setting item does not exist.</param>
        /// <returns>The read string value.</returns>
        string GetString(string settingName, string defaultValue);

        /// <summary>
        /// Writes a string value to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="value">The string value to write.</param>
        void SetString(string settingName, string value);

        /// <summary>
        /// Reads an object from the specified setting item.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read object.</returns>
        T GetObject<T>(string settingName);

        /// <summary>
        /// Reads an object from the specified setting item.
        /// </summary>
        /// <param name="objectType">The type of the object to read.</param>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <returns>The read object.</returns>
        object GetObject(Type objectType, string settingName);

        /// <summary>
        /// Reads an object from the specified setting item.
        /// </summary>
        /// <typeparam name="T">The type of the object to read.</typeparam>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultObj">The default object returned when the specified setting item does not exist.</param>
        /// <returns>The read object.</returns>
        T GetObject<T>(string settingName, T defaultObj);

        /// <summary>
        /// Reads an object from the specified setting item.
        /// </summary>
        /// <param name="objectType">The type of the object to read.</param>
        /// <param name="settingName">The name of the setting item to get.</param>
        /// <param name="defaultObj">The default object returned when the specified setting item does not exist.</param>
        /// <returns>The read object.</returns>
        object GetObject(Type objectType, string settingName, object defaultObj);

        /// <summary>
        /// Writes an object to the specified setting item.
        /// </summary>
        /// <typeparam name="T">The type of the object to write.</typeparam>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="obj">The object to write.</param>
        void SetObject<T>(string settingName, T obj);

        /// <summary>
        /// Writes an object to the specified setting item.
        /// </summary>
        /// <param name="settingName">The name of the setting item to write.</param>
        /// <param name="obj">The object to write.</param>
        void SetObject(string settingName, object obj);
    }
}
