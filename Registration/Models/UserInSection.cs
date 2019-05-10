using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Registration.Models
{
    public class UserInSection
    {
        public int Section_Id { get; set; }
        public int Class_Id { get; set; }
        public int Class_Version { get; set; }
        public int UserId { get; set; }
        public int Play_Clicks { get; set; }
        public int Pause_Clicks { get; set; }
        public int Stop_Clicks { get; set; }
        public int Backward_Clicks { get; set; }
        public int Forward_Clicks { get; set; }
        public int Mute_Clicks { get; set; }
        public int Unmute_Click { get; set; }
        public DateTime Section_Start_Time { get; set; }
        public DateTime Section_End_Time { get; set; }
        public bool Section_Is_Started { get; set; }
        public bool Section_Is_Finished { get; set; }
        public string User_Last_Point { get; set; }
        public string Pause_Duration { get; set; }
        public int Repeat_Section_Counter { get; set; }
        public string Section_Total_Duration { get; set;}
        public string File_Path { get; set; }
        public string Section_Title { get; set; }
        public string Class_Title { get; set; }
        public int Approved_Section_Position { get; set;}

        public UserInSection(int section_id, int class_id,int class_version,int userId,int play_clicks,int pause_clicks,int stop_clicks,int backward_clicks,
            int forward_clicks,int mute_clicks,int unmute_click,DateTime section_start_time, DateTime section_end_time, bool section_is_started,
            bool section_is_finished,string user_last_point,string pause_duration,int repeat_section_counter,string section_total_duration,
            string file_path,string section_title,string class_title,int approved_section_position)
        {
            Section_Id = section_id;
            Class_Id = class_id;
            Class_Version = class_version;
            UserId = userId;
            Play_Clicks = play_clicks;
            Pause_Clicks = pause_clicks;
            Stop_Clicks = stop_clicks;
            Backward_Clicks = backward_clicks;
            Forward_Clicks = forward_clicks;
            Mute_Clicks = mute_clicks;
            Unmute_Click = unmute_click;
            Section_Start_Time = section_start_time;
            Section_End_Time = section_end_time;
            Section_Is_Started = section_is_started;
            Section_Is_Finished = section_is_finished;
            User_Last_Point = user_last_point;
            Pause_Duration = pause_duration;
            Repeat_Section_Counter = repeat_section_counter;
            Section_Total_Duration = section_total_duration;
            File_Path = file_path;
            Section_Title = section_title;
            Class_Title = class_title;
            Approved_Section_Position = approved_section_position;
        }


        public UserInSection()
        {

        }

        public List<UserInSection> GetUserInSectionReact(int userId, int classVersion, int classId)
        {
            List<UserInSection> userInSections = new List<UserInSection>();
            DBservices db = new DBservices();
            userInSections = db.GetUserInSectionReact(userId, classVersion, classId);
            return userInSections;
        }
    }
}