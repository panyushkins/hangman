using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hangman.DB;

namespace Hangman.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<string> _disabledLetters;
        private char _selectedLetter;
        private Category _selectedCategory;
        private IEnumerable<Category> _wordCategories;
        private int _guessesLeftCount;
        private string _wrongGuesses;

        private RelayCommand _loadCommand;
        private RelayCommand _newGameCommand;
        private RelayCommand _exitCommand;
        private DateTime _dateTimeStart;
        private DateTime _dateTimeEnd;
        private TimeSpan _elapsedTime;
        private bool _isStatVisible;
        private HangState _currentHangState;

        public DateTime DateTimeStart
        {
            get { return _dateTimeStart; }
            set
            {
                if (Equals(_dateTimeStart, value)) return;
                _dateTimeStart = value;
                RaisePropertyChanged(() => DateTimeStart);
            }
        }

        public DateTime DateTimeEnd
        {
            get { return _dateTimeEnd; }
            set
            {
                if (Equals(_dateTimeEnd, value)) return;
                _dateTimeEnd = value;
                RaisePropertyChanged(() => DateTimeEnd);
            }
        }

        public TimeSpan ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                if (Equals(_elapsedTime, value)) return;
                _elapsedTime = value;
                RaisePropertyChanged(() => ElapsedTime);
            }
        }

        public bool IsStatVisible
        {
            get { return _isStatVisible; }
            set
            {
                if (Equals(_isStatVisible, value)) return;
                _isStatVisible = value;
                RaisePropertyChanged(() => IsStatVisible);
            }
        }

        public MainViewModel()
        {
            DisabledLetters = new ObservableCollection<string>();
            LettersField = new LettersField();
        }

        public ObservableCollection<string> DisabledLetters
        {
            get { return _disabledLetters; }
            set
            {
                if (Equals(_disabledLetters, value)) return;
                _disabledLetters = value;
                RaisePropertyChanged(() => DisabledLetters);
            }
        }

        public LettersField LettersField { get; set; }

        public RelayCommand LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = new RelayCommand(() =>
                {
                    using (var context = new HangmanContext())
                    {
                        var allCat = context.Categories.Include(c => c.Words).ToList();
                        WordCategories = allCat;
                        SelectedCategory = allCat.FirstOrDefault();
                    }
                }));
            }
        }

        public RelayCommand NewGameCommand
        {
            get
            {
                return _newGameCommand ?? (_newGameCommand = new RelayCommand(() =>
                {
                    IsStatVisible = false;
                    Reset();
                    SelectWord();
                    DateTimeStart = DateTime.Now;
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

        public char SelectedLetter
        {
            get { return _selectedLetter; }
            set
            {
                _selectedLetter = value;
                ProcessLetter();
            }
        }

        public IEnumerable<Category> WordCategories
        {
            get { return _wordCategories; }
            set
            {
                if (Equals(_wordCategories, value)) return;
                _wordCategories = value;
                RaisePropertyChanged(() => WordCategories);
            }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (Equals(_selectedCategory, value)) return;
                _selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
            }
        }

        public HangState CurrentHangState
        {
            get { return _currentHangState; }
            set
            {
                if (Equals(_currentHangState, value)) return;
                 _currentHangState = value;
                RaisePropertyChanged(() => CurrentHangState);
            }
        }

        public string WrongGuesses
        {
            get { return _wrongGuesses; }
            set
            {
                if (Equals(_wrongGuesses, value)) return;
                _wrongGuesses = value;
                RaisePropertyChanged(() => WrongGuesses);
            }
        }

        public int GuessesLeftCount
        {
            get { return _guessesLeftCount; }
            set
            {
                if (Equals(_guessesLeftCount, value)) return;
                _guessesLeftCount = value;
                RaisePropertyChanged(() => GuessesLeftCount);
            }
        }

        private int HangStateSize => (Enum.GetValues(typeof(HangState)).Length - 1);

        private void Reset()
        {
            CurrentHangState = HangState.None;
            GuessesLeftCount = HangStateSize;
            DisabledLetters.Clear();
            LettersField.ResetField();
            WrongGuesses = null;
        }

        private void ProcessLetter()
        {
            var letter = new ExtendedLetterLabel(SelectedLetter, LabelState.Visible);
            var lFieldLabel = LettersField.GetLetterLabel(letter.Symbol);
            DisabledLetters.Add(SelectedLetter.ToString());
            if (lFieldLabel != null)
            {
                lFieldLabel.LabelState = LabelState.Visible;
                if (LettersField.IsGuessedWord)
                {
                    ShowStatistics();
                    MessageBox.Show("Вы победили!", "Поздавления", MessageBoxButton.OK, MessageBoxImage.Information);
                    Reset();
                }
            }
            else
            {
                if (CurrentHangState != HangState.RightLeg)
                    CurrentHangState++;
                GuessesLeftCount = HangStateSize - (int)CurrentHangState;
                WrongGuesses += string.IsNullOrWhiteSpace(WrongGuesses) ? SelectedLetter.ToString() : "," + SelectedLetter;
                if (CurrentHangState == HangState.RightLeg)
                {
                    ShowStatistics();
                    LettersField.BoldMissedLetters();
                    MessageBox.Show("Вы проиграли!", "Соболезнования", MessageBoxButton.OK, MessageBoxImage.Error);
                    Reset();
                }
            }
        }

        private void ShowStatistics()
        {
            DateTimeEnd = DateTime.Now;
            ElapsedTime = DateTimeEnd - DateTimeStart;
            IsStatVisible = true;
        }

        private void SelectWord()
        {
            var allWords = SelectedCategory.Words.ToList();
            if (allWords.Count == 0)
            {
                MessageBox.Show("Отстутствуют слова в базе. Дальнейшая игра невозможна.");
                return;
            }
            var r = new Random();
            var currentWord = allWords[r.Next(0, allWords.Count - 1)];
            foreach (var symbol in currentWord.Text)
            {
                LettersField.AddSymbol(symbol);
            }
        }
    }

    public enum HangState
    {
        None,
        Piller,
        Rope,
        Head,
        Body,
        LeftHand,
        RightHand,
        LeftLeg,
        RightLeg
    }
}