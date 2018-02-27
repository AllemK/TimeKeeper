using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Helper
{
    public static class Pagination
    {
        public static IQueryable<Employee> Header(this IQueryable<Employee> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h, totalPages);

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

        public static IQueryable<Customer> Header(this IQueryable<Customer> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h, totalPages);

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

        public static IQueryable<Day> Header(this IQueryable<Day> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h, totalPages);

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

        public static IQueryable<Detail> Header(this IQueryable<Detail> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h, totalPages);

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

        public static IQueryable<Engagement> Header(this IQueryable<Engagement> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h, totalPages);

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

        public static IQueryable<Project> Header(this IQueryable<Project> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h, totalPages);

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

        public static IQueryable<Role> Header(this IQueryable<Role> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h, totalPages);

            switch (h.sort)
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

        public static IQueryable<Team> Header(this IQueryable<Team> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            InsertHeader(h,totalPages);

            switch (h.sort)
            {
                case 1: return list.OrderBy(x => x.Name)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
                default: return list.OrderBy(x => x.Id)
                        .Skip(h.pageSize * h.page)
                        .Take(h.pageSize);
            }
        }

        public static void InsertHeader(Header h, int totalPages)
        {
            var header = new
            {
                nextPage = (h.page == totalPages - 1) ? -1 : h.page + 1,
                previousPage = h.page - 1,
                h.pageSize,
                totalPages,
                h.page,
                h.sort,
                h.filter
            };
            HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(header));
        }
    }
}