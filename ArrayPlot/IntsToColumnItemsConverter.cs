using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace ArrayPlot
{
    class IntsToColumnItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IList<int> ints))
                throw new InvalidCastException();

            var ret = new ColumnItem[ints.Count];

            for (int i = 0; i < ints.Count; i++)            
                ret[i] = new ColumnItem { Value = ints[i] };

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
