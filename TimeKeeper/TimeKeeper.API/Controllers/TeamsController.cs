using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.API.Controllers
{
    public class TeamsController : ApiController
    {
        public IHttpActionResult Get()
        {
            UnitOfWork unit = new UnitOfWork();
            ModelFactory factory = new ModelFactory();
            var list = unit.Teams.Get().ToList()
                           .Select(t => factory.Create(t))
                           .ToList();
            return Ok(list); //Ok - status 200
        }
    }
}
