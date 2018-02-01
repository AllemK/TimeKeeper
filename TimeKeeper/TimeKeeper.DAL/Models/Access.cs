using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Models
{
	public class Access
	{
		
		public int RoleId { get; set; }
		public string Entity { get; set; }
		public string Right { get; set; }
		public string TeamFlag { get; set; }
		public int PersonFlag { get; set; }
		
	}
}
