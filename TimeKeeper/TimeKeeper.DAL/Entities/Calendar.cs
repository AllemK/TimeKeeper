using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public enum CategoryDay
    {
        WorkingDay = 1,
        Holiday,
        Vacation,
        SickDay,
        BusinessAbsence,
        ReligiousDay,
        Other

    }

    public class Calendar : BaseClass<int>
    {
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public CategoryDay TypeOfDay { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<Detail> Details { get; set; }
    }
}
