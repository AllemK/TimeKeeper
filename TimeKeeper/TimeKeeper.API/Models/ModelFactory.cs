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
                Projects = t.Projects.Select(p => Create(p.Id, p.Name)).ToList()
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
                Projects = tm.Projects.Select(x => unit.Projects.Get(x.Id)).ToList()
            };
        }

        public RoleModel Create(Role r)
        {
            return new RoleModel()
            {
                Id = r.Id,
                Name = r.Name,
                Type = (int)r.Type,
                HourlyRate = r.HourlyRate,
                MonthlyRate = r.MonthlyRate,
            };
        }

        public DetailModel Create(Detail x)
        {
            return new DetailModel()
            {
                Id=x.Id,
                Deleted=x.Deleted,
                Description=x.Description,
                Hours=x.Hours,
                Project=Create(x.Project.Id,x.Project.Name)
            };
        }

        public Role Create(RoleModel rm, UnitOfWork unit)
        {
            return new Role()
            {
                Id = rm.Id,
                Name = rm.Name,
                Type = (RoleType)rm.Type,
                HourlyRate = rm.HourlyRate,
                MonthlyRate = rm.MonthlyRate,
            };
        }

        public EngagementModel Create(Engagement e)
        {
            return new EngagementModel()
            {
                Id = e.Id,
                Hours = e.Hours,
                Employee = Create(e.Employee.Id,e.Employee.FullName),
                Role = Create(e.Role.Id, e.Role.Name),
                Team = Create(e.Team.Id,e.Team.Name)
            };
        }

        public Engagement Create(EngagementModel em, UnitOfWork unit)
        {
            return new Engagement
            {
                Id = em.Id,
                Hours = em.Hours,
                Team = unit.Teams.Get(em.Team.Id),
                Role = unit.Roles.Get(em.Role.Id),
                Employee = unit.Employees.Get(em.Employee.Id)
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
                Status = (int)p.Status,
                Pricing = (int)p.Pricing,
                Amount = p.Amount,
                Customer = Create(p.Customer.Id,p.Customer.Name),
                Team = Create(p.Team.Id,p.Team.Name)
            };
        }

        public Project Create(ProjectModel pm, UnitOfWork unit)
        {
            return new Project()
            {
                Id = pm.Id,
                Name = pm.Name,
                Monogram = pm.Monogram,
                Description = pm.Description,
                StartDate = pm.StartDate,
                EndDate = pm.EndDate,
                Status = (ProjectStatus)pm.Status,
                Pricing = (Pricing)pm.Pricing,
                Amount = pm.Amount,
                Customer = unit.Customers.Get(pm.Customer.Id),
                Team = unit.Teams.Get(pm.Team.Id)
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
                Status = (int)e.Status,
                Position = Create(e.Role.Id, e.Role.Name)
            };
        }

        public Employee Create(EmployeeModel em, UnitOfWork unit)
        {
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
                Status = (EmployeeStatus)em.Status,
                Role = unit.Roles.Get(em.Position.Id),
            };
        }

        public Day Create(DayModel dm, UnitOfWork unit)
        {
            return new Day()
            {
                Id = dm.Id,
                Date = dm.Date,
                Hours = dm.Hours,
                Type = (DayType)dm.Type,
                Employee = unit.Employees.Get(dm.Employee.Id)
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
                Status = (int)c.Status,
                Projects = c.Projects.Select(x =>Create(x)).ToList()
            };
        }

        public Customer Create(CustomerModel cm, UnitOfWork unit)
        {
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
                Status = (CustomerStatus)cm.Status,
                Projects = cm.Projects.Select(x => unit.Projects.Get(x.Id)).ToList()
            };
        }

        public BaseModel<int> Create(int id, string name = "")
        {
            return new BaseModel<int>()
            {
                Id = id,
                Name = name
            };
        }

        public BaseModel<string> Create(string id, string name = "")
        {
            return new BaseModel<string>()
            {
                Id = id,
                Name = name
            };
        }

        public UserModel Create(string email, string id_token, UnitOfWork unit)
        {
            Employee emp = unit.Employees.Get(x => x.Email == email).FirstOrDefault();
            if (emp != null)
            {
                return new UserModel()
                {
                    Id = emp.Id,
                    Name = emp.FullName,
                    Role = "Admin",
                    Teams = emp.Engagements.Select(x => x.Team.Name).ToList(),
                    Token = id_token
                };
            }
            return null;
        }
    }
}