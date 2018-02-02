﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Models
{
	public class Customers
	{

		public string Name { get; set; }
		public string Image { get; set; }
		public string Monogram { get; set; }
		public string Contact { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public Address Address { get; set; }
		public int Status { get; set; }

	}
}
