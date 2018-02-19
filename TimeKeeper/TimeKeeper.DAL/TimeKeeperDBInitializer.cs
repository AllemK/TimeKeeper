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
                    //AddDays(unit);
                    //AddTasks(unit);
                    //AddEngagements(unit);
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
                Name = "Charlie Project",
                Monogram = "CHP",
                Description = "Charlie test project on charlie",
                StartDate = new DateTime(2018, 02, 15),
                Pricing = Pricing.FixedPrice,
                Status = ProjectStatus.InProgress,
                Amount = 10000m,
                Customer = new Customer()
                {
                    Name = "Charlie company",
                    Contact = "Charlie person",
                    Email = "charliemail@alpha.com",
                    Image = "CharlieImage.jpg",
                    Monogram = "CHP",
                    Phone = "Charlie number",
                    Address = new Address()
                    {
                        Road = "Charlie road, 1",
                        ZipCode = "1000",
                        City = "Charlie city"
                    },
                    Status = CustomerStatus.Client
                },
                Team = new Team()
                {
                    Name = "Charlie",
                    Id = "C",
                    Image = "C.jpg",
                    Description = "Charlie Team"
                }
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

        }
    }
}

