using DayDayUp.Models;
using Microsoft.UI.Xaml.Data;
using System;

namespace DayDayUp.UI.Converters
{
    public class StatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var status = (value as TodoStatus?);
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            return status == TodoStatus.Doing ? loader.GetString("DoingStatus") : loader.GetString("PauseStatus");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
