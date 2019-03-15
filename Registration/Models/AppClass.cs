﻿using System;
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
        public DateTime CreationDate { get; set; }
        public int[] Sections { get; set;}

        public AppClass(int id,string description,string title,int status,int position,int score,int version,DateTime creationDate,int [] sections)
        {
            Id = id;
            Description = description;
            Title = title;
            Status = status;
            Position = position;
            Score = score;
            Version = version;
            CreationDate = creationDate;
            Sections = sections;
        }
        public AppClass()
        {
                
        
        }
    }
}