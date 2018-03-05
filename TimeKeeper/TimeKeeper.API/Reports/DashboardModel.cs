using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Reports
{
    public class DashboardModel
    {
        public decimal TotalHours { get; set; }
        public int NumberOfProjects { get; set; }
        public int NumberOfEmployees { get; set; }
        public decimal PTOHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal Revenue { get; set; }

    }
}