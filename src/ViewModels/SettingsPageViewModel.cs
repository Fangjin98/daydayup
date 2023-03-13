using CommunityToolkit.Mvvm.ComponentModel;
using DayDayUp.Core.Settings;
using DayDayUp.Services;

namespace DayDayUp.ViewModels
{
    public class SettingsPageViewModel : ObservableObject
    {
        public SettingsPageViewModel(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        internal AppTheme Theme
        {
            get => _settingsProvider.GetSetting(PredefinedSettings.Theme);
            set => _settingsProvider.SetSetting(PredefinedSettings.Theme, value);
        }

        private readonly ISettingsProvider _settingsProvider;
    }
}
