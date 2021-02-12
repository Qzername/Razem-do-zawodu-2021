using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzkolnikMobileApp.Code;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SzkolnikMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonsPage : ContentPage
    {
        DayOfWeek currentDay;
        Account acc;

        public LessonsPage()
        {
            InitializeComponent();
            string accJson = HTTPRequest.Get("/api/Accounts/GetVulcanToken/" + (string)Application.Current.Properties["login"]);
            Account acc = JsonConvert.DeserializeObject<Account>(accJson);
            currentDay = DateTime.Today.DayOfWeek;

            this.acc = acc;

            var lessons = GetTodaysLessons(acc.token, currentDay);

            UpdateDay();

            foreach (Lesson les in lessons)
            {
                content.Children.Add(new Label()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Text = $"{les.subject}\nOd:{les.fromTime.Substring(11,5)}\nDo:{les.toTime.Substring(11, 5)}\n--------------------"
                });
            }
        }

        public void PerDay(object sender, EventArgs e)
        {
            if ((int)currentDay == 0)
                currentDay = DayOfWeek.Saturday;
            else
                currentDay -= 1;

            var lessons = GetTodaysLessons(acc.token, currentDay);

            content.Children.Clear();

            UpdateDay();

            if(lessons.Length == 0)
            {
                content.Children.Add(new Label() {
                    VerticalOptions = LayoutOptions.Center,
                    Text = "Brak lekcji"
                });;
                return;
            }

            foreach (Lesson les in lessons)
            {
                content.Children.Add(new Label()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Text = $"{les.subject}\nOd:{les.fromTime.Substring(11, 5)}\nDo:{les.toTime.Substring(11, 5)}\n--------------------"
                });
            }
        }

        public void NextDay(object sender, EventArgs e)
        {
            if ((int)currentDay == 6)
                currentDay = DayOfWeek.Sunday;
            else
                currentDay += 1;

            var lessons = GetTodaysLessons(acc.token, currentDay);

            content.Children.Clear();

            UpdateDay();

            if (lessons.Length == 0)
            {
                content.Children.Add(new Label()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Text = "Brak lekcji"
                }); ;
                return;
            }

            foreach (Lesson les in lessons)
            {
                content.Children.Add(new Label()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Text = $"{les.subject}\nOd:{les.fromTime.Substring(11, 5)}\nDo:{les.toTime.Substring(11, 5)}\n--------------------"
                });
            }
        }

        public void UpdateDay()
        {
            title.Children.Clear();

            string dayPolish;

            switch (currentDay)
            {
                case DayOfWeek.Monday: dayPolish = "Poniedziałek"; break;
                case DayOfWeek.Tuesday: dayPolish = "Wtorek"; break;
                case DayOfWeek.Wednesday: dayPolish = "Środa"; break;
                case DayOfWeek.Thursday: dayPolish = "Czwartek"; break;
                case DayOfWeek.Friday: dayPolish = "Piątek"; break;
                case DayOfWeek.Saturday: dayPolish = "Sobota"; break;
                case DayOfWeek.Sunday: dayPolish = "Niedziela"; break;
                default: throw new Exception("Not valid day");
            }

            title.Children.Add(new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                Text = dayPolish,
                FontSize = 72
            });
        }

        public Lesson[] GetTodaysLessons(string token, DayOfWeek day)
        {
            string lessonsJson = HTTPRequest.Get($"/api/Vulcan/GetLessons/{token}/{2021}/{2}/{(int)day}");
            return JsonConvert.DeserializeObject<Lesson[]>(lessonsJson);
        }
    }
}