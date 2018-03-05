using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.API.Reports
{
    public class ReportFactory
    {
        private UnitOfWork TimeKeeperUnit;

        public ReportFactory(UnitOfWork _unit)
        {
            TimeKeeperUnit = _unit;
        }

        public List<AnnualModel> AnnualReport(int year)
        {
            if (year == 0) year = Convert.ToInt32(ConfigurationManager.AppSettings["currentYear"]);

            var query = TimeKeeperUnit.Projects.Get()
                                .OrderBy(x => x.Name)
                                .Select(x => new
                                {
                                    project = x.Name,
                                    details = x.Details.Where(d => d.Day.Date.Year == year)
                                                       .GroupBy(d => d.Day.Date.Month)
                                                       .Select(w => new
                                                       {
                                                           month = w.Key,
                                                           hours = w.Sum(d => d.Hours)
                                                       })
                                                       .ToList()
                                }).ToList();

            List<AnnualModel> list = new List<AnnualModel>();
            AnnualModel total = new AnnualModel { ProjectName = "T O T A L" };
            foreach (var q in query)
            {
                AnnualModel item = new AnnualModel { ProjectName = q.project };
                foreach (var w in q.details)
                {
                    item.TotalHours += w.hours;
                    total.TotalHours += w.hours;
                    item.MonthlyHours[w.month - 1] = w.hours;
                    total.MonthlyHours[w.month - 1] += w.hours;
                }
                if (item.TotalHours > 0) list.Add(item);
            }
            list.Add(total);
            return list;
        }

        public MonthlyModel MonthlyReport(int year, int month)
        {
            if (year == 0) year = Convert.ToInt32(ConfigurationManager.AppSettings["currentYear"]);
            if (month == 0) month = 1;

            MonthlyModel result = new MonthlyModel() { Year = year, Month = month };
            result.Projects = TimeKeeperUnit.Projects.Get().OrderBy(x => x.Monogram).Select(x => new ProjectItem
            {
                Project = x.Monogram,
                Hours = x.Details.Where(d => d.Day.Date.Year == year && d.Day.Date.Month == month).Sum(d => d.Hours)
            }).ToList();

            result.Projects.RemoveAll(q => q.Hours == null);
            result.Total = result.Projects.Sum(x => x.Hours);
            int listSize = result.Projects.Count();

            var query = TimeKeeperUnit.Employees.Get().OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList()
                                .Select(x => new
                                {
                                    employee = x.FirstName + " " + x.LastName,
                                    details = x.Days.Where(c => c.Date.Year == year && c.Date.Month == month)
                                                        .SelectMany(c => c.Details)
                                                        .OrderBy(p => p.Project.Monogram)
                                                        .GroupBy(p => p.Project.Monogram)
                                                        .Select(d => new { project = d.Key, hours = d.Sum(w => w.Hours) }).ToList()
                                }).ToList();
            foreach (var q in query)
            {
                MonthlyItem item = new MonthlyItem(listSize) { Employee = q.employee, Total = q.details.Sum(w => w.hours) };
                foreach (var d in q.details)
                {
                    int i = result.Projects.FindIndex(x => x.Project == d.project);
                    item.Hours[i] = d.hours;
                }
                if (item.Total != 0) result.Items.Add(item);
            }

            return result;
        }

        //public DashboardModel Dashboard()
        //{
        //    DashboardModel dm = new DashboardModel();
        //    return result;
        //}

        public PersonalModel PersonalReport(int id, int year, int month)
        {
            int daysInCurrentMonth = 0;
            if (year == 0) year = DateTime.Today.Year;
            if (month == 0 || month == DateTime.Today.Month)
            {
                daysInCurrentMonth = DateTime.Today.Day;
                month = DateTime.Today.Month;
            }
            else
                daysInCurrentMonth = DateTime.DaysInMonth(year, month);

            int tempDays = daysInCurrentMonth;
            for (int i = 1; i <= tempDays; i++)
            {
                var date = new DateTime(year, month, i);

                if (DayOfWeek.Saturday == date.DayOfWeek || DayOfWeek.Sunday == date.DayOfWeek || TimeKeeperUnit.Calendar.Get(x=>x.Date.Year==year&&x.Date.Month==month&&x.Date.Day==i).FirstOrDefault().Type!=DAL.Entities.DayType.WorkingDay)
                    daysInCurrentMonth--;
            }

            var query = TimeKeeperUnit.Employees.Get(id);

            PersonalModel pm = new PersonalModel(DateTime.DaysInMonth(year, month));
            pm.TotalHours = query.Days.Where(x => x.Date.Month == month && x.Date.Year == year).Sum(x => x.Hours);
            pm.Utilization = (month == DateTime.Today.Month) ? pm.TotalHours /
            return pm;
        }
    }
}