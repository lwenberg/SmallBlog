using Microsoft.AspNetCore.Identity;
using SmallPost.Domain.DTOs.BlogDTOs;
using SmallPost.Domain.Helpers;

namespace SmallPost.Domain.Services.BlogService
{
    public interface IBlogRepository
    {
        Task<BlogDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateBlogDTO createBlogDto, IdentityUser currentUser);
        Task<bool> UpdateAsync(BlogDTO blogDto);
        Task<bool> DeleteAsync(int id);
        Task<PaginationListHelper<BlogDTO>> GetPaginatedBlogsAsync(int pageSize, int pageIndex, CancellationToken cancellationToken);
    }
}
