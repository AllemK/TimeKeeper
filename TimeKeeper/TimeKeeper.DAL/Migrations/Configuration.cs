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
            for (int i = 0; i < 5; i++)
            {
                int j = 0;
                Employee tmp = new Employee()
                {
                    FirstName = "Emp" + i.ToString(),
                    LastName = "Loy" + i.ToString(),
                    Role = context.Role.Find("Rola" + i.ToString()),
                    Birthday = DateTime.Now,
                    Status = Status.Active,
                    BeginDate = DateTime.Now
                };
                if (j > 3)
                {
                    j = 0;
                }
                tmp.Role = context.Role.Find("Rola" + j.ToString());
                j++;
                context.Employee.Add(tmp);
            }

        }
    }
}
