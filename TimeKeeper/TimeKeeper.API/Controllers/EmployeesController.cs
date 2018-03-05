using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class EmployeesController : BaseController
    {
        /// <summary>
        /// Get all Employees
        /// </summary>
        /// <returns></returns>

        [Authorize]
        public IHttpActionResult Get(int page = 0, int pageSize = 10, int sort = 0, string filter = "")
        {
            int itemCount = TimeKeeperUnit.Employees.Get().Count();
            int totalPages = (int)Math.Ceiling((double)itemCount / pageSize);

            var query = TimeKeeperUnit.Employees.Get();
            if (filter != "") query = query.Where(x => x.LastName.Contains(filter));
            switch (sort)
            {
                case 1: query = query.OrderBy(x => x.LastName); break;
                case 2: query = query.OrderBy(x => x.BirthDate); break;
                default: query = query.OrderBy(x => x.Id); break;

            }
            var list = query.Skip(pageSize * page)
                            .Take(pageSize)
                            .ToList()
                            .Select(x => TimeKeeperFactory.Create(x)).ToList();



            var header = new
            {
                nextPage = (page == totalPages - 1) ? -1 : page + 1,
                prevPage = page - 1,
                pageSize = pageSize,
                totalPages = totalPages,
                page = page,
                sort
            };
            System.Web.HttpContext.Current.Response.Headers
                      .Add("Pagination", JsonConvert.SerializeObject(header));

            Utility.Log("Returned all records for employees", "INFO");
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
                    Utility.Log($"Inserted new employee {emp.FullName}","INFO");
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
