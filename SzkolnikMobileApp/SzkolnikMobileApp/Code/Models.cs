using System;
using System.Collections.Generic;
using System.Text;

namespace SzkolnikMobileApp.Code
{
    public class Account
    {
        public string login { set; get; }
        public string password { set; get; }
        public string token { set; get; }
        public string symbol { set; get; }
        public string pin { set; get; }
    }

    public class Mark
    {
        public string content { set; get; }
        public string subject { get; set; }
        public string teacher { get; set; }
    }
}
