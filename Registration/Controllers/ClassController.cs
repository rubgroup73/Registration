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
        /*******Insert a new class into DB*******/
        [HttpPost]
        [Route("api/class/addnewclass")]
        public int InsertClass(AppClass appClass)
        {
         int numEffected = appClass.InsertClassToDB(appClass);
         return numEffected;
        }

        /**********GET All Classes From DB*********/
        [HttpGet]
        [Route("api/class")]
        public List<AppClass> GetAllClass()
        {
            AppClass appClass = new AppClass();
           return appClass.GetAllClassFromDB();
            
        }
        /**********GET The Latest Class_Id From DB*********/
        [HttpGet]
        [Route("api/class/getid")]
        public AppClass GetLastId()
        {
            AppClass appClass = new AppClass();
            return appClass.GetLastId();

        }

        /**********Insert Class Array to DB*********/
        [HttpPost]
        [Route("api/class/classArray")]
        public int InsertNewClassArray(List<AppClass> appClass)
        {
            AppClass app = new AppClass();
            int numEffected = app.InsertNewClassArray(appClass);
            return numEffected;
        }

 
    }
}
