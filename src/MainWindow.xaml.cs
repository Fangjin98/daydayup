using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUICommunity.Common.Helpers;
using WinUIEx;

namespace DayDayUp;


public sealed partial class MainWindow : Window
{
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

    private void AlwaysOnTopButton_Checked(object sender, RoutedEventArgs e)
    {
        this.SetIsAlwaysOnTop(true);
    }

    private void AlwaysOnTopButton_Unchecked(object sender, RoutedEventArgs e)
    {
        this.SetIsAlwaysOnTop(false);
    }
}
