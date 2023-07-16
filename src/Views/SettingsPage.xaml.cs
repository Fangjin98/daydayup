using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace DayDayUp.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsPageViewModel ViewModel => (SettingsPageViewModel)DataContext;

    public SettingsPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetService<SettingsPageViewModel>();
    }
}
