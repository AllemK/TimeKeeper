using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Reports
{
    public class MonthlyModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal? Total { get; set; }
        public List<ProjectItem> Projects { get; set; }
        public ICollection<MonthlyItem> Items { get; set; }

        public MonthlyModel()
        {
            Projects = new List<ProjectItem>();
            Items = new List<MonthlyItem>();
        }
    }

    public class ProjectItem
    {
        public string Project { get; set; }
        public decimal? Hours { get; set; }
    }

    public class MonthlyItem
    {
        public string Employee { get; set; }
        public decimal Total { get; set; }
        public decimal[] Hours { get; set; }

        public MonthlyItem(int listSize)
        {
            Hours = new decimal[listSize];
        }
    }
}