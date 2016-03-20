using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for LettersMonitor.xaml
    /// </summary>
    public partial class LettersMonitor : UserControl
    {
        public LettersMonitor()
        {
            InitializeComponent();

            ListBoxLetters.ItemsSource = Letters;
        }

        public static readonly DependencyProperty LettersProperty =
            DependencyProperty.Register(
                "Letters", typeof(IEnumerable<ExtendedLetterLabel>),
                typeof(LettersMonitor), new PropertyMetadata(LettersChanged));

        public IEnumerable<ExtendedLetterLabel> Letters
        {
            get { return (IEnumerable<ExtendedLetterLabel>) GetValue(LettersProperty); }
            set { SetValue(LettersProperty, value); }
        } 

        private static void LettersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var keyboard = d as LettersMonitor;
            if (keyboard == null) return;

            var eLetters = e.NewValue as IEnumerable<ExtendedLetterLabel>;
            if (eLetters == null) return;

            keyboard.ListBoxLetters.ItemsSource = eLetters;
        }
    }

    public class ExtendedLetterLabel : INotifyPropertyChanged
    {
        private LabelState _labelState;

        public ExtendedLetterLabel(char symbol)
        {
            Symbol = symbol;
            LabelState = LabelState.Hidden;
        }

        public ExtendedLetterLabel(char symbol, LabelState labelState)
        {
            Symbol = symbol;
            LabelState = labelState;
        }

        public char Symbol { get; set; }

        public LabelState LabelState
        {
            get { return _labelState; }
            set
            {
                if (value == _labelState) return;
                _labelState = value;
                OnPropertyChanged("LabelState");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum LabelState
    {
        Visible,
        Hidden,
        Bold
    }
}
