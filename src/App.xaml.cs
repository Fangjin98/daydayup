using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.Core.Settings;
using DayDayUp.Helpers;
using DayDayUp.Services;
using DayDayUp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using WinUICommunity.Common.Helpers;

namespace DayDayUp;

public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();

        ThemeHelper.Initialize(m_window, BackdropType.Mica);

        Ioc.Default.ConfigureServices(
            new ServiceCollection()
            .AddSingleton<IDataAccess, LiteDbDataAccess>()
            .AddSingleton<ISettingsProvider, SettingsProvider>()
            .AddSingleton<ThemeSelector>()
            .AddSingleton<TodoManagementHelper>()
            .AddTransient<HomePageViewModel>()
            .AddTransient<ArchivePageViewModel>()
            .AddTransient<SettingsPageViewModel>()
            .BuildServiceProvider());
        Ioc.Default.GetRequiredService<ThemeSelector>().SetRequestedTheme();

        m_window.Activate();
    }

    private Window m_window;
}
