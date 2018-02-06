using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.ConApp
{
    class Program
    {
        public class Rezultat
        {
            public DAL.Entities.Employee Emp { get; set; }
            public decimal TotalHours { get; set; }
            public decimal AverageHours { get; set; }
        }
        static void Main(string[] args)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                //var query = unit.Calendar
                //    .Get(x => x.Type == DAL.Entities.DayType.SickLeave)
                //    .ToList()
                //    .GroupBy(x => x.Employee)
                //    .OrderBy(x => x.Key.Days.Count())
                //    .Select(x => new
                //    {
                //        employee = x.Key,
                //        days = x.Count()
                //    });

                //var query = unit.Calendar
                //               .Get(x => x.Type == DAL.Entities.DayType.WorkingDay)
                //               .GroupBy(x => x.Employee)
                //               .OrderBy(x => x.Key.LastName)
                //               .Select(x => new Rezultat
                //               {
                //                   Emp = x.Key,
                //                   TotalHours = x.Sum(y => y.Hours),
                //                   AverageHours = x.Average(y => y.Hours)
                //               })
                //               .OrderBy(x=>x.AverageHours);

                //var query = unit.Calendar.Get()
                //    .SelectMany(x => x.Details)
                //    .GroupBy(x => new { x.Project, x.Day.Employee })
                //    .Select(x => new
                //    {
                //        projectName = x.Key.Project.Name,
                //        employee = x.Key.Employee,
                //        hours = x.Sum(y => y.Hours)
                //    })
                //    .OrderBy(x => x.projectName)
                //    .ToList();

                //var queryMY = unit.Details.Get()
                //    .GroupBy(x => new { x.Project, x.Day.Employee })
                //    .Select(x => new
                //    {
                //        projectName = x.Key.Project.Name,
                //        employee = x.Key.Employee,
                //        hours = x.Sum(y => y.Hours)
                //    })
                //    .OrderBy(x=>x.projectName)
                //    .ToList();

                string year = "";
                string month;
                while (true)
                {
                    Console.WriteLine("Enter year: ");
                    year = Console.ReadLine();
                    if (year.Equals("quit"))
                    {
                        break;
                    }
                    Console.WriteLine("Enter month: ");
                    month = Console.ReadLine();
                    int y = Convert.ToInt32(year);
                    int m = Convert.ToInt32(month);
                    Console.Write("Testo testic: missing");
                    List<int> daysMissing = new List<int>();

                    foreach (var day in unit.Calendar.Get(x => x.Date.Year == y && x.Date.Month == m))
                    {
                        for (int i = 1; i <= DateTime.DaysInMonth(y, m); i++)
                        {
                            if (day.Date.Day != i)
                            {
                                daysMissing.Add(i);
                            }
                        }
                    }
                    daysMissing.ForEach()
                    Console.Write(daysMs", ")
                }
            }
            Console.Write("--- press any key ---");
            Console.ReadKey();
        }
    }
}
