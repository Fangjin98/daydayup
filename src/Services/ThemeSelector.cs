using DayDayUp.Core.Settings;
using Microsoft.UI.Xaml;
using System;
using WinUICommunity.Common.Helpers;

namespace DayDayUp.Services;

public class ThemeSelector
{
    public AppTheme CurrentSystemTheme { get; private set; }

    public AppTheme CurrentTheme => _settingsProvider.GetSetting(PredefinedSettings.Theme);

    public event EventHandler? ThemeChanged;

    public ThemeSelector(ISettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
        CurrentSystemTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark ? AppTheme.Dark : AppTheme.Light;

        _settingsProvider.SettingChanged += SettingsProvider_SettingChanged;
    }

    public void SetRequestedTheme()
    {
        AppTheme theme = CurrentTheme;

        if (theme == AppTheme.Default)
        {
            theme = CurrentSystemTheme;
        }

        if (theme == AppTheme.Dark)
        {
            ThemeHelper.ChangeTheme(ElementTheme.Dark);
        }
        else if (theme == AppTheme.Light)
        {
            ThemeHelper.ChangeTheme(ElementTheme.Light);
        }
    }

    private void SettingsProvider_SettingChanged(object sender, SettingChangedEventArgs e)
    {
        if (string.Equals(PredefinedSettings.Theme.Name, e.SettingName, StringComparison.Ordinal))
        {
            SetRequestedTheme();
            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private readonly ISettingsProvider _settingsProvider;
}

public enum AppTheme
{
    Default,
    Light,
    Dark
}
