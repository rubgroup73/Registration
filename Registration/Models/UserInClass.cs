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
        public int ClassPosition { get; set; }
        public int NextLesson { get; set; }
        public bool IsNextLesson { get; set; }

        public UserInClass(int userId, int classId, int classVersion, DateTime startTime, DateTime endTime, bool isStarted, bool isFinished, int classPosition, int nextLesson , bool isNextLesson = false)
        {
            UserId = userId;
            ClassId = classId;
            ClassVersion = classVersion;
            StartTime = startTime;
            EndTime = endTime;
            IsStarted = isStarted;
            IsFinished = isFinished;
            ClassPosition = classPosition;
            NextLesson = nextLesson;
            IsNextLesson = isNextLesson;
        }

        public UserInClass()
        {

        }

        public List<UserInClass> GetAllUsersInClassFromDb()
        {
            DBservices db = new DBservices();
            return db.GetAllUsersInClassFromDb("userInClass", "ConnectionStringPerson");
        }

        public List<UserInClass> GetUserInClassReact(int userId)
        {
            UserInClass nextUserInClass = new UserInClass();
            List<UserInClass> userInClass = new List<UserInClass>();
            DBservices db = new DBservices();
            userInClass = db.GetUserInClassReact(userId, "userInClass", "ConnectionStringPerson");
           userInClass = OrderUserClasses(userInClass);
            nextUserInClass = GetNextClass(userInClass);
           userInClass.Add(nextUserInClass);//last index is the next lesson the user need to do.
            return userInClass;

        }

        public List<UserInClass> OrderUserClasses(List<UserInClass> userInClass)
        {

            int LastIndexOfClass = userInClass.Count - 1;

            for (int i = 0; i < userInClass.Count; i++)
            {
                if (userInClass[i].ClassPosition < LastIndexOfClass)
                {
                    userInClass[i].NextLesson = userInClass[i].ClassPosition + 1;
                }


            }

            return userInClass;
        }

        public UserInClass GetNextClass(List<UserInClass> userInClass)
        {
            UserInClass temp = new UserInClass();
            temp.NextLesson = 8888;

            for (int i = 0; i < userInClass.Count; i++)
            {
                if (userInClass[i].IsFinished == false)
                {
                    userInClass[i].IsNextLesson = true;
                    return userInClass[i];
                }

            }

            return temp;
        }
    }
}