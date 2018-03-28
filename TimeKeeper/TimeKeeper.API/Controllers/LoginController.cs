using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using TimeKeeper.API.Models;

namespace TimeKeeper.API.Controllers
{
    public class LoginController : BaseController
    {
        public UserModel CurrentUser;
        string id_token;
        Dictionary<string, string> token = new Dictionary<string, string>();

        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Request.Headers["Authorization"] != null)
            {
                token = GenToken();
                CurrentUser = TimeKeeperFactory.Create(token["sub"], id_token, TimeKeeperUnit);
            }
            return Ok(CurrentUser);
        }

        Dictionary<string, string> GenToken()
        {
            id_token = HttpContext.Current.Request.Headers.GetValues("Authorization").FirstOrDefault().Substring(7);
            string[] jwt = id_token.Split('.');
            string header = Encoding.UTF8.GetString(Convert.FromBase64String(FitToB64(jwt[0])));
            string payload = Encoding.UTF8.GetString(Convert.FromBase64String(FitToB64(jwt[1])));
            string signature = Encoding.UTF8.GetString(Convert.FromBase64String(FitToB64(jwt[2])));
            payload = payload.Replace('[', ' ').Replace(']', ' ');
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);
        }

        string FitToB64(string X)
        {
            while (X.Length % 4 != 0) X += "=";
            return X.Replace('-', '+').Replace('_', '/');
        }
    }
}
