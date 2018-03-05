using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    public class MonthlyController : BaseController
    {
        public IHttpActionResult Get(int year = 0, int month = 0)
        {
            if (year == 0) year = DateTime.Today.Year;
            if (month == 0) month = DateTime.Today.Month;
            return Ok(TimeKeeperReports.MonthlyReport(year, month));
        }
    }
}

