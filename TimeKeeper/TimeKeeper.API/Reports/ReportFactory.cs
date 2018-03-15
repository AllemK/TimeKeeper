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
            var currentDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            int numberOfEmployees = TimeKeeperUnit.Employees
                .Get(x => x.BeginDate.CompareTo(currentDate) <= 0 || (x.EndDate.HasValue && x.EndDate.Value.CompareTo(currentDate) < 0))
                .Count();
            var activeProjects = TimeKeeperUnit.Projects
                .Get(x => x.StartDate.CompareTo(currentDate) <= 0 || (x.EndDate.HasValue && x.EndDate.Value.CompareTo(currentDate) > 0) && x.Status != ProjectStatus.Canceled);
            var activeTeams = TimeKeeperUnit.Teams
                .Get(x => x.Projects.Contains(activeProjects.FirstOrDefault()));

            var tempDate = new DateTime(year, month, 1);
            //foreach(var item in activeTeams)
            //{
            //    for(int i = 1; i<DateTime.DaysInMonth(year,month))
            //    var missingEntries = item.Engagements.SelectMany(x=>x.Employee.Days)
            //        .Where(x=>x.Hours<8 || x.Date)
            //}

            DashboardModel dm = new DashboardModel(activeTeams.Count, activeProjects.Count);
            dm.NumberOfEmployees = numberOfEmployees;
            dm.NumberOfProjects = activeProjects.Count;
            dm.TotalHours = TimeKeeperUnit.Calendar.Get(x => x.Date.Year == year && x.Date.Month == month).Sum(x => x.Hours);
            int i = 0;
            foreach (var item in activeTeams)
            {
                dm.OvertimeHours[i] = item.Engagements
                    .SelectMany(x => x.Employee.Days)
                    .Where(x=>x.Hours>8)
                    .Sum(x => (x.Hours-8));
                dm.PTOHours[i] = item.Engagements
                    .SelectMany(x => x.Employee.Days)
                    .Where(x => x.Type != DayType.WorkingDay)
                    .Sum(x => x.Hours);
                //dm.MissingEntries[i] = 
            }

            return dm;
        }

        public DashboardModel TeamDashboard()
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

            if (year == 0) year = DateTime.Today.Year;
            if (month == 0 || month == DateTime.Today.Month)
            {
                month = DateTime.Today.Month;
            }
            int daysInCurrentMonth = GetWorkingDays(year, month);

            PersonalModel pm = new PersonalModel(DateTime.DaysInMonth(year, month));
            pm.TotalHours = query.Days.Where(x => x.Date.Month == month && x.Date.Year == year).Sum(x => x.Hours);
            pm.Utilization = (pm.TotalHours / (daysInCurrentMonth * 8)) * 100;
            //pm.BradfordFactor = 

            int days = DateTime.DaysInMonth(year, month);
            if (month == DateTime.Today.Month)
                days = DateTime.Today.Day;
            var nonWorkingDays = TimeKeeperUnit.Calendar.Get().Where(x => x.Type != DayType.WorkingDay && x.Date.Month == month && x.Date.Year == year).ToList();
            if (nonWorkingDays != null)
            {
                nonWorkingDays.Where(x => x.Type == DayType.PublicHoliday).ToList().ForEach(x => pm.Days[x.Date.Day] = "PH");
                nonWorkingDays.Where(x => x.Type == DayType.ReligiousDay).ToList().ForEach(x => pm.Days[x.Date.Day] = "RD");
                nonWorkingDays.Where(x => x.Type == DayType.Vacation).ToList().ForEach(x => pm.Days[x.Date.Day] = "V");
                nonWorkingDays.Where(x => x.Type == DayType.SickLeave).ToList().ForEach(x => pm.Days[x.Date.Day] = "SL");
                nonWorkingDays.Where(x => x.Type == DayType.BusinessAbsence).ToList().ForEach(x => pm.Days[x.Date.Day] = "BA");
            }
            for (int i = 1; i <= days; i++)
            {
                DateTime tempDate = new DateTime(year, month, i);
                var item = query.Days.Where(x => x.Date.CompareTo(tempDate) == 0).FirstOrDefault();
                if (item.Hours >= 8 && item != null && item.Type == DayType.WorkingDay)
                    pm.Days[i - 1] = "8";
            }
            return pm;
        }

        public int GetWorkingDays(int year, int month)
        {
            int currentWorkingDays = DateTime.DaysInMonth(year, month);
            if (DateTime.Today.Month == month && DateTime.Today.Year == year)
            {
                currentWorkingDays = DateTime.Today.Day;
            }
            
            for(int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                var tempDate = new DateTime(year, month, i);
                var typeOfDay = TimeKeeperUnit.Calendar.Get(x => x.Date.CompareTo(tempDate) == 0).First().Type;
                if (tempDate.DayOfWeek==DayOfWeek.Saturday || tempDate.DayOfWeek==DayOfWeek.Sunday || typeOfDay == DayType.PublicHoliday || typeOfDay == DayType.ReligiousDay)
                {
                    currentWorkingDays--;
                }
            }
            return currentWorkingDays;
        }
    }
}