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
            return new TeamModel()
            {
                Id = t.Id,
                Name = t.Name,
                Image = t.Image,
                Members = t.Members.Select(e => Create(e)).ToList()

            };
        }


        public MemberModel Create(Engagement e)
        {
            return new MemberModel()
            {
                Rola = e.Role.Name,
                Employee = e.Employee.FirstName,
                Hours = e.Hours
            };
        }
    }
}