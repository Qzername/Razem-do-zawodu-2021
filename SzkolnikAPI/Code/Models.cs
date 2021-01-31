using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SzkolnikAPI.Code
{
    public class Account
    {
        public string login { set; get; }
        public string password { set; get; }
        public string token { set; get; }
        public string symbol { set; get; }
        public string pin { set; get; }
    }

    public class Lesson
    {
        public string teacher { set; get; }
        public string subject { set; get; }
        public string fromTime { set; get; }
        public string toTime { set; get; }
    }

    public class Mark
    {
        public string content { set; get; }
        public string subject { set; get; }
        public string teacher { set; get; }
    }
}
