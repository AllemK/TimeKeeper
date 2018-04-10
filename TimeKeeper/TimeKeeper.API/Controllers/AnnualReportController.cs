using System;
using System.Web.Http;
using TimeKeeper.API.Helper;

namespace TimeKeeper.API.Controllers
{
    public class AnnualReportController : BaseController
    {
        [TimeKeeperAuth(Roles: "Admin")]
        public IHttpActionResult Get(int year = 0)
        {
            if (year == 0) year = DateTime.Today.Year;
            return Ok(new
            {
                year,
                list = TimeKeeperReports.AnnualReport(year)
            });
        }
    }
}
