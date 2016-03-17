using System.Collections.Generic;
using System.Net.Mime;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Hangman.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEnumerable<string> _disabledLetters;
        private string _selectedLetter;
        private RelayCommand _newGameCommand;
        private RelayCommand _exitCommand;

        public MainViewModel()
        {
            DisabledLetters = new[] {"À", "Á"};
            LettersField = new List<LetterField>();
        }

        public IEnumerable<string> DisabledLetters
        {
            get { return _disabledLetters; }
            set
            {
                if (Equals(_disabledLetters, value)) return;
                _disabledLetters = value;
                RaisePropertyChanged(() => DisabledLetters);
            }
        }

        public List<LetterField> LettersField { get; set; }

        public RelayCommand NewGameCommand
        {
            get { return _newGameCommand ?? (_newGameCommand = new RelayCommand(() =>
                {
                    
                }));
            }
        }

        public RelayCommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new RelayCommand(() =>
                {
                    Application.Current.Shutdown();
                }));
            }
        }

        public string SelectedLetter
        {
            get { return _selectedLetter; }
            set
            {
                _selectedLetter = value;
                ProcessLetter();
            }
        }

        private void ProcessLetter()
        {
            var letter = new LetterField(SelectedLetter);
            LettersField.Add(letter);
        }
    }

    public class LetterField
    {
        public LetterField(string letter)
        {
            Letter = letter;
            LetterFieldStatus = LetterFieldStatus.Guessed;
        }

        public LetterField(string letter, LetterFieldStatus letterFieldStatus)
        {
            Letter = letter;
            LetterFieldStatus = letterFieldStatus;
        }

        public string Letter { get; set; }
        
        public LetterFieldStatus  LetterFieldStatus { get; set; }
    }

    public enum LetterFieldStatus
    {
        Guessed,
        Hidden,
        Loosed
    }
}