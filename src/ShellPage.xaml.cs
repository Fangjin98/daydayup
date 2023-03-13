using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.ViewModels;
using DayDayUp.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Data.Xml.Dom;
using WinUICommunity.Common.ViewModel;

namespace DayDayUp;

public sealed partial class ShellPage : Page
{
    public static ShellPage Instance { get; private set; }

    public ShellPageViewModel ViewModel => (ShellPageViewModel) DataContext;

    public ShellPage()
    {
        this.InitializeComponent();

        DataContext = new ShellPageViewModel();

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
