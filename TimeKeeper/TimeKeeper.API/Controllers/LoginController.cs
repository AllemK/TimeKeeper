//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web;
//using System.Web.Http;
//using TimeKeeper.API.Models;

//namespace TimeKeeper.API.Controllers
//{
//    public class LoginController : BaseController
//    {
//        public UserModel CurrentUser;
//        string id_token;
//        Dictionary<string, string> token = new Dictionary<string, string>();

//        public IHttpActionResult Get()
//        {
//            if (HttpContext.Current.Request.Headers["Authorization"] != null)
//            {
//                token = GenToken();
//                CurrentUser = Create(token["sub"]);
//            }
//            return Ok(CurrentUser);
//        } 
//    }
//}
