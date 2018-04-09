﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Reports
{
    public class ProjectHistory
    {
        public int FirstYear { get; set; }
        public int LastYear { get; set; }
        public int TotalHours { get; set; }
        public string Employee { get; set; }
        public int[] YearlyHours { get; set; }

        public ProjectHistory(int years)
        {
            YearlyHours = new int[years];
        }
    }
}