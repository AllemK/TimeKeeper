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
        public static IOrderedEnumerable<Employee> SortBy(this IEnumerable<Employee> list, int sort)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.LastName);
                case 2: return list.OrderBy(x => x.BirthDate);
                default: return list.OrderBy(x => x.Id);
            }
        }

        public static IOrderedEnumerable<Customer> SortBy(this IEnumerable<Customer> list, int sort)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.Name);
                case 2: return list.OrderBy(x => x.Contact);
                default: return list.OrderBy(x => x.Id);
            }
        }

        public static IOrderedEnumerable<Day> SortBy(this IEnumerable<Day> list, int sort)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.Date);
                case 2: return list.OrderBy(x => x.Type);
                default: return list.OrderBy(x => x.Id);
            }
        }

        public static IOrderedEnumerable<Detail> SortBy(this IEnumerable<Detail> list, int sort)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.Day.Date);
                case 2: return list.OrderBy(x => x.Project.Name);
                default: return list.OrderBy(x => x.Id);
            }
        }

        public static IOrderedEnumerable<Engagement> SortBy(this IEnumerable<Engagement> list, int sort)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.Employee.LastName);
                case 2: return list.OrderBy(x => x.Role.Name);
                case 3: return list.OrderBy(x => x.Team.Name);
                default: return list.OrderBy(x => x.Id);
            }
        }

        public static IOrderedEnumerable<Project> SortBy(this IEnumerable<Project> list, int sort)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.Name);
                case 2: return list.OrderBy(x => x.StartDate);
                default: return list.OrderBy(x => x.Id);
            }
        }

        public static IOrderedEnumerable<Role> SortBy(this IEnumerable<Role> list, int sort)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.Name);
                case 2: return list.OrderBy(x => x.Type);
                default: return list.OrderBy(x => x.Id);
            }
        }

        public static IOrderedEnumerable<Team> SortBy(this IEnumerable<Team> list, int sort)
        {
            switch (sort)
            {
                case 1: return list.OrderBy(x => x.Name);
                default: return list.OrderBy(x => x.Id);
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
            HttpContext.Current.Response.Headers.Add("Pagination", JsonConvert.SerializeObject(header));
        }
    }
}