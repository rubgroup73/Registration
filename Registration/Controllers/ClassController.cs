using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{
    public class ClassController : ApiController
    {
        [HttpPost]
        [Route("api/class")]
        public int InsertClass(AppClass appClass)
        {
         int numEffected = appClass.InsertClassToDB(appClass);
            return numEffected;
        }

        [HttpGet]
        [Route("api/class")]
        public List<AppClass> GetAllClass()
        {
            AppClass appClass = new AppClass();
           return appClass.GetAllClassFromDB();
            
        }

        [HttpGet]
        [Route("api/class/getid")]
        public int GetLastId()
        {
            AppClass appClass = new AppClass();
            return appClass.GetLastId();

        }


    }
}
