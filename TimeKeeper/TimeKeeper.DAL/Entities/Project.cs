using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
	public enum Pricing
	{
		HourlyRate = 1,
		PerCapitaRate,
		FixedPrice,
		NotBillable
	}
	public enum ProjectStatus
	{
		InProgress = 1,
		OnHold,
		Finished,
		Canceled
	}
	public class Project : BaseClass<int>
	{
        [Required]
		public string Name { get; set; }
        [Required]
		public string Description { get; set; }
        [Required]
        public string Monogram { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
        [Required]
		public Pricing Pricing { get; set; }
        [Required]
		public ProjectStatus Status { get; set; }

        [Required]
        public virtual Team Team { get; set; }
        [Required]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Detail> Details { get; set; }
    }
}
