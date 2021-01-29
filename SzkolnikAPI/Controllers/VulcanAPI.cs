using Lekcjobot.Code;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return VulcanAPI.runScript("./Code/Python/getLessons.py", string.Format("{0} {1} {2} {3}", token, year, month, day)); 
        }

        [HttpGet("[action]/{token}")]
        public string GetMarks(string token)
        {
            return VulcanAPI.runScript("./Code/Python/getMarks.py", token);
        }

    }
}
