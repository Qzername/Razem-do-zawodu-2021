using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzkolnikMobileApp.Code;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SzkolnikMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TokenRegistery : ContentPage
    {
        public TokenRegistery()
        {
            InitializeComponent();
        }

        private void RegisterTokenClicked(object sender, EventArgs e)
        {
            Account acc = new Account()
            {
                login = (string)Application.Current.Properties["login"],
                password = (string)Application.Current.Properties["password"],
                token = tokenentry.Text,
                symbol = symbolentry.Text,
                pin = pinentry.Text
            };

            HTTPRequest.Post("/api/Accounts/RegisterVulcan/", acc);
            Application.Current.MainPage =new NavigationPage(new MainPage());
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            Application.Current.Properties["login"] = null;
            Application.Current.Properties["password"] = null;
            Application.Current.MainPage = new LoginPage();
        }
    }
}