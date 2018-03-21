using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace TimeKeeper.API.Models
{
    public class CalendarModel
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public DayModel[] Days { get; set; }

        public CalendarModel(int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            Days = new DayModel[daysInMonth];
            for(int i = 0; i < daysInMonth; i++)
            {
                Days[i] = new DayModel() {
                    Date = new DateTime(year, month, i + 1),
                    Ordinal = i + 1,
                    Hours = 0,
                    Id = 0
                };
                Days[i].TypeOfDay = "empty";
                if (Days[i].Date.DayOfWeek == DayOfWeek.Saturday || Days[i].Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    Days[i].TypeOfDay = "weekend";
                }
                if (Days[i].Date >= DateTime.Today)
                {
                    Days[i].TypeOfDay = "future";
                }
            }
        }
    }
}