﻿using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Registration.Controllers
{
    public class SectionController : ApiController
    {
        [HttpGet]
        [Route("api/section")]
        public List<Section> GetAllClass()
        {
            Section SectionClass = new Section();
            return SectionClass.GetAllSectionFromDB();

        }
    }
}
