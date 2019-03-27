using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Registration.Controllers
{
    public class SectionController : ApiController
    {
        [HttpGet]
        [Route("api/section/GetAllClasses")]
        public List<Section> GetAllClass()
        {
            Section SectionClass = new Section();
            return SectionClass.GetAllSectionFromDB();

        }

        [HttpGet]
        [Route("api/section/getid")]
        public Section GetLastSectionId()
        {
            Section section = new Section();
            return section.GetLastSectionId();

        }
        /*******Insert a new class into DB*******/
        [HttpPost]
        [Route("api/section/addnewsection")]
        public int InsertClass(List<Section> Sections)
        {
            Section section = new Section();
            int numEffected = section.InsertNewSessionsToDB(Sections);
            return numEffected;
        }
    }
}
