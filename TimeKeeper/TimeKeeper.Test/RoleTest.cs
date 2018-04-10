using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Helper;
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
        public void RoleCheck()
        {
            int checkNumberOfRoles = 0;
            int expected = 2;

            checkNumberOfRoles = unit.Roles.Get().Count();

            Assert.AreEqual(expected, checkNumberOfRoles);
        }

        [TestMethod]
        public void RoleAdd()
        {
            Role r = new Role()
            {
                Id = "ADM",
                HourlyRate = 30m,
                MonthlyRate = 4000m,
                Name = "Admin",
                Type = RoleType.Position
            };

            unit.Roles.Insert(r);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual("ADM", unit.Roles.Get("ADM").Id);
        }

        [TestMethod]
        public void RoleUpdate()
        {
            Role r = unit.Roles.Get("ADM");
            decimal expectedHourly = 25m;
            decimal expectedMonthly = 3000m;

            r.HourlyRate = 25m;
            r.MonthlyRate = 5000m;
            unit.Roles.Update(r, r.Id);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(expectedHourly, unit.Roles.Get("ADM").HourlyRate);
            Assert.AreNotEqual(expectedMonthly, unit.Roles.Get("ADM").MonthlyRate);
        }

        [TestMethod]
        public void RoleDelete()
        {
            Role r = unit.Roles.Get("ADM");

            unit.Roles.Delete(r);
            unit.Save();
            
            Assert.IsNull(unit.Roles.Get("ADM"));
        }

        [TestMethod]
        public void RoleCheckValidity()
        {
            Role r = new Role();
            Role r1 = unit.Roles.Get().FirstOrDefault();

            unit.Roles.Insert(r);

            Assert.IsFalse(unit.Save());

            r1.Name = "";

            Assert.IsFalse(unit.Save());
        }

        //Test for controller
        [TestMethod]
        public void RoleControllerGet()
        {
            var controller = new RolesController();

            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<RoleModel>>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void RoleControllerGetById()
        {
            var controller = new RolesController();

            var response = controller.Get("SD");
            var result = (OkNegotiatedContentResult<RoleModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void RoleControllerPost()
        {
            var controller = new RolesController();
            var mf = new ModelFactory();
            Role r = new Role()
            {
                Id = "AD",
                Name = "Admin",
                HourlyRate = 30m,
                MonthlyRate = 4000m,
                Type = RoleType.Position
            };

            var response = controller.Post(mf.Create(r));
            var result = (OkNegotiatedContentResult<RoleModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void RoleControllerPut()
        {
            var controller = new RolesController();
            var mf = new ModelFactory();
            Role r = unit.Roles.Get("AD");

            r.Name = "Administrator";
            var response = controller.Put(mf.Create(r), "AD");
            var result = (OkNegotiatedContentResult<RoleModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void RoleControllerDelete()
        {
            var controller = new RolesController();

            var response = controller.Delete("AD");
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }
    }
}
