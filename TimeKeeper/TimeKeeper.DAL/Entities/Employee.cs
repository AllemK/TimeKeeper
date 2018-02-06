using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public enum EmployeeStatus
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
        public decimal Salary { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EmployeeStatus Status { get; set; }

        public virtual Role Position { get; set; }
        public virtual ICollection<Day> Days { get; set; }
        public virtual ICollection<Engagement> Members { get; set; }
    }
}
