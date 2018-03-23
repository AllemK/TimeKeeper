using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
using TimeKeeper.Utility;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class CustomersController : BaseController
    {
        public IHttpActionResult GetAll(string all)
        {
            var list = TimeKeeperUnit.Customers.Get().OrderBy(x => x.Name)
                    .ToList()
                    .Select(x => new { x.Id, x.Name })
                    .ToList();
            return Ok(list);
        }
        ///<summary>
        ///Get all Customers
        ///</summary>
        ///<returns>All Customers</returns>
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Customers
                .Get(x => x.Name.Contains(h.filter))
                .AsQueryable()
                .Header(h)
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Logger.Log("Returned all customers", "INFO");
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
                Logger.Log($"Found no customer with id {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned customer with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(customer));
            }
        }

        /// <summary>
        /// Insert new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>A new Customer</returns>
        public IHttpActionResult Post([FromBody] CustomerModel customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = "Failed inserting new customer" + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Customers.Insert(TimeKeeperFactory.Create(customer, TimeKeeperUnit));
                TimeKeeperUnit.Save();
                Logger.Log($"Inserted new customer with name {customer.Name}", "INFO");
                return Ok(customer);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="id"></param>
        /// <returns>Updated wanted Customer</returns>
        public IHttpActionResult Put([FromBody] CustomerModel customer, int id)
        {
            try
            {
                if (TimeKeeperUnit.Customers.Get(id) == null)
                {
                    Logger.Log($"No such customer with id {id}");
                    return NotFound();
                }
                customer.Id = id;
                if (!ModelState.IsValid)
                {
                    var message = $"Failed updating customer with id {id}, " + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Customers.Update(TimeKeeperFactory.Create(customer, TimeKeeperUnit), id);
                TimeKeeperUnit.Save();
                Logger.Log($"Updated record for customer with id {id}", "INFO");
                return Ok(customer);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
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
                    Logger.Log($"No customer found with id {id}");
                    return NotFound();
                }

                TimeKeeperUnit.Customers.Delete(customer);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted customer with id {id}", "INFO");
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
