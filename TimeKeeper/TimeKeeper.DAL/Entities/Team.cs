using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public class Team : BaseClass<string>
    {
        //public object engagements;

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Engagement> Members { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
