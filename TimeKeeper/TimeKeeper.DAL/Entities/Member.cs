using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public class Member : BaseClass<int>
    {
        public decimal Hours { get; set; }

        public virtual Team Team { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }

        public string TeamId { get; set; }
        public int EmployeeId { get; set; }
        public string RoleId { get; set; }
    }
}
