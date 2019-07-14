using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class UserInHomeWork
    {
        public int UserId { get; set;}
        public int Class_Id { get; set;}
        public int Class_Version { get; set;}
        public int HomeWorkId { get; set; }
        public DateTime Start_Time { get; set;}
        public DateTime End_Time { get; set;}
        public bool Is_Started { get; set;}
        public bool Is_Finished { get; set;}
        public DateTime Should_Start_Time { get; set;}
        public int User_Feeling_Start { get; set;}
        public int User_Feeling_Finish { get; set;}
        public string HomeWork_Name { get; set; }
        public string HomeWork_Desc { get; set; }
        public string HomeWork_Image { get; set; }
        public string HomeWork_Audio { get; set; }
        public bool IsHomeWork { get; set; }

        public UserInHomeWork(int userId,int class_id, int class_version,int homeWorkId,DateTime start_time,
            DateTime end_time, bool is_started,bool is_finished,DateTime should_start_time,
            int user_feeling_start, int user_feeling_finish,string homeWork_name,string homeWork_desc,string homeWork_image,string homeWork_audio,
            bool isHomeWork)
        {
            UserId = userId;
            Class_Id = class_id;
            Class_Version = class_version;
            HomeWorkId = homeWorkId;
            Start_Time = start_time;
            End_Time = end_time;
            Is_Started = is_started;
            Is_Finished = is_finished;
            Should_Start_Time = should_start_time;
            User_Feeling_Start = user_feeling_start;
            User_Feeling_Finish = user_feeling_finish;
            HomeWork_Name = homeWork_name;
            HomeWork_Desc = homeWork_desc;
            HomeWork_Image = homeWork_image;
            HomeWork_Audio = homeWork_audio;
            IsHomeWork = isHomeWork;
        }
        public UserInHomeWork()
        {

        }

        public List<UserInHomeWork> GetUserInHomeWorkReact(int userId, int classVersion, int classId)
        {
            List<UserInHomeWork> userInHomeWork = new List<UserInHomeWork>();
            DBservices db = new DBservices();
            userInHomeWork = db.GetUserInHomeWorkReact(userId, classVersion, classId);
            return userInHomeWork;
        }
        public UserInHomeWork GetUserInHomeWorkFromDb(int userId)
        {
            DBservices db = new DBservices();
             return db.GetUserInHomeWorkFromDb(userId, "ConnectionStringPerson");
            
        }
        public int UpdateHomeWorkFinishedReact(UserInHomeWork userInHomeWork)
        {
            DBservices db = new DBservices();
            return db.UpdateHomeWorkFinishedReact(userInHomeWork, "ConnectionStringPerson");
        }
        public int UpdateUserStartHomeWork(UserInHomeWork userInHomeWork)
        {
            DBservices db = new DBservices();
            return db.UpdateUserStartHomeWork(userInHomeWork, "ConnectionStringPerson");
        }
    }
}