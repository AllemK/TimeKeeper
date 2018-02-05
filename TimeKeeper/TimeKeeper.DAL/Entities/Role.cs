using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal MonthlyRate { get; set; }
        public RoleType Type { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Engagement> Members { get; set; }
    }
}