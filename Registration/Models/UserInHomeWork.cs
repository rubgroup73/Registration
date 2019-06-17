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
        public DateTime Start_Time { get; set;}
        public DateTime End_Time { get; set;}
        public bool Is_Started { get; set;}
        public bool Is_Finished { get; set;}
        public DateTime Should_Start_Time { get; set;}
        public int User_Feeling_Start { get; set;}
        public int User_Feeling_Finish { get; set;}

        public UserInHomeWork(int userId,int class_id, int class_version,DateTime start_time,
            DateTime end_time, bool is_started,bool is_finished,DateTime should_start_time,
            int user_feeling_start, int user_feeling_finish)
        {
            UserId = userId;
            Class_Id = class_id;
            Class_Version = class_version;
            Start_Time = start_time;
            End_Time = end_time;
            Is_Started = is_started;
            Is_Finished = is_finished;
            Should_Start_Time = should_start_time;
            User_Feeling_Start = user_feeling_start;
            User_Feeling_Finish = user_feeling_finish;
        }
        public UserInHomeWork()
        {

        }
    }
}