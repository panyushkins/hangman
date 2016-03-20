using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Hangman.ViewModel;

namespace Hangman.Converters
{
    class HangmanVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var paramHangState = (HangState)parameter;
            var currentHangState = (HangState)value;
            if (currentHangState >= paramHangState)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
