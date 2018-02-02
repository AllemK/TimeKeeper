using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.DAL.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly TimeKeeperContext timeKeeperContext = new TimeKeeperContext();

        private IRepository<Calendar> calendars { get; set; }
        private IRepository<Customer> customers { get; set; }
        private IRepository<Employee> employees { get; set; }
        private IRepository<Member> members { get; set; }
        private IRepository<Project> projects { get; set; }
        private IRepository<Role> roles { get; set; }
        private IRepository<Entities.Task> tasks { get; set; }
        private IRepository<Team> teams { get; set; }

        public IRepository<Calendar> Calendars
        {
            get
            {
                return calendars ?? (calendars = new Repository<Calendar>(timeKeeperContext));
            }
        }

        public IRepository<Customer> Customers
        {
            get
            {
                return customers ?? (customers = new Repository<Customer>(timeKeeperContext));
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                return employees ?? (employees = new Repository<Employee>(timeKeeperContext));
            }
        }

        public IRepository<Member> Members
        {
            get
            {
                return members ?? (members = new Repository<Member>(timeKeeperContext));
            }
        }

        public IRepository<Project> Projects
        {
            get
            {
                return projects ?? (projects = new Repository<Project>(timeKeeperContext));
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                return roles ?? (roles = new Repository<Role>(timeKeeperContext));
            }
        }

        public IRepository<Entities.Task> Tasks
        {
            get
            {
                return tasks ?? (tasks = new Repository<Entities.Task>(timeKeeperContext));
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                return teams ?? (teams = new Repository<Team>(timeKeeperContext));
            }
        }

        public void Dispose()
        {
            timeKeeperContext.Dispose();
        }

        public bool Save()
        {
            return (timeKeeperContext.SaveChanges() > 0);
        }
    }
}
