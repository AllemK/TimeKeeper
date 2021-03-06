﻿using System.ComponentModel.DataAnnotations;

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
