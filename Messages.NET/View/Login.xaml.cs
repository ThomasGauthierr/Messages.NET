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
using System.Web.Script.Serialization;
using System.Net.Http;
using Newtonsoft.Json;

namespace Messages.NET
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private static readonly HttpClient client = new HttpClient();

        public Login()
        {
            InitializeComponent();
        }

        async public void submit(object sender, RoutedEventArgs e)
        {

            var user = ((TextBox)this.FindName("username")).Text;
            var pass = ((TextBox)this.FindName("password")).Text;

            string json = new JavaScriptSerializer().Serialize(new
            {
                username = user,
                password = pass
            });

            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://baobab.tokidev.fr/api/login", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var dataContext = App.Current.MainWindow.DataContext as ViewModel;

            dynamic responseJson = JsonConvert.DeserializeObject(responseString);

            if (responseJson != null && responseJson.access_token != null)
            {
                var token = responseJson.access_token;

                Window1 main = new Window1();

                dataContext.SessionToken = token;
                App.Current.MainWindow = main;
                main.DataContext = dataContext;
                this.Close();
                main.Show();
            }

            //Console.WriteLine(token);
        }

        public void register(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            App.Current.MainWindow = registration;
            registration.DataContext = App.Current.MainWindow.DataContext;
            this.Close();
            registration.Show();
        }
    }
}
