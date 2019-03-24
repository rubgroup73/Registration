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

        public City(int id,string cityName)
        {
            Id = id;
            CityName = cityName;
        }

        public City()
        {

        }

        public List<City> GetAllCitiesFromDB()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllCitiesFromDB("cities", "ConnectionStringPerson");
        }
    }
}