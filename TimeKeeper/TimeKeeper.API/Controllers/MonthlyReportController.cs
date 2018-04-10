using System;
using System.Web.Http;
using TimeKeeper.API.Helper;

namespace TimeKeeper.API.Controllers
{
    [TimeKeeperAuth(Roles: "Admin")]
    public class MonthlyReportController : BaseController
    {
        public IHttpActionResult Get(int year = 0, int month = 0)
        {
            if (year == 0) year = DateTime.Today.Year;
            if (month == 0) month = DateTime.Today.Month;
            return Ok(TimeKeeperReports.MonthlyReport(year, month));
        }
    }
}

