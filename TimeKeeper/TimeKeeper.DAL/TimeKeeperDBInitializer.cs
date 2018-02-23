using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.DAL
{
    internal class TimeKeeperDBInitializer<T> : DropCreateDatabaseAlways<TimeKeeperContext>
    {
        public override void InitializeDatabase(TimeKeeperContext context)
        {
            try
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                    $"ALTER DATABASE {context.Database.Connection.Database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            }
            catch
            {
                //database does not exist, excellent!
            }
            finally
            {
                base.InitializeDatabase(context);

                using (UnitOfWork unit = new UnitOfWork())
                {
                    AddRoles(unit);
                    AddTeams(unit);
                    AddCustomers(unit);
                    AddProjects(unit);
                    AddEmployees(unit);
                    AddDays(unit);
                    AddDetails(unit);
                    AddEngagements(unit);
                }
            }
        }

        void AddRoles(UnitOfWork unit)
        {
            unit.Roles.Insert(new Role()
            {
                Id = "SD",
                Name = "Software Developer",
                Type = RoleType.TeamRole,
                HourlyRate = 30,
                MonthlyRate = 4500
            });
            unit.Roles.Insert(new Role()
            {
                Id = "UX",
                Name = "UI/UX Designer",
                Type = RoleType.TeamRole,
                HourlyRate = 45,
                MonthlyRate = 6500
            });
            unit.Save();
        }

        void AddTeams(UnitOfWork unit)
        {
            unit.Teams.Insert(new Team()
            {
                Name = "Alpha",
                Id = "A",
                Image = "A.jpg",
                Description = "Alpha Team"
            });
            unit.Teams.Insert(new Team()
            {
                Name = "Bravo",
                Id = "B",
                Image = "B.jpg",
                Description = "Bravo Team"
            });
            unit.Save();
        }

        void AddCustomers(UnitOfWork unit)
        {
            unit.Customers.Insert(new Customer()
            {
                Name = "Alpha company",
                Contact = "Alpha person",
                Email = "alphamail@alpha.com",
                Image = "AlphaImage.jpg",
                Monogram = "ALP",
                Phone = "Alpha number",
                Address = new Address()
                {
                    Road = "Alpha road, 1",
                    ZipCode = "1000",
                    City = "Alpha city"
                },
                Status = CustomerStatus.Prospect
            });
            unit.Save();
        }

        void AddProjects(UnitOfWork unit)
        {
            unit.Projects.Insert(new Project()
            {
                Name = "Alpha Project",
                Monogram = "ALP",
                Description = "Alpha test project on alpha",
                StartDate = new DateTime(2018, 01, 15),
                Pricing = Pricing.HourlyRate,
                Status = ProjectStatus.InProgress,
                Amount = 0m,
                Customer = unit.Customers.Get(1),
                Team = unit.Teams.Get("A")
            });
            unit.Projects.Insert(new Project()
            {
                Name = "Bravo Project",
                Monogram = "BRP",
                Description = "Bravo test project on bravo",
                StartDate = new DateTime(2018, 02, 15),
                Pricing = Pricing.FixedPrice,
                Status = ProjectStatus.InProgress,
                Amount = 10000m,
                Customer = new Customer()
                {
                    Name = "Bravo company",
                    Contact = "Bravo person",
                    Email = "bravomail@alpha.com",
                    Image = "BravoImage.jpg",
                    Monogram = "BRV",
                    Phone = "Bravo number",
                    Address = new Address()
                    {
                        Road = "Bravo road, 1",
                        ZipCode = "1000",
                        City = "Bravo city"
                    },
                    Status = CustomerStatus.Client
                },
                Team = unit.Teams.Get("B")
            });
            unit.Save();
        }

        void AddEmployees(UnitOfWork unit)
        {
            unit.Employees.Insert(new Employee()
            {
                FirstName = "First1",
                LastName = "Last1",
                BirthDate = new DateTime(1990, 01, 01),
                BeginDate = new DateTime(2018, 01, 15),
                Email = "first@testmail.com",
                Image = "FirstEmp.jpg",
                Phone = "Emp1 Phone number",
                Position = unit.Roles.Get("SD"),
                Salary = 3000m,
                Status = EmployeeStatus.Active
            });
            unit.Employees.Insert(new Employee()
            {
                FirstName = "First2",
                LastName = "Last2",
                BirthDate = new DateTime(1991, 01, 01),
                BeginDate = new DateTime(2018, 01, 15),
                Email = "second@testmail.com",
                Image = "SecondEmp.jpg",
                Phone = "Emp2 Phone number",
                Position = unit.Roles.Get("UX"),
                Salary = 5000m,
                Status = EmployeeStatus.Active
            });
            unit.Save();
        }

        void AddDays(UnitOfWork unit)
        {
            unit.Calendar.Insert(new Day()
            {
                Date = DateTime.Today,
                Hours = 6,
                Type = DayType.WorkingDay,
                Employee = unit.Employees.Get(1)
            });
            unit.Calendar.Insert(new Day()
            {
                Date = DateTime.Today,
                Hours = 6,
                Type = DayType.WorkingDay,
                Employee = unit.Employees.Get(2)
            });
            unit.Save();
        }
           
        void AddDetails(UnitOfWork unit)
        {
            unit.Details.Insert(new Detail()
            {
                Hours = 7,
                Description = "Lorem ipsum 1",
                Day = unit.Calendar.Get(1),
                Project = unit.Projects.Get(1)
            });
            unit.Details.Insert(new Detail()
            {
                Hours = 7,
                Description = "Lorem ipsum 2",
                Day = unit.Calendar.Get(2),
                Project = unit.Projects.Get(2)
            });
            unit.Save();
        }

        void AddEngagements(UnitOfWork unit)
        {
            unit.Engagements.Insert(new Engagement()
            {
                Hours=6,
                Employee = unit.Employees.Get(1),
                Team = unit.Teams.Get("A"),
                Role = unit.Roles.Get("SD")
            });
            unit.Engagements.Insert(new Engagement()
            {
                Hours = 3,
                Employee = unit.Employees.Get(2),
                Team = unit.Teams.Get("B"),
                Role = unit.Roles.Get("UX")
            });
            unit.Save();
        }
    }
}

