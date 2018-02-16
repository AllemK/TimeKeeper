using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class DetailsController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Details.Get().ToList().Select(x => TimeKeeperFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Detail detail = TimeKeeperUnit.Details.Get(id);
            if (detail == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeKeeperFactory.Create(detail));
            }
        }

        public IHttpActionResult Post([FromBody] Detail detail)
        {
            try
            {
                TimeKeeperUnit.Details.Insert(detail);
                TimeKeeperUnit.Save();
                return Ok(detail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Detail detail, int id)
        {
            try
            {
                if (TimeKeeperUnit.Details.Get(id) == null) return NotFound();
                TimeKeeperUnit.Details.Update(detail, id);
                TimeKeeperUnit.Save();
                return Ok(detail);
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
                Detail detail = TimeKeeperUnit.Details.Get(id);
                if (detail == null) return NotFound();
                TimeKeeperUnit.Details.Delete(detail);
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
