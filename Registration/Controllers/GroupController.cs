﻿using Registration.Models;
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
        [HttpPost]
        [Route("api/group/insertNewGroup")]
        public int AddNewGroup([FromBody]Group group)
        {

            int numEffected = group.insert();
            return numEffected;
        }

        [HttpGet]
        [Route("api/getAllGroup")]
        public List<Group> GetAllClass(int day,int grouptime,int education)
        {
            Group groupClass = new Group();
            return groupClass.GetAllGroupsFromDB(day,grouptime,education);

        }
    }
}
