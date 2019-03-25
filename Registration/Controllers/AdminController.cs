using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{
    public class AdminController : ApiController
    {
        [HttpPost]
        [Route("api/loginAuth")]
        public bool AdminAuthentication([FromBody]Admin admin)
        {
            return admin.AdminAuthentication(admin);


        }

    }
}
