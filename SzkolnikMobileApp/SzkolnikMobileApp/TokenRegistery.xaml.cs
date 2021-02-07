using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SzkolnikMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TokenRegistery : ContentPage
    {
        string login, password;

        public TokenRegistery(string login, string password)
        {
            this.login = login;
            this.password = password;
            
            InitializeComponent(); 
            main.Text = login + " " + password;
        }
    }
}