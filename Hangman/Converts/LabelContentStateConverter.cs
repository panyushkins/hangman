using System;
using System.Globalization;
using System.Windows.Data;

namespace Hangman.Converters
{
    class LabelContentStateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var state = values[1] as LabelState?;
            if (state.HasValue)
            {
                if (targetType == typeof(object))
                {
                    if (state == LabelState.Hidden)
                        return "__";
                }
            }
            return values[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
