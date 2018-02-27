using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class TeamModel
    {
        [Required(ErrorMessage = "ID is required")]
        [MaxLength(128,ErrorMessage = "ID is too long")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20,ErrorMessage = "Name is too long, must be 20 characters")]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public ICollection<EngagementModel> Engagements { get; set; }
        public ICollection<ProjectModel> Projects { get; set; }

        public TeamModel()
        {
            Engagements = new List<EngagementModel>();
            Projects = new List<ProjectModel>();
        }
    }
}