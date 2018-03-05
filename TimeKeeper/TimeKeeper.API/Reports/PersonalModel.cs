using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Reports
{
    public class PersonalModel
    {
        public decimal TotalHours { get; set; }
        public decimal Utilization { get; set; }
        public int BradfordFactor { get; set; }
        public string[] Days { get; set; }

        public PersonalModel(int size)
        {
            Days = new string[size];
        }
    }
}