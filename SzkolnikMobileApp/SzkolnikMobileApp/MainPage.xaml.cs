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
        bool isHidden;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, true);
            InitializeComponent();

            frame.TranslateTo(-550, 0);
            isHidden = true;
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            Application.Current.Properties["login"] = null;
            Application.Current.Properties["password"] = null;
            Application.Current.MainPage = new LoginPage();
        }

        private void Move(object sender, EventArgs e)
        {
            if(isHidden)
                frame.TranslateTo(0, 0);
            else
                frame.TranslateTo(-550, 0);

            isHidden = !isHidden;
        }
    }
}