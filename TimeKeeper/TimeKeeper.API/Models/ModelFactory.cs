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
                TeamId = t.Id,
                TeamName = t.Name,
                TeamImage = t.Image,
                Members = t.Engagements.Select(e=>Create(e)).ToList(),
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

        public MemberModel Create(Engagement e)
        {
            return new MemberModel
            {
                Team = e.Team.Name,
                Role = e.Role.Name,
                Employee = e.Employee.FirstName+" "+e.Employee.LastName,
                Hours = e.Hours
            };
        }

        public ProjectModel Create(Project p)
        {
            return new ProjectModel()
            {
                Name = p.Name,
                Monogram = p.Monogram,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status.ToString(),
                Pricing = p.Pricing.ToString(),
                Amount = p.Amount,
                Customer = p.Customer.Name,
                Team = p.Team.Name
            };
        }

        public EmployeeModel Create(Employee e)
        {
            return new EmployeeModel()
            {
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
                Position = e.Position.Name,
                Engagements = e.Engagements.Select(eng => Create(eng)).ToList()
            };
        }

        public DetailModel Create(Detail d)
        {
            return new DetailModel()
            {
                Description = d.Description,
                Hours = d.Hours,
                Day = d.Day.Date.ToString(),
                Project = d.Project.Name
            };
        }

        public CalendarModel Create(Day d)
        {
            return new CalendarModel()
            {
                Date = d.Date,
                Hours = d.Hours,
                Type = d.Type.ToString(),
                Employee = d.Employee.FullName,
                Details = d.Details.Select(de => Create(de)).ToList()
            };
        }

        public CustomerModel Create(Customer c)
        {
            return new CustomerModel()
            {
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
                Projects = c.Projects.Select(x=>Create(x)).ToList()
            };
        }
    }
}