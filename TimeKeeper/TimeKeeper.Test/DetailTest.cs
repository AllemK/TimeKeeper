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
    public class DetailTest
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
        public void DetailCheck()
        {
            int expected = 2;

            int numberOfDetails = unit.Details.Get().Count();

            Assert.AreEqual(expected, numberOfDetails);
        }

        [TestMethod]
        public void DetailAdd()
        {
            Detail d = new Detail()
            {
                Description = "Add new tasks",
                Hours = 9
            };

            unit.Details.Insert(d);

            Assert.IsTrue(unit.Save());
            Assert.IsNotNull(unit.Details.Get(d.Id));
        }

        [TestMethod]
        public void DetailUpdate()
        {
            Detail d = new Detail()
            {
                Id = 1,
                Description = "Modified existing task",
                Hours = 8
            };

            unit.Details.Update(d, 1);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(d.Description, unit.Details.Get(1).Description);
        }

        [TestMethod]
        public void DetailDelete()
        {
            Detail d = unit.Details.Get(2);

            unit.Details.Delete(d);
            unit.Save();

            Assert.IsNull(unit.Details.Get(2));
        }

        [TestMethod]
        public void DetailCheckValidity()
        {
            Detail d = new Detail();

            unit.Details.Insert(d);

            Assert.IsFalse(unit.Save());
        }
    }
}