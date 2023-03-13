using DayDayUp.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using WinUICommunity.Common.ViewModel;

namespace DayDayUp;

public sealed partial class ShellPage : Page
{
    public static ShellPage Instance { get; private set; }

    public ShellViewModel ViewModel { get; } = new ShellViewModel();

    public ShellPage()
    {
        this.InitializeComponent();
        
        Instance = this;

        ViewModel.InitializeNavigation(ContentFrame, NavView)
            .WithSettingsPage(typeof(SettingsPage))
            .WithDefaultPage(typeof(HomePage));
    }

    public void Navigate(string uniqeId)
    {
        Type pageType = Type.GetType(uniqeId);
        ContentFrame.Navigate(pageType);
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.OnLoaded();
    }

    private void navigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        ViewModel.OnItemInvoked(args);
    }
}
