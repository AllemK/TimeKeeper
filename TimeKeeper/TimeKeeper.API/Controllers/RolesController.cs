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
            var list = TimeUnit.Roles.Get().ToList().Select(r => TimeFactory.Create(r)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(string id)
        {
            Role role = TimeUnit.Roles.Get(id);
            if (role == null)
                return NotFound();
            else
                return Ok(TimeFactory.Create(role));
        }

        public IHttpActionResult Post([FromBody] Role role)
        {
            try
            {
                TimeUnit.Roles.Insert(role);
                TimeUnit.Save();
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
                if (TimeUnit.Roles.Get(id) == null) return NotFound();
                TimeUnit.Roles.Update(role, id);
                TimeUnit.Save();
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
                Role role = TimeUnit.Roles.Get(id);
                if (role == null) return NotFound();
                TimeUnit.Roles.Delete(role);
                TimeUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
