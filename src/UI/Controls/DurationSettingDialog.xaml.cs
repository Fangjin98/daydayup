﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;


namespace DayDayUp.Controls;

public sealed partial class DurationSettingDialog : UserControl
{
    public DurationSettingDialog(int duration)
    {
        this.InitializeComponent();

        durationPicker = new DurationPicker(duration);
        
        for(int i = 0; i <= 60; i++)
        {
            offsetValues.Add(i.ToString()); 
        }

        for (int i = 0; i < 8; i++)
        {
            dayOffsetValues.Add(i.ToString());
        }

    }
    public int DurationResult
    {
        get => durationPicker.Duration;
    }
    private DurationPicker durationPicker;

    private List<string> offsetValues = new List<string>();

    private List<string> dayOffsetValues = new List<string>();

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
