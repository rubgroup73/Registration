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

        [HttpPost]
        [Route("api/admin/addNewAdmin")]
        public void AddNewAdmin([FromBody]Admin admin)
        {
          admin.AddNewAdmin(admin);
        }

        [HttpGet]
        [Route("api/admin/getAlladmins")]
        public bool GetAllAdmins(string username,string email)
        {
            Admin admin = new Admin();
           return admin.GetAllAdminsFromDb(username,email);
        }


    }
}
