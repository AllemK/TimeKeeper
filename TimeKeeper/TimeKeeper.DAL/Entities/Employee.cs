using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Utility;

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
        public Employee()
        {
            Days = new List<Day>();
            Engagements = new List<Engagement>();
        }

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(35)]
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Image { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }
        [Required]
        [Precision(8,2)]
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
        public virtual ICollection<Engagement> Engagements { get; set; }
    }
}
