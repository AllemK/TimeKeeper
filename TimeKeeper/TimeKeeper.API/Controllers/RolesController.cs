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
    public class RolesController : BaseController
    {
        /// <summary>
        /// Get all Roles
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Roles
                .Get(x => x.Name.Contains(h.filter))
                .AsQueryable()
                .Header(h)
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
        public IHttpActionResult Post([FromBody] RoleModel role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string message = "Failed inserting new role" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Roles.Insert(TimeKeeperFactory.Create(role, TimeKeeperUnit));
                TimeKeeperUnit.Save();
                Logger.Log("Inserted new role", "INFO");
                return Ok(role);
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
        public IHttpActionResult Put([FromBody] RoleModel role, string id)
        {
            try
            {
                if (TimeKeeperUnit.Roles.Get(id) == null)
                {
                    Logger.Log($"No such role with id {id}");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    string message = $"Failed updating project with id {id}" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Roles.Update(TimeKeeperFactory.Create(role, TimeKeeperUnit), id);
                TimeKeeperUnit.Save();
                Logger.Log($"Updated role with id {id}", "INFO");
                return Ok(role);
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
