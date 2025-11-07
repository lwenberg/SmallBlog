using Azure;
using Infrastructure.DTOs.BlogDTOs;
using Microsoft.AspNetCore.Identity;
using SmallPost.Infrastructure.Helpers;

namespace Infrastructure.Repositories.BlogRespository
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
