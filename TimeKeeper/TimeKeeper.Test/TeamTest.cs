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
    public class TeamTest
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
        public void TeamCheck()
        {
            int expected = 2;

            int numberOfTeams = unit.Teams.Get().Count();

            Assert.AreEqual(expected, numberOfTeams);
        }

        [TestMethod]
        public void TeamAdd()
        {
            Team t = new Team()
            {
                Id = "OMG",
                Name = "Omega",
                Description = "Work on backend"                
            };

            unit.Teams.Insert(t);

            Assert.IsTrue(unit.Save());
            Assert.IsNotNull(unit.Teams.Get("OMG"));
        }

        [TestMethod]
        public void TeamUpdate()
        {
            Team t = unit.Teams.Get("OMG");
            string expected = "OM";

            t.Name = "OM";
            unit.Teams.Update(t, t.Id);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(expected, unit.Teams.Get("OMG").Name);
        }

        [TestMethod]
        public void TeamDelete()
        {
            Team t = unit.Teams.Get("OMG");

            unit.Teams.Delete(t);
            unit.Save();

            Assert.IsNull(unit.Teams.Get("OMG"));
        }

        [TestMethod]
        public void TeamCheckValidity()
        {
            Team t = new Team();
            Team t1 = unit.Teams.Get().FirstOrDefault();

            unit.Teams.Insert(t);

            Assert.IsFalse(unit.Save());

            t1.Name = "";

            Assert.IsFalse(unit.Save());
        }

        //Tests for controllers
        [TestMethod]
        public void TeamControllerGetAll()
        {
            var controller = new TeamsController();
            var h = new Header();

            var response = controller.Get(h);
            var result = (OkNegotiatedContentResult<List<TeamModel>>)response;


            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void TeamControllerGetById()
        {
            var controller = new TeamsController();

            var response = controller.Get("A");
            var result = (OkNegotiatedContentResult<TeamModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

        }

        [TestMethod]
        public void TeamControllerPost()
        {
            var controller = new TeamsController();
            TeamModel t = new TeamModel()
            {
                Id = "DAK",
                Name = "Dakota",
                Description = "Team will work on few related projects"
            };

            var response = controller.Post(t);
            var result = (OkNegotiatedContentResult<TeamModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

        }

        [TestMethod]
        public void TeamControllerPut()
        {
            var controller = new TeamsController();
            TeamModel t = new TeamModel()
            {
                Id = "DAK",
                Name = "DAKOTA",
                Description = "Team will work on few related projects like TimeKEEPER"
            };

            var response = controller.Put(t, "DAK");
            var result = (OkNegotiatedContentResult<TeamModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

        }

        [TestMethod]
        public void TeamControllerDelete()
        {
            var controller = new TeamsController();

            var response = controller.Delete("DAK");
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }
    }
}
