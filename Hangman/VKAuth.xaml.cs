using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hangman
{
    /// <summary>
    /// Логика взаимодействия для VKAuth.xaml
    /// </summary>
    public partial class VKAuth : Window
    {
        public string AccessToken { get; set; }
        public VKAuth()
        {
            InitializeComponent();
          //  string appid = "5340623";
            VK_Auth.Navigate(new Uri("https://oauth.vk.com/authorize?client_id=5340623&display=popup&redirect_uri=http://oauth.vk.com/blank.html&scope=wall&response_type=token&v=5.45", UriKind.Absolute));

        }

        private void VK_Auth_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.ToString().StartsWith("https://oauth.vk.com/blank.html#access_token="))
            {
                MessageBox.Show(e.Uri.ToString());
            }
        }
    }
}
