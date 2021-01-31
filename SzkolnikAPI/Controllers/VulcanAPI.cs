using Lekcjobot.Code;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SzkolnikAPI.Code;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SzkolnikAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Vulcan : ControllerBase
    {
        [HttpGet("[action]/{token}/{year}/{month}/{day}")]
        public string GetLessons(string token, string year, string month, string day)
        {
            string text = VulcanAPI.runScript("./Code/Python/getLessons.py", string.Format("{0} {1} {2} {3}", token, year, month, day));

            List<string> singleobjects = new List<string>(text.Split('|'));
            singleobjects.RemoveAt(singleobjects.Count - 1);

            List<Lesson> marks = new List<Lesson>();

            foreach (string line in singleobjects)
            {
                string[] values = line.Split('!');
                marks.Add(new Lesson()//t.name, l.name from to
                {
                    teacher = values[0],
                    subject = values[1],
                    fromTime = values[2],
                    toTime = values[3]
                });
            }

            return JSONReader.Serialize(marks);
        }

        [HttpGet("[action]/{token}")]
        public string GetMarks(string token)
        {
            string text = VulcanAPI.runScript("./Code/Python/getMarks.py", token);

            List<string> singleobjects = new List<string>(text.Split('|'));
            singleobjects.RemoveAt(singleobjects.Count - 1);

            List<Mark> marks = new List<Mark>();

            foreach(string line in singleobjects)
            {
                string[] values = line.Split('!');
                marks.Add(new Mark()
                {
                    content = values[0],
                    subject = values[1],
                    teacher = values[2]
                });
            }

            return JSONReader.Serialize(marks);
        }

    }
}
