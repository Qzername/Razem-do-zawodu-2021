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
    public partial class MarksPage : ContentPage
    {
        public MarksPage()
        {
            InitializeComponent();

            string accJson = HTTPRequest.Get("/api/Accounts/GetVulcanToken/" + (string)Application.Current.Properties["login"]);
            Account acc = JsonConvert.DeserializeObject<Account>(accJson);

            string marksJson = HTTPRequest.Get("/api/Vulcan/GetMarks/" + acc.token);
            Mark[] marks = JsonConvert.DeserializeObject<Mark[]>(marksJson);

            List<string> diffrentSubjects = new List<string>();

            foreach(Mark m in marks)
                if(!diffrentSubjects.Any(x => m.subject == x))
                    diffrentSubjects.Add(m.subject);

            foreach(string s in diffrentSubjects)
            {
                content.Children.Add(new Label() {
                    Text = s,
                });

                Mark[] thisSubjectMarks = marks.Where(x => x.subject == s).ToArray();

                string marksXMLtext = string.Empty;

                foreach (Mark m in thisSubjectMarks)
                    marksXMLtext += m.content + ", ";

                marksXMLtext = marksXMLtext.Remove(marksXMLtext.Length - 2);

                content.Children.Add(new Label()
                {
                    Text = marksXMLtext,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Padding = new Thickness(10, 0, 10, 0)
                });
            }
        }
    }
}