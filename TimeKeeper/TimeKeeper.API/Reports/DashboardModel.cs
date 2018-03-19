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
        public decimal[] PTOHours { get; set; }
        public decimal[] OvertimeHours { get; set; }
        public decimal[] Revenue { get; set; }
        public decimal[] MissingEntries { get; set; }
        public decimal[] Utilization { get; set; }

        public DashboardModel(int numberOfTeams, int numberOfProjects, int numberOfMissingEntries)
        {
            PTOHours = new decimal[numberOfTeams];
            OvertimeHours = new decimal[numberOfTeams];
            Revenue = new decimal[numberOfProjects];
            MissingEntries = new decimal[numberOfMissingEntries];
            Utilization = new decimal[4];
        }

        public DashboardModel(int numberOfEmployees, int numberOfMissingEntries)
        {
            PTOHours = new decimal[numberOfEmployees];
            OvertimeHours = new decimal[numberOfEmployees];
            Revenue = null;
            MissingEntries = new decimal[numberOfMissingEntries];
            Utilization = new decimal[numberOfEmployees];
        }
    }
}