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
        public int NextLessonInReact { get; set; }
        public AppClass AppClass { get; set; }
        public Section [] Section { get; set;}
        public string User_Feeling { get; set; }
        public string User_Feeling_Finish { get; set;}
        public bool BeforeClass { get; set; }
        public DateTime ShouldStart { get; set; }
        public UserInHomeWork UserInHomeWork { get; set;}
        public bool IsNextLesson { get; set; }

        Dictionary<string, int> feelingStatus = new Dictionary<string, int>
            {
                {"קודר" , 1},
                {"עצוב",2 },
                {"בסדר",3 },
                {"שמח",4 },
                {"מאושר",5 }
            };

        public UserInClass(int userId, int classId, int classVersion, DateTime startTime, DateTime endTime, bool isStarted,
            bool isFinished,int classPosition,AppClass appClass,Section [] section,string user_feeling, 
            string user_feeling_finish,bool beforeClass,DateTime shouldStart,UserInHomeWork userInHomeWork,
             bool isNextLesson = false, int nextLessonInReact=-20)
        {
            UserId = userId;
            ClassId = classId;
            ClassVersion = classVersion;
            StartTime = startTime;
            EndTime = endTime;
            IsStarted = isStarted;
            IsFinished = isFinished;
            ClassPosition = classPosition;
            NextLessonInReact = nextLessonInReact;
            AppClass = appClass;
            Section = section;
            User_Feeling = user_feeling;
            User_Feeling_Finish = user_feeling_finish;
            BeforeClass = beforeClass;
            ShouldStart = shouldStart;
            UserInHomeWork = userInHomeWork;
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
            //DateTime.Now.DayOfWeek.ToString();
            UserInClass nextUserInClass = new UserInClass();
            List<UserInClass> userInClass = new List<UserInClass>();
            List<Section> section = new List<Section>();
            DBservices db = new DBservices();
            userInClass = db.GetUserInClassReact(userId, "userInClass", "ConnectionStringPerson");
            section = db.GetAllSectionsReact(userInClass[0].ClassVersion, "section", "ConnectionStringPerson");
            userInClass=InsertSectionsToClasses(section, userInClass);
            userInClass = GetNextClassReact(userInClass);
            //userInClass = OrderUserClasses(userInClass);
            //nextUserInClass = GetNextClass(userInClass);
            //userInClass.Add(nextUserInClass);//last index is the next lesson the user need to do.
            return userInClass;

        }

        public List<UserInClass> InsertSectionsToClasses(List<Section> sections, List<UserInClass> userInClasses )
        {
            for (int i = 0; i < userInClasses.Count; i++)
            {
                userInClasses[i].AppClass.Sections = new List<Section>();
                for (int j = 0; j < sections.Count; j++)
                {
                    if (userInClasses[i].AppClass.Id == sections[j].ClassId)
                    {

                        userInClasses[i].AppClass.Sections.Add(sections[j]);
                    }
                }          
            }

            return userInClasses;
        }
       public List<UserInClass> GetNextClassReact(List<UserInClass> userInClasses)
        {
            for (int i = 0; i < userInClasses.Count; i++)
            {
                if (userInClasses[i].IsFinished == false)
                {
                    userInClasses[i].NextLessonInReact = userInClasses[i].ClassId;
                    break;
                }
            }

            return userInClasses;
        }

       
        public int UserFeelingsReact(UserInClass userInClass)
        {
            string userFeelingForDB = "";
            DBservices db = new DBservices();
            if (userInClass.BeforeClass != true)
                userFeelingForDB = "user_feeling_finish";
            else
                userFeelingForDB = "user_feeling";
            int feeling = SetUserFeeling(userInClass);
            if (feeling != -1)
            {
                return db.UserFeelingsReact(feeling, userInClass, "UserInClass", userFeelingForDB);
            }
            else
                return 0;
            
          
        }
        public int SetUserFeeling(UserInClass userInClass)
        {
            foreach (var feel in feelingStatus.ToArray())
            {
                if (feel.Key == userInClass.User_Feeling)
                {
                    return feel.Value;
                }
            }
            return -1;
        }

        public int UpdateClassStatuscReact(UserInClass userInClass)
        {
            DBservices db = new DBservices();
            return db.UpdateClassStatuscReact(userInClass, "ConnectionStringPerson");
        }
        public int UpdateClassStartedReact(UserInClass userInClass)
        {
            DBservices db = new DBservices();
            return db.UpdateClassStartedReact(userInClass, "ConnectionStringPerson");
        }
    }
}