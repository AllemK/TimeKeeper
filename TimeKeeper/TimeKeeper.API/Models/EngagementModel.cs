using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class EngagementModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Hours is required")]
        [Range(1,40,ErrorMessage = "Hours must be between 1 and 40")]
        public decimal Hours { get; set; }

        public BaseModel<int> Employee { get; set; }
        public BaseModel<string> Role { get; set; }
        public BaseModel<string> Team { get; set; }
    }
}