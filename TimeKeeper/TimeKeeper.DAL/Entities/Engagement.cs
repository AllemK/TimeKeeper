using System.ComponentModel.DataAnnotations;
using TimeKeeper.Utility;

namespace TimeKeeper.DAL.Entities
{
    public class Engagement : BaseClass<int>
    {
        [Required]
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
