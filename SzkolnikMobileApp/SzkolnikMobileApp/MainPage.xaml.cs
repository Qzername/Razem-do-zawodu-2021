using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SzkolnikMobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            Application.Current.Properties["login"] = null;
            Application.Current.Properties["password"] = null;
            Application.Current.MainPage = new LoginPage();
        }

        void MarksClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MarksPage());
        }

        void LessonsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LessonsPage());
        }
    }
}