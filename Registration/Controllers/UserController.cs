using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{
    public class UserController : ApiController
    {
        public int PostAdd([FromBody]User u)
        {

           int num = u.insert();
            return num;
        }

        [HttpGet]
        [Route("api/user/inDB")]
        public User GetPerson(string username)
        {
            User p = new User();

            return p.UserConfirmation(username);
        }
    }
}
