using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class DaysController : BaseController
    {

        public IHttpActionResult Get()
        {
            var list = TimeUnit.Calendar.Get().ToList().Select(x => TimeFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Day day = TimeUnit.Calendar.Get(id);
            if (day == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeFactory.Create(day));
            }
        }

        public IHttpActionResult Post([FromBody] Day day)
        {
            try
            {
                TimeUnit.Calendar.Insert(day);
                TimeUnit.Save();
                return Ok(day);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Day day, int id)
        {
            try
            {
                if (TimeUnit.Calendar.Get(id) == null) return NotFound();
                TimeUnit.Calendar.Update(day, id);
                TimeUnit.Save();
                return Ok(day);
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
                Day day = TimeUnit.Calendar.Get(id);
                if (day == null) return NotFound();
                TimeUnit.Calendar.Delete(day);
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