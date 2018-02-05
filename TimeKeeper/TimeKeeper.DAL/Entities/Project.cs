using System;
using System.Collections.Generic;
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
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public Pricing Pricing { get; set; }
		public ProjectStatus ProjectStatus { get; set; }

        public virtual Team Team { get; set; }
        public virtual Customer Customer { get; set; }

        public string TeamId { get; set; }
        public int CustomerId { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
