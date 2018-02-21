using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Helper
{
    public static class UtilityController
    {
        public static IEnumerable<Employee> Header(this IEnumerable<Employee> list, Header h)
        {
            list.Where(x => x.LastName.Contains(h.filter) || x.FirstName.Contains(h.filter));
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h.page, h.pageSize, totalPages, h.sort, h.filter);

            switch (h.sort)
            {
                case 1:
                    return list.OrderBy(x => x.LastName)
                    .Skip(h.pageSize * h.page)
                    .Take(h.pageSize);
                case 2:
                    return list.OrderBy(x => x.BirthDate)
                    .Skip(h.pageSize * h.page)
                    .Take(h.pageSize);
                default:
                    return list.OrderBy(x => x.Id)
                   .Skip(h.pageSize * h.page)
                   .Take(h.pageSize);
            }

        }

        public static IEnumerable<Customer> Header(this IEnumerable<Customer> list, Header h)
        {
            list.Where(x => x.Name.Contains(h.filter));
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h.page, h.pageSize, totalPages, h.sort, h.filter);

            switch (h.sort)
            {
                case 1: return list.OrderBy(x => x.Name)
                        .Skip(h.pageSize*h.page)
                        .Take(h.pageSize);
                case 2: return list.OrderBy(x => x.Contact)
                        .Skip(h.pageSize*h.page)
                        .Take(h.pageSize);
                default: return list.OrderBy(x => x.Id)
                        .Skip(h.pageSize*h.page)
                        .Take(h.pageSize);
            }
        }

        public static IEnumerable<Day> Header(this IEnumerable<Day> list, Header h)
        {
            list.Where(x => x.Date.CompareTo(Convert.ToDateTime(h.filter))==0);
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h.page, h.pageSize, totalPages, h.sort, h.filter);

            switch (h.sort)
            {
                case 1: return list.OrderBy(x => x.Date)
                        .Skip(h.pageSize*h.page)
                        .Take(h.pageSize);
                case 2: return list.OrderBy(x => x.Type)
                        .Skip(h.pageSize*h.page)
                        .Take(h.pageSize);
                default: return list.OrderBy(x => x.Id)
                        .Skip(h.pageSize*h.page)
                        .Take(h.pageSize);
            }
        }

        public static IEnumerable<Detail> Header(this IEnumerable<Detail> list, Header h)
        {
            list.Where(x => x.Description.Contains(h.filter));
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h.page, h.pageSize, totalPages, h.sort, h.filter);

            switch (h.sort)
            {
                case 1: return list.OrderBy(x => x.Day.Date)
                        .Skip(h.pageSize*h.page)
                        .Take(h.pageSize);
                case 2: return list.OrderBy(x => x.Project.Name)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                default: return list.OrderBy(x => x.Id)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
            }
        }

        public static IEnumerable<Engagement> Header(this IEnumerable<Engagement> list, Header h)
        {
            switch (h.sort)
            {
                case 1: return list.OrderBy(x => x.Employee.LastName)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                case 2: return list.OrderBy(x => x.Role.Name)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                case 3: return list.OrderBy(x => x.Team.Name)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                default: return list.OrderBy(x => x.Id)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
            }
        }

        public static IEnumerable<Project> SortBy(this IEnumerable<Project> list, Header h)
        {
            switch (h.sort)
            {
                case 1: return list.OrderBy(x => x.Name)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                case 2: return list.OrderBy(x => x.StartDate)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                default: return list.OrderBy(x => x.Id)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
            }
        }

        public static IEnumerable<Role> SortBy(this IEnumerable<Role> list, Header h)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.Name)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                case 2: return list.OrderBy(x => x.Type)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                default: return list.OrderBy(x => x.Id);
            }
        }

        public static IEnumerable<Team> SortBy(this IEnumerable<Team> list, Header h)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.Name)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                default: return list.OrderBy(x => x.Id)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
            }
        }

        public static void InsertHeader(int page, int pageSize, int totalPages, int sort, string filter)
        {
            var header = new
            {
                nextPage = (page == totalPages - 1) ? -1 : page + 1,
                previousPage = page - 1,
                pageSize,
                totalPages,
                page,
                sort,
                filter
            };
            HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(header));
        }
    }
}