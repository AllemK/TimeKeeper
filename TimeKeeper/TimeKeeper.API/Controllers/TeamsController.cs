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
    public class TeamsController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Teams.Get().ToList()
                           .Select(t => TimeKeeperFactory.Create(t))
                           .ToList();
            return Ok(list); //Ok - status 200
        }

        public IHttpActionResult Get(string id)
        {
            Team team = TimeKeeperUnit.Teams.Get(id);
            if (team == null)
                return NotFound();
            else
                return Ok(TimeKeeperFactory.Create(team));
        }

        public IHttpActionResult Post([FromBody] Team team)
        {
            try
            {
                TimeKeeperUnit.Teams.Insert(team);
                TimeKeeperUnit.Save();
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Team team, string id)
        {
            try
            {
                if (TimeKeeperUnit.Teams.Get(id) == null) return NotFound();
                TimeKeeperUnit.Teams.Update(team, id);
                TimeKeeperUnit.Save();
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                Team team = TimeKeeperUnit.Teams.Get(id);
                if (team == null) return NotFound();
                TimeKeeperUnit.Teams.Delete(team);
                TimeKeeperUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
