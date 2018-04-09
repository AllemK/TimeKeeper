using System;
using System.Web.Http;
using TimeKeeper.API.Helper;

namespace TimeKeeper.API.Controllers
{
    [TimeKeeperAuth]
    public class PersonalReportController : BaseController
    {
        public IHttpActionResult Get(int id, int year = 0, int month = 0)
        {
            if (year == 0) year = DateTime.Today.Year;
            if (month == 0) month = DateTime.Today.Month;
            return Ok(new
            {
                year,
                month,
                id,
                list = TimeKeeperReports.PersonalReport(id, year, month)
            });
        }
    }
}
