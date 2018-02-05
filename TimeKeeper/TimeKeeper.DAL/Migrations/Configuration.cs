namespace TimeKeeper.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TimeKeeper.DAL.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<TimeKeeper.DAL.TimeKeeperContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TimeKeeperContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            
            var customers = new List<Customer>();
            var teams = new List<Team>();
            var roles = new List<Role>();
            var employees = new List<Employee>();
            var projects = new List<Project>();
            var days = new List<Calendar>();
            var members = new List<Member>();
            var tasks = new List<Task>();

            for (int i = 1; i <= 15; i++)
            {
                Address a = new Address()
                {
                    City = "City" + i.ToString(),
                    Road = "Road" + i.ToString(),
                    Zip = i * 1000
                };
                customers.Add(new Customer()
                {
                    Id = i,
                    Name = "Customer" + i.ToString(),
                    Image = "ImgCust" + i.ToString(),
                    Address = a,
                    Contact = "Cont" + i.ToString(),
                    Email = "mail" + i.ToString() + "@something.com",
                    Monogram = "Monogram" + i.ToString(),
                    Phone = "PhoneCust" + i.ToString(),
                    Status = (i % 3) == 0 ? CustomerStatus.Client : CustomerStatus.Prospect
                });
            }
            customers.ForEach(c => context.Customer.AddOrUpdate(x => x.Id, c));
            context.SaveChanges();
            
            for (int i = 1; i <= 5; i++)
            {
                teams.Add(new Team()
                {
                    Id = "Team" + i.ToString(),
                    Name = "Team" + i.ToString(),
                    Description = "Desc" + i.ToString()
                });
            }
            teams.ForEach(t => context.Team.AddOrUpdate(x => x.Id, t));
            context.SaveChanges();
            
            int counter = 0;
            foreach (Customer c in context.Customer.Where(x => x.Status == CustomerStatus.Client))
            {
                Project p = new Project()
                {
                    Id = counter + 1,
                    Name = "Project" + (counter + 1).ToString(),
                    Pricing = Pricing.FixedPrice,
                    Description = "Desc" + (counter + 1).ToString(),
                    StartDate = new DateTime(2017, counter + 2, counter + 5),
                    ProjectStatus = ProjectStatus.InProgress,
                    CustomerId = c.Id,
                    TeamId = teams[counter++].Id
                };
                context.Project.AddOrUpdate(x => x.Id, p);
            }
            context.SaveChanges();
            
            for (int i = 1; i <= 6; i++)
            {
                roles.Add(new Role()
                {
                    Id = "Role" + i.ToString(),
                    Name = "Role" + i.ToString(),
                    HourlyPrice = 30m,
                    MonthlyPrice = 1200m
                });
            }
            roles.ForEach(r => context.Role.AddOrUpdate(x => x.Id, r));
            context.SaveChanges();
            
            counter = 0;
            for (int i = 1; i <= 30; i++)
            {
                employees.Add(new Employee()
                {
                    FirstName = "Emp" + i.ToString(),
                    LastName = "Loy" + i.ToString(),
                    Image = "ImgEmp" + i.ToString(),
                    Email = "mail" + i.ToString() + "@mistral.ba",
                    Phone = "PhoneEmp" + i.ToString(),
                    Birthday = new DateTime(1960+i,(i%12)+1,(i%30)+1),
                    BeginDate = DateTime.UtcNow,
                    Status = i % 3 == 0 ? Status.Trial : Status.Active,
                   // RoleId = roles[counter].Id,
                    Role = roles[counter++]
                });
                if (counter == roles.Count)
                {
                    counter = 0;
                }
            }
            employees.ForEach(e => context.Employee.AddOrUpdate(x => x.FirstName, e));
            context.SaveChanges();
            /*
            for (int i = 1; i <= 100; i++)
            {
                days.Add(new Calendar()
                {
                    Id = i,
                    Date = new DateTime(2017, (i % 12) + 1, (i % 30) + 1),
                    Hours = (i % 8) + 1,
                    TypeOfDay = CategoryDay.WorkingDay,
                    EmployeeId = (i % (context.Employee.Count())) + 1
                });
            }
            days.ForEach(d => context.Calendar.AddOrUpdate(x => x.Id, d));
            context.SaveChanges();

            counter = 0;
            for (int i = 1; i <= 100; i++)
            {
                tasks.Add(new Task()
                {
                    Id = i,
                    Hours = (i % 8) + 1,
                    Description = "Desc" + i.ToString(),
                    CalendarId = i,
                    ProjectId = (i % context.Project.Count()) + 1
                });
            }
            tasks.ForEach(ta => context.Task.AddOrUpdate(x => x.Id, ta));
            context.SaveChanges();

            for (int i = 1; i <= 100; i++)
            {
                members.Add(new Member()
                {
                    Id = i,
                    Hours = (i % 8)+1,
                    EmployeeId = (i % context.Employee.Count()) + 1,
                    TeamId = teams[(i%5)].Id,
                    RoleId = roles[(i%6)].Id
                });
            }
            members.ForEach(m => context.Member.AddOrUpdate(x => x.Id, m));
            context.SaveChanges();

            base.Seed(context);
            */
        }
    }
}
