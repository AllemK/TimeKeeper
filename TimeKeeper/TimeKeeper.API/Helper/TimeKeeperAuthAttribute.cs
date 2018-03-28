using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.API.Helper
{
    public class TimeKeeperAuthAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            UnitOfWork unit = new UnitOfWork();
            ModelFactory factory = new ModelFactory();
            UserModel CurrentUser = new UserModel();
            Dictionary<string, string> token = new Dictionary<string, string>();
            string provider = "iserver";

            try
            {

            }
        }
    }
}