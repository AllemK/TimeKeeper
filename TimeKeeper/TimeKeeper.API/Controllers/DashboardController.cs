using System;
using System.Web.Http;
using TimeKeeper.API.Helper;

namespace TimeKeeper.API.Controllers
{
    [TimeKeeperAuth(Roles: "Admin,Lead")]
    public class DashboardController : BaseController
    {
        [TimeKeeperAuth(Roles:"Admin")]
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

        [TimeKeeperAuth(Roles: "Admin,Lead")]
        public IHttpActionResult Get(string teamId, int year = 0, int month = 0)
        {
            if (year == 0) year = DateTime.Today.Year;
            if (month == 0) month = DateTime.Today.Month;
            return Ok(new
            {
                year,
                month,
                list = TimeKeeperReports.TeamDashboard(teamId, year, month)
            });
        }
    }
}
