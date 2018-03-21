﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.API.Models
{
    public class ModelFactory
    {
        public TeamModel Create(Team t)
        {
            return new TeamModel()
            {
                Id = t.Id,
                Name = t.Name,
                Image = t.Image,
                Description = t.Description,
                Engagements = t.Engagements.Select(e => Create(e)).ToList(),
                Projects = t.Projects.Select(p => Create(p)).ToList()
            };
        }

        public Team Create(TeamModel tm, UnitOfWork unit)
        {
            return new Team()
            {
                Id = tm.Id,
                Name = tm.Name,
                Image = tm.Image,
                Description = tm.Description,
                Engagements = tm.Engagements.Select(x => Create(x, unit)).ToList(),
                Projects = tm.Projects.Select(x => Create(x, unit)).ToList()
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
                Members = r.Engagements.Select(e => Create(e)).ToList(),
                Employees = r.Employees.Select(e => Create(e)).ToList()
            };
        }

        public Role Create(RoleModel rm, UnitOfWork unit)
        {
            Enum.TryParse(rm.Type, out RoleType type);
            return new Role()
            {
                Id = rm.Id,
                Name = rm.Name,
                Type = type,
                HourlyRate = rm.HourlyRate,
                MonthlyRate = rm.MonthlyRate,
                Engagements = rm.Members.Select(x => Create(x, unit)).ToList(),
                Employees = rm.Employees.Select(x => Create(x, unit)).ToList()
            };
        }

        public EngagementModel Create(Engagement e)
        {
            return new EngagementModel()
            {
                Id = e.Id,
                Team = (e.Team != null) ? e.Team.Name : "",
                TeamId = (e.Team != null) ? e.Team.Id : "",
                Role = (e.Role != null) ? e.Role.Name : "",
                RoleId = (e.Role != null) ? e.Role.Id : "",
                Employee = (e.Employee != null) ? e.Employee.FullName : "",
                EmployeeId = (e.Employee != null) ? e.Employee.Id : 0,
                Hours = e.Hours
            };
        }

        public Engagement Create(EngagementModel em, UnitOfWork unit)
        {
            return new Engagement
            {
                Id = em.Id,
                Hours = em.Hours,
                Team = unit.Teams.Get(em.TeamId),
                Role = unit.Roles.Get(em.RoleId),
                Employee = unit.Employees.Get(em.EmployeeId)
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
                CustomerId = (p.Customer != null) ? p.Customer.Id : 0,
                Team = (p.Team != null) ? p.Team.Name : "",
                TeamId = (p.Team != null) ? p.Team.Id : "",
                Details = p.Details.Select(x => Create(x)).ToList()
            };
        }

        public Project Create(ProjectModel pm, UnitOfWork unit)
        {
            Enum.TryParse(pm.Status, out ProjectStatus status);
            Enum.TryParse(pm.Pricing, out Pricing pricing);
            return new Project()
            {
                Id = pm.Id,
                Name = pm.Name,
                Monogram = pm.Monogram,
                Description = pm.Description,
                StartDate = pm.StartDate,
                EndDate = pm.EndDate,
                Status = status,
                Pricing = pricing,
                Amount = pm.Amount,
                Customer = unit.Customers.Get(pm.CustomerId),
                Team = unit.Teams.Get(pm.TeamId),
                Details = pm.Details.Select(x => Create(x, unit)).ToList()
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
                Password = e.Password,
                Phone = e.Phone,
                Salary = e.Salary,
                BirthDate = e.BirthDate,
                BeginDate = e.BeginDate,
                EndDate = e.EndDate,
                Status = e.Status.ToString(),
                RoleId = (e.Role != null) ? e.Role.Id : "",
                Role = (e.Role != null) ? e.Role.Name : "",
                Engagements = e.Engagements.Select(eng => Create(eng)).ToList(),
                Days = e.Days.Select(d => Create(d)).ToList()
            };
        }

        public Employee Create(EmployeeModel em, UnitOfWork unit)
        {
            Enum.TryParse(em.Status, out EmployeeStatus status);
            return new Employee()
            {
                Id = em.Id,
                FirstName = em.FirstName,
                LastName = em.LastName,
                Image = em.Image,
                Email = em.Email,
                Password = em.Password,
                Phone = em.Phone,
                Salary = em.Salary,
                BirthDate = em.BirthDate,
                BeginDate = em.BeginDate,
                EndDate = em.EndDate,
                Status = status,
                Role = unit.Roles.Get(em.RoleId),
                Engagements = em.Engagements.Select(x => Create(x, unit)).ToList(),
                Days = em.Days.Select(x => Create(x, unit)).ToList()
            };
        }

        public DetailModel Create(Detail d)
        {
            return new DetailModel()
            {
                Id = d.Id,
                Description = d.Description,
                Hours = d.Hours,
                DayId = (d.Day != null) ? d.Day.Id : 0,
                Day = d.Day.Date,
                Project = (d.Project != null) ? d.Project.Name : "",
                ProjectId = (d.Project != null) ? d.Project.Id : 0
            };
        }

        public Detail Create(DetailModel dm, UnitOfWork unit)
        {
            return new Detail()
            {
                Id = dm.Id,
                Description = dm.Description,
                Hours = dm.Hours,
                Day = unit.Calendar.Get(dm.DayId),
                Project = unit.Projects.Get(dm.ProjectId)
            };
        }

        public DayModel Create(Day d)
        {
            return new DayModel()
            {
                Id = d.Id,
                Date = d.Date,
                Hours = d.Hours,
                TypeOfDay = d.Type.ToString(),
                Employee = (d.Employee != null) ? d.Employee.FullName : "",
                EmployeeId = (d.Employee != null) ? d.Employee.Id : 0,
                Details = d.Details.Select(de => Create(de)).ToList()
            };
        }

        public Day Create(DayModel dm, UnitOfWork unit)
        {
            Enum.TryParse(dm.TypeOfDay, out DayType type);
            return new Day()
            {
                Id = dm.Id,
                Date = dm.Date,
                Hours = dm.Hours,
                Type = type,
                Employee = unit.Employees.Get(dm.EmployeeId),
                Details = dm.Details.Select(x => Create(x, unit)).ToList()
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

        public Customer Create(CustomerModel cm, UnitOfWork unit)
        {
            Enum.TryParse(cm.Status, out CustomerStatus status);
            return new Customer()
            {
                Id = cm.Id,
                Name = cm.Name,
                Image = cm.Image,
                Monogram = cm.Monogram,
                Contact = cm.Contact,
                Email = cm.Email,
                Phone = cm.Phone,
                Address = new Address()
                {
                    Road = cm.Address_Road,
                    ZipCode = cm.Address_ZipCode,
                    City = cm.Address_City
                },
                Status = status,
                Projects = cm.Projects.Select(x => Create(x, unit)).ToList()
            };
        }
    }
}