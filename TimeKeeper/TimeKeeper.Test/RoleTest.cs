using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.Test
{
    [TestClass]
    public class RoleTest
    {
        UnitOfWork unit = new UnitOfWork();

        [TestInitialize]
        public void InitializeHttpContext()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );
        }

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
            Role r = unit.Roles.Get("ADM");

            unit.Roles.Delete(r);
            unit.Save();
            
            Assert.IsNull(unit.Roles.Get("ADM"));
        }

        [TestMethod]
        public void CheckValidityForRoles()
        {
            Role r = new Role();
            Role r1 = unit.Roles.Get().FirstOrDefault();

            unit.Roles.Insert(r);
            r1.Name = "";

            Assert.IsFalse(unit.Save());
        }

        //Test for controller
        [TestMethod]
        public void ControllerGetAllRoles()
        {
            var controller = new RolesController();

            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<RoleModel>>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerGetRoleById()
        {
            var controller = new RolesController();

            var response = controller.Get("SD");
            var result = (OkNegotiatedContentResult<RoleModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerPostRole()
        {
            var controller = new RolesController();
            Role r = new Role()
            {
                Id = "AD",
                Name = "Admin",
                HourlyRate = 30m,
                MonthlyRate = 4000m,
                Type = RoleType.AppRole
            };

            var response = controller.Post(r);
            var result = (OkNegotiatedContentResult<Role>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerPutRole()
        {
            var controller = new RolesController();
            Role r = unit.Roles.Get("AD");

            r.Name = "Administrator";
            var response = controller.Put(r, "AD");
            var result = (OkNegotiatedContentResult<Role>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerDeleteRole()
        {
            var controller = new RolesController();

            var response = controller.Delete("AD");
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }
    }
}
