using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeKeeper.Utility;

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
        public Day()
        {
            Details = new List<Detail>();
        }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Precision(3,1)]
        public decimal Hours { get; set; }
        [Required]
        public DayType Type { get; set; }

        [Required]
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Detail> Details { get; set; }
    }
}
