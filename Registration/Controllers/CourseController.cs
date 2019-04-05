using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{
    public class CourseController : ApiController
    {
        /**********GET All courses From DB*********/
        [HttpGet]
        [Route("api/courses")]
        public List<Course> GetAllClass()
        {
            Course course = new Course();
            return course.GetCoursesFromDB();

        }
    }
}
