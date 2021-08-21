using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ControlEx.Converters
{
    public class BoolToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)value;
            BoolToVisiblityConverterMode mode;
            Enum.TryParse(parameter.ToString(), out mode);
            switch (mode)
            {
                case BoolToVisiblityConverterMode.FalseCollapsed:
                    if (b == false) return Visibility.Collapsed;
                    break;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public enum BoolToVisiblityConverterMode
    {

        FalseCollapsed,
    }
}
