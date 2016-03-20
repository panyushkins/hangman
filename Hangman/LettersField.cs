using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hangman
{
    public class LettersField : INotifyPropertyChanged
    {
        private int _lettersCount;
        private IEnumerable<ExtendedLetterLabel> _letterItems;

        public LettersField()
        {
            LettersFieldCollection = new ObservableCollection<ExtendedLetterLabel>();
            LettersFieldCollection.CollectionChanged += (sender, args) =>
            {
                LettersCount = LettersFieldCollection.Count;
                LetterItems = LettersFieldCollection;
            };
        }

        private ObservableCollection<ExtendedLetterLabel> LettersFieldCollection { get; set; }

        public IEnumerable<ExtendedLetterLabel> LetterItems
        {
            get { return _letterItems; }
            set
            {
                if (Equals(value, _letterItems)) return;
                _letterItems = value;
                OnPropertyChanged("LetterItems");
            }
        }

        public bool IsGuessedWord
        {
            get
            {
                return LettersFieldCollection.All(l => l.LabelState == LabelState.Visible);
            }
        }

        public int LettersCount
        {
            get { return _lettersCount; }
            private set
            {
                if (value == _lettersCount) return;
                _lettersCount = value;
                OnPropertyChanged("LettersCount");
            }
        }

        public void ResetField()
        {
            LettersFieldCollection.Clear();
        }

        public ExtendedLetterLabel GetLetterLabel(char symbol)
        {
            return LettersFieldCollection.FirstOrDefault(l => l.Symbol == char.ToUpper(symbol));
        }

        public void BoldMissedLetters()
        {
            foreach (var l in LettersFieldCollection)
            {
                if (l.LabelState == LabelState.Hidden)
                    l.LabelState = LabelState.Bold;
            }
        }

        public void AddSymbol(char symbol)
        {
            LettersFieldCollection.Add(new ExtendedLetterLabel(Char.ToUpper(symbol)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
