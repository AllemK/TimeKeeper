using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class TaskModel
    {
        public string Description { get; set; }
        public decimal Hours { get; set; }
        public string Project { get; set; }
        public string Day { get; set; }
    }
}