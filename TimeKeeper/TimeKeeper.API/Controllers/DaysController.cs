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
    public class DaysController : BaseController
    {
        /// <summary>
        /// Get all Days
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get(int id, int year = 0, int month = 0)
        {
            if (year == 0)
            {
                year = DateTime.Today.Year;
            }
            if (month == 0)
            {
                month = DateTime.Today.Month;
            }

            CalendarModel calendar = new CalendarModel(year, month);
            Employee employee = TimeKeeperUnit.Employees.Get(employeeId);
            if (employee != null)
            {
                calendar.Employee = employee.FullName;
                calendar.EmployeeId = employee.Id;
                var listOfDays = employee.Days.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();
                foreach( var day in listOfDays)
                {
                    calendar.Days[day.Date.Day - 1].Id = day.Id;
                    calendar.Days[day.Date.Day - 1].TypeOfDay = day.Type.ToString().ToLower();
                    calendar.Days[day.Date.Day - 1].Hours = day.Hours;
                    calendar.Days[day.Date.Day - 1].Details = day.Details.Select
                }
            }

            Logger.Log("Returned all days", "INFO");
            return Ok(calendar);
        }

        /// <summary>
        /// Get specific Day
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            Day day = TimeKeeperUnit.Calendar.Get(id);
            if (day == null)
            {
                Logger.Log($"No such day with id {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned day with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(day));
            }
        }

        /// <summary>
        /// Insert new Day
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] CalendarModel day)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = "Failed inserting new day, " + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Calendar.Insert(TimeKeeperFactory.Create(day, TimeKeeperUnit));
                TimeKeeperUnit.Save();
                Logger.Log("Inserted new day", "INFO");
                return Ok(day);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Day
        /// </summary>
        /// <param name="day"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] CalendarModel day, int id)
        {
            try
            {
                if (TimeKeeperUnit.Calendar.Get(id) == null)
                {
                    Logger.Log($"No such day with id {id}");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    var message = $"Failed updating day with id {id}, " + Environment.NewLine;
                    message += string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    throw new Exception(message);
                }
                TimeKeeperUnit.Calendar.Update(TimeKeeperFactory.Create(day, TimeKeeperUnit), id);
                TimeKeeperUnit.Save();
                Logger.Log($"Updated day with id {id}", "INFO");
                return Ok(day);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete chosen Day
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Day day = TimeKeeperUnit.Calendar.Get(id);
                if (day == null)
                {
                    Logger.Log($"No such day with id {id}");
                    return NotFound();
                }                

                TimeKeeperUnit.Calendar.Delete(day);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted day with id {id}", "INFO");
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
