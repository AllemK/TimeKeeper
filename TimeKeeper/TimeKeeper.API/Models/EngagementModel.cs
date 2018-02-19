using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class EngagementModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Employee { get; set; }
        public decimal Hours { get; set; }
        public string Team { get; set; }
    }
}