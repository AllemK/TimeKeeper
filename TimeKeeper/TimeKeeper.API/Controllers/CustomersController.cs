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
        ///<summary>
        ///Get all Customers
        ///</summary>
        ///<returns></returns>
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Customers.Get().ToList().Select(x => TimeKeeperFactory.Create(x)).ToList();
            return Ok(list);
        }

        //Get specific Customer
        public IHttpActionResult Get(int id)
        {
            Customer customer = TimeKeeperUnit.Customers.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(TimeKeeperFactory.Create(customer));
            }
        }

        public IHttpActionResult Post([FromBody] Customer customer)
        {

            try
            {
                TimeKeeperUnit.Customers.Insert(customer);
                bool b = TimeKeeperUnit.Save();
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
                if (TimeKeeperUnit.Customers.Get(id) == null) return NotFound();
                TimeKeeperUnit.Customers.Update(customer, id);
                TimeKeeperUnit.Save();
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
                Customer customer = TimeKeeperUnit.Customers.Get(id);
                if (customer == null) return NotFound();
                TimeKeeperUnit.Customers.Delete(customer);
                TimeKeeperUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
