using Registration.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int NumOfUsers { get; set; }

        public City(int id,string cityName,int numOfUsers)
        {
            Id = id;
            CityName = cityName;
            NumOfUsers = numOfUsers;
        }

     

        public City()
        {

        }

        public List<City> GetAllCitiesFromDB()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllCitiesFromDB("cities", "ConnectionStringPerson");
        }

        public List<City> GetTopFiveCitiesFromDB()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllCitiesFromDB("ConnectionStringPerson");
        }
    }
}