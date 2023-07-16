using System.Globalization;
using DayDayUp.Services;

namespace DayDayUp.Models;

public class ApplicationLanguage
{
    public string InternalName { get; }
    public string Identifier { get; }
    public string DisplayName { get; }
    public CultureInfo Culture { get; }

    public ApplicationLanguage()
        : this(null)
    {
    }

    public ApplicationLanguage(string identifier)
    {
        if (string.IsNullOrEmpty(identifier))
        {
            Culture = new CultureInfo(Windows.System.UserProfile.GlobalizationPreferences.Languages[0]);
            DisplayName = new SettingsPageStrings().DefaultLanguage;
            InternalName = "default";
        }
        else
        {
            Culture = new CultureInfo(identifier!);
            DisplayName = Culture.NativeName;
            InternalName = Culture.Name;
        }

        Identifier = Culture.Name;
    }
}