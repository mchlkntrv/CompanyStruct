using CompanyStruct.Models;
namespace CompanyStruct.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int departmentId);
        Task<(bool IsSuccess, IList<string> Errors)> AddAsync(Department department);
        Task<(bool IsSuccess, IList<string> Errors)> UpdateAsync(int departmentId, Department department);
        Task<(bool IsSuccess, IList<string> Errors)> DeleteAsync(int departmentId);
    }
}
