using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.Test
{
    [TestClass]
    public class TeamTest
    {
        UnitOfWork unit = new UnitOfWork();
        [TestMethod]
        public void CheckTeamCount()
        {
            int expected = 2;

            int numberOfTeams = unit.Teams.Get().ToList().Count;

            Assert.AreEqual(expected, numberOfTeams);
        }

        [TestMethod]
        public void AddTeam()
        {
            Team t = new Team()
            {
                Id = "DA",
                Name = "Dakota",
                Description = "Team consisting of members",
                Image = "ImageLink"
            };

            unit.Teams.Insert(t);

            Assert.IsTrue(unit.Save());
        }

        [TestMethod]
        public void UpdateTeam()
        {
            Team t = unit.Teams.Get("DA");

            t.Description = "Backend development";
            unit.Save();

            Assert.AreSame(unit.Teams.Get("DA"), t);
        }

    }
}
