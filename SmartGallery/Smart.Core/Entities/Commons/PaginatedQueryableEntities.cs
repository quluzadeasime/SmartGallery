using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Entities.Commons
{
    public class PaginatedQueryableEntities<T>
    {
        public IQueryable<T> Items { get; set; }
        public int PageIndex { get; }
        public int TotalPages { get; }

        public PaginatedQueryableEntities(IQueryable<T> items, int pageIndex, int totalPages)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = totalPages;
        }
    }
}
