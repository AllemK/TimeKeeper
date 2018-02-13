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
            var list = TimeUnit.Engagements.Get().ToList().Select(x => TimeFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Engagement member = TimeUnit.Engagements.Get(id);
            if (member == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeFactory.Create(member));
            }
        }

        public IHttpActionResult Post([FromBody] Engagement member)
        {
            try
            {
                TimeUnit.Engagements.Insert(member);
                TimeUnit.Save();
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
                if (TimeUnit.Engagements.Get(id) == null) return NotFound();
                TimeUnit.Engagements.Update(member, id);
                TimeUnit.Save();
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
                Engagement member = TimeUnit.Engagements.Get(id);
                if (member == null) return NotFound();
                TimeUnit.Engagements.Delete(member);
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
