using DayDayUp.Views.Items;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUICommunity.Common.Helpers;

namespace DayDayUp
{
    public class Scenario
    {
        public string Title { get; set; }
        public string ClassName { get; set; }
        public string Icon { get; set; }
    }

    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public List<Scenario> Scenarios => scenarios;

        public Grid ApplicationTitleBar => CustomTitleBar;

        public MainWindow()
        {
            this.InitializeComponent();

            TitleBarHelper.Initialize(this, TitleTextBlock, 
                CustomTitleBar, 
                LeftPaddingColumn, 
                IconColumn, 
                TitleColumn, 
                LeftDragColumn, 
                SearchColumn, 
                RightDragColumn, 
                RightPaddingColumn);
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Scenario item in scenarios)
            {
                NavView.MenuItems.Add(new NavigationViewItem
                {
                    Content = item.Title,
                    Tag = item.ClassName,
                    Icon = new FontIcon() { FontFamily = new("Segoe MDL2 Assets"), Glyph = item.Icon }
                });
            }


            // NavView doesn't load any page by default, so load home page.
            NavView.SelectedItem = NavView.MenuItems[0];

            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            if (scenarios is not null && scenarios.Count > 0)
            {
                NavView_Navigate(scenarios.First().ClassName, new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
            }
        }

        private void NavView_Navigate(string navItemTag, Microsoft.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            Type page;

            if (navItemTag == nameof(SettingsPage))
            {
                page = typeof(SettingsPage);
            }
            else
            {
                Scenario item = scenarios.First(p => p.ClassName.Equals(navItemTag));
                page = Type.GetType(item.ClassName);
            }

            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (page is not null && !Type.Equals(preNavPageType, page))
            {
                ContentFrame.Navigate(page, null, transitionInfo);
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var naViewItemInvoked = (NavigationViewItem)args.InvokedItemContainer;

            if (args.IsSettingsInvoked)
            {
                NavView_Navigate(nameof(SettingsPage), args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer is not null)
            {
                var navItemTag = args.InvokedItemContainer.Tag?.ToString();
                if (!string.IsNullOrEmpty(navItemTag))
                {
                    NavView_Navigate(navItemTag, new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
                }
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                NavView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = scenarios.First(p => p.ClassName == e.SourcePageType.FullName);
                var menuItems = NavView.MenuItems;

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.ClassName));

                NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();

            }
        }

        private readonly List<Scenario> scenarios = new()
        {
            new Scenario() { Title = "Home", ClassName = typeof(HomePage).FullName, Icon = "\uE80F" },
            new Scenario() { Title = "Archive", ClassName = typeof(ArchivePage).FullName, Icon = "\uE8B7" }
        };
    }
}
