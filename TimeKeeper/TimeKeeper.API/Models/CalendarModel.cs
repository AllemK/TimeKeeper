using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TimeKeeper.DAL.Helper;

namespace TimeKeeper.API.Models
{
    public class CalendarModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Hours is required")]
        [RegularExpression(@"^([8]|[1-7]([.][5]){0,1})$")]
        public string Hours { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Type of day is required")]
        public string Type { get; set; }

        public string Employee { get; set; }
        public int EmployeeId { get; set; }
        public ICollection<DetailModel> Details { get; set; }

        public CalendarModel()
        {
            Details = new List<DetailModel>();
        }
    }
}