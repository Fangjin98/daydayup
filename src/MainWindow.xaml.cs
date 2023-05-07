using DayDayUp.Services;
using DayDayUp.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using WinUICommunity;
using WinUIEx;

namespace DayDayUp;


public sealed partial class MainWindow : Window
{
    public Grid ApplicationTitleBar => CustomTitleBar;
    public NavigationManager navigationManager { get; set; }
    internal static MainWindow Instance { get; private set; }
    internal ShellPageStrings Strings => LanguageManager.Instance.ShellPage;

    public MainWindow()
    {
        InitializeComponent();
        Instance = this;

        var titleBarHelper = TitleBarHelper.Initialize(this, TitleTextBlock,
            CustomTitleBar,
            LeftPaddingColumn,
            IconColumn,
            TitleColumn,
            LeftDragColumn,
            SearchColumn,
            RightDragColumn,
            RightPaddingColumn);
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

    private void AlwaysOnTopButton_Checked(object sender, RoutedEventArgs e)
    {
        this.SetIsAlwaysOnTop(true);
    }

    private void AlwaysOnTopButton_Unchecked(object sender, RoutedEventArgs e)
    {
        this.SetIsAlwaysOnTop(false);
    }
}
