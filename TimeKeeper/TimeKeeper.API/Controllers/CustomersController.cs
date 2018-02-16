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
    public class CustomersController : BaseController
    {
        ///<summary>
        ///Get all Customers
        ///</summary>
        ///<returns></returns>
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Customers.Get().ToList().Select(x => TimeKeeperFactory.Create(x)).ToList();
            Utility.Log("returned all records for customers","INFO");
            return Ok(list);
        }

        /// <summary>
        /// Get specific Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            Customer customer = TimeKeeperUnit.Customers.Get(id);
            if (customer == null)
            {
                Utility.Log($"found no customer", "ERROR");
                return NotFound();
            }
            else
            {
                Utility.Log($"returned customer with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(customer));
            }
        }
        /// <summary>
        /// Post new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] Customer customer)
        {

            try
            {
                TimeKeeperUnit.Customers.Insert(customer);
                bool b = TimeKeeperUnit.Save();
                Utility.Log($"inserted new customer with name {customer.Name}", "INFO");
                return Ok(customer);
            }
            catch (Exception ex)
            {
                Utility.Log($"wrong data sent", "ERROR");
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Update existing customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] Customer customer, int id)
        {
            try
            {
                if (TimeKeeperUnit.Customers.Get(id) == null)
                {
                    Utility.Log($"customer not found with id {id}", "ERROR");
                    return NotFound();
                }
                TimeKeeperUnit.Customers.Update(customer, id);
                TimeKeeperUnit.Save();
                Utility.Log($"updated record for customer with id {id}", "INFO");
                return Ok(customer);
            }
            catch (Exception ex)
            {
                Utility.Log($"wrong data inserted", "ERROR");
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete chosen customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
