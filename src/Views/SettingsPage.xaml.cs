using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

namespace DayDayUp.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsPageViewModel ViewModel => (SettingsPageViewModel)DataContext;

    public SettingsPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetService<SettingsPageViewModel>();
    }

    private async void OpenExportFolder_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ExportData.Description = "";
        FolderPicker openPicker = new FolderPicker();
        openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
        openPicker.FileTypeFilter.Add("*");

        var window = (Application.Current as App)?.MWindow as MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);
        
        StorageFolder folder = await openPicker.PickSingleFolderAsync();
        if (folder != null)
        {
            StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
            var fileName = "Todo.json";
            var outputFile = string.Format("{0}/{1}",folder.Path.Replace("\\", "/").Replace("\"", "/"),fileName); 
            ViewModel.ExportToJson(outputFile);
            ExportData.Description = string.Format("{0} {1}",ViewModel.Strings.ExportAs,outputFile);
        }
        else
        {
            ExportData.Description = ViewModel.Strings.OperationCanceled;
        }

    }
}
