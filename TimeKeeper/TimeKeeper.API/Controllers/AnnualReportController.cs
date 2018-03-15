using System;
using System.Web.Http;

namespace TimeKeeper.API.Controllers
{
    public class AnnualReportController : BaseController
    {
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
