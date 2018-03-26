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
    public class EngagementTest
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
        public void EngagementCheck()
        {
            int expected = 2;

            int numberOfEngagements = unit.Engagements.Get().Count();

            Assert.AreEqual(expected, numberOfEngagements);
        }

        [TestMethod]
        public void EngagementAdd()
        {
            Engagement e = new Engagement()
            {
                Hours = 8,
                Employee = unit.Employees.Get(1),
                Role = unit.Roles.Get("SD"),
                Team = unit.Teams.Get("A")
            };

            unit.Engagements.Insert(e);

            Assert.IsTrue(unit.Save());
            Assert.IsNotNull(unit.Engagements.Get(e.Id));
        }

        [TestMethod]
        public void EngagementUpdate()
        {
            Engagement e = new Engagement()
            {
                Id=3,
                Hours = 6
            };

            unit.Engagements.Update(e, 3);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(6, unit.Engagements.Get(3).Hours);
        }

        [TestMethod]
        public void EngagementDelete()
        {
            Engagement e = unit.Engagements.Get(3);

            unit.Engagements.Delete(e);
            unit.Save();

            Assert.IsNull(unit.Engagements.Get(3));
        }

        [TestMethod]
        public void EngagementCheckValidity()
        {
            Engagement e = new Engagement();

            unit.Engagements.Insert(e);

            Assert.IsFalse(unit.Save());
        }
    }
}
