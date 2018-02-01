using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Models
{
    public abstract class BaseClass
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }

        public BaseClass()
        {
            CreatedBy = 0;
            CreatedOn = DateTime.UtcNow;
            Deleted = false;
        }
    }
}
