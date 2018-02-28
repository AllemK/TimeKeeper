using System;
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

        public Team Create(TeamModel tm)
        {
            return new Team()
            {
                Id = tm.Id,
                Name = tm.Name,
                Image = tm.Image,
                Description = tm.Description,
                Engagements = tm.Engagements.Select(x => Create(x)).ToList(),
                Projects = tm.Projects.Select(x => Create(x)).ToList()
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

        public Role Create(RoleModel rm)
        {
            Enum.TryParse(rm.Type, out RoleType type);
            return new Role()
            {
                Id = rm.Id,
                Name = rm.Name,
                Type = type,
                HourlyRate = rm.HourlyRate,
                MonthlyRate = rm.MonthlyRate,
                Engagements = rm.Members.Select(x => Create(x)).ToList(),
                Employees = rm.Employees.Select(x => Create(x)).ToList()
            };
        }

        public EngagementModel Create(Engagement e)
        {
            return new EngagementModel()
            {
                Id = e.Id,
                Team = (e.Team != null) ? e.Team.Name : "",
                Role = (e.Role != null) ? e.Role.Name : "",
                Employee = (e.Employee != null) ? e.Employee.FullName : "",
                Hours = e.Hours
            };
        }

        public Engagement Create(EngagementModel em)
        {
            string[] names = em.Employee.Split(' ');
            using (UnitOfWork unit = new UnitOfWork())
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

        public Project Create(ProjectModel pm)
        {
            Enum.TryParse(pm.Status, out ProjectStatus status);
            Enum.TryParse(pm.Pricing, out Pricing pricing);
            using (UnitOfWork unit = new UnitOfWork())
            {
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
                    Team = unit.Teams.Get(pm.TeamId)
                };
            }
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
                Engagements = e.Engagements.Select(eng => Create(eng)).ToList(),
                Days = e.Days.Select(d => Create(d)).ToList()
            };
        }

        public Employee Create(EmployeeModel em)
        {
            Enum.TryParse(em.Status, out EmployeeStatus status);
            using (UnitOfWork unit = new UnitOfWork())
            {
                return new Employee()
                {
                    Id = em.Id,
                    FirstName = em.FirstName,
                    LastName = em.LastName,
                    Image = em.Image,
                    Email = em.Email,
                    Phone = em.Phone,
                    Salary = em.Salary,
                    BirthDate = em.BirthDate,
                    BeginDate = em.BeginDate,
                    EndDate = em.EndDate,
                    Status = status,
                    Position = unit.Roles.Get(em.RoleId),
                    Engagements = em.Engagements.Select(x => Create(x)).ToList(),
                    Days = em.Days.Select(x => Create(x)).ToList()
                };
            }
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

        public Detail Create(DetailModel dm)
        {
            using (UnitOfWork unit = new UnitOfWork())
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
        }

        public CalendarModel Create(Day d)
        {
            return new CalendarModel()
            {
                Id = d.Id,
                Date = d.Date,
                Hours = d.Hours.ToString(),
                Type = d.Type.ToString(),
                Employee = (d.Employee != null) ? d.Employee.FullName : "",
                Details = d.Details.Select(de => Create(de)).ToList()
            };
        }

        public Day Create(CalendarModel cm)
        {
            Enum.TryParse(cm.Type, out DayType type);
            using (UnitOfWork unit = new UnitOfWork())
            {
                return new Day()
                {
                    Id = cm.Id,
                    Date = cm.Date,
                    Hours = decimal.Parse(cm.Hours),
                    Type = type,
                    Employee = unit.Employees.Get(cm.EmployeeId),
                    Details = cm.Details.Select(x => Create(x)).ToList()
                };
            }
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

        public Customer Create(CustomerModel cm)
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
                Projects = cm.Projects.Select(x => Create(x)).ToList()
            };
        }
    }
}