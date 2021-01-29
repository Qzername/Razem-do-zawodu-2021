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
    public class Accounts : ControllerBase
    {
        // GET: api/<Accounts>
        [HttpGet("[action]/{login}/{password}/")]
        public string Login(string login, string password)
        {
            var act = SQLAccounts.GetAccount(login);

            if (act is null)
                return "null";
            else if (act.password != password)
                return "null";
            else
                return JSONReader.Serialize(act);
        }
        
        [HttpGet("[action]/{login}/")]
        public string GetVulcanToken(string login)
        {
            var act = SQLAccounts.GetAccount(login);
            return act.token;
        }

        [HttpPost("[action]")]
        public string Register([FromBody]Account json)
        {
            SQLAccounts.NewAccount(json);
            return "OK";
        }

        [HttpPost("[action]")]
        public string RegisterVulcan([FromBody] Account act)
        {
            SQLAccounts.UpdateToken(act.login, act.token);
            VulcanAPI.runScript("./Code/Python/register.py", string.Format("{0} {1} {2}", act.token, act.symbol, act.pin));
            return "done";
        }
    }
}
