using Infrastructure.DTOs.BlogDTOs;
using SmallPost.Infrastructure.Helpers;

namespace SmallPost.Web.ViewModels
{
    public class BlogsViewModel
    {
        public PaginationListHelper<BlogDTO> BlogsPagination { get; private set; }
        public string PrevPageDisable { get; private set; }
        public string NextPageDisable { get; private set; }

        public BlogsViewModel(PaginationListHelper<BlogDTO> blogs) 
        {
            BlogsPagination = blogs;
            PrevPageDisable = !blogs.HasPreviousPage ? "disabled" : "";
            NextPageDisable = !blogs.HasNextPage ? "disabled" : "";
        }
    }
}
