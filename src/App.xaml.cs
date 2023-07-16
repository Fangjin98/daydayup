using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.Models;
using DayDayUp.Services;
using DayDayUp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System.Linq;
using WinUICommunity;
using WinUIEx;

namespace DayDayUp;

public partial class App : Application
{
    public Window CurrentWindow => _window;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        ConfigureServices();
       
        _window = new MainWindow();
        
        InitApplicationTheme();
        InitApplicationLanguage();

        _window.SetWindowSize(500, 600); // WinUIEx method
        _window.CenterOnScreen();
        _window.Activate();
    }

    private void ConfigureServices()
    {
        Ioc.Default.ConfigureServices(
            new ServiceCollection()
            .AddSingleton<IDataAccess, LiteDbDataAccess>()
            .AddSingleton<ISettingsManager, ApplicationDataSettingsManager>()
            .AddSingleton<LanguageManager>()
            .AddSingleton<TodoManager>()
            .AddSingleton<UserSettings>()
            .AddSingleton<SettingsPageViewModel>()
            .AddTransient<HomePageViewModel>()
            .AddTransient<ArchivePageViewModel>()
            .BuildServiceProvider());
    }

    private void InitApplicationTheme()
    {
        ThemeManager.Initialize(_window, new ThemeOptions
        {
            BackdropType = BackdropType.Mica,
            ElementTheme = ElementTheme.Default,
            TitleBarCustomization = new TitleBarCustomization
            {
                TitleBarType = TitleBarType.AppWindow
            }
        });
    }

    private void InitApplicationLanguage()
    {
        string? userdefinedLanguage = Ioc.Default.GetRequiredService<ISettingsManager>().GetValue<string>("Language");
        Language languageDefinition
            = LanguageManager.Instance.AvailableLanguages.FirstOrDefault(l => string.Equals(l.InternalName, userdefinedLanguage))
            ?? LanguageManager.Instance.AvailableLanguages[0];
        LanguageManager.Instance.SetCurrentCulture(languageDefinition);
    }

    private Window _window;
}
