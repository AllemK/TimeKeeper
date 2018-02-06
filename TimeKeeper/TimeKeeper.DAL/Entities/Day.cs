using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public enum DayType
    {
        WorkingDay = 1,
        PublicHoliday,
        OtherAbsence,
        ReligiousDay,
        SickLeave,
        Vacation,
        BusinessAbsence
    }

    public class Day : BaseClass<int>
    {
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public DayType Type { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<Detail> Details { get; set; }
    }
}
