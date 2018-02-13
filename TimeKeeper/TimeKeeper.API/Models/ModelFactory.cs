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
                Members = t.Engagements.Select(e=>Create(e)).ToList(),
                Projects = t.Projects.Select(p => Create(p)).ToList()
            };
        }

        public RoleModel Create(Role r)
        {
            return new RoleModel()
            {
                Id = r.Id,
                Name = r.Name,
                Type = r.Type.ToString(),
                HourlyRate = r.HourlyRate,
                MonthlyRate = r.MonthlyRate,
                Members = r.Engagements.Select(e => Create(e)).ToList()
            };
        }

        public MemberModel Create(Engagement e)
        {
            return new MemberModel
            {
                Team = e.Team.Name,
                Role = e.Role.Name,
                Employee = e.Employee.FirstName+" "+e.Employee.LastName,
                Hours = e.Hours
            };
        }

        public ProjectModel Create(Project p)
        {
            return new ProjectModel()
            {
                Name = p.Name,
                Monogram = p.Monogram,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status.ToString(),
                Pricing = p.Pricing.ToString(),
                Amount = p.Amount,
                Customer = p.Customer.Name,
                Team = p.Team.Name
            };
        }
    }
}