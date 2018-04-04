using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class LoginController : BaseController
    {

        public IHttpActionResult Get()
        {
            UserModel CurrentUser = BuildUser("iserver", "sub");
            return Ok(CurrentUser);
        }

        public IHttpActionResult Post()
        {
            UserModel CurrentUser = BuildUser("google", "email");
            return Ok(CurrentUser);
        }

        UserModel BuildUser(string provider, string field)
        {
            Dictionary<string, string> token = new Dictionary<string, string>();
            UserModel CurrentUser = new UserModel();
            if (HttpContext.Current.Request.Headers["Authorization"] != null)
            {
                string id_token = HttpContext.Current.Request.Headers.GetValues("Authorization").FirstOrDefault().Substring(7);
                string test = HttpContext.Current.Request.Headers.GetValues("Authorization").FirstOrDefault();
                token = TokenUtility.GenToken(id_token);
                DateTime expTime = new DateTime(1970, 1, 1)
                    .AddSeconds(Convert.ToDouble(token["exp"]));
                Employee emp = TimeKeeperUnit.Employees.Get(x => x.Email == token[field]).FirstOrDefault();
                CurrentUser = TimeKeeperFactory.Create(emp, provider);
            }
            return CurrentUser;
        }
    }
}