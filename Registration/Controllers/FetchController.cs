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
        /*Part of a proccess when insert a new user*/
        /***********************************************/

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
        /*Return classes group for user*/
        /***********************************************/
        [HttpGet]
        [Route("api/Fetch/GetClassVersionReact")]
        public List<AppClass> GetClassVersionReact(int userId)
        {
            Group groupClass = new Group();
           return groupClass.GetClassVersionReact(userId);

        }

        /***********************************************/
        /*Return user in class from DB*/
        /***********************************************/
        [HttpGet]
        [Route("api/Fetch/GetUserInClassReact")]
        public List<UserInClass> GetUserInClassReact(int userId)
        {
            
            UserInClass userInClass = new UserInClass();
            return userInClass.GetUserInClassReact(userId);

        }

        /***********************************************/
        /*Return user In class Section */
        /***********************************************/

        [HttpGet]
        [Route("api/Fetch/GetUserInSectionReact")]
        public List<UserInSection> GetUserInSectionReact(int userId,int classVersion,int classId)
        {
            UserInSection userInSection = new UserInSection();
            return userInSection.GetUserInSectionReact(userId, classVersion, classId);
        }
        /***********************************************/
        /*POST user In class Section */
        /***********************************************/
        [HttpPut]
        [Route("api/Fetch/UserFeelingsReact")]
        public int UserFeelingsReact([FromBody]UserInClass userInClass)
        {
            UserInClass user = new UserInClass();
            return user.UserFeelingsReact(userInClass);
    
        }

        [HttpPut]
        [Route("api/Fetch/UpdateDataUserInClassReact")]
        public int UpdateDataUserInClassReact([FromBody]UserInSection userInSection)
        {
            UserInSection user = new UserInSection();
            return user.UpdateDataUserInClassReact(userInSection);
        }
        [HttpPut]
        [Route("api/Fetch/UpdateDataUserRepeatSecReact")]
        public int UpdateDataUserRepeatSecReact([FromBody]UserInSection userInSection)
        {
            UserInSection user = new UserInSection();
            return user.UpdateDataUserRepeatSecReact(userInSection);

        }

        [HttpPut]
        [Route("api/Fetch/UpdateClassStatuscReact")]
        public int UpdateClassStatuscReact([FromBody]UserInClass userInClass)
        {
            UserInClass user = new UserInClass();
            return user.UpdateClassStatuscReact(userInClass);

        }
        [HttpPut]
        [Route("api/Fetch/UpdateClassStartedReact")]
        public int UpdateClassStartedReact([FromBody]UserInClass userInClass)
        {
            UserInClass user = new UserInClass();
            return user.UpdateClassStartedReact(userInClass);

        }

        /***********************************************/
        /*Return user In class HomeWork */
        /***********************************************/

        [HttpGet]
        [Route("api/Fetch/GetUserInHomeWorkReact")]
        public List<UserInHomeWork> GetUserInHomeWorkReact(int userId, int classVersion, int classId)
        {
            UserInHomeWork userInHomeWork = new UserInHomeWork();
            return userInHomeWork.GetUserInHomeWorkReact(userId, classVersion, classId);
        }

        [HttpGet]
        [Route("api/Fetch/returnhomeworkforuser")]
        public UserInHomeWork UpdateDataUserInClassReact(int userId)
        {
            UserInHomeWork userInHomeWork = new UserInHomeWork();
            return userInHomeWork.GetUserInHomeWorkFromDb(userId);
        }

        [HttpPut]
        [Route("api/Fetch/UpdateDataUserInHomeWork")]
        public int UpdateHomeWorkFinishedReact([FromBody]UserInHomeWork userInHomeWork)
        {
            UserInHomeWork user = new UserInHomeWork();
            return user.UpdateHomeWorkFinishedReact(userInHomeWork);

        }
        [HttpPut]
        [Route("api/Fetch/userStartHomeWork")]
        public int UpdateUserStartHomeWork([FromBody]UserInHomeWork userInHomeWork)
        {
            UserInHomeWork user = new UserInHomeWork();
            return user.UpdateUserStartHomeWork(userInHomeWork);

        }



    }
}
