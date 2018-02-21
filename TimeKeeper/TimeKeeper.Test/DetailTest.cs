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
        public void CheckDetails()
        {
            int expected = 2;

            int numberOfDetails = unit.Details.Get().Count();

            Assert.AreEqual(expected, numberOfDetails);
        }

        [TestMethod]
        public void AddDetail()
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
        public void UpdateDetail()
        {
            Detail d = new Detail()
            {
                Description = "Modified existing task",
                Hours = 8
            };

            unit.Details.Update(d, 1);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(d.Description, unit.Details.Get(d.Id));
        }

        [TestMethod]
        public void DeleteDetail()
        {
            Detail d = unit.Details.Get(2);

            unit.Details.Delete(d);
            unit.Save();

            Assert.IsNull(unit.Details.Get(2));
        }

        [TestMethod]
        public void CheckValidityForDetail()
        {
            Detail d = new Detail();

            unit.Details.Insert(d);

            Assert.IsFalse(unit.Save());
        }

        //Tests for controller
        [TestMethod]
        public void ControllerGetAllDetails()
        {
            var controller = new DetailsController();

            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<DetailModel>>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerGetDetailById()
        {
            var controller = new DetailsController();

            var response = controller.Get(1);
            var result = (OkNegotiatedContentResult<DetailModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerPostDetail()
        {
            var controller = new DetailsController();
            Detail d = new Detail()
            {
                Description = "Add new tasks",
                Hours = 8,
                Day = unit.Calendar.Get(1),
                Project = unit.Projects.Get(1)
            };

            var response = controller.Post(d);
            var result = (OkNegotiatedContentResult<Detail>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerPutDetail()
        {
            var controller = new DetailsController();
            Detail d = new Detail()
            {
                Description = "Add new tasks",
                Hours = 8,
            };
            int id = 3;

            var response = controller.Put(d, id);
            var result = (OkNegotiatedContentResult<Detail>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

        }

        [TestMethod]
        public void ControllerDeleteDetail()
        {
            var controller = new DetailsController();

            var response = controller.Delete(1);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }
    }
}