using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
                .Where(y => y.Date >= firstDay && y.Date <= lastDay && y.Type != DayType.WorkingDay)
                .Sum(y => y.Hours));

                dm.OvertimeHours[i] = query
                .Select(x => x.Team)
                .ElementAt(i)
                .Engagements
                .Select(x => x.Employee)
                .SelectMany(x => x.Days)
                .GroupBy(x => x.Employee)
                .Sum(x => x.Key.Days.Where(y => y.Date >= firstDay && y.Date <= lastDay && y.Hours > 8)
                .Sum(y => y.Hours - 8));
            }

            return dm;
        }

        public DashboardModel TeamDashboard(string teamId, int year, int month)
        {
            var query = TimeKeeperUnit.Teams.Get(teamId);
            DashboardModel list = 
                new DashboardModel(query.Engagements
                .Where(x=>(x.Employee.BeginDate.Year<=year && x.Employee.BeginDate.Month<=month) && (x.Employee.EndDate.HasValue && x.Employee.EndDate.Value.Year >= year && x.Employee.EndDate.Value.Month >= month)).Count());
            list.NumberOfProjects = 
                query.Projects
                .Where(x=>(x.StartDate.Year<=year && x.StartDate.Month<=month) && (x.EndDate.HasValue && x.EndDate.Value.Year >= year && x.EndDate.Value.Month >= month)).Count();
            list.TotalHours = query.Projects.SelectMany(x => x.Details.Select(y => y.Hours)).Sum();
            for(int i = 0; i<list.NumberOfEmployees; i++)
            {
                //list.Utilization[i] = 
            }
            return list;
        }

        public PersonalModel PersonalReport(int id, int year, int month)
        {
            var query = TimeKeeperUnit.Employees.Get(id);
            int daysInCurrentMonth = GetWorkingDays(year, month, query);

            PersonalModel pm = new PersonalModel(DateTime.DaysInMonth(year, month));

            pm.TotalHours = query.Days.Where(x => x.Date.Month == month && x.Date.Year == year && x.Type==DayType.WorkingDay).Sum(x => x.Hours);
            pm.Utilization = (pm.TotalHours / (daysInCurrentMonth*8)) * 100;
            
            var days = query.Days.Where(x => x.Date.Month == month && x.Date.Year == year).OrderBy(x => x.Type).ToList();
            days.Where(x => x.Type == DayType.WorkingDay).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = x.Hours.ToString());
            days.Where(x => x.Type == DayType.PublicHoliday).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "PH");
            days.Where(x => x.Type == DayType.OtherAbsence).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "OA");
            days.Where(x => x.Type == DayType.ReligiousDay).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "RD");
            days.Where(x => x.Type == DayType.SickLeave).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "SL");
            days.Where(x => x.Type == DayType.Vacation).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "V");
            days.Where(x => x.Type == DayType.BusinessAbsence).ToList().ForEach(x => pm.Days[x.Date.Day - 1] = "BA");
            for(int i = 0; i<pm.Days.Length; i++)
            {
                if (pm.Days[i] == null)
                {
                    pm.Days[i] = "ME";
                }
            }
            pm.BradfordFactor = (int)Math.Pow(pm.Days.Where(x => x == "ME").Count(),2) * GetWorkingDays(month,year,query);

            return pm;
        }

        public List<ProjectHistory> ProjectHistory(int projectId)
        {
            List<ProjectHistory> list = new List<ProjectHistory>();
            var query = TimeKeeperUnit.Projects.Get(projectId);
            int lastYear = DateTime.Today.Year;
            if (query.EndDate.HasValue) lastYear = query.EndDate.Value.Year;
            foreach (var emp in query.Team.Engagements.Select(x => new { x.Employee.Id, x.Employee.FullName, x.Employee.Days }))
            {
                ProjectHistory ph = new ProjectHistory(lastYear - query.StartDate.Year + 1);
                ph.FirstYear = query.StartDate.Year;
                ph.LastYear = lastYear;
                if (query.EndDate.HasValue) ph.LastYear = query.EndDate.Value.Year;
                ph.Employee = emp.FullName;
                ph.TotalHours = (int)emp.Days
                    .SelectMany(x => x.Details)
                    .Where(x => x.Project.Id == projectId)
                    .Sum(x => x.Hours);

                for (int i = 0; i < ph.YearlyHours.Length; i++)
                {
                    ph.YearlyHours[i] = (int)emp.Days.Where(x => x.Date.Year == (query.StartDate.Year + i))
                        .SelectMany(x => x.Details).Where(x => x.Project.Id == projectId).Sum(x=>x.Hours);
                }
                list.Add(ph);
            }
            return list;
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