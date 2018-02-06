using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.Test
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void AddEmployee()
        {
            TimeKeeperContext context = new TimeKeeperContext();

            string expected = "John Doe";
            Employee emp = new Employee()
            {
                FirstName = "John",
                LastName = "Doe",
                Position = context.Roles.Find("DEV"),
                BirthDate = DateTime.Now,
                BeginDate = DateTime.Now
            };

            context.Employees.Add(emp);
            context.SaveChanges();

            Assert.AreEqual(10, emp.Id);
            Assert.AreEqual(expected, emp.FirstName + " " + emp.LastName);
        }
    }
}
