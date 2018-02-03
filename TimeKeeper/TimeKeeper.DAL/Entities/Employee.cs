using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public enum Status
    {
        Active=1,
        Trial,
        Leaver
    }

    public class Employee : BaseClass<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Status Status { get; set; }

        public virtual Role Role { get; set; }
        public string RoleId { get; set; }
        public virtual ICollection<Calendar> Days { get; set; }
    }
}
