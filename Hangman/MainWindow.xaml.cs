using System.Data.Entity;
using System.Windows;
using Hangman.DB;

namespace Hangman
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HangmanDb = new HangmanContext();
        }

        private HangmanContext HangmanDb { get; set; }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //HangmanDb.Categories.Load();
            //LstCategories.ItemsSource = HangmanDb.Categories.Local;
            //LstCategories.DisplayMemberPath = "Name";

            //HangmanDb.Words.Load();
            //LstWords.ItemsSource = HangmanDb.Words.Local;
            //LstWords.DisplayMemberPath = "Text";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            VKAuth vk = new VKAuth();
            vk.Show();
        }
    }
}
