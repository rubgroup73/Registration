using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{
    public class TempController : ApiController
    {

        //        public int AddNewUser([FromBody] Group group, [FromBody] User user)
        [HttpPost]
        public int AddNewUser([FromBody] User user)
        {
            int num = user.InsertToGroup(user);
            return num;
        }
    }
}
