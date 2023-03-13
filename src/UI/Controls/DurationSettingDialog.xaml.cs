using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;

namespace DayDayUp.Controls;

public sealed partial class DurationSettingDialog : UserControl
{
    public int DurationResult { get => durationPicker.Duration; }

    public DurationSettingDialog(int duration)
    {
        this.InitializeComponent();

        durationPicker = new DurationPicker(duration);
    }

    private DurationPicker durationPicker;
    private List<string> minuteValues = (from minute in Enumerable.Range(0, 60) select minute.ToString()).ToList();
    private List<string> hourValues = (from hour in Enumerable.Range(0, 24) select hour.ToString()).ToList();
    private List<string> dayValues = (from day in Enumerable.Range(0, 8) select day.ToString()).ToList();
}

internal class DurationPicker : ObservableObject
{
    public DurationPicker(int duration)
    {
        this.duration = duration;
    }

    private int duration;
    public int Duration
    {
        get => duration;
        set=> SetProperty(ref duration, value);
    }

    private int daysOffset;
    public int DaysOffset
    {
        get {
            return Duration / 24 / 60;
        }
        set
        {
            daysOffset = value;
            Duration = daysOffset * 24 * 60 + HoursOffset * 60 + MinsOffset;
        }
    }

    private int hoursOffset;
    public int HoursOffset
    {
        get
        {
            return (Duration-DaysOffset*24*60)/ 60;
        }
        set
        {
            hoursOffset = value;
            Duration = DaysOffset * 24 * 60 + hoursOffset * 60 + MinsOffset;
        }
    }

    private int minsOffset;
    public int MinsOffset
    {
        get
        {
            return Duration - DaysOffset * 24 * 60 - HoursOffset * 60;
        }
        set
        {
            minsOffset = value;
            Duration = DaysOffset * 24 * 60 + HoursOffset * 60 + minsOffset;
        }
    }

}
