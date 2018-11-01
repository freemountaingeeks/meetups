using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Warehouse.Themes.Resources;

namespace Warehouse.Converter
{
    public class Percent2SolidBrushConverter : MarkupExtension, IValueConverter
    {
        #region Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((double)value > 80.0)
            {
                return Application.Current.FindResource(Keys.GreenLevelBrush);
            }

            if ((double)value > 40.0)
            {
                return Application.Current.FindResource(Keys.YellowLevelBrush);
            }  
                      
            return Application.Current.FindResource(Keys.RedLevelBrush);
        }
        #endregion

        #region ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
        #endregion

        #region ProvideValue
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        #endregion
    }
}
