using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.Core.Settings;
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
    public ThemeManager themeManager { get; set; }
    public Window MWindow => m_window;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        Ioc.Default.ConfigureServices(
            new ServiceCollection()
            .AddSingleton<IDataAccess, LiteDbDataAccess>()
            .AddSingleton<ISettingsProvider, SettingsProvider>()
            .AddSingleton<ThemeSelector>()
            .AddSingleton<LanguageManager>()
            .AddSingleton<TodoManager>()
            .AddTransient<HomePageViewModel>()
            .AddTransient<ArchivePageViewModel>()
            .AddTransient<SettingsPageViewModel>()
            .BuildServiceProvider());
        
        m_window = new MainWindow();

        themeManager = new ThemeManager(m_window, new ThemeOptions
        {
            BackdropType = BackdropType.MicaAlt,
            ElementTheme = ElementTheme.Default,
            TitleBarCustomization = new TitleBarCustomization
            {
                TitleBarType = TitleBarType.AppWindow
            }
        });
        Ioc.Default.GetRequiredService<ThemeSelector>().SetRequestedTheme();

        string? userdefinedLanguage = Ioc.Default.GetRequiredService<ISettingsProvider>().GetSetting(PredefinedSettings.Language);
        LanguageDefinition languageDefinition
            = LanguageManager.Instance.AvailableLanguages.FirstOrDefault(l => string.Equals(l.InternalName, userdefinedLanguage))
            ?? LanguageManager.Instance.AvailableLanguages[0];
        LanguageManager.Instance.SetCurrentCulture(languageDefinition);
        
        m_window.SetWindowSize(500, 600); // WinUIEx method
        m_window.CenterOnScreen();
        m_window.Activate();
    }

    private Window m_window;
}
