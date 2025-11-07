using Infrastructure.DTOs.BlogDTOs;
using Microsoft.EntityFrameworkCore;

namespace SmallPost.Web.Helpers
{
    public class PaginationListHelper<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginationListHelper(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double) pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginationListHelper<T>> CreateAsync(List<T> dataSource, int pageIndex, int pageSize, int count)
        {
            return new PaginationListHelper<T>(dataSource,count, pageIndex, pageSize);
        }
    }
}
