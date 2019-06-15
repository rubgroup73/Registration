using Registration.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Registration.Controllers
{
    public class ReportsController : ApiController
    {
        [HttpGet]
        [Route("api/reports/getAllUsersFromDbExcel")]
        public string getAllUsersFromDbExcel(string text, int num)
        {
            string dest = HttpContext.Current.Server.MapPath("~") + "\\reports\\" + text + ".csv";
            ExcelData user = new ExcelData();
            return user.GetAllUsersFromDbExcel("ConnectionStringPerson", "userinclass", dest);
           
        }
    }

}
