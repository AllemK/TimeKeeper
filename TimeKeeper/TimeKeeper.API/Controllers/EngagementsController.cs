using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.Utility;
using TimeKeeper.DAL.Entities;
using TimeKeeper.API.Models;

namespace TimeKeeper.API.Controllers
{
    public class EngagementsController : BaseController
    {
        /// <summary>
        /// Get all Engagements
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get(/*[FromUri] Header h*/)
        {
            var list = TimeKeeperUnit.Engagements
                .Get(/*x => x.Employee.FullName.Contains(h.filter)*/)
                //.AsQueryable()
                //.Header(h)
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Logger.Log("Returned all members", "INFO");
            return Ok(list);
        }

        /// <summary>
        /// Get specific Engagement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            Engagement member = TimeKeeperUnit.Engagements.Get(id);
            if (member == null)
            {
                Logger.Log($"No such engagement with id {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned engagement with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(member));
            }
        }

        /// <summary>
        /// Insert new Engagement
        /// </summary>
        /// <param name="engagement"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] EngagementModel engagement)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string message = "Failed inserting new engagement" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Engagements.Insert(TimeKeeperFactory.Create(engagement, TimeKeeperUnit));
                TimeKeeperUnit.Save();
                Logger.Log("Inserted new engagement", "INFO");
                return Ok(engagement);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Engagement
        /// </summary>
        /// <param name="engagement"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] EngagementModel engagement, int id)
        {
            try
            {
                if (TimeKeeperUnit.Engagements.Get(id) == null)
                {
                    Logger.Log($"No such engagement with id {id}");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    string message = "Failed updating engagement" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Engagements.Update(TimeKeeperFactory.Create(engagement, TimeKeeperUnit), id);
                TimeKeeperUnit.Save();
                Logger.Log($"Updated engagement with id {id}", "INFO");
                return Ok(engagement);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete chosen Engagement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Engagement member = TimeKeeperUnit.Engagements.Get(id);
                if (member == null)
                {
                    Logger.Log($"No such engagement with id {id}");
                    return NotFound();
                }

                TimeKeeperUnit.Engagements.Delete(member);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted engagement with id {id}", "INFO");
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
