using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal MonthlyRate { get; set; }
        public virtual ICollection<MemberModel> Members { get; set; }
    }
}