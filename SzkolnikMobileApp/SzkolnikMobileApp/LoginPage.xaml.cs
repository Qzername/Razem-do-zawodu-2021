using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzkolnikMobileApp.Code;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace SzkolnikMobileApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            HTTPRequest.api = @"http://192.168.2.36:5000"; 
            InitializeComponent();
        }

        private void LoginClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            if(string.IsNullOrWhiteSpace(loginentry.Text) || string.IsNullOrWhiteSpace(passwordentry.Text))
            {
                output.TextColor = Color.Red;
                output.Text = "Musisz podać login i hasło.";
                button.IsEnabled = true;
                return;
            }

            string fromAPI = HTTPRequest.Get($"/api/Accounts/Login/{loginentry.Text}/{passwordentry.Text}/");
            Account acc = JsonConvert.DeserializeObject<Account>(fromAPI);

            if(acc is null)
            {
                output.TextColor = Color.Red;
                output.Text = "Nieprawidłowy login lub hasło.";
                button.IsEnabled = true;
                return;
            }
            button.IsEnabled = true;

            if(acc.password == acc.token)
                Application.Current.MainPage = new TokenRegistery(loginentry.Text, acc.token);

            Application.Current.MainPage = new MainPage();

            output.Text = fromAPI;
        }

        private void RegisterClicked(object sender, EventArgs e)
        {
            //Application.Current.Properties["login"] = "kosak";
            Button button = (Button)sender;
            button.IsEnabled = false;

            if (string.IsNullOrWhiteSpace(loginentry.Text) || string.IsNullOrWhiteSpace(passwordentry.Text))
            {
                output.TextColor = Color.Red;
                output.Text = "Musisz podać login i hasło.";
                button.IsEnabled = true;
                return;
            }

            Account acc = new Account()
            {
                login = loginentry.Text,
                password = passwordentry.Text,
                token = passwordentry.Text
            };

            HTTPRequest.Post("/api/Accounts/Register/", acc);

            output.TextColor = Color.Green;
            output.Text = "Zajerestrowano";
            button.IsEnabled = true;
        }
    }
}
