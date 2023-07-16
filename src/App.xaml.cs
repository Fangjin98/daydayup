using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using DayDayUp.Core.Settings;
using DayDayUp.Services;
using DayDayUp.ViewModels;
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
            .AddSingleton<ISettingsProvider, SettingsProvider>()
            .AddSingleton<LanguageManager>()
            .AddSingleton<TodoManager>()
            .AddTransient<HomePageViewModel>()
            .AddTransient<ArchivePageViewModel>()
            .AddTransient<SettingsPageViewModel>()
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
        string? userdefinedLanguage = Ioc.Default.GetRequiredService<ISettingsProvider>().GetSetting(PredefinedSettings.Language);
        LanguageDefinition languageDefinition
            = LanguageManager.Instance.AvailableLanguages.FirstOrDefault(l => string.Equals(l.InternalName, userdefinedLanguage))
            ?? LanguageManager.Instance.AvailableLanguages[0];
        LanguageManager.Instance.SetCurrentCulture(languageDefinition);
    }

    private Window _window;
}
