using CompanyStruct.Models;

namespace CompanyStruct.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int projectId);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int projectId);
        Task<bool> IsUsedAsync(int projectId);
    }
}
