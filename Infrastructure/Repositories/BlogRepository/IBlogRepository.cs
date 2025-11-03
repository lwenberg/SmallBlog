using Infrastructure.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories.BlogRespository
{
    public interface IBlogRepository
    {
        Task<List<BlogDTO>> GetAllAsync();
        Task<BlogDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(BlogDTO blogDto, IdentityUser currentUser);
        Task<bool> UpdateAsync(BlogDTO blogDto);
        Task<bool> DeleteAsync(int id);
    }
}
