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

        private IRepository<Day, int> calendar { get; set; }
        private IRepository<Customer, int> customers { get; set; }
        private IRepository<Employee, int> employees { get; set; }
        private IRepository<Engagement, int> engagements { get; set; }
        private IRepository<Project, int> projects { get; set; }
        private IRepository<Role, string> roles { get; set; }
        private IRepository<Detail, int> details { get; set; }
        private IRepository<Team, string> teams { get; set; }

        public IRepository<Day, int> Calendar
        {
            get
            {
                return calendar ?? (calendar = new Repository<Day, int>(timeKeeperContext));
            }
        }

        public IRepository<Customer, int> Customers
        {
            get
            {
                return customers ?? (customers = new Repository<Customer, int>(timeKeeperContext));
            }
        }

        public IRepository<Employee, int> Employees
        {
            get
            {
                return employees ?? (employees = new Repository<Employee, int>(timeKeeperContext));
            }
        }

        public IRepository<Engagement, int> Engagements
        {
            get
            {
                return engagements ?? (engagements = new Repository<Engagement, int>(timeKeeperContext));
            }
        }

        public IRepository<Project, int> Projects
        {
            get
            {
                return projects ?? (projects = new Repository<Project, int>(timeKeeperContext));
            }
        }

        public IRepository<Role, string> Roles
        {
            get
            {
                return roles ?? (roles = new Repository<Role, string>(timeKeeperContext));
            }
        }

        public IRepository<Detail, int> Details
        {
            get
            {
                return details ?? (details = new Repository<Detail, int>(timeKeeperContext));
            }
        }

        public IRepository<Team, string> Teams
        {
            get
            {
                return teams ?? (teams = new Repository<Team, string>(timeKeeperContext));
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
