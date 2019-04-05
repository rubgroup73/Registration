using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registration.Controllers
{
    public class CityController : ApiController
    {
        [HttpGet]
        [Route("api/city/allCities")]
        public List<City> GetAllCities()
        {
            City city = new City();
            return city.GetAllCitiesFromDB();

        }

        [HttpGet]
        [Route("api/city/topFiveCities")]
        public List<City> GetTopFiveCities()
        {
            City city = new City();
            return city.GetTopFiveCitiesFromDB();

        }
    }
}
