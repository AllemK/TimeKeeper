using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class RoleModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required")]
        [MaxLength(128,ErrorMessage = "Id cannot be longer than 128 characters")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MaxLength(30,ErrorMessage = "Name cannot be longer than 25 characters")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required")]
        [RegularExpression("^(Position|TeamRole|AppRole)$")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Hourly rate is required")]
        [Range(0,100,ErrorMessage = "Hourly rate must be between 0 and 100")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal HourlyRate { get; set; }
        [Required(ErrorMessage = "Hourly rate is required")]
        [Range(0, 10000, ErrorMessage = "Hourly rate must be between 0 and 10000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal MonthlyRate { get; set; }

        public ICollection<EngagementModel> Members { get; set; }
        public ICollection<EmployeeModel> Employees { get; set; }

        public RoleModel()
        {
            Members = new List<EngagementModel>();
            Employees = new List<EmployeeModel>();
        }
    }
}