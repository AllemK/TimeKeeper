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
    public class RolesController : BaseController
    {
        /// <summary>
        /// Get all Roles
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Roles.Get()
                .Where(x => x.Name.Contains(h.filter))
                .Header(h)
                .ToList()
                .Select(r => TimeKeeperFactory.Create(r))
                .ToList();
            Logger.Log("Returned all roles", "INFO");
            return Ok(list);
        }

        /// <summary>
        /// Get specific Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string id)
        {
            Role role = TimeKeeperUnit.Roles.Get(id);
            if (role == null)
            {
                Logger.Log($"No such role with id {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned role with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(role));
            }
        }

        /// <summary>
        /// Insert new Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] Role role)
        {
            try
            {
                TimeKeeperUnit.Roles.Insert(role);
                if (TimeKeeperUnit.Save())
                {
                    Logger.Log("Inserted new role", "INFO");
                    return Ok(role);
                }
                else
                {
                    throw new Exception("Failed inserting new role, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] Role role, string id)
        {
            try
            {
                if (TimeKeeperUnit.Roles.Get(id) == null)
                {
                    Logger.Log($"No such role with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Roles.Update(role, id);
                if (TimeKeeperUnit.Save())
                {
                    Logger.Log($"Updated role with id {id}", "INFO");
                    return Ok(role);
                }
                else
                {
                    throw new Exception("Failed updating role with id {id}, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete chosen Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(string id)
        {
            try
            {
                Role role = TimeKeeperUnit.Roles.Get(id);
                if (role == null)
                {
                    Logger.Log($"No such role with id {id}");
                    return NotFound();
                }

                /* Tried to delete all of the foreign key contraint items
                 * within the delete function, however it requires more
                 * attetion, and debugging, for now left alone until
                 * more consultation needed
                EngagementsController ec = new EngagementsController();
                foreach (var item in TimeKeeperUnit.Engagements.Get().Where(x => x.Role.Id == role.Id)){
                    ec.Delete(item.Id);
                }

                EmployeesController emc = new EmployeesController();
                foreach (var item in TimeKeeperUnit.Employees.Get().Where(x => x.Position.Id == role.Id)) {
                    emc.Delete(item.Id);
                }
                */

                TimeKeeperUnit.Roles.Delete(role);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted role with id {id}");
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
