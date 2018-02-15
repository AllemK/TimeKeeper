using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class MembersController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Engagements.Get().ToList().Select(x => TimeKeeperFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Engagement member = TimeKeeperUnit.Engagements.Get(id);
            if (member == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeKeeperFactory.Create(member));
            }
        }

        public IHttpActionResult Post([FromBody] Engagement member)
        {
            try
            {
                TimeKeeperUnit.Engagements.Insert(member);
                TimeKeeperUnit.Save();
                return Ok(member);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Engagement member, int id)
        {
            try
            {
                if (TimeKeeperUnit.Engagements.Get(id) == null) return NotFound();
                TimeKeeperUnit.Engagements.Update(member, id);
                TimeKeeperUnit.Save();
                return Ok(member);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                Engagement member = TimeKeeperUnit.Engagements.Get(id);
                if (member == null) return NotFound();
                TimeKeeperUnit.Engagements.Delete(member);
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
