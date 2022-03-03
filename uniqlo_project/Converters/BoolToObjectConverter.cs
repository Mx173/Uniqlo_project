using System;
using System.Globalization;
using Xamarin.Forms;

namespace uniqlo_project.Converters
{
    public class BoolToObjectConverter : IValueConverter
    {
        public object TrueObj { get; set; }
        public object FalseObj { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return TrueObj;
            }
            else
            {
                return FalseObj;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
