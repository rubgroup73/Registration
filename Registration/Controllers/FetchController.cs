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



        [HttpPost]
        [Route("api/Fetch/PostNewUserInClass")]
        public void InsertNewUserInClass(int userId)
        {
            Group groupClass = new Group();
            groupClass.InserNewUserInClass(userId);

        }
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
