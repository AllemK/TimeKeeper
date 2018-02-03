﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
    public class Task : BaseClass<int>
    { 
        public string Description { get; set; }
        public decimal Hours { get; set; }

        public virtual Project Project { get; set; }
        public int ProjectId { get; set; }
        public virtual Calendar Calendar { get; set; }
        public int CalendarId { get; set; }
    }
}
