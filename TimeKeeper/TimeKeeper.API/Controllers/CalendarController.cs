using System;
using System.Linq;
using System.Web.Http;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Entities;
using TimeKeeper.Utility;

namespace TimeKeeper.API.Controllers
{
    public class CalendarController : BaseController
    {
        /// <summary>
        /// Get all Days
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/calendar/{employeeId}/{year?}/{month?}")]
        [Authorize]
        public IHttpActionResult Get(int employeeId, int year = 0, int month = 0)
        {
            if (year == 0) year = DateTime.Today.Year;
            if (month == 0) month = DateTime.Today.Month;
            CalendarModel calendar = new CalendarModel(TimeKeeperFactory.Create(employeeId),year, month);
            Employee employee = TimeKeeperUnit.Employees.Get(employeeId);
            var listOfDays = employee.Days.Where(x => x.Date.Month == month && x.Date.Year == year).ToList();
            foreach (var day in listOfDays)
            {
                calendar.Days[day.Date.Day - 1].Id = day.Id;
                calendar.Days[day.Date.Day - 1].Type = (int)day.Type;
                calendar.Days[day.Date.Day - 1].Hours = day.Hours;
                calendar.Days[day.Date.Day - 1].Details = day.Details.Select(x => TimeKeeperFactory.Create(x)).ToArray();
                calendar.Days[day.Date.Day - 1].Employee.Name = employee.FullName;
                calendar.Days[day.Date.Day - 1].Employee.Id = employee.Id;
            }

            Logger.Log("Returned calendar", "INFO");
            return Ok(calendar);
        }

        /// <summary>
        /// Insert new Day
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public IHttpActionResult Post([FromBody] DayModel model)
        {
            try
            {
                Day day = new Day
                {
                    Id = model.Id,
                    Date = model.Date,
                    Type = (DayType)model.Type,
                    Hours = model.Hours,
                    Employee = TimeKeeperUnit.Employees.Get(model.Employee.Id)
                };
                if (day.Id == 0)
                    TimeKeeperUnit.Calendar.Insert(day);
                else
                    TimeKeeperUnit.Calendar.Update(day, day.Id);
                TimeKeeperUnit.Save();

                foreach (DetailModel task in model.Details)
                {
                    if (task.Deleted&&task.Id!=0)
                    {
                        TimeKeeperUnit.Details.Delete(TimeKeeperUnit.Details.Get(task.Id));
                    }
                    else
                    {
                        Detail detail = new Detail
                        {
                            Id = task.Id,
                            Day = TimeKeeperUnit.Calendar.Get(day.Id),
                            Description = task.Description,
                            Hours = task.Hours,
                            Project = TimeKeeperUnit.Projects.Get(task.Project.Id)
                        };
                        if (detail.Id == 0)
                            TimeKeeperUnit.Details.Insert(detail);
                        else
                            TimeKeeperUnit.Details.Update(detail, detail.Id);
                    }
                }
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
