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

        public Group GetAllGroupsFromDbVer2(int prefday, int prefhour, int education)
        {
            /*groupTime: 1-Morning, 2-Noon, 3-Evening*/
            /*educationLevel:1-Fine,2-ok,3-smarties*/
            prefhour = CheckGroupTime(prefhour);
            education = CheckEducation(education);

            DBservices db = new DBservices();
            Group group = db.GetAllGroupsFromDbVer2("class_group", "ConnectionStringPerson",prefday,prefhour,education);
            return group;
        }
        //Normalize Hour-time to Group-time
        public int CheckGroupTime(int PrefHour1)
        {


            int startMorning = 1;//Start Morning Time
            int endMorning = 9;//End Morning Time
            int startNoon = 10;//Start Noon Time
            int endNoon = 17;//End Noon Time

            if (PrefHour1 >= startMorning && PrefHour1 <= endMorning)
            {
                return 1;
            }
            else if (PrefHour1 >= startNoon && PrefHour1 <= endNoon)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        //Normalize yearsOfEducation to Group-Education
        public int CheckEducation(int YearsOfEducation)
        {
            int educationLevel1 = 1;//No Education
            int educationLevel2 = 2;//Basic Education
            int educationLevel3 = 3;//Highschool Education


            if (YearsOfEducation == educationLevel1 || YearsOfEducation == educationLevel2)
            {
                return 1;
            }
            else if (YearsOfEducation == educationLevel3)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public void InserNewUserInClass(int userId)
        {
            DBservices db = new DBservices();
            db.InserNewUserInClass(userId);
        }

        public List<AppClass> GetClassVersionReact(int userId)
        {
            List<AppClass> classes = new List<AppClass>();
            List<Section> sections = new List<Section>();
            DBservices db = new DBservices();

            int classVersion = db.GetClassVersionReact(userId);
            sections= db.GetAllSectionsReact(classVersion,"section", "ConnectionStringPerson");
            classes = db.GetAllClassesReact(classVersion,"class", "ConnectionStringPerson");
            classes=InsertSectionsToClasses(sections,classes);
            return classes;


        }
        public List<AppClass> InsertSectionsToClasses(List<Section> sections, List<AppClass> classes)
        {
            for (int i = 0; i < classes.Count; i++)
            {
                classes[i].Sections = new List<Section>();
                for (int j = 0; j < sections.Count; j++)
                {
                    if (sections[j].ClassId == classes[i].Id)
                        classes[i].Sections.Add(sections[j]);
                }
            }
            
            return classes;
        }
    }
}