﻿using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class AppClass
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }
        public int Version { get; set; }
        public List<Section> Sections { get; set; }

        public AppClass(int id,string description,string title,int status,int position,int score,int version,List<Section> sections)
        {
            Id = id;
            Description = description;
            Title = title;
            Status = status;
            Position = position;
            Score = score;
            Version = version;
            Sections = sections;
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
    }
}