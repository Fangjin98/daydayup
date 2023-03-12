using Microsoft.UI.Xaml.Data;
using System;

namespace DayDayUp.Helpers.Converters
{
    public class ProgressToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var progress = (value as int?);
            return progress.ToString() + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
