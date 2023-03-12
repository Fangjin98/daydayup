using DayDayUp.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace DayDayUp.Views.Items
{
    public sealed partial class SettingsPage : Page
    {

        public SettingsPage()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<SettingsPageViewModel>();
        }

        public SettingsPageViewModel ViewModel => (SettingsPageViewModel)DataContext;
    }
}
