using Infrastructure.Entities;

namespace Infrastructure.Services.BlogServices
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(int id);
        Task<bool> CreateAsync(Blog blogDto);
        Task<bool> UpdateAsync(Blog blogDto);
        Task<bool> DeleteAsync(int id);
    }
}
