using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.Test
{
    [TestClass]
    public class NameCounter
    {
        [TestMethod]
        public void NameCountCheck()
        {
            TimeKeeperContext context = new TimeKeeperContext();

            Employee emp = new Employee()
            {
                FirstName = "John",
                LastName = "Doe",
                Position = context.Roles.Find("DEV"),
                BirthDate = DateTime.Now,
                BeginDate = DateTime.Now
            };

            context.Employees.Add(emp);


            Employee emp2 = new Employee()
            {
                FirstName = "John",
                LastName = "Smith",
                Position = context.Roles.Find("DEV"),
                BirthDate = DateTime.Now,
                BeginDate = DateTime.Now,
            };
            context.Employees.Add(emp2);
            context.SaveChanges();

            int counter = 0;
            foreach (var k in context.Employees)
            {
                if (k.FirstName == "John")
                {
                    counter++;
                }
            }

            Assert.AreEqual(2, counter); // emp.Id, context.SaveChanges()
        }
    }
}
