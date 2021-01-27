using System;
using System.Collections.Generic;
using System.Text;

namespace Lekcjobot.Code.Models
{
    public class Lesson
    {
        public Lesson() { }

        public Lesson(string teacher, string lesson, DateTime dateFrom, DateTime dateTo)
        {
            this.teacher = teacher;
            this.lesson = lesson;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
        }

        public string teacher;
        public string lesson;
        public DateTime dateFrom;
        public DateTime dateTo;
    }
}
