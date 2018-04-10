using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeKeeper.DAL.Entities
{
    public class Team : BaseClass<string>
    {
        public Team()
        {
            Engagements = new List<Engagement>();
            Projects = new List<Project>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Engagement> Engagements { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
