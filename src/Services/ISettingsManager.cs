using System;
using System.Diagnostics.Contracts;

namespace DayDayUp.Services
{
    public interface ISettingsManager
    {
        /// <summary>
        /// Raised when a setting value has changed.
        /// </summary>
        event EventHandler<SettingChangedEventArgs>? SettingChanged;

        /// <summary>
        /// Gets the value of a defined setting.
        /// </summary>
        /// <typeparam name="T">The type of value that will be retrieved.</typeparam>
        /// <returns>Return the value of the setting or its default value.</returns>
        [Pure]
        T GetValue<T>(string key);

        /// <summary>
        /// Sets the value of a given setting.
        /// </summary>
        /// <typeparam name="T">The type of value that will be set.</typeparam>
        /// <param name="value">The value to set</param>
        void SetValue<T>(string key, T value);
    }
}
