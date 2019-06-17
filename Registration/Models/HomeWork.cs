using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class HomeWork
    {
        public int Class_Id { get; set;}
        public int Class_Version { get; set;}
        public string Homework_Name { get; set;}
        public string Homework_Desc { get; set;}
        public string Homework_Image { get; set;}
        public string Homework_Audio { get; set; }

        public HomeWork(int class_id,int class_version, string homework_name, 
            string homework_desc,string homework_image,string homework_audio)
        {
            Class_Id = class_id;
            Class_Version = class_version;
            Homework_Name = homework_name;
            Homework_Desc = homework_desc;
            Homework_Image = homework_image;
            Homework_Audio = homework_audio;
        }
        public HomeWork()
        {

        }
    }
}