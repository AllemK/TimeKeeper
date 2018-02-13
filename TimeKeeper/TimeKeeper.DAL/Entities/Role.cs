using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Helper;

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