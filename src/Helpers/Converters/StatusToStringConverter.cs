using DayDayUp.Models;
using Microsoft.UI.Xaml.Data;
using System;

namespace DayDayUp.Helpers.Converters
{
    public class StatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var status = (value as TodoStatus?);
            return status == TodoStatus.Doing ? "Doing" : "Pause";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
