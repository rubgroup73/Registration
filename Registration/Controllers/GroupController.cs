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
        //[HttpPost]
        //[Route("api/group/insertNewGroup")]
        //public int AddNewGroup([FromBody]Group group)
        //{

        //    int numEffected = group.insert();
        //    return numEffected;
        //}

        [HttpGet]
        [Route("api/group/getAllGroup")]
        public List<Group> GetAllClass(int day,int grouptime,int education)
        {
            Group groupClass = new Group();
            return groupClass.GetAllGroupsFromDB(day,grouptime,education);

        }
        [HttpPut]
        [Route("api/group/UpdateGroup")]
        public void UpdateGroupParticipant([FromBody]Group group)
        {

            group.UpdateGroupParticipant(group);
        }

        //Get all groups from DB --> this function is related to UserManegement.js Page
        [HttpGet]
        [Route("api/group/getAllGroups")]
        public List<Group> GetAllGroupsFromDb()
        {
            Group groupClass = new Group();
            return groupClass.GetAllGroupsFromDB();

        }

        [HttpGet]
        [Route("api/group/NewAlgoritem")]
        public Group GetAllGroupsFromDbVer2(int prefday,int prefhour,int education)
        {
            Group groupClass = new Group();
            return groupClass.GetAllGroupsFromDbVer2(prefday,prefhour,education);

        }

       
    }
}
