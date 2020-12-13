﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWithEFCore
{
    public class PaginatedList<T>:List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public PaginatedList(List<T> items,int totalCount,int pageIndx,int pageSize)
        {
            PageIndex = pageIndx;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
