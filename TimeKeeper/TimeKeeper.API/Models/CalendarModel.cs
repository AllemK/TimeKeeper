using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace TimeKeeper.API.Models
{
    public class DetailModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Hours is required")]
        [Range(0.5, 24, ErrorMessage = "Hours must be between 0.5 and 12")]
        [RegularExpression(@"^\d{1,2}(\.5)?$", ErrorMessage = "Hours must be whole number or .5")]
        public decimal Hours { get; set; }

        public BaseModel<int> Project { get; set; }
        public BaseModel<int> Day { get; set; }
        public bool Deleted { get; set; }
    }

    public class DayModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public decimal Hours { get; set; }

        public BaseModel<int> Employee { get; set; }
        public DetailModel[] Details { get; set; }

        public DayModel(BaseModel<int> employee)
        {
            Employee = employee;
            Details = new DetailModel[0];
        }
    }

    public class CalendarModel
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public BaseModel<int> Employee { get; set; }
        public DayModel[] Days { get; set; }

        public CalendarModel(BaseModel<int> employee, int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            Year = year;
            Month = month;
            Employee = employee;
            Days = new DayModel[daysInMonth];
            for(int i = 0; i < daysInMonth; i++)
            {
                Days[i] = new DayModel(employee) {
                    Date = new DateTime(year, month, i + 1),
                    Hours = 0,
                    Id = 0
                };
                if (Days[i].Date.DayOfWeek == DayOfWeek.Saturday || Days[i].Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    Days[i].Type = 8;
                }
                if (Days[i].Date >= DateTime.Today)
                {
                    Days[i].Type = 9;
                }
            }
        }
    }
}