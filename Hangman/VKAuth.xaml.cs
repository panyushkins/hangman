using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net.Http;
using Hangman.DTO.Response;

namespace Hangman
{
    /// <summary>
    /// Логика взаимодействия для VKAuth.xaml
    /// </summary>
    public partial class VKAuth : Window
    {
        public string AccessToken { get; set; }
        public string User_id { get; set; }
        public double Experies_in { get; set; }

        public VKAuth()
        {
            InitializeComponent();
            VK_Auth.Navigate(new Uri(CreateAuthorizeUrlFor(5340623, "wall"), UriKind.Absolute));
        }

        private void VK_Auth_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.ToString().Contains("access_token="))
            {
                string pattern = "&";
                string[] response = Regex.Split(e.Uri.ToString().Substring("https://oauth.vk.com/blank.html#access_token=".Length), pattern);
                DateTime now = DateTime.Now;

                AccessToken = response[0];
                User_id = response[2].Substring("user_id=".Length);
                Experies_in = Convert.ToDouble(response[1].Substring("expires_in=".Length));


                MakingPost();

                this.Close();
            }
            if (e.Uri.ToString().Contains("access_denied"))
            {
                MessageBox.Show("Ошибка авторизации! Разрешите доступ приложению.");
                this.Close();
            }
        }

        internal static string CreateAuthorizeUrlFor(ulong appId, string scope)
        {
            var builder = new StringBuilder("https://oauth.vk.com/authorize?");

            builder.AppendFormat("client_id={0}&", appId);
            builder.AppendFormat("scope={0}&", scope);
            builder.Append("redirect_uri=https://oauth.vk.com/blank.html&");
            builder.Append("display=wap&");
            builder.Append("response_type=token");

            return builder.ToString();
        }

        internal void MakingPost()
        {
            Dictionary<string, object> request = new Dictionary<string, object>();

            request["request"] = "https://api.vk.com/method/wall.post?user_id=-" + User_id + "&message=а+теперь+еще+и+по-русски&v=5.50&access_token=" + AccessToken;

            HttpClient client = new HttpClient();

            var response2 = client.GetAsync("https://api.vk.com/method/wall.post?user_id=-" + User_id + "&message=а+теперь+еще+и+по-русски&v=5.50&access_token=" + AccessToken).Result;

            string ser_list = response2.Content.ReadAsStringAsync().Result;

            Response4 k = JsonConvert.DeserializeObject<Response4>(ser_list);

            var t = 1;

            MessageBox.Show("Запись появилась на стене.");
        }
    }
}
