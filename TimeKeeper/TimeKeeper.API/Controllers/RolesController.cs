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
    [RoutePrefix("api/roles")]
    public class RolesController : BaseController
    {
        /// <summary>
        /// Get all Roles
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Roles.Get().ToList().Select(r => TimeKeeperFactory.Create(r)).ToList();
            Utility.Log("Returned all roles", "INFO");
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
                Utility.Log($"No such role with id {id}");
                return NotFound();
            }
            else
            {
                Utility.Log($"Returned role with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(role));
            }
        }

        /// <summary>
        /// Insert new Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] Role role)
        {
            try
            {
                TimeKeeperUnit.Roles.Insert(role);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log("Inserted new role", "INFO");
                    return Ok(role);
                }
                else
                {
                    throw new Exception("Failed inserting new role, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
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
                    Utility.Log($"No such role with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Roles.Update(role, id);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"Updated role with id {id}", "INFO");
                    return Ok(role);
                }
                else
                {
                    throw new Exception("Failed updating role with id {id}, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
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
                    Utility.Log($"No such role with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Roles.Delete(role);
                TimeKeeperUnit.Save();
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
