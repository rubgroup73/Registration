using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class UserInClass
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public int ClassVersion { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }

        public UserInClass(int userId,int classId,int classVersion,DateTime startTime, DateTime endTime,bool isStarted,bool isFinished)
        {
            UserId = userId;
            ClassId = classId;
            ClassVersion = classVersion;
            StartTime = startTime;
            EndTime = endTime;
            IsStarted = isStarted;
            IsFinished = isFinished;
        }

        public UserInClass()
        {

        }

        public List<UserInClass> GetAllUsersInClassFromDb()
        {
            DBservices db = new DBservices();
            return db.GetAllUsersInClassFromDb("userInClass", "ConnectionStringPerson");
        }
    }
}