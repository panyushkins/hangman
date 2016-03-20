using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hangman
{
	public partial class AlphabeticalKeyboard : UserControl
	{
		public AlphabeticalKeyboard()
		{
			InitializeComponent();

		    _lettersList = _abc.Select(l => new LetterLabel(l.ToString())).ToList();
		    ListBoxLetters.ItemsSource = _lettersList;
		}

        private string _abc = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
	    private readonly List<LetterLabel> _lettersList;

        public static readonly DependencyProperty SelectedLetterProperty =
            DependencyProperty.Register(
                "SelectedLetter", typeof(char),
                typeof(AlphabeticalKeyboard), new PropertyMetadata(null));

        public static readonly DependencyProperty DisabledLettersProperty =
            DependencyProperty.Register(
                "DisabledLetters", typeof(ObservableCollection<string>),
                typeof(AlphabeticalKeyboard), new PropertyMetadata(DisabledButtonsChanged));

	    private static void DisabledButtonsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	    {
            var dLetters = e.NewValue as IEnumerable<string>;
            if (dLetters == null) return;
            var keyboard = d as AlphabeticalKeyboard;
            if (keyboard == null) return;
	        var dLettersList = dLetters.ToList();
            ProcessKeyboard(keyboard, dLettersList);
            keyboard.DisabledLetters.CollectionChanged += (sender, args) =>
	        {
	            switch (args.Action)
	            {
	                case NotifyCollectionChangedAction.Add:
                    case NotifyCollectionChangedAction.Remove:
                        ProcessKeyboard(keyboard, keyboard.DisabledLetters.ToList());
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        foreach (var letter in keyboard._lettersList)
                        {
                            letter.Enabled = true;
                        }
                        break;
                }
            };
	    }

	    private static void ProcessKeyboard(AlphabeticalKeyboard keyboard, List<string> dLettersList)
	    {
	        foreach (var letter in keyboard._lettersList)
	        {
	            var firstLetter = dLettersList.SingleOrDefault(l => l == letter.Symbol);
	            letter.Enabled = firstLetter == null;
	        }
	    }

	    public char SelectedLetter
        {
            get
            {
                return (char)GetValue(SelectedLetterProperty);
            }
            set
            {
                SetValue(SelectedLetterProperty, value);
            }
        }

        public ObservableCollection<string> DisabledLetters
        {
            get
            {
                return (ObservableCollection<string>)GetValue(DisabledLettersProperty);
            }
            set
            {
                SetValue(DisabledLettersProperty, value);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
	    {
	        var btn = sender as Button;
	        if (btn != null)
	        {
                SelectedLetter = Convert.ToChar(btn.Content);
	        }
	    }

        private sealed class LetterLabel : INotifyPropertyChanged
        {
            private bool _enabled;

            public LetterLabel(string symbol)
            {
                Symbol = symbol;
                Enabled = true;
            }

            public LetterLabel(string symbol, bool enabled)
            {
                Symbol = symbol;
                Enabled = enabled;
            }

            public string Symbol { get; }

            public bool Enabled
            {
                get { return _enabled; }
                set
                {
                    if (value == _enabled) return;
                    _enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}