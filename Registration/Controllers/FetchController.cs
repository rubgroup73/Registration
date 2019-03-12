using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{

    public class FetchController : ApiController
    {
        /***********************************************/
        /*Return True or False - From React login page*/
        /***********************************************/
        [HttpGet]
        [Route("api/Fetch")]
        public User GetPerson(string username,string password)
        {
            User p = new User();

            return p.UserConfirmation(username,password);
        }
        /***********************************************/

        /***********************************************/


    }
}
