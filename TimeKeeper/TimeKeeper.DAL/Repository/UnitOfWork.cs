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

        private IRepository<Address> _addresses;
        private IRepository<Calendar> _calendars;
        private IRepository<Customer> _customers;
        private IRepository<Employee> _employees;
        private IRepository<Member> _members;
        private IRepository<Project> _projects;
        private IRepository<Role> _roles;
        private IRepository<Entities.Task> _tasks;
        private IRepository<Team> _teams;

        public IRepository<Address> Adressess
        {
            get
            {
                if (_addresses == null) _addresses = new Repository<Address>(context);
                return _addresses;
            }
        }

        public IRepository<Calendar> Calendars
        {
            get
            {
                if (_calendars == null) _calendars = new Repository<Calendar>(context);
                return _calendars;
            }
        }

        public IRepository<Customer> Customers
        {
            get
            {
                if (_customers == null) _customers = new Repository<Customer>(context);
                return _customers;
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (_employees == null) _employees = new Repository<Employee>(context);
                return _employees;
            }
        }

        public IRepository<Member> Members
        {
            get
            {
                if (_members == null) _members = new Repository<Member>(context);
                return _members;
            }
        }

        public IRepository<Project> Projects
        {
            get
            {
                if (_projects == null) _projects = new Repository<Project>(context);
                return _projects;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (_roles == null) _roles = new Repository<Role>(context);
                return _roles;
            }
        }

        public IRepository<Entities.Task> Tasks
        {
            get
            {
                if (_tasks == null) _tasks = new Repository<Entities.Task>(context);
                return _tasks;
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                if (_teams == null) _teams = new Repository<Team>(context);
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
