using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class Course
    {
        public int Course_Version { get; set; }
        public string Course_Name { get; set; }
        public DateTime Date_Created { get; set; }

        public Course(int course_version,string course_name, DateTime date_created)
        {
            Course_Version = course_version;
            Course_Name = course_name;
            Date_Created = date_created;
        }

        public Course()
        {

        }

        public List<Course> GetCoursesFromDB()
        {
            DBservices db = new DBservices();
            return db.GetCoursesFromDB("course", "ConnectionStringPerson");
        }
    }
}