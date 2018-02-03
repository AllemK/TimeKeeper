using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public class Role : BaseClass<string>
    {
        public string Name { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}