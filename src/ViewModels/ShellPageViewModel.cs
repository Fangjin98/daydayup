using DayDayUp.Services;
using WinUICommunity.Common.ViewModel;

namespace DayDayUp.ViewModels;

public class ShellPageViewModel : ShellViewModel
{
    internal ShellPageStrings Strings => LanguageManager.Instance.ShellPage;

    public ShellPageViewModel() : base()
    {
        
    }
}
