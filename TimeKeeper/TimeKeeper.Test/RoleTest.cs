using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.Test
{
    [TestClass]
    public class RoleTest
    {
        DAL.Repository.UnitOfWork unit = new DAL.Repository.UnitOfWork();
        [TestMethod]
        public void CheckRoles()
        {
            int checkNumberOfRoles = 0;
            int expected = 2;

            checkNumberOfRoles = unit.Roles.Get().Count();

            Assert.AreEqual(expected, checkNumberOfRoles);
        }

        [TestMethod]
        public void AddRole()
        {
            Role r = new Role()
            {
                Id = "ADM",
                HourlyRate = 30m,
                MonthlyRate = 4000m,
                Name = "Admin",
                Type = RoleType.AppRole
            };

            unit.Roles.Insert(r);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual("ADM", unit.Roles.Get("ADM").Id);
        }

        [TestMethod]
        public void UpdateRole()
        {
            Role r = unit.Roles.Get().FirstOrDefault();
            decimal expectedHourly = 25m;
            decimal expectedMonthly = 3000m;

            r.HourlyRate = 25m;
            r.MonthlyRate = 5000m;

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(expectedHourly, unit.Roles.Get().FirstOrDefault().HourlyRate);
            Assert.AreNotEqual(expectedMonthly, unit.Roles.Get().FirstOrDefault().MonthlyRate);
        }

        [TestMethod]
        public void DeleteRole()
        {
            Role r = unit.Roles.Get().LastOrDefault();

            unit.Roles.Delete(r);

            Assert.IsTrue(unit.Save());
            Assert.IsNull(r);
        }

        //Test for Roles controller
        //[TestMethod]
        //public void GetRoles()
        //{
        //    RolesController rc = new RolesController();

        //    rc.Get();
        //}
    }
}
