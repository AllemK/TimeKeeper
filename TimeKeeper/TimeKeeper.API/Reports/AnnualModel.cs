using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Reports
{
    public class AnnualModel
    {
        public string ProjectName { get; set; }
        public decimal TotalHours { get; set; }
        public decimal[] MonthlyHours { get; set; }

        public AnnualModel()
        {
            MonthlyHours = new decimal[12];
        }
    }
}