using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class TeamModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection <MemberModel> Members { get; set; }
    }
}