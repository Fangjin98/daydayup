using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using DayDayUp.Models;
using DayDayUp.Services;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinUICommunity;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        public event EventHandler? ThemeChanged;

        internal List<Language> AvailableLanguages => LanguageManager.Instance.AvailableLanguages;
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
        private string exportDescription;
        internal string ExportDescription { 
            get => exportDescription;
            set => SetProperty(ref exportDescription, value);
        }
        
        public SettingsPageViewModel(ISettingsManager settingsManager, UserSettings settings)
        {
            this.settingsManager = settingsManager;
            this.settings = settings;
            settingsManager.SettingChanged += SettingsManager_ThemeChanged;
        }

        [RelayCommand]
        private async Task ExportAsJson()
        {
            ExportDescription = "";
            FolderPicker openPicker = new FolderPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add("*");

            var window = (Application.Current as App)?.CurrentWindow as MainWindow;
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            StorageFolder folder = await openPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                var fileName = "Todo.json";
                var outputFile = string.Format("{0}/{1}", folder.Path.Replace("\\", "/").Replace("\"", "/"), fileName);
                Ioc.Default.GetRequiredService<IDataAccess>().ExportToJson(outputFile);
                ExportDescription = outputFile;
            }
            else
            {
                ExportDescription = "Canceled";
            }
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
