using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
	public enum CustomerStatus
	{
		Prospect = 1,
		Client
	}

	public class Customer : BaseClass<int>
	{
		public string Name { get; set; }
		public string Image { get; set; }
		public string Monogram { get; set; }
		public string Contact { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public Address Address { get; set; }
		public CustomerStatus Status { get; set; }
	}
}
