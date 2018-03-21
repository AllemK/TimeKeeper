using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class DetailModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Hours is required")]
        [Range(0.5,24,ErrorMessage = "Hours must be between 0.5 and 24")]
        [RegularExpression(@"^\d{1,2}(\.5)?$", ErrorMessage = "Hours must be whole number or .5")]
        public decimal Hours { get; set; }

        public string Project { get; set; }
        public int ProjectId { get; set; }
        public DateTime? Day { get; set; }
        public int DayId { get; set; }
    }
}