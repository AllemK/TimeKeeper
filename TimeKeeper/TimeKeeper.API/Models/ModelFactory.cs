using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Models
{
    public class ModelFactory
    {
        public TeamModel Create(Team t)
        {
            return new TeamModel
            {
                TeamId = t.Id,
                TeamName = t.Name,
                TeamImage = t.Image,
                Members = t.Engagements.Select(e=>Create(e)).ToList()
            };
        }

        public MemberModel Create(Engagement e)
        {
            return new MemberModel
            {
                Role = e.Role.Name,
                Employee = e.Employee.FirstName+" "+e.Employee.LastName,
                Hours = e.Hours
            };
        }
    }
}