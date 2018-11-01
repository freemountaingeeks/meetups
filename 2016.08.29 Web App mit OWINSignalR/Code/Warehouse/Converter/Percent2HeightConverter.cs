using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Warehouse.Converter
{
    public class Percent2HeightConverter : MarkupExtension, IMultiValueConverter
    {
        #region Convert
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double percent = (double) values[0];
            double actualHeight = (double)values[1];
            if (actualHeight > 0.0)
            {
                return percent / 100 * actualHeight;
            }
            return 0.0;
        }
        #endregion

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
