using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class TeamModel
    {
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamImage { get; set; }
        public ICollection<MemberModel> Members { get; set; }
    }
}