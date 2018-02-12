using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Helper;

namespace TimeKeeper.DAL.Entities
{
    public class Engagement : BaseClass<int>
    {
        [Required]
        [Range(0,40)]
        [Precision(3,1)]
        public decimal Hours { get; set; }

        [Required]
        public virtual Team Team { get; set; }
        [Required]
        public virtual Employee Employee { get; set; }
        [Required]
        public virtual Role Role { get; set; }
    }
}
