using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.DAL
{
    public class TimeKeeperContext : DbContext
    {
        public TimeKeeperContext() : base("TimeKeeper") { }

        public DbSet<Address> Adresses { get; set; }
        public DbSet<Calendar> Calendars { get; set; }                       /*u konstruktoru prvo je singular. komentarisanje je plural*/
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
