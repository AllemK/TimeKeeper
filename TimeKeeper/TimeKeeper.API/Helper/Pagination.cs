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
        public static IEnumerable<Customer> Header(this IQueryable<Customer> list, Header h)
        {
            h.pageSize = 6;
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            int totalItems = list.Count();
            InsertHeader(h, totalPages, totalItems);

            switch (h.sort)
            {
                case 1:
                    return list.OrderBy(x => x.Name)
                    .Skip(h.pageSize * h.page)
                    .Take(h.pageSize)
                    .ToList();
                case 2:
                    return list.OrderBy(x => x.Contact)
                    .Skip(h.pageSize * h.page)
                    .Take(h.pageSize)
                    .ToList();
                default:
                    return list.OrderBy(x => x.Id)
                   .Skip(h.pageSize * h.page)
                   .Take(h.pageSize)
                   .ToList();
            }
        }

        public static IEnumerable<Employee> Header(this IQueryable<Employee> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            int totalItems = list.Count();
            InsertHeader(h, totalPages, totalItems);

            switch (h.sort)
            {
                case 1:
                    return list.OrderBy(x => x.LastName)
                    .Skip(h.pageSize * h.page)
                    .Take(h.pageSize)
                    .ToList();
                case 2:
                    return list.OrderBy(x => x.BirthDate)
                    .Skip(h.pageSize * h.page)
                    .Take(h.pageSize)
                    .ToList();
                default:
                    return list.OrderBy(x => x.Id)
                   .Skip(h.pageSize * h.page)
                   .Take(h.pageSize)
                   .ToList();
            }
        }
        
        public static IEnumerable<Project> Header(this IQueryable<Project> list, Header h)
        {
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            int totalItems = list.Count();
            InsertHeader(h, totalPages, totalItems);

            switch (h.sort)
            {
                case 1:
                    return list.OrderBy(x => x.Name)
                    .Skip(h.pageSize * h.page)
                    .Take(h.pageSize)
                    .ToList();
                case 2:
                    return list.OrderBy(x => x.StartDate)
                    .Skip(h.pageSize * h.page)
                    .Take(h.pageSize)
                    .ToList();
                default:
                    return list.OrderBy(x => x.Id)
                   .Skip(h.pageSize * h.page)
                   .Take(h.pageSize)
                   .ToList();
            }
        }

        //public static IEnumerable<Role> Header(this IQueryable<Role> list, Header h)
        //{
        //    h.pageSize = list.Count();
        //    int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
        //    InsertHeader(h, totalPages);

        //    switch (h.sort)
        //    {
        //        case 1:
        //            return list.OrderBy(x => x.Name)
        //            .Skip(h.pageSize * h.page)
        //            .Take(h.pageSize)
        //            .ToList();
        //        case 2:
        //            return list.OrderBy(x => x.Type)
        //            .Skip(h.pageSize * h.page)
        //            .Take(h.pageSize)
        //            .ToList();
        //        default:
        //            return list.OrderBy(x => x.Id)
        //           .Skip(h.pageSize * h.page)
        //           .Take(h.pageSize)
        //           .ToList();
        //    }
        //}

        public static IEnumerable<Team> Header(this IQueryable<Team> list, Header h)
        {
            h.pageSize = 3;
            int totalPages = (int)Math.Ceiling((double)list.Count() / h.pageSize);
            int totalItems = list.Count();
            InsertHeader(h, totalPages, totalItems);

            switch (h.sort)
            {
                case 1:
                    return list.OrderBy(x => x.Name)
                    .Skip(h.pageSize * h.page)
                    .Take(h.pageSize)
                    .ToList();
                default:
                    return list.OrderBy(x => x.Id)
                   .Skip(h.pageSize * h.page)
                   .Take(h.pageSize)
                   .ToList();
            }
        }

        public static void InsertHeader(Header h, int totalPages, int totalItems)
        {
            var header = new
            {
                nextPage = (h.page == totalPages - 1) ? -1 : h.page + 1,
                previousPage = h.page - 1,
                h.pageSize,
                totalPages,
                h.page,
                h.sort,
                h.filter,
                totalItems
            };
            HttpContext.Current.Response.AddHeader("Pagination", JsonConvert.SerializeObject(header));
        }
    }
}