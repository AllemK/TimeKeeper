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
                Email = "johndo@mistral.ba",
                Phone = "061-555-111",
                Salary = 1000m,
                Status = EmployeeStatus.Active,
                BirthDate = DateTime.Now,
                BeginDate = DateTime.Now
            };

            context.Employees.Add(emp);
            context.SaveChanges();

            Assert.AreEqual(1, emp.Id);
            Assert.AreEqual(expected, emp.FirstName + " " + emp.LastName);
        }
    }
}
