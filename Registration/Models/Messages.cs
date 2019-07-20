using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class Messages
    {
        public int Index { get; set; }
        public int UserId { get; set; }
        public string Full_Name { get; set;}
        public int Group_Id { get; set;}
        public int Group_Version { get; set;}
        public DateTime SentDate { get; set; }
        public string Content { get; set; }

        //        userId int,
        //group_id int, 
        //group_version int,
        //SentDate date default '1900-01-01',
        //content nvarchar(2000),
        //imageUrl nvarchar(200),
        public Messages(int index,int userId,string full_Name,int group_id,int group_version,DateTime sentDate,string content)
        {
            Index = index;
            UserId = userId;
            Full_Name = full_Name;
            Group_Id = group_id;
            Group_Version = group_version;
            SentDate = sentDate;
            Content = content;
        }
        public Messages()
        {
                
        }
        public List<Messages> GetAllMessagesDb(int groupId, int groupVersion)
        {
            DBservices db = new DBservices();
            return db.GetAllMessagesDb(groupId, groupVersion, "ConnectionStringPerson");
        }
        public int UpdateMessagesDb(Messages message)
        {
            DBservices db = new DBservices();
            return db.UpdateMessagesDb(message);
        }
    }
}