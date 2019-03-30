using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{
    public class UserInClassController : ApiController
    {
        [HttpGet]
        [Route("api/userinclass/getAllUserInClass")]
        public List<UserInClass> GetAllUsersInClassFromDb()
        {
            UserInClass allUsersInClass = new UserInClass();
            return allUsersInClass.GetAllUsersInClassFromDb();

        }
    }
}
