using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeKeeper.Utility;

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
        public Project()
        {
            Details = new List<Detail>();
        }

        [Required]
        [MaxLength(25)]
		public string Name { get; set; }
        [Required]
		public string Description { get; set; }
        [Required]
        [MaxLength(3)]
        public string Monogram { get; set; }
        [Required]
        [Precision(9,2)]
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
