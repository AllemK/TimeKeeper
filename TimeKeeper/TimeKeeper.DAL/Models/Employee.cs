using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Models
{
    public enum Status
    {
        Active,

    }

    public class Employee:BaseClass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public enum Status { get; set; }

        public virtual Role Role { get; set; }

    }   
}
