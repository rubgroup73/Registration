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
        public void PostAdd([FromBody]User u)
        { 
            //u.insert();
        }
    }
}
