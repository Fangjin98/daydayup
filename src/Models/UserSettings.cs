namespace DayDayUp.Models;

public class UserSettings
{
    public SettingDefinition<string> Language { get; set; }
    public SettingDefinition<AppTheme> Theme { get; set; }

    public UserSettings() {
        Language = new SettingDefinition<string>(nameof(Language),"en");
        Theme = new SettingDefinition<AppTheme>(nameof(Theme),  AppTheme.Default);
    }
}

public readonly struct SettingDefinition<T>
{
    public string Title { get; }
    public T DefaultValue { get; }

    public SettingDefinition(string title, T defaultValue)
    { 
        Title = title;
        DefaultValue = defaultValue;
    }
}
