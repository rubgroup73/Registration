using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class Group
    {
        public int Class_Version { get; set; }
        public int Group_Id { get; set; }
        public string Group_Name { get; set; }
        public int Day1 { get; set; }
        public int Hour1 { get; set; }
        public int Max_Partcipants { get; set; }
        public int Num_Of_Registered { get; set; }
        public int Group_Version { get; set; }
        public int Education { get; set; }
        public bool IsFinished { get; set; }
        public bool IsStarted { get; set; }

        public Group(int class_version,int group_id,string group_name,int day1,int hour1,int max_partcipants,int num_of_registered,int group_version,int education,bool isFinished,bool isStarted=false)
        {
            Class_Version = class_version;
            Group_Id = group_id;
            Group_Name = group_name;
            Day1 = day1;
            Hour1 = hour1;
            Max_Partcipants = max_partcipants;
            Num_Of_Registered = num_of_registered;
            Group_Version = group_version;
            Education = education;
            IsFinished = isFinished;
            IsStarted = isStarted;
        }

        public Group()
        {
                
        }

        /***************Get All Group From DB*************************/
        public List<Group> GetAllGroupsFromDB(int day,int grouptime,int education)
        {
            DBservices db = new DBservices();
            return db.GetAllGroupsFromDB(day, grouptime, education, "class_group", "ConnectionStringPerson");
        }

        /***************************************************************/
        /********Insert New Group Into DB********************************/
        /***************************************************************/
        public int insert()
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.InsertNewGroupToDB(this);
            return numEffected;
        }

        /***************************************************************/
        /********Update Group Participant Number***********************/
        /***************************************************************/

        public int UpdateGroupParticipant(Group group)
        {
            DBservices db = new DBservices();
            return db.UpdateGroupParticipant(group, "class_group", "ConnectionStringPerson");
        }

        /***************************************************************/
        /********Retrive All Groups From DB*****************************/
        /***************************************************************/

        public List<Group> GetAllGroupsFromDB()
        {
            DBservices db = new DBservices();
            return db.GetAllGroupsFromDB("class_group", "ConnectionStringPerson");
        }
    }
}