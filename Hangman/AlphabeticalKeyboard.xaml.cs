using System.Collections.Generic;
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
                "SelectedLetter", typeof(string),
                typeof(AlphabeticalKeyboard), new PropertyMetadata(null));

        public static readonly DependencyProperty DisabledLettersProperty =
            DependencyProperty.Register(
                "DisabledLetters", typeof(IEnumerable<string>),
                typeof(AlphabeticalKeyboard), new PropertyMetadata(DisabledButtonsChanged));

	    private static void DisabledButtonsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	    {
            var dLetters = e.NewValue as IEnumerable<string>;
            if (dLetters == null) return;
            var keyboard = d as AlphabeticalKeyboard;
            if (keyboard == null) return;
	        var dLettersList = dLetters.ToList();
	        foreach (var letter in keyboard._lettersList)
	        {
                var firstLetter = dLettersList.SingleOrDefault(l => l == letter.Symbol);
	            letter.Enabled = firstLetter == null;
	        }
        }

        public string SelectedLetter
        {
            get
            {
                return (string)GetValue(SelectedLetterProperty);
            }
            set
            {
                SetValue(SelectedLetterProperty, value);
            }
        }

        public IEnumerable<string> DisabledLetters
        {
            get
            {
                return (IEnumerable<string>)GetValue(DisabledLettersProperty);
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
                SelectedLetter = btn.Content.ToString();
	        }
	    }

        private class LetterLabel
        {
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

            public string Symbol { get; set; }

            public bool Enabled { get; set; }
        }
    }
}