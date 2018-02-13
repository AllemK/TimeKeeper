using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.DAL.Entities
{
	public class Address
	{
        [Required]
		public string Road { get; set; }
        [Required]
        [Display(Name = "Zip Code")]
        [MaxLength(10)]
		public string ZipCode { get; set; }
        [Required]
		public string City { get; set; }
	}
}
