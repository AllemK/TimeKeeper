using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Repository;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.APP
{
    class Program
    {
        public class Result
        {
            public Employee Employee { get; set; }
            public decimal TotalHours { get; set; }
            public decimal AverageHours { get; set; }
        }
        static void Main(string[] args)
        {
            using (UnitOfWork unit = new UnitOfWork())
            {
                // SELECT e.id, e.FirstName, e.LastName, Count(c.Id), e.Email, E.Phone, e.BirthDate
                //FROM Calendar c JOIN Employees e ON c.Employee_Id
                //WHERE c.TYPE = 5
                //GROUP BY e.Id, e.FirstName, e.LastName, e.Email, e.Phone, e.BirtDate, ORDER BY COUNT(c.Id)

                //var list = unit.Calendars.Get()
                //                  .Where(x => x.Type == DayType.SickDay)
                //                  .GroupBy(x => x.Employee)
                //                  .OrderBy(x => x.Key.LastName)
                //                  .Select(w => new
                //             {
                //                 employee = w.Key.LastName + " " + w.Key.FirstName, days = w.Count()
                //             })
                //             .ToList();

                //foreach(var empl in list)
                //{
                //    Console.WriteLine($"{empl.employee} {empl.days}");
                //}

                //var list = unit.Calendars.Get()
                //                  .Where(x => x.Type == DayType.WorkingDay)
                //                  .GroupBy(x => x.Employee)
                //                  .OrderBy(x => x.Key.LastName)
                //                  .Select(w => new Result
                //                  {
                //                      Employee = w.Key,
                //                      TotalHours = w.Sum(c => c.Hours),
                //                      AverageHours = w.Average(c => c.Hours)
                //                  })
                //              .OrderBy(x => x.AverageHours) //sortiranje po prosjeku sati 
                //             .ToList();

                //foreach (var empl in list)
                //{
                //    Console.WriteLine($"{empl.Employee.FirstName} {empl.Employee.LastName} {empl.TotalHours} ({empl.AverageHours})");
                //}

                //var list = unit.Calendars.Get() // suma sati radnika po projektu
                //               .SelectMany(d => d.Details)
                //               .GroupBy(d => new { d.Project, d.Day.Employee })
                //               .Select(x => new
                //               {
                //                   project = x.Key.Project.Name,
                //                   employee = x.Key.Employee.FirstName,
                //                   hours = x.Sum(y => y.Hours)
                //               })
                //               .ToList();

                //foreach (var time in list)
                //{
                //    Console.WriteLine($"{time.project} : {time.employee} ({time.hours})");
                //}

                //var list = unit.Details.Get() // suma sati radnika po projektu - other wqy
                //               .GroupBy(d => new { d.Project, d.Day.Employee })
                //               .Select(x => new
                //               {
                //                   project = x.Key.Project.Name,
                //                   employee = x.Key.Employee.FirstName,
                //                   hours = x.Sum(y => y.Hours)
                //               })
                //               .ToList();

                //foreach (var time in list)
                //{
                //    Console.WriteLine($"{time.project} : {time.employee} ({time.hours})");
                //}

                string year, month;
                Console.WriteLine("Enter year: ");
                year = Console.ReadLine();
                Console.WriteLine("Enter month: ");
                month = Console.ReadLine();
                int godina = Convert.ToInt32(year);
                int mjesec = Convert.ToInt32(month);

                var list = unit.Calendars.Get(x => x.Date.Year == godina && x.Date.Month == mjesec)
                               .Select(x => x.Date)
                               //.Where()
                               .ToList();

                foreach (var date in list)
                {
                    Console.WriteLine($"{date.Day}");
                }

                Console.WriteLine("*** PRESS ANY KEY ***");
                Console.ReadKey();
            }
        }
    }
}
