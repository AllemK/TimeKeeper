using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }

        public string Position { get; set; }
        public string RoleId { get; set; }
        public ICollection<CalendarModel> Days { get; set; }
        public ICollection<EngagementModel> Engagements { get; set; }

        public EmployeeModel()
        {
            Days = new List<CalendarModel>();
            Engagements = new List<EngagementModel>();
        }
    }
}