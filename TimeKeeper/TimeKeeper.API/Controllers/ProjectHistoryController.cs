using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TimeKeeper.API.Controllers
{
    public class ProjectHistoryController : BaseController
    {
        public IHttpActionResult Get(int projectId)
        {
            return Ok(new { list = TimeKeeperReports.ProjectHistory(projectId) });
        }
    }
}
