using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class CustomersController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeUnit.Customers.Get().ToList().Select(x => TimeFactory.Create(x)).ToList();
            return Ok(list);
        }

        public IHttpActionResult Get(int id)
        {
            Customer customer = TimeUnit.Customers.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeFactory.Create(customer));
            }
        }

        public IHttpActionResult Post([FromBody] Customer customer)
        {

            try
            {
                TimeUnit.Customers.Insert(customer);
                bool b = TimeUnit.Save();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Customer customer, int id)
        {
            try
            {
                if (TimeUnit.Customers.Get(id) == null) return NotFound();
                TimeUnit.Customers.Update(customer, id);
                TimeUnit.Save();
                return Ok(customer);
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
                Customer customer = TimeUnit.Customers.Get(id);
                if (customer == null) return NotFound();
                TimeUnit.Customers.Delete(customer);
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
