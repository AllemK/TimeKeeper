using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Models
{
    public class ModelFactory
    {
        public TeamModel Create(Team t)
        {
            return new TeamModel
            {
                Id = t.Id,
                Name = t.Name,
                Image = t.Image,
                Engagements = t.Engagements.Select(e => Create(e)).ToList(),
                Projects = t.Projects.Select(p => Create(p)).ToList()
            };
        }

        public RoleModel Create(Role r)
        {
            return new RoleModel()
            {
                Id = r.Id,
                Name = r.Name,
                Type = r.Type.ToString(),
                HourlyRate = r.HourlyRate,
                MonthlyRate = r.MonthlyRate,
                Members = r.Engagements.Select(e => Create(e)).ToList()
            };
        }

        public EngagementModel Create(Engagement e)
        {
            return new EngagementModel
            {
                Id = e.Id,
                Team = (e.Team != null) ? e.Team.Name : "",
                Role = (e.Role != null) ? e.Role.Name : "",
                Employee = (e.Employee != null) ? e.Employee.FullName : "",
                Hours = e.Hours
            };
        }

        public ProjectModel Create(Project p)
        {
            return new ProjectModel()
            {
                Id = p.Id,
                Name = p.Name,
                Monogram = p.Monogram,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status.ToString(),
                Pricing = p.Pricing.ToString(),
                Amount = p.Amount,
                Customer = (p.Customer != null) ? p.Customer.Name : "",
                Team = (p.Team != null) ? p.Team.Name : ""
            };
        }

        public EmployeeModel Create(Employee e)
        {
            return new EmployeeModel()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Image = e.Image,
                Email = e.Email,
                Phone = e.Phone,
                Salary = e.Salary,
                BirthDate = e.BirthDate,
                BeginDate = e.BeginDate,
                EndDate = e.EndDate,
                Status = e.Status.ToString(),
                Position = (e.Position != null) ? e.Position.Name : "",
                Engagements = e.Engagements.Select(eng => Create(eng)).ToList()
            };
        }

        public DetailModel Create(Detail d)
        {
            return new DetailModel()
            {
                Id = d.Id,
                Description = d.Description,
                Hours = d.Hours,
                Day = (d.Day != null) ? d.Day.Date.ToString() : "",
                Project = (d.Project != null) ? d.Project.Name : ""
            };
        }

        public CalendarModel Create(Day d)
        {
            return new CalendarModel()
            {
                Id = d.Id,
                Date = d.Date,
                Hours = d.Hours,
                Type = d.Type.ToString(),
                Employee = (d.Employee != null) ? d.Employee.FullName : "",
                Details = d.Details.Select(de => Create(de)).ToList()
            };
        }

        public CustomerModel Create(Customer c)
        {
            return new CustomerModel()
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.Image,
                Monogram = c.Monogram,
                Contact = c.Contact,
                Email = c.Email,
                Phone = c.Phone,
                Address_Road = c.Address.Road,
                Address_ZipCode = c.Address.ZipCode,
                Address_City = c.Address.City,
                Status = c.Status.ToString(),
                Projects = c.Projects.Select(x => Create(x)).ToList()
            };
        }
    }
}