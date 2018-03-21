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
    public class DayTest
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
        public void DayCheck()
        {
            int numberOfDays = unit.Calendar.Get().Count();

            int expected = 2;

            Assert.AreEqual(expected, numberOfDays);
        }

        [TestMethod]
        public void DayAdd()
        {
            Day d = new Day()
            {
                Date = DateTime.Today,
                Hours = 4,
                Type = DayType.WorkingDay,
                Employee = unit.Employees.Get(1)
            };

            unit.Calendar.Insert(d);

            Assert.IsTrue(unit.Save());
            Assert.IsNotNull(unit.Calendar.Get(3));
        }

        [TestMethod]
        public void DayUpdate()
        {
            Day d = unit.Calendar.Get(3);
            DateTime expected = new DateTime(2018, 1, 5);

            d.Date = new DateTime(2018, 1, 5);
            unit.Calendar.Update(d, 3);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(expected, unit.Calendar.Get(3).Date);
        }

        [TestMethod]
        public void DayDelete()
        {
            Day d = unit.Calendar.Get(3);

            unit.Calendar.Delete(d);
            unit.Save();

            Assert.IsNull(unit.Calendar.Get(3));
        }

        [TestMethod]
        public void DayCheckValidity()
        {
            Day d = new Day();

            unit.Calendar.Insert(d);

            Assert.IsFalse(unit.Save());
        }

        //Test for controller
        [TestMethod]
        public void DayControllerGet()
        {
            var controller = new DaysController();
            var h = new Header();

            var response = controller.Get(1,2017,1);
            var result = (OkNegotiatedContentResult<List<CalendarModel>>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DayControllerGetById()
        {
            var controller = new DaysController();

            var response = controller.Get(1);
            var result = (OkNegotiatedContentResult<CalendarModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DayControllerPost()
        {
            var controller = new DaysController();
            DayModel d = new DayModel()
            {
                Date = DateTime.Today,
                Hours = 8,
                TypeOfDay = "SickLeave",
                //Employee = unit.Employees.Get(2)
            };

            var response = controller.Post(d);
            var result = (OkNegotiatedContentResult<CalendarModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DayControllerPut()
        {
            var controller = new DaysController();
            Day d = unit.Calendar.Get(5);
            d.Date = new DateTime(2018, 02, 16);
            ModelFactory mf = new ModelFactory();

            var response = controller.Put(mf.Create(d),5);
            var result = (OkNegotiatedContentResult<CalendarModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void DayControllerDelete()
        {
            var controller = new DaysController();

            var response = controller.Delete(5);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }
    }
}
