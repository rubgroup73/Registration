using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class User
    {
        
        public User(int id,string fullName,string gender, int status, int yearsOfEducation,string userName,string password,int residence,int prefDay1,string phone,int city, string birthDate,int prefHour1 , int prefHour2 , string mail, int group_id,int group_version,Group group ,int score=0,bool credentials = false)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
            Status = status;
            YearsOfEducation = yearsOfEducation;
            UserName = userName;
            Password = password;
            Residence = residence;
            PrefDay1 = prefDay1;
            Phone = phone;
            City = city;
            PrefHour1 = prefHour1;
            PrefHour2 = prefHour2;
            Score = score;
            Credentials1 = credentials;
            Group_Id = group_id;
            Group_Version = group_version;
            Group = group;
            Mail = mail;
        }

        public User(string education_name,int numOfRegistered)
        {
            Education_Name = education_name;
            NumOfRegistered = numOfRegistered;
        }

        public string Education_Name { get; set; }
        public int NumOfRegistered { get; set; }

        public User()
        {
           
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public int Status { get; set; }
        public int YearsOfEducation { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Residence { get; set; }
        public int PrefDay1 { get; set; }
        public string Phone { get; set; }
        public int City { get; set; }
        public int PrefHour1 { get; set; }
        public int PrefHour2 { get; set; }
        public int Score { get; set; }
        public bool Credentials1 { get; set; }
        public int Group_Id { get; set; }
        public int Group_Version { get; set; }
        public Group Group { get; set; }
        public string Mail { get; set; }
        

        /***************************************************************/
        /********Insert New User Into DB********************************/
        /***************************************************************/
        public int insert()
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.insert(this);
            return numEffected;
        }
        /***************************************************************/
        /*Return True or False - check if User exists in DB*/
        /***************************************************************/
        public User UserConfirmation(string username, string password)
        {
            User user = new User();
            DBservices dbs = new DBservices();
            user = dbs.GetUserForConfirmation(username, "ConnectionStringPerson", "AppUser");
            
            return Credentials(user,username,password);
        }
        
        public User Credentials(User DBuser,string username,string password)
        {
            User u = new User();
            u = DBuser;
            if (DBuser.UserName == username && DBuser.Password == password)
            { u.Credentials1 = true; return u; }

            else
            { u.Credentials1 = false; return u; }
        }
        /***************************************************************/
        /***************Get All Person From DB*************************/
        public List<User> GetAllSectionFromDB()
        {
            DBservices db = new DBservices();
            return db.GetAllUsersFromDB("AppUser", "ConnectionStringPerson");
        }

        /***************************************************************/
        /***************Get All Person From DB*************************/

        public List<User> GetAllUsersFromDB()
        {
            DBservices db = new DBservices();
            return db.GetAllUsersFromDB("AppUser", "ConnectionStringPerson");
        }

        /***************************************************************/
        /***********Get All Users Per Education From DB*****************/

        public List<User> GetAllUsersPerEducationFromDb()
        {
            DBservices db = new DBservices();
            return db.GetAllUsersPerEducationFromDb("ConnectionStringPerson");
        }

        public int InsertToGroup(User user)
        {
            DBservices db = new DBservices();        
            user.Group_Id = user.Group.Group_Id;
            user.Group_Version = user.Group.Group_Version;
            int numEffected = db.InsertToGroup(user, "AppUser", "class_group", "ConnectionStringPerson");
            return numEffected;
        }




    }
}