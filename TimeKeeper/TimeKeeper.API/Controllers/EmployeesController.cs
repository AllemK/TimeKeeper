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
    public class EmployeesController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Employees.Get().ToList().Select(x => TimeKeeperFactory.Create(x)).ToList();
            Utility.Log("returned all records for employees", "INFO");
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Employee emp = TimeKeeperUnit.Employees.Get(id);
            if (emp == null)
            {
                Utility.Log($"no record of employee with id: {id}");
                return NotFound();
            }
            else
            {
                Utility.Log($"returned record for employee {emp.FullName}", "INFO");
                return Ok(TimeKeeperFactory.Create(emp));
            }
        }

        public IHttpActionResult Post([FromBody] Employee emp)
        {
            try
            {
                TimeKeeperUnit.Employees.Insert(emp);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"inserted data for new employee {emp.FullName}");
                    return Ok(emp);
                }
                else
                {
                    Utility.Log("Failed inserting employee","ERROR");
                    throw new Exception("wrong ");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Employee emp, int id)
        {
            try
            {
                if (TimeKeeperUnit.Employees.Get(id) == null)
                {

                    return NotFound();
                }
                TimeKeeperUnit.Employees.Update(emp, id);
                TimeKeeperUnit.Save();
                Utility.Log($"updated record for employee {emp.FullName}", "INFO");
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                Employee emp = TimeKeeperUnit.Employees.Get(id);
                if (emp == null)
                {
                    return NotFound();
                }
                TimeKeeperUnit.Employees.Delete(emp);
                TimeKeeperUnit.Save();
                Utility.Log($"returned record for employee {emp.FullName}", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
