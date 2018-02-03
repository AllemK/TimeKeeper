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
            base.Seed(context);

            var customers = new List<Customer>();
            var teams = new List<Team>();
            var roles = new List<Role>();
            var employees = new List<Employee>();
            var projects = new List<Project>();

            for (int i = 1; i < 15; i++)
            {
                Address a = new Address
                {
                    City = "City" + i.ToString(),
                    Road = "Road" + i.ToString(),
                    Zip = i * 1000
                };
                customers.Add(new Customer
                {
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

            for (int i = 1; i < 4; i++)
            {
                teams.Add(new Team
                {
                    Id = "Team" + i.ToString(),
                    Name = "Team" + i.ToString(),
                    Description = "Desc" + i.ToString()
                });
            }

            for (int i = 1; i < 6; i++)
            {
                roles.Add(new Role
                {
                    Id = "Role" + i.ToString(),
                    Name = "Role" + i.ToString(),
                    HourlyPrice = 30m,
                    MonthlyPrice = 1200m
                });
            }

            int x = 0;
            for (int i = 1; i < 30; i++)
            {
                employees.Add(new Employee
                {
                    FirstName = "Emp" + i.ToString(),
                    LastName = "Loy" + i.ToString(),
                    Image = "ImgEmp" + i.ToString(),
                    Email = "mail" + i.ToString() + "@mistral.ba",
                    Phone = "PhoneEmp" + i.ToString(),
                    Birthday = DateTime.MinValue.AddMonths(i),
                    BeginDate = DateTime.UtcNow,
                    Status = i % 3 == 0 ? Status.Trial : Status.Active,
                    Role = roles[x++]
                });
                if (x == roles.Count)
                {
                    x = 0;
                }
            }

            x = 0;
            foreach (var c in customers.Where(y=>y.Status==CustomerStatus.Client))
            {
                Project p = new Project
                {
                    Name = "Project" + (x+1).ToString(),
                    Pricing = Pricing.FixedPrice,
                    Description = "Desc"+(x+1).ToString(),
                    StartDate= new DateTime(2017,x+2,x+5),
                    ProjectStatus=ProjectStatus.InProgress,
                    Customer = c,
                    Team = teams[x++]
                };
                context.Project.AddOrUpdate(p);
            }
        }
    }
}
