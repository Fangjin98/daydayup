using System.Globalization;

namespace DayDayUp.Services;

public class LanguageDefinition
{
    public string InternalName { get; }
    public string Identifier { get; }
    public string DisplayName { get; }
    public CultureInfo Culture { get; }

    public LanguageDefinition()
        : this(null)
    {
    }

    public LanguageDefinition(string? identifier)
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