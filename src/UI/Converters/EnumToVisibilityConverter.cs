﻿using DayDayUp.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace DayDayUp.UI.Converters
{
    public class EnumToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var status = value as TodoStatus?;
            return status.Value == TodoStatus.Doing ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var status = value as Visibility?;
            return status.Value == Visibility.Visible ? TodoStatus.Doing : TodoStatus.Pause;
        }
    }

    public class EnumToVisibilityNegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var status = value as TodoStatus?;
            return status.Value == TodoStatus.Pause ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var status = value as Visibility?;
            return status.Value == Visibility.Visible ? TodoStatus.Pause : TodoStatus.Doing;
        }
    }
}
