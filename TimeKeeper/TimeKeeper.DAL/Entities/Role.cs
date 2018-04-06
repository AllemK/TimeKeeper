using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeKeeper.Utility;

namespace TimeKeeper.DAL.Entities
{
    public enum RoleType
    {
        Position,
        TeamRole,
        AppRole
    }

    public class Role : BaseClass<string>
    {
        public Role()
        {
            Employees = new List<Employee>();
            Engagements = new List<Engagement>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [Precision(5,2)]
        public decimal HourlyRate { get; set; }
        [Required]
        [Precision(7,2)]
        public decimal MonthlyRate { get; set; }
        [Required]
        public RoleType Type { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Engagement> Engagements { get; set; }
    }
}