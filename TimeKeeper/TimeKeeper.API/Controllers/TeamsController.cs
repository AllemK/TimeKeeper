using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.API.Controllers
{
    public class TeamsController : BaseController
    {
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
            Utility.Log("Returned all teams", "INFO");
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
                Utility.Log($"No such team with id {id}");
                return NotFound();
            }
            else
            {
                Utility.Log($"Returned team with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(team));
            }
        }

        /// <summary>
        /// Insert new Team
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] Team team)
        {
            try
            {
                TimeKeeperUnit.Teams.Insert(team);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log("Inserted new team", "INFO");
                    return Ok(team);
                }
                else
                {
                    throw new Exception("Failed inserting new team, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);                
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Team
        /// </summary>
        /// <param name="team"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] Team team, string id)
        {
            try
            {
                if (TimeKeeperUnit.Teams.Get(id) == null)
                {
                    Utility.Log($"No such team with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Teams.Update(team, id);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"Updated team with id {id}", "INFO");
                    return Ok(team);
                }
                else
                {
                    throw new Exception($"Failed updating team with id {id}, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
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
                    Utility.Log($"No such team with id {id}");
                    return NotFound();
                }

                /*Tried to delete all of the foreign key contraint items
                 * within the delete function, however it requires more
                 * attetion, and debugging, for now left alone until
                 * more consultation needed
                
                ProjectsController pc = new ProjectsController();
                foreach(var item in TimeKeeperUnit.Projects.Get().Where(x=>x.Team.Id==team.Id))
                {
                    pc.Delete(item.Id);
                }

                EngagementsController ec = new EngagementsController();
                foreach(var item in TimeKeeperUnit.Engagements.Get().Where(x => x.Team.Id == team.Id))
                {
                    ec.Delete(item.Id);
                }
                */

                TimeKeeperUnit.Teams.Delete(team);
                TimeKeeperUnit.Save();
                Utility.Log($"Deleted team with id {id}", "INFO");
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
