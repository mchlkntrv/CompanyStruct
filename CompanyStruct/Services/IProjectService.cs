using CompanyStruct.Models;

namespace CompanyStruct.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int projectId);
        Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Project project);
        Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int projectId, Project project);
        Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int projectId);
    }
}
