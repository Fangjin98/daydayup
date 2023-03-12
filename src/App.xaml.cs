// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.Helpers;
using DayDayUp.Services;
using DayDayUp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using System;
using WinRT.Interop;
using WinUICommunity.Common.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DayDayUp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();

            ThemeHelper.Initialize(m_window,BackdropType.Mica);
         
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<ISettingsService, SettingsHelper>()
                .AddSingleton<IDataAccess, LiteDbDataAccess>()
                .AddSingleton<TodoManagementHelper>()
                .AddTransient<HomePageViewModel>()
                .AddTransient<ArchivePageViewModel>()
                .AddTransient<SettingsPageViewModel>()
                .BuildServiceProvider());

            m_window.Activate();
        }

        private Window m_window;
    }
}
