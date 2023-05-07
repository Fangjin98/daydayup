using DayDayUp.Services;
using DayDayUp.ViewModels;
using DayDayUp.Views;
using Microsoft.UI.Xaml.Controls;
using System;
using WinUICommunity;

namespace DayDayUp;

public sealed partial class ShellPage : Page
{
    internal ShellPageStrings Strings => LanguageManager.Instance.ShellPage;
    public static ShellPage Instance { get; private set; }
    public NavigationManager navigationManager { get; set; }

    public ShellPage()
    {
        InitializeComponent();
        navigationManager = new NavigationManager(NavView, new NavigationViewOptions
        {
            DefaultPage = typeof(HomePage),
            SettingsPage = typeof(SettingsPage)
        }, ContentFrame);
    }

    public void Navigate(string uniqeId)
    {
        Type pageType = Type.GetType(uniqeId);
        ContentFrame.Navigate(pageType);
    }
}
