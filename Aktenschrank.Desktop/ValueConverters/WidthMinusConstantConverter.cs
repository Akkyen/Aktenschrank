using System.Globalization;
using System.Windows.Data;

namespace Aktenschrank.Desktop.ValueConverters;

public class WidthMinusConstantConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double actualWidth = (double)value;
        double constant = System.Convert.ToDouble(parameter);
        return actualWidth - constant;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}