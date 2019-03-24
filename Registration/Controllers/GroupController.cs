using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{
    public class GroupController : ApiController
    {
        [HttpGet]
        [Route("api/getAllGroup")]
        public List<Group> GetAllClass()
        {
            Group groupClass = new Group();
            return groupClass.GetAllGroupsFromDB();

        }
    }
}
