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
        Trial,
        Active,
        Leaver
    }

    public class Employee : BaseClass<int>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Image { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public EmployeeStatus Status { get; set; }

        public virtual Role Position { get; set; }

        public virtual ICollection<Day> Days { get; set; }
        public virtual ICollection<Engagement> Members { get; set; }
    }
}
