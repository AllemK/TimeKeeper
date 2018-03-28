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
    public class TeamsController : BaseController
    {
        public IHttpActionResult GetAll(string all)
        {
            var list = TimeKeeperUnit.Teams.Get().OrderBy(x => x.Name)
                    .ToList()
                    .Select(x => new { x.Id, x.Name })
                    .ToList();
            return Ok(list);
        }
        /// <summary>
        /// Get all Teams
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Teams.Get()
                .Where(x => x.Name.Contains(h.filter))
                .Header(h)
                .ToList()
                .Select(t => TimeKeeperFactory.Create(t))
                .ToList();
            Logger.Log("Returned all teams", "INFO");
            return Ok(list); //Ok - status 200
        }

        /// <summary>
        /// Get specific Team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string id)
        {
            Team team = TimeKeeperUnit.Teams.Get(id);
            if (team == null)
            {
                Logger.Log($"No such team with id {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned team with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(team));
            }
        }

        /// <summary>
        /// Insert new Team
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] TeamModel team)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = "Failed inserting new team" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Teams.Insert(TimeKeeperFactory.Create(team, TimeKeeperUnit));
                TimeKeeperUnit.Save();
                Logger.Log("Inserted new team", "INFO");
                return Ok(team);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Team
        /// </summary>
        /// <param name="team"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] TeamModel team, string id)
        {
            try
            {
                if (TimeKeeperUnit.Teams.Get(id) == null)
                {
                    Logger.Log($"No such team with id {id}");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    var message = $"Failed updating team with id {id}" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Teams.Update(TimeKeeperFactory.Create(team, TimeKeeperUnit), id);
                TimeKeeperUnit.Save();
                Logger.Log($"Updated team with id {id}", "INFO");
                return Ok(team);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete chosen Team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(string id)
        {
            try
            {
                Team team = TimeKeeperUnit.Teams.Get(id);
                if (team == null)
                {
                    Logger.Log($"No such team with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Projects.Delete(team.Projects);
                TimeKeeperUnit.Engagements.Delete(team.Engagements);
                TimeKeeperUnit.Teams.Delete(team);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted team with id {id}", "INFO");
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
