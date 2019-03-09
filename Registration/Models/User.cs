using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class User
    {
        
        public User(string fullName,string gender, string status, int yearsOfEducation,string userName,string password,int residence,int prefDay1, int prefDay2,string phone,int city, DateTime birthDate, DateTime prefHour1 =default(DateTime), DateTime prefHour2 = default(DateTime), int score=0 ,string mail = "NO" )
        {
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
            Status = status;
            YearsOfEducation = yearsOfEducation;
            UserName = userName;
            Password = password;
            Residence = residence;
            PrefDay1 = prefDay1;
            PrefDay2 = prefDay2;
            Phone = phone;
            City = city;
            PrefHour1 = prefHour1;
            PrefHour2 = prefHour2;
            Score = score;
            Mail = mail;
        }
        public User()
        {

        }

        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public int YearsOfEducation { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Residence { get; set; }
        public int PrefDay1 { get; set; }
        public int PrefDay2 { get; set; }
        public string Phone { get; set; }
        public int City { get; set; }
        public DateTime PrefHour1 { get; set; }
        public DateTime PrefHour2 { get; set; }
        public int Score { get; set; }
        public string Mail { get; set; }

        public int insert()
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.insert(this);
            return numEffected;
        }
    }
}