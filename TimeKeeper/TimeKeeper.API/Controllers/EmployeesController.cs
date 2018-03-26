using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.Utility;
using TimeKeeper.DAL.Entities;
using TimeKeeper.API.Models;
using System.Security.Claims;
using Thinktecture.IdentityModel.WebApi;
using System.Web.WebPages.Html;

namespace TimeKeeper.API.Controllers
{
    public class EmployeesController : BaseController
    {
        public IHttpActionResult GetAll(string all)
        {
            var list = TimeKeeperUnit.Employees.Get().OrderBy(x=>x.FirstName)
                .ToList()
                .Select(x => new { x.Id, Name = x.FullName})
                .ToList();
            return Ok(list);
        }

        /// <summary>
        /// Get all Employees
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Employees
                .Get(x => x.FirstName.Contains(h.filter) || x.LastName.Contains(h.filter))
                .AsQueryable()
                .Header(h)
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Logger.Log("Returned all records for employees", "INFO");
            return Ok(list);
        }

        /// <summary>
        /// Get specific Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ScopeAuthorize("read")]
        public IHttpActionResult Get(int id)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            string username = claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            Employee emp = TimeKeeperUnit.Employees.Get(x => x.Email == username).FirstOrDefault();
            if (emp == null)
            {
                Logger.Log($"No record of employee with id: {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned record for employee {emp.FullName}", "INFO");
                return Ok(TimeKeeperFactory.Create(emp));
            }
        }

        /// <summary>
        /// Insert new Employee
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] EmployeeModel emp)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string message = "Failed inserting new employee, " + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Employees.Insert(TimeKeeperFactory.Create(emp, TimeKeeperUnit));
                TimeKeeperUnit.Save();
                Logger.Log($"Inserted new employee {emp.FullName}", "INFO");
                return Ok(emp);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update wanted Employee
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] EmployeeModel emp, int id)
        {
            try
            {
                if (TimeKeeperUnit.Employees.Get(id) == null)
                {
                    Logger.Log($"No existing employee with id {id}");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    string message = $"Failed updating employee with id {id}" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Employees.Update(TimeKeeperFactory.Create(emp, TimeKeeperUnit), id);
                TimeKeeperUnit.Save();
                Logger.Log($"Updated record for employee {emp.FullName}", "INFO");
                return Ok(emp);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete wanted Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Employee emp = TimeKeeperUnit.Employees.Get(id);
                if (emp == null)
                {
                    Logger.Log($"No such employee with id {id}");
                    return NotFound();
                }

                TimeKeeperUnit.Employees.Delete(emp);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted employee with id {id}", "INFO");
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
