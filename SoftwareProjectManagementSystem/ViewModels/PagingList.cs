using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProjectManagementSystem
{
    public class PagingList<T> : List<T>
    {
        public int pageIndex { get; set; }
        public int totalPage { get; set; }
        public PagingList(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.pageIndex = pageIndex;
            this.totalPage = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool prevPage { get { return (pageIndex > 1); } }
        public bool nextPage { get { return (pageIndex < totalPage); } }

        public static async Task<PagingList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagingList<T>(items, count, pageIndex, pageSize);
        }
    }
}
