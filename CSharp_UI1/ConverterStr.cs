using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CSharp_UI1
{
    public class ConverterStr : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double minAll_EP_to_HA = 0;
            double minAll_LA_to_HA = 0;
            string output = string.Empty;
            minAll_EP_to_HA = System.Convert.ToDouble(values[0]);
            minAll_LA_to_HA = System.Convert.ToDouble(values[1]);
            output = $"\n\tminAll_EP_to_HA = {string.Format("{0:f5}", minAll_EP_to_HA)}\n\tminAll_LA_to_HA = {string.Format("{0:f5}", minAll_LA_to_HA)}";
            return output;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
