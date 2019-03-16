using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public int Position { get; set; }
        public bool HasFeedback { get; set; }
        public int ClassId { get; set; }
        public int Version { get; set; }

        public Section(int id, string description, string title, int status, int position,bool hasFeedback,int classId,int version)
        {
            Id = id;
            Description = description;
            Title = title;
            Status = status;
            Position = position;
            HasFeedback = hasFeedback;
            ClassId = classId;
            Version = version;
        }
        public Section()
        {

        }
        public List<Section> GetAllSectionFromDB()
        {
            DBservices db = new DBservices();
            return db.GetAllSectionFromDB("Section", "ConnectionStringPerson");
        }
    }
}