using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class User
    {
        
        public User(string fullName,string gender, string status, int yearsOfEducation,string userName,string password,int residence,int prefDay1, int prefDay2, string prefHour1 = "14:00", string prefHour2="15:00",int score=0 ,string mail = "NO", string birthDate = "02/02/1934")
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
            PrefHour1 = prefHour1;
            PrefHour2 = prefHour2;
        }
        public User()
        {

        }

        public string FullName { get; }
        public string BirthDate { get; }
        public string Gender { get; }
        public string Status { get; }
        public int YearsOfEducation { get; }
        public string UserName { get; }
        public string Password { get; }
        public int Residence { get; }
        public int PrefDay1 { get; }
        public int PrefDay2 { get; }
        public string PrefHour1 { get; }
        public string PrefHour2 { get; }
    }
}