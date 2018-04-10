using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Reports
{
    public class ProjectHistory
    {
        public int FirstYear { get; set; }
        public int LastYear { get; set; }
        public ICollection<YearlyHours> YearlyHours { get; set; }

        public ProjectHistory()
        {
            YearlyHours = new List<YearlyHours>();
        }
    }

    public class YearlyHours
    {
        public int TotalHours { get; set; }
        public string Employee { get; set; }
        public int[] Hours { get; set; }

        public YearlyHours(int years)
        {
            Hours = new int[years];
        }
    }
}