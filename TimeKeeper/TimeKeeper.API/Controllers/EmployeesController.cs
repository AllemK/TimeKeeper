using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class EmployeesController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeUnit.Employees.Get().ToList().Select(x => TimeFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Employee emp = TimeUnit.Employees.Get(id);
            if (emp == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeFactory.Create(emp));
            }
        }

        public IHttpActionResult Post([FromBody] Employee emp)
        {
            try
            {
                TimeUnit.Employees.Insert(emp);
                TimeUnit.Save();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Employee emp, int id)
        {
            try
            {
                if (TimeUnit.Employees.Get(id) == null) return NotFound();
                TimeUnit.Employees.Update(emp, id);
                TimeUnit.Save();
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
                Employee emp = TimeUnit.Employees.Get(id);
                if (emp == null) return NotFound();
                TimeUnit.Employees.Delete(emp);
                TimeUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
