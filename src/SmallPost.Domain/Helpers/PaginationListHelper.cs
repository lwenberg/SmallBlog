

namespace SmallPost.Domain.Helpers
{
    public class PaginationListHelper<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginationListHelper(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double) pageSize);

            this.AddRange(items);
        }

        public static async Task<PaginationListHelper<T>> CreateAsync(List<T> dataSource, int count, int pageIndex, int pageSize)
        {
            return new PaginationListHelper<T>(dataSource, count, pageIndex, pageSize);
        }
    }
}
