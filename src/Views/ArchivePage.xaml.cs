using DayDayUp.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace DayDayUp.Views;

public sealed partial class ArchivePage : Page
{
    public ArchivePageViewModel ViewModel => (ArchivePageViewModel) DataContext;

    public ArchivePage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetService<ArchivePageViewModel>();
    }

    private void DataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.LoadTodoCommand.ExecuteAsync(null);
    }
}
