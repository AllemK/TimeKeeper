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

                //    string year = "";
                //    string month;
                //    //bool missing = false;
                //    while (true)
                //    {
                //        Console.WriteLine("Enter year: ");
                //        year = Console.ReadLine();
                //        if (year.Equals("quit"))
                //        {
                //            break;
                //        }
                //        Console.WriteLine("Enter month: ");
                //        month = Console.ReadLine();
                //        int y = Convert.ToInt32(year);
                //        int m = Convert.ToInt32(month);

                //        //List<int> daysMissing = new List<int>();

                //        var list = unit.Calendar.Get()
                //            .GroupBy(x => x.Employee)
                //            .OrderBy(x => x.Key.LastName)
                //            .Select(x => new
                //            {
                //                Employee = x.Key,
                //                Day = x.Key.Days.Where(w => w.Date.Year == y && w.Date.Month == m)
                //            })
                //            .ToList();

                //        foreach (var day in list)
                //        {
                //            Console.Write($"{day.Employee.FirstName}, {day.Employee.LastName}: missing ");
                //            var list1 = list.AsQueryable().Except(day.Day);
                //            for (int i = 1; i <= DateTime.DaysInMonth(y, m); i++)
                //            {
                //                if (day.Day.Where(x => x.Date.Day == i).Count()==0)
                //                {
                //                    Console.Write($"{i}, ");
                //                }
                //            }
                //            Console.WriteLine();
                //        }
                //        Console.WriteLine();
                //    }       
                while (true)
                {
                    Console.Write("Year: ");
                    int year = Convert.ToInt32(Console.ReadLine());
                    if (year == 0) break;
                    Console.Write("Month: ");
                    int month = Convert.ToInt32(Console.ReadLine());
                    if (month >= 1 && month <= 12)
                    {
                        int daysInMonth = DateTime.DaysInMonth(year, month);
                        DateTime days;
                        for(int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                        {
                            days = new DateTime(year, month, i);
                            if (days.DayOfWeek == DayOfWeek.Saturday || days.DayOfWeek == DayOfWeek.Sunday)
                            {
                                daysInMonth--;
                            }
                        }
                        var query = unit.Employees.Get()
                            .SelectMany(x => x.Days)
                            .Where(x => x.Date.Month == month &&
                                    x.Date.Year == year &&
                                    x.Type == DAL.Entities.DayType.WorkingDay)
                            .GroupBy(x => x.Employee)
                            .Select(x => new
                            {
                                x.Key,
                                wd = x.Count()
                            }).ToList();
                        
                        foreach(var emp in query)
                        {
                            Console.WriteLine($"{emp.Key.FirstName} {emp.Key.LastName}: {Math.Round(100f * (float)emp.wd / daysInMonth, 2)}%");
                        }

                    }
                }
            }
            Console.Write("--- press any key ---");
            Console.ReadKey();
        }
    }
}
