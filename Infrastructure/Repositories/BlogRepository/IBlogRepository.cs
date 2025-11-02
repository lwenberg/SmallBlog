using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories.BlogRespository
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(int id);
        Task<bool> CreateAsync(Blog blogDto, IdentityUser currentUser);
        Task<bool> UpdateAsync(Blog blogDto);
        Task<bool> DeleteAsync(int id);
    }
}
