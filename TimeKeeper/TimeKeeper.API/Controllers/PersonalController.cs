using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TimeKeeper.API.Controllers
{
    public class PersonalController : BaseController
    {
        public IHttpActionResult Get(int id, int year = 0, int month = 0)
        {
            return Ok(new
            {
                year,
                month,
                list = TimeKeeperReports.PersonalReport(id, year, month)
            });
        }
    }
}
