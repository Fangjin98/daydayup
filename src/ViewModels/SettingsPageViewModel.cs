using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.Core.Settings;
using DayDayUp.Services;
using System;
using System.Collections.Generic;

namespace DayDayUp.ViewModels
{
    public class SettingsPageViewModel : ObservableObject
    {
        public SettingsPageViewModel(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
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

        private readonly ISettingsProvider _settingsProvider;
    }
}
