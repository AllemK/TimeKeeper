using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TimeKeeper.DAL.Entities;
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

        public DashboardModel CompanyDashboard(int year, int month)
        {
            var firstDay = new DateTime(year, month, 1);
            var lastDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            var query = TimeKeeperUnit.Projects
                .Get(x => x.StartDate <= firstDay && (!x.EndDate.HasValue || x.EndDate.Value >= firstDay));

            DashboardModel dm = new DashboardModel(query.Select(x => x.Team).Count(), query.Count);
            dm.NumberOfEmployees = query
                .Select(x => x.Team)
                .SelectMany(x => x.Engagements)
                .Select(x => x.Employee)
                .GroupBy(x => x.Engagements.GroupBy(y => y.Team.Projects.GroupBy(z => z.Id)))
                .Count();
            dm.NumberOfProjects = query.Count;
            dm.TotalHours = query
                .Select(x => x.Team)
                .SelectMany(x => x.Engagements)
                .Select(x => x.Employee)
                .SelectMany(x => x.Days)
                .GroupBy(x => x.Employee)
                .Sum(x => x.Key.Days.Where(y => y.Date >= firstDay && y.Date <= lastDay).Sum(y => y.Hours));

            for (int i = 0; i < dm.PTOHours.Count(); i++)
            {
                dm.PTOHours[i] = query
                .Select(x => x.Team)
                .ElementAt(i)
                .Engagements
                .Select(x => x.Employee)
                .SelectMany(x => x.Days)
                .GroupBy(x => x.Employee)
                .Sum(x => x.Key.Days
                .Where(y => y.Date >= firstDay && y.Date <= lastDay && y.Type!=DayType.WorkingDay)
                .Sum(y => y.Hours));

                dm.OvertimeHours[i] = query
                .Select(x=>x.Team)
                .ElementAt(i)
                .Engagements
                .Select(x => x.Employee)
                .SelectMany(x => x.Days)
                .GroupBy(x => x.Employee)
                .Sum(x => x.Key.Days.Where(y => y.Date >= firstDay && y.Date <= lastDay && y.Hours>8)
                .Sum(y => y.Hours-8));
            }

            return dm;
        }

        public DashboardModel TeamDashboard(string teamId, int year, int month)
        {
            int numberOfEmployees = TimeKeeperUnit.Employees.Get().Count();
            int numberOfProjets = TimeKeeperUnit.Projects.Get().Count();
            int numberOfTeams = TimeKeeperUnit.Teams.Get().Count();
            //DashboardModel dm = new DashboardModel();
            return null;
        }

        public PersonalModel PersonalReport(int id, int year, int month)
        {
            var query = TimeKeeperUnit.Employees.Get(id);
            int daysInCurrentMonth = GetWorkingDays(year, month, query);

            PersonalModel pm = new PersonalModel(DateTime.DaysInMonth(year, month));

            pm.TotalHours = query.Days.Where(x => x.Date.Month == month && x.Date.Year == year && x.Hours >= 8).Sum(x => x.Hours);
            pm.Utilization = (query.Days.Where(x => x.Date.Month == month && x.Date.Year == year && x.Hours >= 8).Count() / daysInCurrentMonth) * 100;
            //pm.BradfordFactor = 

            var days = query.Days.Where(x => x.Date.Month == month && x.Date.Year == year).OrderBy(x => x.Date).ToList();
            days.Where(x => x.Type == DayType.WorkingDay).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "8");
            days.Where(x => x.Type == DayType.PublicHoliday).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "PH");
            days.Where(x => x.Type == DayType.ReligiousDay).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "RD");
            days.Where(x => x.Type == DayType.BusinessAbsence).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "BA");
            days.Where(x => x.Type == DayType.OtherAbsence).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "OA");
            days.Where(x => x.Type == DayType.Vacation).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "V");
            days.Where(x => x.Type == DayType.SickLeave).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "SL");

            return pm;
        }

        public int GetWorkingDays(int year, int month, Employee query)
        {
            int day = 1;
            int currentWorkingDays = DateTime.DaysInMonth(year, month);
            if (DateTime.Today.Month == month && DateTime.Today.Year == year)
            {
                currentWorkingDays = DateTime.Today.Day;
            }
            if (query.BeginDate.Month == month && query.BeginDate.Year == year)
            {
                currentWorkingDays = DateTime.DaysInMonth(year, month) - query.BeginDate.Day + 1;
                day = query.BeginDate.Day;
            }

            int tempDays = currentWorkingDays;
            for (int i = day; i <= tempDays; i++)
            {
                var tempDate = new DateTime(year, month, i);
                if (tempDate.DayOfWeek == DayOfWeek.Saturday || tempDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    currentWorkingDays--;
                }
            }
            return currentWorkingDays;
        }
    }
}