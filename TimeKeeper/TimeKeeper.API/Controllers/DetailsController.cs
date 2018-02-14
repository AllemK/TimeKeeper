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
            var list = TimeUnit.Details.Get().ToList().Select(x => TimeFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Detail detail = TimeUnit.Details.Get(id);
            if (detail == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeFactory.Create(detail));
            }
        }

        public IHttpActionResult Post([FromBody] Detail detail)
        {
            try
            {
                TimeUnit.Details.Insert(detail);
                TimeUnit.Save();
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
                if (TimeUnit.Details.Get(id) == null) return NotFound();
                TimeUnit.Details.Update(detail, id);
                TimeUnit.Save();
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
                Detail detail = TimeUnit.Details.Get(id);
                if (detail == null) return NotFound();
                TimeUnit.Details.Delete(detail);
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
