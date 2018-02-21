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
    public class ProjectTest
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
        public void CheckProjects()
        {
            int checkNumberOfProjects = 0;
            int expected = 2;

            checkNumberOfProjects = unit.Projects.Get().Count();

            Assert.AreEqual(expected, checkNumberOfProjects);
        }

        [TestMethod]
        public void AddProject()
        {
            Project p = new Project()
            {
                Name = "Delta",
                Description = "Delta person",
                Monogram = "DEP",
                Amount = 23.5m,
                StartDate = new DateTime(2018, 01, 15),
                Pricing = Pricing.HourlyRate,
                Status = ProjectStatus.Finished,
                Customer = unit.Customers.Get(1),
                Team = unit.Teams.Get("B")
            };

            unit.Projects.Insert(p);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual("Delta", unit.Projects.Get(x => x.Id == p.Id).FirstOrDefault().Name);
        }

        [TestMethod]
        public void UpdateProject()
        {
            Project p = unit.Projects.Get().FirstOrDefault();
            string expected = "Delta";

            p.Name = "Delta";

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(expected, unit.Projects.Get().FirstOrDefault().Name);
        }

        [TestMethod]
        public void DeleteProject()
        {
            Project p = unit.Projects.Get(x => x.Id == 2).FirstOrDefault();

            unit.Projects.Delete(p);
            unit.Save();

            Assert.IsNull(unit.Projects.Get(2));
        }

        [TestMethod]
        public void CheckValidityForProjects()
        {
            Project p = new Project();
            Project p1 = unit.Projects.Get().FirstOrDefault();

            unit.Projects.Insert(p);
            p1.Name = "";

            Assert.IsFalse(unit.Save());
        }

        //Tests for controller
        [TestMethod]
        public void ControllerGetAllProjects()
        {
            var controller = new ProjectsController();
            var h = new Header();

            var response = controller.Get(h);
            var result = (OkNegotiatedContentResult<List<ProjectModel>>)response;


            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerGetProjectById()
        {
            var controller = new ProjectsController();

            var response = controller.Get(1);
            var result = (OkNegotiatedContentResult<ProjectModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerPostProject()
        {
            var Controller = new ProjectsController();
            Project p = new Project()
            {

                Name = "NewProject",
                Description = "This is a new project",
                Monogram = "PRO",
                Amount = 25.6m,
                Pricing = Pricing.FixedPrice,
                StartDate = new DateTime(2018, 02, 15),
                Status = ProjectStatus.OnHold

            };

            var response = Controller.Post(p);
            var result = (OkNegotiatedContentResult<Project>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerPutProject()
        {
            var controller = new ProjectsController();
            Project p = new Project()
            {
                Id=1,
                Name = "NewProject",
                Description = "Teaam will work on few projects",
                Pricing = Pricing.NotBillable,
                Amount = 23.56m,
                Monogram = "NEW",
                Status = ProjectStatus.InProgress,
                StartDate = new DateTime(2018, 05, 05)

            };

            var response = controller.Put(p, 1);
            var result = (OkNegotiatedContentResult<Project>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerDeleteProject()
        {
            var controller = new ProjectsController();

            var response = controller.Delete(1);
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }
    }
}
