using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal HourlyRate { get; set; }
        [Required]
        public decimal MonthlyRate { get; set; }
        [Required]
        public RoleType Type { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Engagement> Engagements { get; set; }
    }
}