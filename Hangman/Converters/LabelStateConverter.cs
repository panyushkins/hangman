using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Hangman.Converters
{
    public class LabelStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = value as LabelState?;
            if (state.HasValue)
            {
                if (targetType == typeof(Brush))
                {
                    switch (state)
                    {
                        case LabelState.Bold:
                            return new SolidColorBrush(Colors.DarkRed);
                        case LabelState.Visible:
                        case LabelState.Hidden:
                        case null:
                            return new SolidColorBrush(Colors.Black);
                        default:
                            return new SolidColorBrush(Colors.Black);
                    }
                }
            }
            state = parameter as LabelState?;
            if (state.HasValue)
            {
                if (targetType == typeof (object))
                {
                    if (state == LabelState.Hidden)
                        return "_";
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
