using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Windows.Storage;


namespace DayDayUp.Services
{
    public class ApplicationDataSettingsManager: ISettingsManager
    {
        private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public event EventHandler<SettingChangedEventArgs>? SettingChanged;

        public ApplicationDataSettingsManager()
        {
        }

        public T GetValue<T>(string key)
        {
            if (_localSettings.Values.ContainsKey(key))
            {
                if (typeof(T).IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), _localSettings.Values[key]?.ToString() ?? string.Empty);
                }
                else if (typeof(IList).IsAssignableFrom(typeof(T)))
                {
                    return JsonConvert.DeserializeObject<T>(_localSettings.Values[key]?.ToString() ?? string.Empty)!;
                }

                return (T)Convert.ChangeType(_localSettings.Values[key], typeof(T), CultureInfo.InvariantCulture);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public void SetValue<T>(string key, T value)
        {
            object? valueToSave = value;
            if (value is Enum valueEnum)
            {
                valueToSave = valueEnum.ToString();
            }
            else if (value is IList list)
            {
                valueToSave = JsonConvert.SerializeObject(list, Formatting.None);
            }
            _localSettings.Values[key] = valueToSave;

            SettingChanged?.Invoke(this, new SettingChangedEventArgs(key, value));
        }
    }

    public sealed class SettingChangedEventArgs : EventArgs
    {
        public string key { get; }
        public object? newVlaue { get; }

        public SettingChangedEventArgs(string key, object? newValue)
        {
            this.key = key;
            this.newVlaue = newValue;
        }
    }
}
