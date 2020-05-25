using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

/*
 * https://www.wpf-tutorial.com/data-binding/value-conversion-with-ivalueconverter/?fbclid=IwAR2tt8Wogh1ids0Q9bLwylGzfDQH8MNjv4zTuXjqx1kvOmhT30fntRLgMyc
 */

namespace FFC.Services
{
    class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = value.ToString();
            return Int32.Parse(temp);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
