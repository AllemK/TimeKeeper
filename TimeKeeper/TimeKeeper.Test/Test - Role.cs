using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.Test
{

    [TestClass]
    public class RoleTest
    {
        UnitOfWork unit = new UnitOfWork();
        [TestMethod]
        public void CheckRoles()
        {
            //Arrange
            TimeKeeperContext context = new TimeKeeperContext();
            //Act
            int roles = context.Roles.Count();
            //Assert
            Assert.AreEqual(2, roles);
        }
        [TestMethod]
        public void AddRole()
        {
            unit.Roles.Insert(new Role
            {
                Id = "ADM",
                Name = "Administrator",
                HourlyRate = 30m,
                MonthlyRate = 4000m,
                Type = RoleType.AppRole
            });

            Assert.AreEqual(true, unit.Save());
        }

        [TestMethod]
        public void UpdateRole()
        {
            Role r = unit.Roles.Get("ADM");

            r.MonthlyRate = 5000m;
            r.HourlyRate = 35m;

            Assert.AreEqual(35m, unit.Roles.Get("ADM").HourlyRate);
            Assert.AreEqual(5000m, unit.Roles.Get("ADM").MonthlyRate);
        }

        [TestMethod]
        public void DeleteRole()
        {
            Role r = unit.Roles.Get("SD");

            unit.Roles.Delete(r);
            unit.Save();

            Assert.AreEqual(null, unit.Roles.Get("SD"));
        }

        [TestMethod]
        public void InsertingNegativeHoursRole()
        {
            Role r = unit.Roles.Get("ADM");

            r.HourlyRate = -30m;
            unit.Save();
            unit.Dispose();
            unit = new UnitOfWork();
            Assert.AreEqual(-30m, unit.Roles.Get("ADM").HourlyRate);
        }
    }
}
