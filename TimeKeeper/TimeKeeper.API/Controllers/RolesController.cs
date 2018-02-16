using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class RolesController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Roles.Get().ToList().Select(r => TimeKeeperFactory.Create(r)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(string id)
        {
            Role role = TimeKeeperUnit.Roles.Get(id);
            if (role == null)
                return NotFound();
            else
                return Ok(TimeKeeperFactory.Create(role));
        }

        public IHttpActionResult Post([FromBody] Role role)
        {
            try
            {
                TimeKeeperUnit.Roles.Insert(role);
                TimeKeeperUnit.Save();
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Role role, string id)
        {
            try
            {
                if (TimeKeeperUnit.Roles.Get(id) == null) return NotFound();
                TimeKeeperUnit.Roles.Update(role, id);
                TimeKeeperUnit.Save();
                return Ok(role);
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
                Role role = TimeKeeperUnit.Roles.Get(id);
                if (role == null) return NotFound();
                TimeKeeperUnit.Roles.Delete(role);
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
