using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.Models;
using DayDayUp.Services;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using WinUICommunity;

namespace DayDayUp.ViewModels
{
    public class SettingsPageViewModel : ObservableObject
    {
        internal List<LanguageDefinition> AvailableLanguages => LanguageManager.Instance.AvailableLanguages;
        internal SettingsPageStrings Strings => LanguageManager.Instance.SettingsPage;
        internal string Language
        {
            get => settingsManager.GetValue<string>(settings.Language.Title);
            set => settingsManager.SetValue(settings.Language.Title, value);
        }
        internal AppTheme Theme
        {
            get => settingsManager.GetValue<AppTheme>(settings.Theme.Title);
            set => settingsManager.SetValue(settings.Theme.Title, value);
        }
        public event EventHandler? ThemeChanged;

        public SettingsPageViewModel(ISettingsManager settingsManager, UserSettings settings)
        {
            this.settingsManager = settingsManager;
            this.settings = settings;
            settingsManager.SettingChanged += SettingsManager_ThemeChanged;
        }

        internal void ExportToJson(string filePath)
        {
            Ioc.Default.GetRequiredService<IDataAccess>().ExportToJson(filePath);
        }

        private void SettingsManager_ThemeChanged(object sender, SettingChangedEventArgs e)
        {
            var CurrentTheme = settingsManager.GetValue<AppTheme>(settings.Theme.Title);
            var CurrentSystemTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark ? AppTheme.Dark : AppTheme.Light;

            if (string.Equals(settings.Theme.Title, e.key, StringComparison.Ordinal))
            {
                if (CurrentTheme == AppTheme.Default)
                {
                    CurrentTheme = CurrentSystemTheme;
                }

                switch (CurrentTheme)
                {
                    case AppTheme.Dark:
                        ThemeManager.Instance.SetCurrentTheme(ElementTheme.Dark); break;
                    case AppTheme.Light:
                        ThemeManager.Instance.SetCurrentTheme(ElementTheme.Light); break;
                }

                ThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private readonly UserSettings settings;
        private readonly ISettingsManager settingsManager;
    }
}
