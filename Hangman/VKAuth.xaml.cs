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
        public string User_id { get; set; }
        public VKAuth()
        {
            InitializeComponent();
            VK_Auth.Navigate(new Uri("https://oauth.vk.com/authorize?client_id=5340623&display=popup&redirect_uri=https://oauth.vk.com/blank.html&scope=wall&response_type=token&v=5.45", UriKind.Absolute));
        }

        private void VK_Auth_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            AccessToken = e.Uri.ToString().Substring("https://oauth.vk.com/blank.html#access_token=".Length);
            User_id = AccessToken.Substring(2, AccessToken.IndexOf('&'));
            AccessToken = AccessToken.Substring(0, AccessToken.IndexOf('&'));

            string url_post = "https://api.vk.com/method/wall.post?user_id=-" + User_id + "&тестовая+публикация&v=5.50&access_token=" + AccessToken;

            VK_Auth.Navigate(new Uri(url_post, UriKind.Absolute));

            MessageBox.Show(AccessToken);
        }
    }
}
