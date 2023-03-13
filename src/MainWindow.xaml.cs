using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUICommunity.Common.Helpers;

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
}
