using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return 0;
            }
            double number = (double)value;
            return number.ToString("N1");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return 0F;
            }
            String number = value as String;
            return double.Parse(number, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}
