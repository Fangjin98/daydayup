using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.Core.Settings;
using DayDayUp.Services;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using WinUICommunity;

namespace DayDayUp.ViewModels
{
    public class SettingsPageViewModel : ObservableObject
    {
        public SettingsPageViewModel(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
            _settingsProvider.SettingChanged += SettingsProvider_ThemeChanged;
        }

        internal List<LanguageDefinition> AvailableLanguages => LanguageManager.Instance.AvailableLanguages;
        internal SettingsPageStrings Strings => LanguageManager.Instance.SettingsPage;

        internal string Language
        {
            get => _settingsProvider.GetSetting(PredefinedSettings.Language);
            set => _settingsProvider.SetSetting(PredefinedSettings.Language, value);
        }

        internal AppTheme Theme
        {
            get => _settingsProvider.GetSetting(PredefinedSettings.Theme);
            set => _settingsProvider.SetSetting(PredefinedSettings.Theme, value);
        }

        internal void ExportToJson(string filePath)
        {
            Ioc.Default.GetRequiredService<IDataAccess>().ExportToJson(filePath);
        }

        private void SettingsProvider_ThemeChanged(object sender, SettingChangedEventArgs e)
        {
            var CurrentTheme = _settingsProvider.GetSetting(PredefinedSettings.Theme);
            var CurrentSystemTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark ? AppTheme.Dark : AppTheme.Light;

            if (string.Equals(PredefinedSettings.Theme.Name, e.SettingName, StringComparison.Ordinal))
            {
                if (CurrentTheme == AppTheme.Default)
                {
                    CurrentTheme = CurrentSystemTheme;
                }

                if (CurrentTheme == AppTheme.Dark)
                {
                    ThemeManager.Instance.SetCurrentTheme(ElementTheme.Dark);
                }
                else if (CurrentTheme == AppTheme.Light)
                {
                    ThemeManager.Instance.SetCurrentTheme(ElementTheme.Light);
                }
            }
        }

        private readonly ISettingsProvider _settingsProvider;
    }
}
