using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.Test
{
    [TestClass]
    public class TeamTest
    {
        UnitOfWork unit = new UnitOfWork();

        [TestMethod]
        public void CheckTeams()
        {
            int expected = 3;

            int numberOfTeams = unit.Teams.Get().Count();

            Assert.AreEqual(expected, numberOfTeams);
        }

        [TestMethod]
        public void AddTeam()
        {
            Team t = new Team()
            {
                Id = "OMG",
                Name="Omega",
                Description="Work on backend"
            };

            unit.Teams.Insert(t);

            Assert.IsTrue(unit.Save());
            Assert.IsNotNull(unit.Teams.Get("OMG"));
        }

        [TestMethod]
        public void UpdateTeam()
        {
            Team t = unit.Teams.Get().FirstOrDefault();
            string expected = "OM";

            t.Name = "OM";

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(expected, unit.Teams.Get().FirstOrDefault().Name);
        }

        [TestMethod]
        public void DeleteTeam()
        {
            Team t = unit.Teams.Get("OMG");

            unit.Teams.Delete(t);
            unit.Save();

            Assert.IsNull(unit.Teams.Get("OMG"));
        }

        [TestMethod]
        public void CheckValidityForTeams()
        {
            Team t = new Team();
            Team t1 = unit.Teams.Get().FirstOrDefault();

            unit.Teams.Insert(t);
            t1.Name = "";

            Assert.IsFalse(unit.Save());
        }

        //Tests for controllers
        [TestMethod]
        public void ControllerGetAllTeams()
        {
            var controller = new TeamsController();

            var response = controller.Get();
            var result = (OkNegotiatedContentResult<List<TeamModel>>)response;


            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public void ControllerGetTeamWithId()
        {
            var controller = new TeamsController();

            var response = controller.Get("A");
            var result = (OkNegotiatedContentResult<TeamModel>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

        }

        [TestMethod]
        public void ControllerPostTeam()
        {
            var controller = new TeamsController();
            Team t = new Team()
            {
                Id = "DAK",
                Name = "Dakota",
                Description = "Teaam will work on few related projects"

            };

            var response = controller.Post(t);
            var result = (OkNegotiatedContentResult<Team>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

        }

        [TestMethod]
        public void ControllerPutTeam()
        {
            var controller = new TeamsController();
            Team t = new Team()
            {
                Id = "DAK",
                Name = "DAKOTA",
                Description = "Teaam will work on few related projects like TimeKEEPER"

            };

            string id = "DAK";
            var response = controller.Put(t, id);
            var result = (OkNegotiatedContentResult<Team>)response;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

        }

        [TestMethod]
        public void ControllerDeleteTeam()
        {
            var controller = new TeamsController();

            var response = controller.Delete("DAK");
            var result = (OkResult)response;

            Assert.IsNotNull(result);
        }
    }
}
