using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using static CommonDatabase.Data.Operator;

namespace TabletManagerWPF.ValueConverters
{
    internal class PositionToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
    
            switch (value)
            {
                //case null:return new SolidColorBrush(Colors.Black);
                case PositionEnum.Service:return new SolidColorBrush(Colors.Violet);
                case PositionEnum.SL: return new SolidColorBrush(Colors.Red);
                case PositionEnum.Normal: return new SolidColorBrush(Colors.Black) ;
                case PositionEnum.TL: return new SolidColorBrush(Colors.DarkOrange);
                default:
                    return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
