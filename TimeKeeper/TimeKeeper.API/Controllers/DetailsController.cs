using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class DetailsController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Details.Get().ToList().Select(x => TimeKeeperFactory.Create(x)).ToList();
            Utility.Log("returned all records for tasks", "INFO");
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Detail detail = TimeKeeperUnit.Details.Get(id);
            if (detail == null)
            {
                Utility.Log($"no record for task with id {id}", "INFO");
                return NotFound();
            }
            else
            {
                Utility.Log($"returned records for task with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(detail));
            }
        }

        public IHttpActionResult Post([FromBody] Detail detail)
        {
            try
            {
                TimeKeeperUnit.Details.Insert(detail);
                TimeKeeperUnit.Save();
                Utility.Log($"inserted data for new task {detail.Id}", "INFO");
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
                Utility.Log($"updated task with id {id}", "INFO");
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
                Utility.Log($"deleted task with id {id}", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
