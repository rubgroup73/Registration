using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class AppClass
    {
        public int Id {get; set;}
        public string Description {get; set;}
        public string Title {get; set;}
        public int Status {get; set;}
        public int Position {get; set;}
        public int Score {get; set;}
        public int Version {get; set;}
        public List<Section> Sections {get; set;}
        public string Class_File_Path {get; set;}
        public HomeWork HomeWork {get; set;}

        public AppClass(int id,string description,string title,int status,int position,int score,int version,
            List<Section> sections,string class_file_path,HomeWork homeWork)
        {
            Id = id;
            Description = description;
            Title = title;
            Status = status;
            Position = position;
            Score = score;
            Version = version;
            Sections = sections;
            Class_File_Path = class_file_path;
            HomeWork = homeWork;
        }
        public AppClass()
        {
                
        
        }

        public int InsertClassToDB(AppClass appClass)
        {
            DBservices db = new DBservices();
            return db.InsertClassToDB(appClass);
        }

        public List<AppClass> GetAllClassFromDB()
        {
            DBservices db = new DBservices();
            return db.GetAllClassFromDB("Class", "ConnectionStringPerson");
        }

        public AppClass GetLastId()
        {
            DBservices db = new DBservices();
            return db.GetLastId("Class", "ConnectionStringPerson");
        }

        public int InsertNewClassArray(List<AppClass> appClass)
        {
            DBservices db = new DBservices();
            return db.InsertNewClassArray(appClass,"ConnectionStringPerson");
             
        }
    }
}