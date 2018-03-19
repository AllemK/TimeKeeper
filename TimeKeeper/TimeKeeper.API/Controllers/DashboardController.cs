using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TimeKeeper.API.Controllers
{
    public class DashboardController : BaseController
    {
        public IHttpActionResult Get(int year = 0, int month=0)
        {
            if (year == 0) year = DateTime.Today.Year;
            if (month == 0) month = DateTime.Today.Month;
            return Ok(new
            {
                year,
                month,
                list = TimeKeeperReports.CompanyDashboard(year, month)
            });
        }
    }
}
