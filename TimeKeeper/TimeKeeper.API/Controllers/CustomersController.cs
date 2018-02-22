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
    public class CustomersController : BaseController
    {
        ///<summary>
        ///Get all Customers
        ///</summary>
        ///<returns>All Customers</returns>
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Customers.Get()
                .Header(h)
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Utility.Log("Returned all customers", "INFO");
            return Ok(list);
        }

        /// <summary>
        /// Get specific Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific Customer</returns>
        public IHttpActionResult Get(int id)
        {
            Customer customer = TimeKeeperUnit.Customers.Get(id);
            if (customer == null)
            {
                Utility.Log($"Found no customer with id {id}");
                return NotFound();
            }
            else
            {
                Utility.Log($"Returned customer with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(customer));
            }
        }

        /// <summary>
        /// Insert new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>A new Customer</returns>
        public IHttpActionResult Post([FromBody] Customer customer)
        {
            //customer.Deleted = false;
            try
            {
                TimeKeeperUnit.Customers.Insert(customer);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"Inserted new customer with name {customer.Name}", "INFO");
                    return Ok(customer);
                }
                else
                {
                    throw new Exception("Failed inserting new customer, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Update chosen customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="id"></param>
        /// <returns>Updated wanted Customer</returns>
        public IHttpActionResult Put([FromBody] Customer customer, int id)
        {
            try
            {
                if (TimeKeeperUnit.Customers.Get(id) == null)
                {
                    Utility.Log($"Customer not found with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Customers.Update(customer, id);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"Updated record for customer with id {id}", "INFO");
                    return Ok(customer);
                }
                else
                {
                    throw new Exception($"Failed updating customer with id {id}, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete chosen customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted wanted Customer</returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Customer customer = TimeKeeperUnit.Customers.Get(id);
                if (customer == null)
                {
                    Utility.Log($"No customer found with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Customers.Delete(customer);
                TimeKeeperUnit.Save();
                Utility.Log($"Deleted customer with id {id}", "INFO");
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
