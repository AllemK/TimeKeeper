using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.Test
{
    [TestClass]
    public class EmployeeTest
    {
        UnitOfWork unit = new UnitOfWork();

        [TestMethod]
        public void CheckEmployeeCount()
        {
            int expected = 0;

            int numberOfEmployees = unit.Employees.Get().ToList().Count;

            Assert.AreEqual(expected, numberOfEmployees);
        }

        [TestMethod]
        public void AddEmployee()
        {
            string expected = "John Doe";

            Employee emp = new Employee()
            {
                FirstName = "John",
                LastName = "Doe",
                Position = unit.Roles.Get("ADM"),
                Email = "johndo@mistral.ba",
                Phone = "061-555-111",
                Salary = 1000m,
                Status = EmployeeStatus.Active,
                BirthDate = new DateTime(1992, 1, 29),
                BeginDate = new DateTime(2018, 1, 15)
            };
            unit.Employees.Insert(emp);
            unit.Save();
			
            Assert.AreEqual(1, emp.Id);
            Assert.AreEqual(expected, emp.FirstName + " " + emp.LastName);
        }

        [TestMethod]
        public void UpdateEmployee()
        {
            Employee e = unit.Employees.Get(1);
            string expectedFullName = "James Smith";
            DateTime expectedBirthDay = new DateTime(1996, 10, 18);

            e.BirthDate = new DateTime(1996, 10, 18);
            e.FirstName = "James";
            e.LastName = "Smith";
            unit.Save();

            Assert.AreEqual(expectedFullName, unit.Employees.Get(1).FirstName + " " + unit.Employees.Get(1).LastName);
            Assert.AreEqual(expectedBirthDay, unit.Employees.Get(1).BirthDate);
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            Employee e = unit.Employees.Get(1);

            unit.Employees.Delete(e);
            unit.Save();
            Employee deleted = unit.Employees.Get(1);

            Assert.IsNull(deleted);
        }

        [TestMethod]
        public void InsertValidationEmployee()
        {
            var e = new Employee()
            {
                FirstName = "",
                LastName = "",
                BirthDate = new DateTime(),
                BeginDate = new DateTime(),
                Phone = ""
            };

            unit.Employees.Insert(e);

            Assert.AreEqual(false, unit.Save());
        }
    }
}
