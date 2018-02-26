using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class DetailsController : BaseController
    {
        /// <summary>
        /// Get all Tasks
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Details.Get()
                .Header(h)
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Utility.Log("Returned all tasks", "INFO");
            return Ok(list);
        }

        /// <summary>
        /// Get specific Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            Detail detail = TimeKeeperUnit.Details.Get(id);
            if (detail == null)
            {
                Utility.Log($"No task with id {id}");
                return NotFound();
            }
            else
            {
                Utility.Log($"Returned task with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(detail));
            }
        }

        /// <summary>
        /// Insert new Task
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] Detail detail)
        {
            try
            {
                TimeKeeperUnit.Details.Insert(detail);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"Inserted new task {detail.Id}", "INFO");
                    return Ok(detail);
                }
                else
                {
                    throw new Exception("Failed inserting new task, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Task
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] Detail detail, int id)
        {
            try
            {
                if (TimeKeeperUnit.Details.Get(id) == null)
                {
                    Utility.Log($"No such task with id {id}", "ERROR");
                    return NotFound();
                }
                TimeKeeperUnit.Details.Update(detail, id);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"Updated task with id {id}", "INFO");
                    return Ok(detail);
                }
                else
                {
                    throw new Exception($"Failed updating task with id {id}, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete chosen Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Detail detail = TimeKeeperUnit.Details.Get(id);
                if (detail == null)
                {
                    Utility.Log($"No such task with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Details.Delete(detail);
                TimeKeeperUnit.Save();
                Utility.Log($"Deleted task with id {id}", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
