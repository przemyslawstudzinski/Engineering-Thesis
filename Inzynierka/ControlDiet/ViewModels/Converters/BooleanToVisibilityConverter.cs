using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        private enum Parameters
        {
            Normal, Inverted
        }

        private Visibility OnTrue { get; set; }
        private Visibility OnFalse { get; set; }

        public BooleanToVisibilityConverter()
        {
            OnFalse = Visibility.Collapsed;
            OnTrue = Visibility.Visible;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolValue = (bool)value;
            var direction = (Parameters)Enum.Parse(typeof(Parameters), (string)parameter);

            if (direction == Parameters.Inverted)
            {
                return !boolValue ? OnTrue : OnFalse;
            }
        
            return boolValue ? OnTrue : OnFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility == false)
            {
                return DependencyProperty.UnsetValue;
            }

            if ((Visibility)value == OnTrue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}