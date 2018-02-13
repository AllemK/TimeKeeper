using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class CalendarModel
    {
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public string Type { get; set; }
        public string Employee { get; set; }
        public ICollection<DetailModel> Details { get; set; }
    }
}