using Newtonsoft.Json;
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
    public class EmployeesController : BaseController
    {
        /// <summary>
        /// Get all Employees
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int page = 0, int pageSize = 10, int sort = 0, string filter = "")
        {
            int itemCount = TimeKeeperUnit.Employees.Get().Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            var list = TimeKeeperUnit.Employees.Get(x => x.LastName.Contains(filter) || x.FirstName.Contains(filter))
                .SortBy(sort)
                .Skip(pageSize * page)
                .Take(pageSize)
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();

            Utility.Log("Returned all records for employees", "INFO");
            UtilityController.InsertHeader(page, pageSize, totalPages, sort, filter);
            return Ok(list);
        }

        /// <summary>
        /// Get specific Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetById(int id)
        {
            Employee emp = TimeKeeperUnit.Employees.Get(id);
            if (emp == null)
            {
                Utility.Log($"No record of employee with id: {id}");
                return NotFound();
            }
            else
            {
                Utility.Log($"Returned record for employee {emp.FullName}", "INFO");
                return Ok(TimeKeeperFactory.Create(emp));
            }
        }

        /// <summary>
        /// Insert new Employee
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] Employee emp)
        {
            try
            {
                TimeKeeperUnit.Employees.Insert(emp);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"Inserted new employee {emp.FullName}", "INFO");
                    return Ok(emp);
                }
                else
                {
                    throw new Exception("Failed inserting new employee. Wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update wanted Employee
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] Employee emp, int id)
        {
            try
            {
                if (TimeKeeperUnit.Employees.Get(id) == null)
                {
                    Utility.Log($"No existing employee with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Employees.Update(emp, id);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"Updated record for employee {emp.FullName}", "INFO");
                    return Ok(emp);
                }
                else
                {
                    throw new Exception($"Failed updating employee with id {id}. Wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
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
                    Utility.Log($"No such employee with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Employees.Delete(emp);
                TimeKeeperUnit.Save();
                Utility.Log($"Deleted employee with id {id}", "INFO");
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
