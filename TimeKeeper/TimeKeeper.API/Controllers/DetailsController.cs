using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
using TimeKeeper.Utility;
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
            var list = TimeKeeperUnit.Details
                .Get(x => x.Description.Contains(h.filter))
                .AsQueryable()
                .Header(h)
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Logger.Log("Returned all tasks", "INFO");
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
                Logger.Log($"No task with id {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned task with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(detail));
            }
        }

        /// <summary>
        /// Insert new Task
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] DetailModel detail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string message = "Failed inserting new task, " + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Details.Insert(TimeKeeperFactory.Create(detail));
                TimeKeeperUnit.Save();
                Logger.Log($"Inserted new task {detail.Id}", "INFO");
                return Ok(detail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Task
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] DetailModel detail, int id)
        {
            try
            {
                if (TimeKeeperUnit.Details.Get(id) == null)
                {
                    Logger.Log($"No such task with id {id}", "ERROR");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    string message = $"Failed updating task with id {id}, " + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Details.Update(TimeKeeperFactory.Create(detail), id);
                TimeKeeperUnit.Save();
                Logger.Log($"Updated task with id {id}", "INFO");
                return Ok(detail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
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
                    Logger.Log($"No such task with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Details.Delete(detail);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted task with id {id}", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
