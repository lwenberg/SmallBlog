using SmallPost.Domain.DTOs.BlogDTOs;
using SmallPost.Domain.Helpers;

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

        public static BlogsViewModel Create(PaginationListHelper<BlogDTO> blogsPagination)
        {
            return new BlogsViewModel(blogsPagination);
        }
    }
}
