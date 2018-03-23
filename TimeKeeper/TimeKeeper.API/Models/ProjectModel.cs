using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MaxLength(25, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        [MaxLength(3,ErrorMessage = "Monogram cannot be longer than 3 characters")]
        public string Monogram { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Project status is required")]
        public int Status { get; set; }
        [Required(ErrorMessage = "Project pricing type is required")]
        public int Pricing { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [Range(0,1000000,ErrorMessage = "Amount must be between 0 and 1,000,000.00")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal Amount { get; set; }

        public BaseModel<int> Customer { get; set; }
        public BaseModel<string> Team { get; set; }
    }
}