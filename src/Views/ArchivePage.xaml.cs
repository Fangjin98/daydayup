using DayDayUp.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DayDayUp.Views.Items
{
    public sealed partial class ArchivePage : Page
    {
        public ArchivePageViewModel ViewModel => (ArchivePageViewModel)DataContext;

        public ArchivePage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<ArchivePageViewModel>();
        }

        
    }
}
