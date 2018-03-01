using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Utility;

namespace TimeKeeper.DAL.Entities
{
    public class Detail : BaseClass<int>
    { 
        [Required]
        public string Description { get; set; }
        [Required]
        [Precision(3,1)]
        public decimal Hours { get; set; }

        //[Required]
        public virtual Project Project { get; set; }
        //[Required]
        public virtual Day Day { get; set; }
    }
}
