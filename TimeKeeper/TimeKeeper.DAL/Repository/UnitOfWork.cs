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
        private readonly TimeKeeperContext context = new TimeKeeperContext();

        private IRepository<Address, int> _addresses;
        private IRepository<Calendar, int> _calendars;
        private IRepository<Customer, int> _customers;
        private IRepository<Employee, int> _employees;
        private IRepository<Member, int> _members;
        private IRepository<Project, int> _projects;
        private IRepository<Role, string> _roles;
        private IRepository<Entities.Task, int> _tasks;
        private IRepository<Team, string> _teams;

        public IRepository<Address, int> Adressess
        {
            get
            {
                if (_addresses == null) _addresses = new Repository<Address, int>(context);
                return _addresses;
            }
        }

        public IRepository<Calendar, int> Calendars
        {
            get
            {
                if (_calendars == null) _calendars = new Repository<Calendar, int>(context);
                return _calendars;
            }
        }

        public IRepository<Customer, int> Customers
        {
            get
            {
                if (_customers == null) _customers = new Repository<Customer, int>(context);
                return _customers;
            }
        }

        public IRepository<Employee, int> Employees
        {
            get
            {
                if (_employees == null) _employees = new Repository<Employee, int>(context);
                return _employees;
            }
        }

        public IRepository<Member, int> Members
        {
            get
            {
                if (_members == null) _members = new Repository<Member, int>(context);
                return _members;
            }
        }

        public IRepository<Project, int> Projects
        {
            get
            {
                if (_projects == null) _projects = new Repository<Project, int>(context);
                return _projects;
            }
        }

        public IRepository<Role, string> Roles
        {
            get
            {
                if (_roles == null) _roles = new Repository<Role, string>(context);
                return _roles;
            }
        }

        public IRepository<Entities.Task, int> Tasks
        {
            get
            {
                if (_tasks == null) _tasks = new Repository<Entities.Task, int>(context);
                return _tasks;
            }
        }

        public IRepository<Team, string> Teams
        {
            get
            {
                if (_teams == null) _teams = new Repository<Team, string>(context);
                return _teams;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public bool Save()
        {
            return (context.SaveChanges() > 0);
        }
    }
}
