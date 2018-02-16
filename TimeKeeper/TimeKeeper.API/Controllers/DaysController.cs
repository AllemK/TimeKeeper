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
            var list = TimeKeeperUnit.Calendar.Get().ToList().Select(x => TimeKeeperFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Day day = TimeKeeperUnit.Calendar.Get(id);
            if (day == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeKeeperFactory.Create(day));
            }
        }

        public IHttpActionResult Post([FromBody] Day day)
        {
            try
            {
                TimeKeeperUnit.Calendar.Insert(day);
                TimeKeeperUnit.Save();
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
                if (TimeKeeperUnit.Calendar.Get(id) == null) return NotFound();
                TimeKeeperUnit.Calendar.Update(day, id);
                TimeKeeperUnit.Save();
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
                Day day = TimeKeeperUnit.Calendar.Get(id);
                if (day == null) return NotFound();
                TimeKeeperUnit.Calendar.Delete(day);
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