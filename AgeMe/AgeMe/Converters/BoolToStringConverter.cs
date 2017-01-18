using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgeMe.Converters
{
    public class BoolToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;
            Boolean.TryParse(value.ToString(), out result);
            if (result)
                return "yes";
            return "no";
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            string param = value as string;

            if (param == "yes")
                return true;
            else
                return false;
        }
    }
}
