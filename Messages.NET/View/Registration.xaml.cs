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
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace Messages.NET
{
    /// <summary>
    /// Logique d'interaction pour Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public Registration()
        {
            InitializeComponent();
        }

        async void submit(object sender, RoutedEventArgs e)
        {

            var user = ((TextBox)this.FindName("username")).Text;
            var pass = ((TextBox)this.FindName("password")).Text;

            string json = new JavaScriptSerializer().Serialize(new
            {
                username = user,
                password = pass
            });

            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://baobab.tokidev.fr/api/createUser", content);

            var responseString = await response.Content.ReadAsStringAsync();


            Login login = new Login();
            App.Current.MainWindow = login;
            login.DataContext = App.Current.MainWindow.DataContext;
            this.Close();
            login.Show();
        }
    }
}
